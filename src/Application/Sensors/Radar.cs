using UnityGERunner;
using Coroutine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;


namespace UnityGERunner.UnityApplication
{
	
	public class RadarLockData : TargetData
	{
	    public float failTimer = 0;
	
	    public bool lockedChaff = false;
	    public float chaffLockEndTime = 0;
	
	    public float actualReturnSignal = 0;
	
	    public ChaffCountermeasure chaffModule;
	
	    public RadarLockData(Actor actor)
	    {
	        this.actor = actor;
	        position = actor.transform.position;
	        velocity = actor.velocity;
	        chaffModule = actor.GetComponent<ChaffCountermeasure>();
	        // Logger.Info("[HSGE] " + $"RLD Chaff: {chaffModule}");
	    }
	
	    public TargetData GetTargetData()
	    {
	        return new TargetData
	        {
	            position = position,
	            velocity = velocity,
	            actor = actor,
	        };
	    }
	}
	
	public class TargetData
	{
	    public Vector3 position;
	    public Vector3 velocity;
	    public Actor actor;
	    public float detectedTime;
	
	    public TargetData() { detectedTime = Time.time; }
	    public TargetData(Actor actor)
	    {
	        this.actor = actor;
	        position = actor.transform.position;
	        velocity = actor.velocity;
	        detectedTime = Time.time;
	    }
	
	    public void UpdateFromActor(Actor actor)
	    {
	        position = actor.transform.position;
	        velocity = actor.velocity;
	        detectedTime = Time.time;
	    }
	}
	
	public struct StateTargetData
	{
	    public NetVector position;
	    public NetVector velocity;
	    public Team team;
	    public int id;
	
	    public static StateTargetData FromTargetData(TargetData td)
	    {
	        if (td == null)
	        {
	            return StateTargetData.invalid;
	        }
	
	        return new StateTargetData
	        {
	            position = NetVector.From(td.position),
	            velocity = NetVector.From(td.velocity),
	            id = td.actor.entityId,
	            team = td.actor.team
	        };
	    }
	
	    public static StateTargetData invalid = new StateTargetData { id = -1, team = Team.Unknown };
	}
	
	public struct MinimalDetectedTargetData
	{
	    public int id;
	    public Team team;
	    public float detectedTime;
	
	    public MinimalDetectedTargetData(TargetData td)
	    {
	        id = td.actor.entityId;
	        team = td.actor.team;
	        detectedTime = td.detectedTime;
	    }
	}
	
	public struct RadarState
	{
	    public float angle;
	    public float elevationAdjust;
	    public float azimuthAdjust;
	    public float fov;
	
	    public StateTargetData[] twsedTargets;
	    public MinimalDetectedTargetData[] detectedTargets;
	    public StateTargetData? sttedTarget;
	}
	
	
	public class Radar : ActorBehaviour
	{
	    private float angle = 0;
	    private float rotationDirection = 1f;
	
	    public float rotationSpeed;
	    public float rotationRange;
	    public float verticalFov;
	    public float lockFov = 120;
	
	    public float transmissionStrength;
	    public float receiverSensitivity;
	
	    public float detectPersistTime = 0.5f;
	
	    public bool debugRadar = false;
	
	    public float maxAzimuthOffset = -1;
	    public float maxElevationOffset = 30;
	
	    
	    public float currentElevationAdjust = 0;
	    
	    public float currentAzimuthAdjust = 0;
	
	    public bool autolock = false;
	    public bool unlock = false;
	    public int radarActorId = -1;
	    private bool hasLoggedRwrNotifError = false;
	
	    
	    public float radarFov = 120;
	
	    private float speedGate = 10f;
	    private float rangeGate = 20f;
	
	    private const float RADAR_LOCK_FOV = 2;
	
	
	    public Transform rotationTransform;
	
	    public Transform elevationTf;
	    public Transform azimuthTf;
	    public Transform referenceForwardTf;
	
	    public List<Actor> twsedTargets = new List<Actor>();
	    public int pdtTwsIdx = 0;
	    public Actor initSttedTarget = null;
	    public RadarLockData lockData;
	    public bool hasSttTarget { get { return enabled && lockData != null; } }
	
	    public List<TargetData> detectedTargets = new List<TargetData>();
	
	    protected override void Start()
	    {
	        if (initSttedTarget != null)
	        {
	            AttemptAcquireSTT(initSttedTarget);
	        }
	    }
	
	    protected override void FixedUpdate()
	    {
	        if (Time.time < 0.1) return; // Hack fix to prevent kplane velocity change from breaking init locks
	
	        UpdateRadarScanVolume();
	        detectedTargets.RemoveAll(td => td.actor == null || Time.time - td.detectedTime > detectPersistTime);
	
	        if (lockData == null)
	        {
	            RunRotation();
	            ProcessUnits();
	
	            if (autolock && detectedTargets.Count() > 0)
	            {
	                AttemptAcquireSTT(detectedTargets[0].actor);
	            }
	        }
	        else
	        {
	            bool targetUpdated = UpdateSTT();
	            if (lockData != null)
	            {
	                if (!targetUpdated) lockData.failTimer += Time.fixedDeltaTime;
	                else lockData.failTimer = 0;
	
	
	                if (lockData.failTimer > 0.5f) DropSTT("Failed for >.5s");
	            }
	        }
	
	        var twsTargets = twsedTargets;
	        twsedTargets = new List<Actor>();
	
	        foreach (var t in twsTargets)
	        {
	            if (t == null) continue;
	            UpdateOrStartTWSOnTarget(t);
	        }
	    }
	
	    public TargetData GetDetectedTargetById(int id)
	    {
	        return detectedTargets.Find(dt => dt.actor.entityId == id);
	    }
	
	    private void AddOrUpdateDetection(Actor actor)
	    {
	        var existing = detectedTargets.Find(td => td.actor == actor);
	        if (existing != null) existing.UpdateFromActor(actor);
	        else detectedTargets.Add(new TargetData(actor));
	    }
	
	    public bool AttemptAcquireSTT(Actor target)
	    {
	        var canDetect = CheckCanDetectActor(target);
	        if (!canDetect) return false;
	        lockData = new RadarLockData(target);
	        UpdateSTT(true);
	        return true;
	    }
	
	    public bool TryGetTargetData(Actor actor, out TargetData targetData)
	    {
	        var td = GetTargetData(actor);
	        targetData = td;
	        return td != null;
	    }
	
	    public TargetData GetTargetData(Actor target)
	    {
	        if (target == null) return null;
	        if (lockData != null && lockData.actor == target) return lockData.GetTargetData();
	        if (twsedTargets.Contains(target))
	        {
	            return new TargetData
	            {
	                actor = target,
	                position = target.transform.position,
	                velocity = target.velocity
	            };
	        }
	
	        return null;
	    }
	
	    public TargetData GetLockedTargetData()
	    {
	        if (!hasSttTarget) return null;
	        return lockData.GetTargetData();
	    }
	
	    private bool InLockFOVLimits(Vector3 pos)
	    {
	        var refForward = referenceForwardTf ? referenceForwardTf : transform;
	
	        var lookDir = pos - refForward.position;
	        var lookAngle = Vector3.Angle(lookDir, refForward.forward);
	
	        return lookAngle <= lockFov / 2;
	    }
	
	    private bool CheckChaff()
	    {
	        if (lockData.chaffModule == null) return false;
	        return Vector3.Angle(lockData.position - rotationTransform.position, lockData.actor.transform.position - rotationTransform.position) > RADAR_LOCK_FOV / 2f;
	    }
	
	    private bool UpdateSTT(bool initialAcquire = false)
	    {
	        var canSeeTarget = CheckCanDetectActor(lockData.actor, 8);
	        if (!canSeeTarget)
	        {
	            DropSTT($"STT Lost (cannot detect)");
	            return false;
	        }
	
	        if (unlock)
	        {
	            unlock = false;
	            DropSTT("Unlock command");
	            return false;
	        }
	
	        //lockData.position = lockData.actor.transform.position;
	        //lockData.velocity = lockData.actor.velocity;
	        //return true; // Temp radar cheats
	
	        // detectedTargets.Add(lockData.actor);
	
	        if (lockData.lockedChaff)
	        {
	            lockData.velocity = Vector3.MoveTowards(lockData.velocity, Vector3.zero, 9.81f * Time.fixedDeltaTime);
	            if (Time.time > lockData.chaffLockEndTime)
	            {
	                return false;
	            }
	
	            if (!CheckChaff())
	            {
	                lockData.lockedChaff = false;
	            }
	        }
	        else if (lockData.chaffModule != null)
	        {
	            var lockLookDir = lockData.position - rotationTransform.position;
	            float distance = lockLookDir.magnitude;
	            float xClosingSpeed = Vector3.Dot(lockLookDir.normalized, lockData.velocity);
	            if (lockData.chaffModule.GetECMAffectedPos(this, lockData.actualReturnSignal, lockLookDir.normalized, RADAR_LOCK_FOV, distance, rangeGate, xClosingSpeed, speedGate, out var rPos, out var rVel, out var receivePing, initialAcquire, debugRadar))
	            {
	                lockData.position = rPos;
	                lockData.velocity = rVel;
	                if (receivePing)
	                {
	                    SendPingTo(lockData.actor, true);
	                }
	                // lockData.targetIdentity = lockData.ecmModule.QueryIdentityByRadar(referenceTransform.position, lockData.actualReturnSignal, out var _);
	            }
	            else
	            {
	                if (debugRadar)
	                {
	                    // DebugPosition.Create(lockData.position, "Lock Pos at Unlock", 5f, 5f, Color.red);
	                    // DebugPosition.Create(lockData.actor.position, "ActorPos at Unlock", 5f, 5f, Color.green);
	                }
	                return false;
	            }
	        }
	        else
	        {
	            lockData.position = lockData.actor.transform.position;
	            lockData.velocity = lockData.actor.velocity;
	        }
	
	        // SendPingTo(lockData.actor, true);
	
	        return true;
	    }
	
	    private void DropSTT(string reason)
	    {
	        Logger.Info("[HSGE] " + reason);
	        lockData = null;
	    }
	
	    public void UpdateOrStartTWSOnTarget(Actor actor)
	    {
	        if (twsedTargets.Count >= 4) return;
	        if (twsedTargets.Contains(actor)) return;
	        var canSee = CheckCanDetectActor(actor, 2);
	        if (!canSee)
	        {
	            Logger.Info("[HSGE] " + $"TWS track on {actor} lost (detectability)");
	            return;
	        }
	
	        if (!InLockFOVLimits(actor.transform.position))
	        {
	            Logger.Info("[HSGE] " + $"TWS track on {actor} lost (FOV limits)");
	            return;
	        }
	
	        SendPingTo(actor, false);
	        twsedTargets.Add(actor);
	        AddOrUpdateDetection(actor);
	    }
	
	    public bool IsActorDetected(Actor actor)
	    {
	        return detectedTargets.Any(dt => dt.actor == actor);
	    }
	
	    private void UpdateRadarScanVolume()
	    {
	        if (referenceForwardTf == null || azimuthTf == null || elevationTf == null) return;
	
	        Vector3 groundedForward = referenceForwardTf.forward;
	        groundedForward.y = 0f;
	        float lookupAngle = SignedAngle(groundedForward, referenceForwardTf.forward, Vector3.up);
	
	        Vector3 cross = Vector3.Cross(Vector3.up, groundedForward);
	        Vector3 target = Quaternion.AngleAxis(currentElevationAdjust, -cross) * groundedForward;
	        target = Vector3.RotateTowards(referenceForwardTf.forward, target, maxElevationOffset * ((float)Math.PI / 180f), 0f);
	        elevationTf.rotation = Quaternion.LookRotation(target, Vector3.up);
	
	        maxAzimuthOffset = (120 / 2) - (radarFov / 2);
	        rotationRange = radarFov;
	
	        currentAzimuthAdjust = Mathf.Clamp(currentAzimuthAdjust, 0f - maxAzimuthOffset, maxAzimuthOffset);
	        azimuthTf.localEulerAngles = new Vector3(0f, currentAzimuthAdjust, 0f);
	
	        azimuthTf.rotation = Quaternion.LookRotation(azimuthTf.forward, Vector3.up);
	    }
	
	    private void ProcessUnits()
	    {
	        float dotThresh = Mathf.Cos(0.6f * Time.fixedDeltaTime * rotationSpeed * ((float)Math.PI / 180f));
	
	        var units = FindObjectsByType<Actor>(FindObjectsSortMode.None);
	
	        foreach (var unit in units)
	        {
	            if (unit == actor) continue;
	            var detected = ProcessUnit(unit, dotThresh);
	            if (detected) AddOrUpdateDetection(unit);
	        }
	    }
	
	    private bool ProcessUnit(Actor actor, float dotThresh)
	    {
	        if (!actor.isActiveAndEnabled) return false;
	        if (actor.isMissile && !actor.isMissileFired) return false;
	        // if (!actor.isSpawned) return;
	        //Logger.Info("[HSGE] " + $"Actor {actor}, pos: {actor.transform.position}");
	
	        var cosHalfSweepFov = Mathf.Cos(verticalFov * ((float)Math.PI / 180f) / 2f);
	
	        var targetPosition = actor.transform.position;
	        Vector3 relativePos = rotationTransform.InverseTransformPoint(targetPosition);
	        relativePos.y = 0f;
	        if (Vector3.Dot(relativePos.normalized, Vector3.forward) < dotThresh) return false; // Not in scan/sweep?
	
	
	        Quaternion prevRotation = rotationTransform.localRotation;
	        float y = SignedAngle(rotationTransform.parent.forward, Vector3.ProjectOnPlane(targetPosition - rotationTransform.position, rotationTransform.parent.up), rotationTransform.right);
	        rotationTransform.localRotation = Quaternion.Euler(0f, y, 0f);
	
	        // Second scan/sweep check?
	        //Logger.Info("[HSGE] " + $"TP: {targetPosition}, rtfPos: {rotationTransform.position}, forward: {rotationTransform.forward}, dot: {Vector3.Dot((targetPosition - rotationTransform.position).normalized, rotationTransform.forward)}, chsf: {cosHalfSweepFov}");
	        if (Vector3.Dot((targetPosition - rotationTransform.position).normalized, rotationTransform.forward) < cosHalfSweepFov)
	        {
	            rotationTransform.localRotation = prevRotation;
	            return false;
	        }
	
	        var detected = CheckCanDetectActor(actor);
	
	        //Logger.Info("[HSGE] " + $"DET: {detected}");
	        //if (detected) Logger.Info("[HSGE] " + $"Detected!!");
	
	        rotationTransform.localRotation = prevRotation;
	        SendPingTo(actor, false);
	
	        return detected;
	    }
	
	    private bool CheckCanDetectActor(Actor actor, float mult = 1)
	    {
	        if (actor == null) return false;
	        var targetPosition = actor.transform.position;
	        if (lockData != null && lockData.actor == actor) targetPosition = lockData.position;
	
	        if (!InLockFOVLimits(targetPosition)) return false;
	
	        var nonClearLOS = Map.instance.SimpleLineCast(rotationTransform.position, targetPosition, out Vector3 hit);
	        if (nonClearLOS) return false;
	
	        var radarSignalStrength = GetRadarSignalStrength(actor);
	        var distanceSqr = (actor.transform.position - rotationTransform.position).sqrMagnitude;
	        float receivedPower = transmissionStrength * radarSignalStrength / distanceSqr;
	
	        if (lockData != null && lockData.actor == actor) lockData.actualReturnSignal = receivedPower;
	
	        if (receivedPower * mult < 1f / receiverSensitivity)
	        {
	            if (debugRadar) Debug.DrawLine(rotationTransform.position, targetPosition, Color.red, 0.25f);
	            return false;
	        }
	        else
	        {
	            if (debugRadar) Debug.DrawLine(rotationTransform.position, targetPosition, Color.green, 0.25f);
	            return true;
	        }
	    }
	
	    private float GetRadarSignalStrength(Actor target)
	    {
	        var rcsScript = target.GetComponent<RadarCrossSection>();
	        if (rcsScript == null)
	        {
	            // Logger.Error("[HSGE] " + $"Actor {target} has no RCS");
	            return 0;
	        }
	
	        var targetPosition = target.transform.position;
	        var viewDir = targetPosition - rotationTransform.position;
	        var rcs = rcsScript.GetCrossSection(viewDir);
	
	        viewDir = viewDir.normalized;
	
	        // Vector3 viewDir = (targetPosition - rotationTransform.position).normalized;
	        float clutterMults = 1f;
	        float value = 0f;
	        bool inClutter = InGroundClutter(rotationTransform.position, targetPosition, out value);
	
	        if (inClutter)
	        {
	            clutterMults = Mathf.Abs(Vector3.Dot(Vector3.ClampMagnitude(target.velocity / 100f, 1f), viewDir));
	            clutterMults = Mathf.Clamp(clutterMults, 0.0125f, 1f);
	        }
	
	        float lookUpFactor = Vector3.Dot(viewDir, Vector3.up);
	        clutterMults = Mathf.Lerp(clutterMults, 1.5f, lookUpFactor);
	        rcs *= clutterMults;
	        if (inClutter)
	        {
	            float num2 = Mathf.InverseLerp(25000f, 500f, value);
	            num2 = Mathf.Lerp(0.2f, 1f, num2);
	            rcs *= num2;
	        }
	
	        return rcs;
	    }
	
	    private bool InGroundClutter(Vector3 radarPosition, Vector3 targetPosition, out float groundDist)
	    {
	        float maxClutterDist = 25000f;
	        Vector3 castDir = (targetPosition - radarPosition).normalized;
	
	        if (Map.instance.SimpleLineCast(targetPosition, castDir * maxClutterDist, out Vector3 hit))
	        {
	            groundDist = (hit - targetPosition).magnitude;
	            // return true;
	        }
	
	        var d = -targetPosition.y / castDir.y;
	        if (d > 0)
	        {
	            var waterHitPos = targetPosition + castDir * (-targetPosition.y / castDir.y);
	            groundDist = (waterHitPos - targetPosition).magnitude;
	            return groundDist < maxClutterDist;
	        }
	
	        groundDist = -1f;
	        return false;
	    }
	
	    private void SendPingTo(Actor actor, bool isLocked)
	    {
	        if (radarActorId == -1)
	        {
	            if (!hasLoggedRwrNotifError)
	            {
	                Logger.Info("[HSGE] " + $"Radar would like to send RWR notification, however not no assigned actorId");
	                hasLoggedRwrNotifError = true;
	            }
	            return;
	        }
	
	
	        if (actor.isLocal)
	        {
	            var rwr = actor.GetComponent<RWR>();
	            if (rwr == null)
	            {
	                if (!actor.isMissile) Logger.Warn("[HSGE] " + $"Cannot ping local actor {actor} as they do not have an RWR");
	                return;
	            }
	
	            var recStrength = transmissionStrength / (transform.position - actor.transform.position).sqrMagnitude;
	            rwr.HandlePing(actor, radarActorId, recStrength, transmissionStrength, 4.11f, NetVector.From(transform.position), NetVector.From(this.actor.velocity), isLocked);
	            return;
	        }
	
	        var entity = actor.GetComponent<HCPlayerEntity>();
	        if (entity == null)
	        {
	            Logger.Warn("[HSGE] " + $"Skipping ping for {actor} as they are not a HCPlayer");
	            return;
	        }
	
	
	
	        var rwrPingPacket = new RWRPing
	        {
	            rwrTargetOwnerId = entity.ownerId.ToString(),
	            actorId = radarActorId,
	            signalStrength = transmissionStrength,
	            frequency = 4.11f,
	            position = NetVector.From(transform.position),
	            velocity = NetVector.From(this.actor.velocity),
	            isLock = isLocked
	        };
	
	        HCConnector.instance.SendCommandPacket(rwrPingPacket);
	    }
	
	    private float SignedAngle(Vector3 fromDirection, Vector3 toDirection, Vector3 referenceRight)
	    {
	        float num = Vector3.Angle(fromDirection, toDirection);
	        return Mathf.Sign(Vector3.Dot(toDirection, referenceRight)) * num;
	    }
	
	    private void RunRotation()
	    {
	        rotationTransform.localRotation = Quaternion.Euler(0, angle, 0);
	        //Logger.Info("[HSGE] " + $"Scan angle: {angle}");
	
	        var rotSpeed = rotationSpeed / (twsedTargets.Count + 1);
	        if (rotationRange < 360f)
	        {
	            angle += rotSpeed * rotationDirection * Time.fixedDeltaTime;
	            if (Mathf.Abs(angle) > rotationRange / 2f)
	            {
	                rotationDirection = 0f - rotationDirection;
	                angle = Mathf.Sign(angle) * (rotationRange / 2f);
	            }
	        }
	        else
	        {
	            angle = Mathf.Repeat(angle + rotSpeed * Time.fixedDeltaTime, 360f);
	        }
	    }
	
	    public RadarState GetState()
	    {
	        RadarState state = new RadarState
	        {
	            angle = angle,
	            azimuthAdjust = currentAzimuthAdjust,
	            elevationAdjust = currentElevationAdjust,
	            fov = radarFov,
	
	            sttedTarget = hasSttTarget ? StateTargetData.FromTargetData(lockData) : null,
	            twsedTargets = twsedTargets.Select(actor => StateTargetData.FromTargetData(GetTargetData(actor))).ToArray(),
	            detectedTargets = detectedTargets.Select(td => new MinimalDetectedTargetData(td)).ToArray()
	        };
	
	        return state;
	    }
	
	}
	
}