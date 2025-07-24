using UnityGERunner;
using Coroutine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityGERunner.UnityApplication
{
	
	public class HeatEmitter : ActorBehaviour
	{
	    public static List<HeatEmitter> emitters = new List<HeatEmitter>();
	    public bool isMissile;
	    public bool isCountermeasure;
	    public float heat;
	
	    public float cooldownRate;
	    private float finalCooldown = 0.0001f;
	
	    private Vector3 _vel;
	    public Vector3 velocity
	    {
	        get
	        {
	            if (!actor)
	            {
	                return _vel;
	            }
	            return actor.velocity;
	        }
	        set
	        {
	            _vel = value;
	        }
	    }
	
	    public void AddHeat(float addHeat)
	    {
	        heat += addHeat;
	    }
	
	    public void SetCooldownRate(float r)
	    {
	        cooldownRate = r;
	        finalCooldown = 0.014f * cooldownRate;
	    }
	
	    protected override void Start()
	    {
	        SetCooldownRate(cooldownRate);
	    }
	
	    protected override void FixedUpdate()
	    {
	        heat = Mathf.Lerp(heat, 0, Mathf.Min(0.5f, finalCooldown * Time.fixedDeltaTime));
	    }
	
	    protected override void OnEnable()
	    {
	        emitters.Add(this);
	    }
	
	    protected override void OnDisable()
	    {
	        emitters.Remove(this);
	    }
	}
	
}