using UnityGERunner;
using Coroutine;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnityGERunner.UnityApplication
{
	
	
	
	public class HCEntityBase : MonoBehaviour
	{
	    public int entityId;
	    public string path;
	    public ulong ownerId;
	    public Team team;
	
	    public HCManager manager;
	}
	
	public class HCManager : MonoBehaviour
	{
	    private string[] vehicleRpcClasses = { "PlayerVehicle", "F45A", "FA26B", "AV42", "AH94", "T55", "EF24" };
	    private string[] vehicleTypes = { "Vehicles/SEVTF", "Vehicles/FA-26B", "Vehicles/F-16", "Vehicles/AH-94", "Vehicles/VTOL4", "Vehicles/T-55", "Vehicles/EF-24" };
	
	    public HCConnector connector;
	    public GameObject playerEntityPrefab;
	    public GameObject missilePrefab;
	
	    public Transform missileParent;
	    public EquipManager equipManager;
	    public AIClient aiClient;
	    public RWR rwr;
	
	    private Dictionary<int, HCEntityBase> entities = new Dictionary<int, HCEntityBase>();
	
	    protected override void Start()
	    {
	    }
	
	    protected override void Update()
	    {
	    }
	
	    public void HandleRPC(RPC rpc)
	    {
	
	        if (rpc.className == "MessageHandler") HandleMessageHandlerPacket(rpc);
	        else if (rpc.className == "MissileEntity") HandleMissilePacket(rpc);
	        else if (rpc.className == "Client") HandleClientPacket(rpc);
	        else if (rpc.className == "AIPilot") HandleAIPPacket(rpc);
	        else if (vehicleRpcClasses.Contains(rpc.className)) HandleVehiclePacket(rpc);
	    }
	
	    private void HandleClientPacket(RPC rpc)
	    {
	        if (rpc.method != "ping")
	        {
	            Logger.Info("[HSGE] " + $"Unknown client packet: {rpc.method}");
	            return;
	        }
	
	        var pong = new RPC
	        {
	            className = "Client",
	            method = "pong",
	            id = connector.clientId,
	            args = rpc.args,
	        };
	
	        connector.SendRPC(pong);
	    }
	
	    private void HandleAIPPacket(RPC rpc)
	    {
	        switch (rpc.method)
	        {
	            case "EquipSpawned":
	                {
	                    equipManager.HandleEquipSpawned(rpc.arg<string>(0), rpc.arg<int>(1), rpc.arg<int>(2));
	                }
	                break;
	            case "WeaponSpawned":
	                {
	                    var path = rpc.arg<string>(0);
	                    var weaponEntityId = rpc.arg<int>(1);
	                    var hpEquipId = rpc.arg<int>(2);
	                    var railIndex = rpc.arg<int>(3);
	                    equipManager.HandleWeaponSpawned(path, weaponEntityId, hpEquipId, railIndex);
	                }
	                break;
	            case "VehicleReady":
	                {
	                    Logger.Info("[HSGE] " + $"Vehicle is spawned and ready!");
	                    var entityId = rpc.arg<int>(0);
	                    var actorId = rpc.arg<int>(1);
	                    var unitId = rpc.arg<int>(2);
	
	                    aiClient.entityId = entityId;
	                    aiClient.actorId = actorId;
	                    aiClient.unitId = unitId;
	
	                    var handlers = gameObject.GetComponentsInSceneImplementing<IVehicleReadyNotificationHandler>();
	                    foreach (var handler in handlers) handler.OnVehicleReadyNotification();
	                }
	                break;
	            case "RWRPing":
	                {
	                    var entityId = rpc.arg<int>();
	                    var actorId = rpc.arg<int>();
	                    var signalStrength = rpc.arg<int>();
	                    var frequency = rpc.arg<int>();
	                    var globalPos = rpc.arg<NetVector>();
	                    var velocity = rpc.arg<NetVector>();
	                    var isLock = rpc.arg<bool>();
	
	                    var entity = GetEntity(entityId);
	                    if (entity == null)
	                    {
	                        Logger.Error("[HSGE] " + $"Recieved RWR ping from an entity we do not have: {entityId}");
	                        return;
	                    }
	
	                    rwr.HandlePing(entity.GetComponent<Actor>(), actorId, signalStrength, -1, frequency, globalPos, velocity, isLock);
	                }
	                break;
	        }
	    }
	
	    private void HandleMessageHandlerPacket(RPC rpc)
	    {
	        switch (rpc.method)
	        {
	            case "NetInstantiate":
	                var id = rpc.arg<int>();
	                var ownerId = rpc.arg<string>();
	                var path = rpc.arg<string>();
	                var pos = rpc.arg<NetVector>();
	                var rot = rpc.arg<NetVector>();
	                var active = rpc.arg<bool>();
	
	                if (!vehicleTypes.Contains(path) && !path.StartsWith("Weapons/Missiles/")) return;
	
	                Logger.Info("[HSGE] " + $"Net Instantiate: Id: {id}, OwnerId: {ownerId}, Path: {path}");
	
	                HCEntityBase hcEntity;
	                if (vehicleTypes.Contains(path))
	                {
	                    var entity = Instantiate(playerEntityPrefab, new Vector3(pos.x, pos.y, pos.z), Quaternion.Euler(rot.x, rot.y, rot.z));
	                    hcEntity = entity.GetComponent<HCPlayerEntity>();
	                }
	                else
	                {
	                    var entity = Instantiate(missilePrefab, new Vector3(pos.x, pos.y, pos.z), Quaternion.Euler(rot.x, rot.y, rot.z));
	                    entity.transform.parent = missileParent;
	                    hcEntity = entity.GetComponent<HCMissile>();
	                }
	
	                hcEntity.path = path;
	                hcEntity.entityId = id;
	                hcEntity.ownerId = ulong.Parse(ownerId);
	                hcEntity.manager = this;
	
	                entities.Add(id, hcEntity);
	
	                break;
	
	            case "NetDestroy":
	                var destroyId = rpc.arg<int>();
	                if (!entities.ContainsKey(destroyId)) return;
	
	                Destroy(entities[destroyId].gameObject);
	                // entities[destroyId] = null;
	                entities.Remove(destroyId);
	                break;
	        }
	    }
	
	    private void HandleMissilePacket(RPC rpc)
	    {
	        if (rpc.method != "SyncShit") return;
	
	        var pos = rpc.arg<NetVector>(0);
	        var rot = rpc.arg<NetVector>(1);
	        var vel = rpc.arg<NetVector>(2);
	        var accel = rpc.arg<NetVector>(3);
	
	        var entityId = int.Parse(rpc.id);
	
	        if (!entities.ContainsKey(entityId))
	        {
	            Logger.Info("[HSGE] " + $"Unable to find entity {entityId} for missile packet");
	            return;
	        }
	
	        var hcEntity = entities[entityId];
	
	        if (hcEntity is HCMissile entity)
	        {
	            entity.transform.position = new Vector3(pos.x, pos.y, pos.z);
	            entity.velocity = new Vector3(vel.x, vel.y, vel.z);
	            entity.acceleration = new Vector3(accel.x, accel.y, accel.z);
	            entity.transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);
	            entity.fired = true;
	        }
	        else
	        {
	            Logger.Error("[HSGE] " + $"Entity {hcEntity} ({entityId}) is not an HCMissile, however got SyncShit");
	        }
	    }
	
	    private void HandleVehiclePacket(RPC rpc)
	    {
	        var entity = GetPlayerEntity(int.Parse(rpc.id));
	        if (entity == null) return;
	
	        switch (rpc.method)
	        {
	            case "AttachEquip":
	                entity.OnMissileAttach(rpc.arg<int>(0));
	                break;
	            case "UpdateData":
	                var pos = rpc.arg<NetVector>(0);
	                var vel = rpc.arg<NetVector>(1);
	                var accel = rpc.arg<NetVector>(2);
	                var rot = rpc.arg<NetVector>(3);
	                var throttle = rpc.arg<float>(4);
	
	                entity.transform.position = new Vector3(pos.x, pos.y, pos.z);
	                entity.velocity = new Vector3(vel.x, vel.y, vel.z);
	                entity.acceleration = new Vector3(accel.x, accel.y, accel.z);
	                entity.transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);
	                entity.throttle = throttle;
	
	                break;
	        }
	    }
	
	    private HCEntityBase GetEntity(int id)
	    {
	        if (entities.ContainsKey(id)) return entities[id];
	        return null;
	    }
	
	    private HCPlayerEntity GetPlayerEntity(int id)
	    {
	        if (!entities.ContainsKey(id))
	        {
	            Logger.Info("[HSGE] " + $"Unable to find entity {id} for vehicle packet");
	            return null;
	        }
	
	        var hcEntity = entities[id];
	
	        if (hcEntity is HCPlayerEntity entity)
	        {
	            return entity;
	        }
	        else
	        {
	            Logger.Error("[HSGE] " + $"Entity {hcEntity} ({id}) is not an HCEntity in GetPlayerEntity");
	            return null;
	        }
	    }
	
	    public HCMissile GetMissile(int id)
	    {
	        if (!entities.ContainsKey(id)) return null;
	        if (entities[id] is HCMissile entity) return entity;
	        return null;
	    }
	}
	
}