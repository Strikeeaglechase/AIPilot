using UnityGERunner;
using Coroutine;
using Recorder;
using System.Collections;
using System.Collections.Generic;



namespace UnityGERunner.UnityApplication
{
	
	public class KinematicPlane : MonoBehaviour, IVehicleReadyNotificationHandler
	{
	    public AnimationCurve maxAoAcurve;
	    public AnimationCurve maxGCurve;
	    public AnimationCurve rollRateCurve;
	    public AnimationCurve lerpCurve;
	
	    public ModuleEngine engine;
	    public Vector3 input;
	    public float brake = 0;
	    public float flaps = 0;
	
	    public float flapsMultiplier = 1.2f;
	    public float wingSweep = 20f;
	    public float pitchLerpMult;
	    public float rollLerpMult;
	    public float yawGMult;
	    public float yawAoAMult;
	    public float dragArea;
	    public float brakeDrag;
	    private float dragFac;
	    private float currRollRate;
	
	    public Vector3 fixedPoint;
	
	    public Rigidbody rb;
	
	    private float lerpedPitch;
	
	    private Vector3 externalForces;
	
	    // private FlightInfo flightInfo;
	
	    private bool gotActor;
	
	    private Actor _actor;
	
	    private Transform myTransform;
	
	    private float referenceMass;
	
	    private float seaLevelAtmosDens;
	
	
	    public Vector3 velocity { get; private set; }
	
	    public Vector3 acceleration { get; private set; }
	
	    public Actor actor
	    {
	        get
	        {
	            if (!gotActor)
	            {
	                _actor = GetComponent<Actor>();
	                gotActor = true;
	            }
	            return _actor;
	        }
	    }
	
	    public bool forceDynamic { get; private set; }
	
	    private float massAdjust => 1f;
	
	    public bool paused = false;
	    private bool readyForSync = false;
	    public bool debug = false;
	
	    protected override void Awake()
	    {
	        myTransform = base.transform;
	        // flightInfo = GetComponent<FlightInfo>();
	        if (!rb) rb = GetComponent<Rigidbody>();
	        // actor.fixedVelocityUpdate = rb.interpolation == RigidbodyInterpolation.None;
	        MassUpdater component = GetComponent<MassUpdater>();
	        if ((bool)component) referenceMass = component.baseMass;
	        else referenceMass = rb.mass;
	    }
	
	    protected override void Start()
	    {
	        seaLevelAtmosDens = AerodynamicsController.fetch.AtmosDensityAtPosition(Vector3.zero);
	        fixedPoint = transform.position;
	    }
	
	    protected override void FixedUpdate()
	    {
	        // transform.position = Vector3.zero;
	        // rb.velocity = Vector3.zero;
	        // return;
	        if (!rb.isKinematic) return;
	
	
	        if (paused)
	        {
	            velocity = Vector3.zero;
	            rb.linearVelocity = velocity;
	            acceleration = Vector3.zero;
	            SyncData();
	            return;
	        }
	
	
	        fixedPoint = transform.position;
	
	        input.x = Mathf.Clamp(input.x, -1f, 1f);
	        input.y = Mathf.Clamp(input.y, -1f, 1f);
	        input.z = Mathf.Clamp(input.z, -1f, 1f);
	
	        float magnitude = velocity.magnitude;
	        if (float.IsNaN(magnitude))
	        {
	            Logger.Warn("[HSGE] " + "Kplane speed is NaN");
	            velocity = 150f * myTransform.forward;
	            magnitude = 150f;
	            return;
	        }
	
	        if (magnitude < 1E-05f)
	        {
	            Logger.Warn("[HSGE] " + "Kplane speed is too small");
	            velocity = 150f * myTransform.forward;
	            magnitude = 150f;
	            return;
	        }
	
	        float num = lerpCurve.Evaluate(magnitude);
	        float num2 = maxAoAcurve.Evaluate(magnitude);
	        float num3 = maxGCurve.Evaluate(magnitude) * 9.81f * massAdjust;
	        num3 += flaps * (flapsMultiplier - 1f) * num3;
	        float num4 = AerodynamicsController.fetch.AtmosDensityAtPosition(myTransform.position) / seaLevelAtmosDens;
	        num3 = Mathf.Lerp(num3, num3 * num4, 0.5f);
	        if (debug)
	        {
	            //GameRecorder.Graph("up", myTransform.up);
	            //GameRecorder.Graph("lookrot", Quaternion.LookRotation(velocity, myTransform.up).eulerAngles);
	        }
	        Quaternion b = Quaternion.LookRotation(velocity, myTransform.up) * Quaternion.Euler(input.x * num2, input.y * yawAoAMult * num2, 0f);
	        lerpedPitch = Mathf.Lerp(lerpedPitch, input.x, num * pitchLerpMult * Time.fixedDeltaTime);
	        Quaternion quaternion = Quaternion.Slerp(rb.rotation, b, num * pitchLerpMult * Time.fixedDeltaTime);
	        currRollRate = Mathf.Lerp(currRollRate, input.z * rollRateCurve.Evaluate(magnitude), num * rollLerpMult * Time.fixedDeltaTime);
	        quaternion = Quaternion.AngleAxis(currRollRate * Time.fixedDeltaTime, myTransform.forward) * quaternion;
	        Vector3 vector = lerpedPitch * Vector3.Slerp(Vector3.Cross(myTransform.right, velocity).normalized, -myTransform.up, 0.5f);
	        Vector3 vector2 = yawGMult * input.y * myTransform.right;
	        float num5 = AerodynamicsController.fetch.AtmosDensityAtPosition(myTransform.position);
	        dragFac = 0.5f * (dragArea + brakeDrag * brake + flaps * dragArea * 0.75f);
	        float num6 = dragFac * magnitude * num5 * AerodynamicsController.fetch.DragMultiplierAtSpeedAndSweep(magnitude, myTransform.position.y, wingSweep);
	        externalForces += -velocity * num6;
	        Vector3 vector4 = (acceleration = externalForces / rb.mass + Physics.gravity + (vector + vector2) * num3);
	        velocity += vector4 * Time.fixedDeltaTime;
	        externalForces = Vector3.zero;
	        fixedPoint += velocity * Time.fixedDeltaTime;
	
	        rb.MovePosition(fixedPoint);
	        rb.MoveRotation(quaternion);
	
	        if (debug)
	        {
	            GameRecorder.Graph("kppos", transform.position);
	            //GameRecorder.Graph("kprot", transform.rotation.eulerAngles);
	            //GameRecorder.Graph("kpvel", velocity);
	            //GameRecorder.Graph("rotb", b.eulerAngles);
	        }
	
	        rb.linearVelocity = velocity;
	
	        SyncData();
	    }
	
	    private void SyncData()
	    {
	        if (!readyForSync) return;
	
	        var packet = new AIPData
	        {
	            position = NetVector.From(myTransform.position),
	            velocity = NetVector.From(velocity),
	            acceleration = NetVector.From(acceleration),
	            rotation = NetVector.From(transform.rotation.eulerAngles),
	            pyr = NetVector.From(input),
	            throttle = engine.throttle,
	        };
	        HCConnector.instance.SendCommandPacket(packet);
	    }
	
	    public void AddForce(Vector3 force)
	    {
	        externalForces += force;
	    }
	
	    public void SetToDynamic()
	    {
	        if (!rb) rb = GetComponent<Rigidbody>();
	
	        if (rb.isKinematic)
	        {
	            rb.isKinematic = false;
	            rb.linearVelocity = velocity;
	        }
	    }
	
	    public void SetToKinematic()
	    {
	        if (!forceDynamic)
	        {
	            if (!rb) rb = GetComponent<Rigidbody>();
	
	            if (!rb.isKinematic)
	            {
	                // ResetForces();
	                velocity = rb.linearVelocity;
	                rb.isKinematic = true;
	                fixedPoint = rb.position;
	            }
	        }
	    }
	
	    public float GetTurningRadius(float speed)
	    {
	        float num = maxGCurve.Evaluate(speed) * 9.81f;
	        return speed * speed / num;
	    }
	
	    public void OnVehicleReadyNotification()
	    {
	        readyForSync = true;
	    }
	
	    //public void OnDamage()
	    //{
	    //    throw new System.NotImplementedException();
	    //}
	}
}