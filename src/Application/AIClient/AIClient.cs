using UnityGERunner;
using Coroutine;
using Recorder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace UnityGERunner.UnityApplication
{
	
	
	class Reader
	{
	    private int idx = 0;
	    private int[] values;
	
	    public Reader(int[] vals) { values = vals; }
	    public int Read() { return values[idx++]; }
	    public bool Eof() { return values == null || idx >= values.Length; }
	    public InboundAction ReadAction() { return (InboundAction)Read(); }
	}
	
	public class AIClient : MonoBehaviour, IVehicleReadyNotificationHandler, IDamageReceiver
	{
	    
	    public int entityId;
	    public int unitId;
	    public int actorId;
	    public Team team => actor.team;
	
	    public float physicalRadius = 15;
	
	    
	    public Actor actor;
	    public HCManager manager;
	    public Radar radar;
	    public Rigidbody rb;
	    public EquipManager equipManager;
	    public RWR rwr;
	    public VisualSensor visualTargetFinder;
	    public KinematicPlane kp;
	    public ModuleEngine engine;
	    public FuelTank fuelTank;
	
	    
	    public IRCountermeasure irCountermeasure;
	    public ChaffCountermeasure chaffCountermeasure;
	
	    
	    public DatalinkController datalink;
	
	    private IAIPProvider aipProvider;
	    private double totalExecTime = 0;
	    private int totalExecs = 0;
	    private bool hasStopped = false;
	
	    public void OnVehicleReadyNotification()
	    {
	        radar.radarActorId = actorId;
	
	        GameRecorder.InitEntity(entityId, "Vehicles/FA-26B", gameObject.name, team);
	    }
	
	    public void ConfigureAIP(Type ctor, float spawnDist, Vector3 centerPoint, bool enableDebug, int alliedSpawns, int enemySpawns)
	    {
	        aipProvider = (IAIPProvider)Activator.CreateInstance(ctor);
	
	        aipProvider.outputEnabled = enableDebug;
	        var setupInfo = new SetupInfo
	        {
	            id = entityId,
	            team = team,
	            spawnDist = spawnDist,
	            mapCenterPoint = new NetVector(centerPoint),
	            mapPath = Map.instance.mapPath != string.Empty ? Path.GetFullPath(Map.instance.mapPath) : "",
	            mapId = Map.instance.mapId,
	            alliedSpawns = alliedSpawns,
	            enemySpawns = enemySpawns,
	            weaponRestrictions = Options.WeaponCountLimits,
	            args = team == Team.Allied ? Options.AlliedArgs : Options.EnemyArgs
	        };
	
	        var startActions = aipProvider.Start(setupInfo);
	        gameObject.name = startActions.name;
	        aipProvider.__aipName = startActions.name;
	
	        equipManager.equips = startActions.hardpoints.ToList();
	
	        if (startActions.fuel > 0) fuelTank.SetFuel(startActions.fuel);
	    }
	
	    public void ReportAllDlInfo()
	    {
	        radar.ReportDL(datalink, entityId);
	        visualTargetFinder.ReportDL(datalink, entityId);
	        datalink.ReportMyself(entityId, transform.position, rb.linearVelocity);
	    }
	
	    public void ExecuteAIP(List<KillFeedEntry> killFeed)
	    {
	        if (aipProvider == null) return;
	        var state = BuildState(killFeed);
	
	        if (aipProvider.outputEnabled) GameRecorder.RecordState(state, entityId);
	
	        var start = DateTime.Now;
	        var aipResult = aipProvider.Update(state);
	
	        totalExecTime += (DateTime.Now - start).TotalMilliseconds;
	        totalExecs++;
	
	        kp.input = new Vector3(Mathf.Clamp(aipResult.pyr.x, -1f, 1f), Mathf.Clamp(aipResult.pyr.y, -1f, 1f), Mathf.Clamp(aipResult.pyr.z, -1f, 1f));
	        engine.throttle = aipResult.throttle;
	
	        equipManager.SetIRLookDir(aipResult.irLookDir.vec3);
	
	        var reader = new Reader(aipResult.events);
	        while (!reader.Eof()) HandleAIPAction(reader);
	    }
	
	    public void ExecuteStop()
	    {
	        if (hasStopped) return;
	        hasStopped = true;
	
	        if (aipProvider != null) aipProvider.Stop();
	
	        if (totalExecs > 0)
	        {
	            var msPerTick = totalExecTime / totalExecs;
	            Logger.Info("[HSGE] " + $"AIP {aipProvider.__aipName} ({entityId}) executed {totalExecs} frames, {msPerTick} ms/frame");
	            if (msPerTick > Time.fixedDeltaTime)
	            {
	                Logger.Warn("[HSGE] " + $" - ms/frame is greater than fixedDeltaTime of {Time.fixedDeltaTime}");
	            }
	        }
	    }
	
	    private void HandleAIPAction(Reader reader)
	    {
	        InboundAction action = reader.ReadAction();
	        switch (action)
	        {
	            case InboundAction.RadarState:
	                radar.enabled = reader.Read() > 0;
	                break;
	            case InboundAction.RadarSTT:
	                var refedStt = radar.GetDetectedTargetById(reader.Read());
	                if (refedStt != null) radar.AttemptAcquireSTT(refedStt.actor);
	                break;
	            case InboundAction.RadarStopSTT:
	                radar.lockData = null;
	                break;
	            case InboundAction.RadarTWS:
	                var refedTws = radar.GetDetectedTargetById(reader.Read());
	                if (refedTws != null) radar.UpdateOrStartTWSOnTarget(refedTws.actor);
	                break;
	            case InboundAction.RadarDropTWS:
	                var refedDroppedTws = radar.GetDetectedTargetById(reader.Read());
	                if (refedDroppedTws != null) radar.twsedTargets.Remove(refedDroppedTws.actor);
	                break;
	            case InboundAction.RadarSetPDT:
	                radar.pdtTwsIdx = reader.Read();
	                break;
	            case InboundAction.RadarFov:
	                radar.radarFov = reader.Read();
	                break;
	            case InboundAction.RadarAzimuth:
	                radar.currentAzimuthAdjust = reader.Read();
	                break;
	            case InboundAction.RadarElevation:
	                radar.currentElevationAdjust = reader.Read();
	                break;
	            case InboundAction.Fire:
	                equipManager.FireSelectedMissile();
	                break;
	            case InboundAction.Flare:
	                FireFlare();
	                break;
	            case InboundAction.Chaff:
	                FireChaff();
	                break;
	            case InboundAction.ChaffFlare:
	                FireChaffFlare();
	                break;
	            case InboundAction.SelectHardpoint:
	                equipManager.selectedWeapon = reader.Read();
	                break;
	            case InboundAction.SetUncage:
	                equipManager.SetIRTrigUncage(reader.Read() > 0);
	                break;
	        }
	    }
	
	    public void FireChaffFlare()
	    {
	        if (irCountermeasure.count == 0 || chaffCountermeasure.count == 0) return;
	
	        irCountermeasure.FireFlare();
	        chaffCountermeasure.FireChaff();
	
	        manager?.connector?.SendCommandPacket(new UseCountermeasure { cmsType = 2 });
	    }
	
	    private void FireFlare()
	    {
	        if (!irCountermeasure.FireFlare()) return;
	        manager?.connector?.SendCommandPacket(new UseCountermeasure { cmsType = 0 });
	    }
	
	    private void FireChaff()
	    {
	        if (!chaffCountermeasure.FireChaff()) return;
	        manager?.connector?.SendCommandPacket(new UseCountermeasure { cmsType = 1 });
	    }
	
	    public void OnDamage()
	    {
	        var killPacket = new AIPKillEntity
	        {
	            entityId = entityId,
	        };
	
	        manager?.connector?.SendCommandPacket(killPacket);
	        LocalFightController.instance.HandleDamageRequest(this);
	    }
	
	    public OutboundState BuildState(List<KillFeedEntry> killFeed)
	    {
	        var state = new OutboundState
	        {
	            kinematics = new Kinematics
	            {
	                position = NetVector.From(rb.position),
	                rotation = NetQuaternion.From(rb.rotation),
	                velocity = NetVector.From(rb.linearVelocity),
	            },
	            rwrContacts = rwr.GetState(),
	            radar = radar.GetState(),
	            weapons = equipManager.GetAttachedWeapons(),
	            selectedWeapon = equipManager.selectedWeapon,
	            visualTargets = visualTargetFinder.GetState(),
	            ir = equipManager.GetIRWeaponState(),
	            datalink = datalink.GetState(),
	            killFeed = killFeed.ToArray(),
	            flareCount = irCountermeasure.count,
	            chaffCount = chaffCountermeasure.count,
	            gunAmmo = equipManager.hasGun ? equipManager.bulletCount : 0,
	            time = Time.time
	        };
	
	        return state;
	    }
	
	}
	
}