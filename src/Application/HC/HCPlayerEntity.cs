using UnityGERunner;
using Coroutine;
﻿using Recorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UnityGERunner.UnityApplication
{
	
	public class HCPlayerEntity : HCEntityBase, IDamageReceiver, IWeaponRCSProvider
	{
	    public Vector3 velocity = Vector3.zero;
	    public Vector3 acceleration = Vector3.zero;
	    public float throttle = 0;
	
	    public float rwrSensitivity = 1100;
	
	    public List<HCMissile> missiles = new List<HCMissile>();
	
	    protected override void Start()
	    {
	        GameRecorder.InitEntity(entityId, path, "HCPlayerEntity");
	    }
	
	    protected override void FixedUpdate()
	    {
	        velocity += acceleration * Time.fixedDeltaTime;
	        transform.position += velocity * Time.fixedDeltaTime;
	        missiles.RemoveAll(m => m == null || m.fired);
	    }
	
	    public bool CheckRWRDetectability(Vector3 radarPosition, float signalStrength)
	    {
	        float RWR_SENSITIVITY_MULT = 100;
	        return signalStrength * RWR_SENSITIVITY_MULT / (radarPosition - transform.position).sqrMagnitude >= 1 / rwrSensitivity;
	    }
	
	    public void OnMissileAttach(int weaponId)
	    {
	        var missile = manager.GetMissile(weaponId);
	
	        if (missile == null)
	        {
	            // Logger.Warn("[HSGE] " + $"Unable to find missile {weaponId} for missile attach onto {this}");
	            return;
	        }
	
	        missiles.Add(missile);
	    }
	
	    public float GetWeaponRCS()
	    {
	        return missiles.Select(m => m.rcs).Sum();
	    }
	
	    public void OnDamage()
	    {
	        var killPacket = new AIPKillEntity
	        {
	            entityId = entityId,
	        };
	        manager?.connector?.SendCommandPacket(killPacket);
	    }
	}
	
}