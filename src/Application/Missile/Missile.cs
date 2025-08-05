using UnityGERunner;
using Coroutine;
using Recorder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;



namespace UnityGERunner.UnityApplication
{
	
	public enum MissileGuidanceMode
	{
	    Radar,
	    Heat,
	}
	
	public class Missile : MonoBehaviour, IDamageReceiver, IMassObject
	{
	    private static Wait fixedWait = new Wait(GameEngine.FixedUpdateEvent);
	
	    
	    public float mass;
	    public float angularDrag;
	
	    
	    public MissileGuidanceMode guidanceMode;
	    public float leadTimeMultiplier = 1f;
	    
	    public float minGuidanceSimSpeed = 200f;
	
	    
	    public float boostThrust;
	    public float boostTime;
	    public float cruiseThrust;
	    public float cruiseTime;
	    public float thrustDelay;
	    public float initialKick;
	    private bool motorBurntOut = false;
	
	    
	    public float maxTorque;
	    public float maxTorqueSpeed = 300f;
	    public float torqueRampUpRate = 1f;
	    public float minTorqueSpeed = 100f;
	    public float torqueToPrograde;
	
	    
	    public float maxAoA;
	    public float steerMult;
	    public float steerIntegral;
	    public float maxSteerIntegralAccum;
	    public float maxLeadTime = 8f;
	    public bool squareInput;
	    public float maxBallisticOffset = 100f;
	    public float minBallisticCalcSpeed;
	    private Vector3 steerAccum;
	
	    
	    public float warheadRadius;
	    private Vector3 lastPosition;
	    private bool hasDetonated = false;
	
	    
	    public float pitbullRange;
	    // private bool active;
	    public Radar aamSearchRadar;
	    public float aamSearchFov;
	
	    public Actor commandRadarTarget;
	    public Radar commandRadar;
	
	    private float radarLostTime = -1f;
	
	    
	    public HeatSeeker heatSeeker;
	
	    public SimpleDrag simpleDrag;
	
	    private Transform debugCamTf;
	    public bool debugMissile;
	
	    public Rigidbody rb { get; private set; }
	
	
	    private Vector3 estTargetVel;
	    private Vector3 estTargetPos;
	    private Vector3 estTargetAccel;
	
	    private Vector3 finalTorque;
	    public bool fired { get; private set; }
	    private float timeFired;
	
	    private Vector3 prevPoint = Vector3.zero;
	
	    public bool limitMaxTorque = true;
	    public float aeroTorqueMult = 1;
	
	    public Color trailColor = Color.white;
	    public int entityId;
	
	    public string weaponPath;
	    public int hpIndex;
	
	    private WeaponStats.WeaponInfo _info;
	    public WeaponStats.WeaponInfo info
	    {
	        get
	        {
	            if (_info != null) return _info;
	
	            var weaponInfo = LocalFightController.instance.weaponStats.weaponPrefabs.Find(p => p.path == weaponPath);
	            if (weaponInfo == null) Logger.Error("[HSGE] " + $"Unable to resolve weapon stats for {weaponPath} ({entityId})");
	            _info = weaponInfo;
	
	            return _info;
	        }
	    }
	    public float rcs => info?.rcs ?? 0;
	
	
	    private Vector3 ip = Vector3.zero;
	    private Vector3 iv = Vector3.zero;
	
	    protected override void Start()
	    {
	        // CoroutineHandler.Start(DelayFire());
	        ip = transform.position;
	
	        if (aamSearchRadar != null) aamSearchRadar.enabled = false;
	    }
	
	    private IEnumerator<Wait> DelayFire()
	    {
	        yield return new Wait(1f);
	        Logger.Info("[HSGE] " + "Fire!");
	        Fire();
	    }
	
	    public void FireWithRadarData(Radar parentRadar, Actor target)
	    {
	        commandRadar = parentRadar;
	        commandRadarTarget = target;
	
	        Fire();
	    }
	
	    public void Fire()
	    {
	        fired = true;
	        timeFired = Time.time;
	        var parentRb = GetComponentInParent<Rigidbody>();
	        rb = gameObject.AddComponent<Rigidbody>();
	        rb.mass = mass;
	        rb.angularDamping = angularDrag;
	        //Logger.Info("[HSGE] " + $"Angular Damping: {rb.angularDamping} = {angularDrag}");
	        rb.isKinematic = false;
	        rb.linearVelocity = (parentRb?.linearVelocity ?? Vector3.zero) + initialKick * transform.forward;
	        rb.interpolation = RigidbodyInterpolation.Interpolate;
	
	        if (simpleDrag != null)
	        {
	            simpleDrag.enabled = true;
	            simpleDrag.rb = rb;
	        }
	        if (heatSeeker != null) heatSeeker.triggerUncaged = true;
	
	        var wings = GetComponentsInChildren<OmniWing>();
	        foreach (var wing in wings)
	        {
	            wing.enabled = true;
	            wing.SetParentRigidbody(rb);
	        }
	
	#if !HSGE
	        if (debugMissile)
	        {
	            Camera camera = new GameObject().AddComponent<Camera>();
	            camera.transform.parent = base.transform;
	            camera.transform.localPosition = new Vector3(0f, 2f, -15f);
	            camera.transform.localRotation = Quaternion.identity;
	            camera.fieldOfView = 40f;
	            camera.depth = 10f;
	            camera.stereoTargetEye = StereoTargetEyeMask.None;
	            camera.nearClipPlane = 1f;
	            camera.farClipPlane = 15000f;
	            camera.transform.rotation = Quaternion.LookRotation(camera.transform.forward);
	            camera.rect = new Rect(0.64f, 0.36f, 0.32f, 0.32f);
	            debugCamTf = camera.transform;
	        }
	#endif
	
	        transform.parent = null;
	        // iv = rb.velocity;
	
	        GameRecorder.InitEntity(entityId, weaponPath, "Missile", Team.Unknown);
	
	        CoroutineHandler.Start(ThrustRoutine());
	    }
	
	    private IEnumerator<Wait> ThrustRoutine()
	    {
	        yield return new Wait(thrustDelay);
	        //var vtolDefaultTimestep = 0.0111111f;
	
	        float boostEndTime = Time.time + boostTime;
	        float uot = 0;
	        while (Time.time < boostEndTime)
	        {
	            //var relTimestep = Time.fixedDeltaTime / vtolDefaultTimestep;
	
	            rb.AddRelativeForce(boostThrust * Vector3.forward);
	            uot += boostThrust;
	            yield return fixedWait;
	        }
	
	        float cruiseEndTime = Time.time + cruiseTime;
	        while (Time.time < cruiseEndTime)
	        {
	            //var relTimestep = Time.fixedDeltaTime / vtolDefaultTimestep;
	
	            rb.AddRelativeForce(cruiseThrust * Vector3.forward);
	            uot += cruiseThrust;
	            yield return fixedWait;
	        }
	
	        Logger.Info("[HSGE] " + $"Motor burnt out, generated {uot} units of thrust");
	        motorBurntOut = true;
	    }
	
	    private void PredictTargetMotion()
	    {
	        estTargetPos += estTargetVel * Time.fixedDeltaTime;
	    }
	
	    private void RunPitbullRoutine()
	    {
	        if (estTargetPos.sqrMagnitude == 0) return;
	
	        aamSearchRadar.enabled = true;
	
	        Vector3 lookDir = Vector3.RotateTowards(transform.forward, estTargetPos - transform.position, (float)Math.PI / 180f * aamSearchFov / 2f, 0f);
	        aamSearchRadar.transform.rotation = Quaternion.LookRotation(lookDir);
	
	        if (commandRadarTarget != null)
	        {
	            if (aamSearchRadar.IsActorDetected(commandRadarTarget))
	            {
	                if (aamSearchRadar.AttemptAcquireSTT(commandRadarTarget))
	                {
	                    commandRadarTarget = null;
	                    commandRadar = null;
	                    Logger.Info("[HSGE] " + $"Missile pitbulled on target via direct command");
	                }
	            }
	        }
	        else
	        {
	            if (aamSearchRadar.detectedTargets.Count > 0)
	            {
	                if (aamSearchRadar.AttemptAcquireSTT(aamSearchRadar.detectedTargets[0].actor))
	                {
	                    Logger.Info("[HSGE] " + $"Missile pitbulled on target after midcourse was dropped");
	                }
	            }
	        }
	    }
	
	    private void UpdateRadarTargetData()
	    {
	        // Logger.Info("[HSGE] " + $"Has STT target: {aamSearchRadar.sttedTarget != null}");
	        if (aamSearchRadar.hasSttTarget)
	        {
	            var targetData = aamSearchRadar.GetLockedTargetData();
	            estTargetPos = targetData.position;
	            estTargetVel = targetData.velocity;
	
	            Vector3 lookDir = Vector3.RotateTowards(transform.forward, estTargetPos - transform.position, (float)Math.PI / 180f * aamSearchFov / 2f, 0f);
	            aamSearchRadar.transform.rotation = Quaternion.LookRotation(lookDir);
	
	            return;
	        }
	
	        bool pitbull = (transform.position - estTargetPos).sqrMagnitude < pitbullRange * pitbullRange;
	        if (pitbull) RunPitbullRoutine();
	
	        if (commandRadar != null && commandRadarTarget != null)
	        {
	            if (commandRadar.TryGetTargetData(commandRadarTarget, out var td))
	            {
	                estTargetPos = td.position;
	                estTargetVel = td.velocity;
	
	                return;
	            }
	            else
	            {
	                Logger.Info("[HSGE] " + $"Midcourse guidence lost");
	                commandRadar = null;
	                commandRadarTarget = null;
	                return;
	            }
	        }
	
	        PredictTargetMotion();
	    }
	
	    private void UpdateTargetData()
	    {
	        Vector3 vector = estTargetVel;
	        float fixedDeltaTime = Time.fixedDeltaTime;
	        switch (guidanceMode)
	        {
	            case MissileGuidanceMode.Radar:
	                UpdateRadarTargetData();
	                break;
	            case MissileGuidanceMode.Heat:
	                if (heatSeeker.seekerLock > 0)
	                {
	                    estTargetPos = heatSeeker.targetPosition;
	                    estTargetVel = heatSeeker.targetVelocity;
	                }
	                else
	                {
	                    PredictTargetMotion();
	                }
	                break;
	            default:
	                Logger.Error("[HSGE] " + $"Unhandled guidance type {guidanceMode}");
	                break;
	        }
	
	        estTargetAccel = (estTargetVel - vector) / fixedDeltaTime;
	        // lastTargetDistance = Vector3.Distance(transform.position, estTargetPos);
	    }
	
	    private Vector3 BallisticLeadTargetPoint()
	    {
	        float num = 0f;
	        float num2 = Vector3.Distance(estTargetPos, transform.position);
	        Vector3 vector = Mathf.Max(rb.linearVelocity.magnitude, minGuidanceSimSpeed) * rb.linearVelocity.normalized;
	        num = num2 / (estTargetVel - vector).magnitude;
	        num = Mathf.Clamp(leadTimeMultiplier * num, 0f, maxLeadTime);
	        Vector3 vector2 = estTargetPos + estTargetVel * num;
	        if (num2 < 1000f)
	        {
	            vector2 += 0.5f * num * num * estTargetAccel;
	        }
	        Vector3 result = vector2;
	        if (maxBallisticOffset > 0f)
	        {
	            float num3 = Mathf.Lerp(3f, 1f, maxBallisticOffset / 90f);
	            var ree = (estTargetPos.y > transform.position.y) ? 45 : 25;
	            result = BallisticPoint(vector2, rb.linearVelocity.magnitude * num3, (estTargetPos.y > transform.position.y) ? 45 : 25);
	        }
	
	        return result;
	    }
	
	    private Vector3 BallisticPoint(Vector3 targetPosition, float speed, float loftAngle = 45f)
	    {
	        Vector3 up = Vector3.up;
	        Vector3 vector = Vector3.ProjectOnPlane(targetPosition - transform.position, up);
	        float num = speed * speed;
	        float num2 = num * num;
	        float magnitude = Physics.gravity.magnitude;
	        float num3 = targetPosition.y - transform.position.y;
	        float sqrMagnitude = vector.sqrMagnitude;
	        float num4 = Mathf.Sqrt(sqrMagnitude);
	        float num5 = ((1 == 0) ? 1 : (-1));
	        float num6 = num + num5 * Mathf.Sqrt(num2 - magnitude * (magnitude * sqrMagnitude + 2f * num3 * num));
	        float num7 = magnitude * num4;
	        float num8 = Mathf.Atan(num6 / num7);
	
	        Vector3 vector2 = (float.IsNaN(num8) ? (Quaternion.AngleAxis(loftAngle, Vector3.Cross(vector, up)) * vector) : (Quaternion.AngleAxis(num8 * 57.29578f, Vector3.Cross(vector, up)) * vector));
	        var r = transform.position + vector2.normalized * (targetPosition - transform.position).magnitude;
	        return r;
	    }
	
	    private void SteerToTarget()
	    {
	        if (Time.time - timeFired < thrustDelay) return;
	
	        Vector3 aimPoint = BallisticLeadTargetPoint();
	
	        float aoa = Vector3.Angle(rb.transform.forward, rb.linearVelocity);
	        Vector3 missileTravelDir = rb.linearVelocity.normalized;
	        Vector3 aimError = aimPoint - transform.position;
	        Vector3 targetDir = estTargetPos - transform.position;
	
	        if (guidanceMode == MissileGuidanceMode.Radar)
	        {
	            if (aamSearchRadar.hasSttTarget)
	            {
	                var pap = aimError;
	                aimError = Vector3.RotateTowards(targetDir, aimError, aamSearchRadar.lockFov / 2f * ((float)Math.PI / 180f) * 0.75f, 0f);
	            }
	        }
	        else if (guidanceMode == MissileGuidanceMode.Heat)
	        {
	            aimError = Vector3.RotateTowards(targetDir, aimError, heatSeeker.gimbalFOV / 2f * ((float)Math.PI / 180f) * 0.75f, 0f);
	        }
	
	
	        aimError.Normalize();
	
	        float command = Vector3.Angle(missileTravelDir, aimError) * Mathf.Clamp01(2f - 2f * (aoa / Mathf.Max(maxAoA, 0.0001f)));
	        float aeroForce = Mathf.Clamp01(AerodynamicsController.fetch.atmosDensityCurve.Evaluate(transform.position.y) * rb.linearVelocity.sqrMagnitude / (maxTorqueSpeed * maxTorqueSpeed));
	        if (steerIntegral > 0f)
	        {
	            Vector3 vector4 = aimError - missileTravelDir;
	            steerAccum += steerIntegral * vector4 * Time.fixedDeltaTime;
	            steerAccum = Vector3.ClampMagnitude(steerAccum, maxSteerIntegralAccum);
	            aimError += steerAccum;
	        }
	
	        Vector3 direction = -Vector3.Cross(aimError, missileTravelDir);
	        direction = rb.transform.InverseTransformDirection(direction);
	        float finalCommand = (squareInput ? (0.25f * command * command) : command);
	        float torque = Mathf.Clamp(finalCommand * steerMult * aeroForce, 0f, maxTorque);
	
	        if (limitMaxTorque)
	        {
	            // Logger.Info("[HSGE] " + $"Traditional torque: {torque}, aeroForce: {aeroForce}, airspeed: {rb.velocity.magnitude}");
	        }
	        else
	        {
	            // torque = finalCommand * steerMult * aeroForce * aeroTorqueMult;
	            // Logger.Info("[HSGE] " + $"New system torque: {torque} af: {aeroForce}, command: {finalCommand}");
	        }
	
	        direction.z = 0f;
	        finalTorque = direction.normalized * torque;
	    }
	
	    private void UpdateWarhead()
	    {
	        var distanceToTarget = (transform.position - estTargetPos).magnitude;
	        //if (distanceToTarget < warheadRadius)
	        //{
	        //    Logger.Info("[HSGE] " + $"Missile {this} got within {distanceToTarget:f2}m of target, detonating");
	        //    Detonate();
	        //    return;
	        //}
	
	
	        if (lastPosition == Vector3.zero) lastPosition = transform.position;
	        var lastDistanceToTarget = (lastPosition - estTargetPos).magnitude;
	
	        // Do fine-grained check by stepping through points
	        if (distanceToTarget < rb.linearVelocity.magnitude / 10)
	        {
	            int stepCount = 20;
	            var currentPoint = lastPosition;
	            var delta = transform.position - lastPosition;
	            var step = delta.normalized * (delta.magnitude / 20);
	
	            bool willDetonate = false;
	            float lastRange = 0;
	
	            for (int i = 0; i < stepCount; i++)
	            {
	                currentPoint += step;
	                var curPointDistToTarget = (currentPoint - estTargetPos).magnitude;
	                if (willDetonate && curPointDistToTarget > lastRange)
	                {
	                    transform.position = currentPoint;
	                    Logger.Info("[HSGE] " + $"(Fine grained) Missile {this} got within {distanceToTarget:f2}m of target, detonating");
	                    Detonate();
	                    return;
	                }
	
	                if (curPointDistToTarget < warheadRadius)
	                {
	                    willDetonate = true;
	                    lastRange = curPointDistToTarget;
	                }
	            }
	        }
	
	        if (motorBurntOut && distanceToTarget > lastDistanceToTarget)
	        {
	            Logger.Info("[HSGE] " + $"Missile {this} has negative closure, and motor is burnt out, detonating");
	            Detonate();
	            return;
	        }
	
	        lastPosition = transform.position;
	    }
	
	    private void Detonate()
	    {
	        if (hasDetonated)
	        {
	            Logger.Warn("[HSGE] " + $"Missile {this} detonated multiple times");
	            return;
	        }
	
	        hasDetonated = true;
	        var damageReceivers = gameObject.GetComponentsInSceneImplementing<IDamageReceiver>();
	        var warheadRadiusSq = warheadRadius * warheadRadius;
	        foreach (var dmgRec in damageReceivers)
	        {
	            if (dmgRec == this) continue;
	
	            var tf = ((MonoBehaviour)dmgRec)?.transform;
	            if (tf == null)
	            {
	                Logger.Error("[HSGE] " + $"Got IDamageReceiver {dmgRec} that had no transform");
	                continue;
	            }
	
	            var distSq = (transform.position - tf.position).sqrMagnitude;
	            if (distSq < warheadRadiusSq)
	            {
	                Logger.Info("[HSGE] " + $"Warhead hit {dmgRec} (iam: {this})");
	                dmgRec.OnDamage();
	            }
	        }
	
	        Destroy(gameObject);
	    }
	
	    protected override void FixedUpdate()
	    {
	        //Logger.Info("[HSGE] " + $"Missile pos: {transform.position}");
	
	        if (!fired) return;
	        //Logger.Info("[HSGE] " + $"vel={rb.linearVelocity.magnitude:f0}m/s   Forward: {transform.forward}");
	        UpdateTargetData();
	        SteerToTarget();
	        UpdateWarhead();
	        rb.AddRelativeTorque(finalTorque);
	        //Logger.Info("[HSGE] " + $"Time: {Time.time}, ft: {finalTorque}, rot:{transform.rotation.eulerAngles}");
	
	        SyncData();
	
	        // transform.position = ip;
	        // rb.velocity = iv;
	
	        if (debugMissile && (bool)debugCamTf)
	        {
	            debugCamTf.transform.position = base.transform.position + -15f * rb.linearVelocity.normalized + 2f * Vector3.up;
	            debugCamTf.LookAt(base.transform.position);
	        }
	
	        if (prevPoint.sqrMagnitude > 0)
	        {
	            var color = limitMaxTorque ? Color.white : Color.blue;
	            Debug.DrawLine(transform.position, prevPoint, trailColor, 10);
	        }
	        prevPoint = transform.position;
	    }
	
	    private void SyncData()
	    {
	        var packet = new AIPMissileData
	        {
	            position = NetVector.From(transform.position),
	            velocity = NetVector.From(rb.linearVelocity),
	            acceleration = NetVector.From(Vector3.zero),
	            rotation = NetVector.From(transform.rotation.eulerAngles),
	            detonate = hasDetonated,
	            entityId = entityId
	        };
	        HCConnector.instance.SendCommandPacket(packet);
	    }
	
	
	    public void OnDamage()
	    {
	        if (!fired) return;
	        Detonate();
	    }
	
	    public float GetMass()
	    {
	        return mass;
	    }
	}
	
}