using UnityGERunner;
using Coroutine;
using System.Collections;
using System.Collections.Generic;

namespace UnityGERunner.UnityApplication
{
	
	internal class TestClass
	{
	    private TestClass _instance = new TestClass();
	    public TestClass instance { get { return _instance; } }
	
	    private int someValue = 5;
	
	    public void PrintValue()
	    {
	        Logger.Info("[HSGE] " + $"Test Class Some value is: {someValue}");
	    }
	}
	
	public class ModuleEngine : ActorBehaviour
	{
	    public HCPlayerEntity entity;
	
	    public float throttle = 1.0f;
	    public float abThrustMult = 1.22f;
	    public float maxThrust = 250;
	    public float autoAbThreshold = 0.75f;
	
	    public SOCurve speedCurve;
	    public SOCurve atmosCurve;
	
	    public float resultThrust = 0;
	
	    
	    public float thrustHeatMult = 25f;
	    public float abHeatAdd = 1f;
	    public HeatEmitter heatEmitter;
	
	    
	    public FuelTank fuelTank;
	    public float fuelDrain;
	    public float abDrainMult;
	
	
	    // private Rigidbody rb;
	    private KinematicPlane kp;
	
	
	    protected override void Start()
	    {
	        // rb = GetComponent<Rigidbody>();
	        kp = GetComponent<KinematicPlane>();
	    }
	
	    protected override void FixedUpdate()
	    {
	        if (entity != null) throttle = entity.throttle;
	
	        throttle = Mathf.Clamp01(throttle);
	        bool afterburner = throttle > autoAbThreshold;
	        float abMult = afterburner ? 1.0f : 0.0f;
	
	        float finalAbDrainMult = 1 + abMult * (abDrainMult - 1);
	        fuelTank.DrawFuel(finalAbDrainMult * fuelDrain * throttle * Time.fixedDeltaTime);
	
	        resultThrust = (1 + abMult * (abThrustMult - 1)) * throttle * maxThrust;
	        resultThrust *= speedCurve.Evaluate(actor.velocity.magnitude);
	        resultThrust *= atmosCurve.Evaluate(AerodynamicsController.fetch.AtmosPressureAtPosition(transform.position));
	
	        if (heatEmitter != null)
	        {
	            float abHeat = thrustHeatMult * (1 + abHeatAdd * abMult);
	            heatEmitter.AddHeat(resultThrust * abHeat * 0.4f * Mathf.Clamp(Time.fixedDeltaTime, 0.001f, 1f));
	        }
	
	        if (kp != null) kp.AddForce(resultThrust * transform.forward);
	    }
	}
	
}