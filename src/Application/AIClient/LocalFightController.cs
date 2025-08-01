using UnityGERunner;
using Coroutine;
using Recorder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace UnityGERunner.UnityApplication
{
	
	struct EquipData
	{
	    public string weaponPath;
	    public int hpCount;
	
	    public static Dictionary<string, EquipData> equips = new Dictionary<string, EquipData>
	    {
	        { "HPEquips/AFighter/af_amraamRail", new EquipData { weaponPath = "Weapons/Missiles/AIM-120", hpCount = 1 } },
	        { "HPEquips/AFighter/fa26_iris-t-x1", new EquipData { weaponPath = "Weapons/Missiles/AIRS-T", hpCount = 1 } }
	    };
	}
	
	public class LocalFightController : MonoBehaviour
	{
	    public Transform spawnCenterPoint;
	    public float spawnDistance = 72000f;
	    public float spawnAltitude = 6000;
	    public bool doSetupSpawns = false;
	
	    public static LocalFightController instance { get; private set; }
	    public WeaponStats weaponStats;
	
	    public string teamAAIPilotImplementationPath = "";
	    public string teamBAIPilotImplementationPath = "";
	    private Dictionary<Team, Type> aipImplementationCtors = new Dictionary<Team, Type>();
	
	    private IAIPProvider aipProvider;
	
	    public List<AIClient> alliedClients;
	    public List<AIClient> enemyClients;
	    private List<AIClient> clients;
	
	    private int nextId = 1;
	    //private CommandLineArguments cli = null;
	
	
	    protected override void Awake()
	    {
	        if (instance != null)
	        {
	            Logger.Error("[HSGE] " + $"Duplicate LocalFightController created!");
	            Destroy(this);
	            return;
	        }
	
	        instance = this;
	
	        //cli = new CommandLineArguments();
	        //spawnDistance = cli.GetArg("--spawn-dist", spawnDistance);
	        //spawnAltitude = cli.GetArg("--spawn-alt", spawnAltitude);
	        spawnDistance = Options.instance.spawnDist;
	        spawnAltitude = Options.instance.spawnAlt;
	
	        //teamAAIPilotImplementationPath = cli.GetArg("--allied", teamAAIPilotImplementationPath);
	        //teamBAIPilotImplementationPath = cli.GetArg("--enemy", teamBAIPilotImplementationPath);
	        teamAAIPilotImplementationPath = Options.instance.allied;
	        teamBAIPilotImplementationPath = Options.instance.enemy;
	
	        clients = alliedClients.Concat(enemyClients).ToList();
	
	        foreach (var client in clients)
	        {
	            client.entityId = nextId;
	            client.actorId = nextId;
	            client.unitId = nextId;
	            nextId++;
	
	            var actor = client.GetComponent<Actor>();
	            actor.team = alliedClients.Contains(client) ? Team.Allied : Team.Enemy;
	        }
	    }
	
	    protected override void Start()
	    {
	        if (doSetupSpawns) SetupSpawnLocations();
	
	#if HSGE
	        ConfigureAndStartAIPs();
	#endif
	
	        var handlers = gameObject.GetComponentsInSceneImplementing<IVehicleReadyNotificationHandler>();
	        foreach (var handler in handlers) handler.OnVehicleReadyNotification();
	    }
	
	    private void ConfigureAndStartAIPs()
	    {
	        LoadAIPImplementation(teamAAIPilotImplementationPath, Team.Allied);
	        LoadAIPImplementation(teamBAIPilotImplementationPath, Team.Enemy);
	
	        foreach (var client in clients)
	        {
	            if (!aipImplementationCtors.ContainsKey(client.team))
	            {
	                Logger.Warn("[HSGE] " + $"Cannot create AIP for {client} on team {client.team} as no AIPProvider ctor exists");
	                continue;
	            }
	            Logger.Info("[HSGE] " + $"Loading AIP for {client} on team {client.team} from {aipImplementationCtors[client.team]}");
	
	            //var flag = "--debug-" + client.team.ToString();
	            //var debugEnabled = cli.HasArg(flag);
	            var debugEnabled = client.team == Team.Allied ? Options.instance.debugAllied : Options.instance.debugEnemy;
	            //Logger.Info("[HSGE] " + $"Checking for {flag} to enable debugging: {debugEnabled}");
	            client.ConfigureAIP(aipImplementationCtors[client.team], spawnDistance, spawnCenterPoint.position, debugEnabled);
	        }
	    }
	
	    private void LoadAIPImplementation(string path, Team team)
	    {
	        if (path == null || path == string.Empty || !File.Exists(path))
	        {
	            Logger.Warn("[HSGE] " + $"No AIP Implementation for team {team} found at path: {path}");
	            return;
	        }
	
	        var bytes = File.ReadAllBytes(path);
	        var assemb = Assembly.Load(bytes);
	        var types = assemb.GetTypes();
	
	        foreach (var type in types)
	        {
	            if (!typeof(IAIPProvider).IsAssignableFrom(type)) continue;
	
	            aipImplementationCtors.Add(team, type);
	        }
	    }
	
	    private void SetupSpawnLocations()
	    {
	
	        var spawnAngleRad = UnityGERunner.Random.Range(0, 360) * Mathf.Deg2Rad;
	
	        var x = Mathf.Cos(spawnAngleRad) * spawnDistance;
	        var z = Mathf.Sin(spawnAngleRad) * spawnDistance;
	
	        var x2 = Mathf.Cos(spawnAngleRad + Mathf.PI) * spawnDistance;
	        var z2 = Mathf.Sin(spawnAngleRad + Mathf.PI) * spawnDistance;
	
	        var vec = new Vector3(x, spawnAltitude, z) + spawnCenterPoint.position;
	        var vec2 = new Vector3(x2, spawnAltitude, z2) + spawnCenterPoint.position;
	
	        var look = Quaternion.LookRotation(vec);
	
	        Logger.Info("[HSGE] " + $"Spawn1: {vec}, spawn2: {vec2}");
	
	        foreach (var alliedClient in alliedClients)
	        {
	            alliedClient.transform.position = vec;
	            alliedClient.transform.LookAt(vec2, Vector3.up);
	        }
	        foreach (var enemyClient in enemyClients)
	
	        {
	            enemyClient.transform.position = vec2;
	            enemyClient.transform.LookAt(vec, Vector3.up);
	        }
	    }
	
	    protected override void FixedUpdate()
	    {
	
	    }
	
	    public void RequestWeaponSpawn(EquipManager equipManager, SpawnAIPWeapon spawnRequest)
	    {
	        if (!enabled) return;
	        if (!EquipData.equips.ContainsKey(spawnRequest.path))
	        {
	            Logger.Warn("[HSGE] " + $"Attempted to spawn {spawnRequest.path} on hp {spawnRequest.hpIndex} however no equip data for that hardpoint");
	            return;
	        }
	
	
	        var equipData = EquipData.equips[spawnRequest.path];
	
	        for (int i = 0; i < equipData.hpCount; i++)
	        {
	            equipManager.HandleWeaponSpawned(equipData.weaponPath, nextId++, -1, i);
	        }
	    }
	
	    private int GetAliveCount(List<AIClient> list, AIClient minus)
	    {
	        return list.Where(aic => aic != null && aic != minus).Count();
	    }
	
	    public void HandleDamageRequest(AIClient damagedClient)
	    {
	        Destroy(damagedClient.gameObject);
	
	        var alliedCount = GetAliveCount(alliedClients, damagedClient);
	        var enemyCount = GetAliveCount(enemyClients, damagedClient);
	
	        if (alliedCount == 0 && enemyCount == 0) Win(Team.Unknown);
	        else if (alliedCount == 0) Win(Team.Enemy);
	        else if (enemyCount == 0) Win(Team.Allied);
	    }
	
	    private void Win(Team team)
	    {
	        GameRecorder.Event(new WinEvent(team));
	        GameRecorder.EndSimulation();
	    }
	}
	
}