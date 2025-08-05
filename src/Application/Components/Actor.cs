using UnityGERunner;
using Coroutine;
using Recorder;
using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityGERunner.UnityApplication
{
	
	
	public class Actor : MonoBehaviour
	{
	    //private static int nextId = 1;
	
	    public Team team = Team.Unknown;
	
	    // public int entityId = -1;//nextId++;
	
	    private Rigidbody rb = null;
	
	    private HCPlayerEntity hcEntity = null;
	    private HCMissile missile = null;
	    private Missile localMissile = null;
	    private AIClient aiClient = null;
	
	    public bool isMissile { get { return missile != null || localMissile != null; } }
	    public bool isMissileFired
	    {
	        get
	        {
	            if (missile != null) return missile.fired;
	            if (localMissile != null) return localMissile.fired;
	            return false;
	        }
	    }
	    public bool isLocal => aiClient != null || localMissile != null;
	
	    public Vector3 velocity
	    {
	        get
	        {
	            if (rb != null) return rb.linearVelocity;
	            if (hcEntity != null) return hcEntity.velocity;
	            if (localMissile != null) return localMissile.rb.linearVelocity;
	
	            Logger.Error("[HSGE] " + $"Actor has no velocity provider");
	            return Vector3.zero;
	        }
	    }
	
	    private int _entityId = -1;
	    public int entityId
	    {
	        get
	        {
	            if (_entityId >= 0) return _entityId;
	
	            if (hcEntity != null) _entityId = hcEntity.entityId;
	            else if (missile != null) _entityId = missile.entityId;
	            else if (localMissile != null) _entityId = localMissile.entityId;
	            else if (aiClient != null) _entityId = aiClient.entityId;
	            else Logger.Error("[HSGE] " + $"Actor has no entityId provider");
	
	            return _entityId;
	        }
	    }
	
	    protected override void Awake()
	    {
	        if (TryGetComponent<Rigidbody>(out var rb)) this.rb = rb;
	        if (TryGetComponent<HCPlayerEntity>(out var ent)) this.hcEntity = ent;
	        if (TryGetComponent<HCMissile>(out var missile)) this.missile = missile;
	        if (TryGetComponent<Missile>(out var lMissile)) this.localMissile = lMissile;
	        if (TryGetComponent<AIClient>(out var aiClient)) this.aiClient = aiClient;
	    }
	
	    protected override void Start()
	    {
	
	    }
	
	    protected override void FixedUpdate()
	    {
	        if (localMissile != null && !localMissile.fired) return;
	
	        if (aiClient != null) GameRecorder.Record(this, new NetVector(aiClient.kp.input), aiClient.engine.throttle, aiClient.fuelTank.fuelFraction);
	
	        else GameRecorder.Record(this);
	
	    }
	
	    protected override void OnDestroy()
	    {
	        GameRecorder.DeleteEntity(entityId);
	    }
	
	    // public int GetIdentifier()
	    // {
	    //     int num = 3;
	    // 
	    //     if (missile != null)
	    //     {
	    //         int num2 = 2;
	    //         int num3 = 1073741823;
	    //         return ((num2 & num) << 29) | (missile.entityId & num3);
	    //     }
	    // 
	    //     int num4 = 32767;
	    //     int num5 = 8191;
	    //     int num6 = 0;
	    //     int unitID = ;
	    //     int num7 = 0;
	    // 
	    // 
	    // }
	}
	
}