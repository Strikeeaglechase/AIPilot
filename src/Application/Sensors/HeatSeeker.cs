using UnityGERunner;
using Coroutine;
using System;
using System.Collections;
using System.Collections.Generic;


namespace UnityGERunner.UnityApplication
{
	
	public class HeatSeeker : MonoBehaviour
	{
	    public float gimbalFOV = 140f;
	    public float seekerFOV = 4f;
	    public float seekerScanTrackRate = 10f;
	    public float seekerTrackRate = 30f;
	    public float sensitivity = 4000f;
	    public float minThreshold;
	    public float ccm = 1f;
	
	    private Vector3 uncagedScanDir;
	    public bool triggerUncaged = false;
	    public Vector3 commandLookDir = Vector3.zero;
	
	    public float uncagedScanSpeed = 360f;
	    public float uncagedScanFraction = 0.15f;
	
	    private Vector3 lastTargetPosition;
	    public Vector3 targetPosition;
	    public Vector3 targetVelocity;
	
	    public float seekerLock;
	    private Vector3 localSeekerDirection;
	    private Vector3 lastParentForward;
	
	    public float seenHeat;
	
	    protected override void Start()
	    {
	    }
	
	    private void RotateSeekerHead(bool trackingTarget)
	    {
	        if (!trackingTarget) seekerLock = 0;
	
	        if (triggerUncaged)
	        {
	            if (!trackingTarget)
	            {
	                localSeekerDirection = uncagedScanDir;
	                uncagedScanDir = Quaternion.AngleAxis(uncagedScanSpeed * Time.fixedDeltaTime, Vector3.forward) * uncagedScanDir;
	            }
	        }
	        else
	        {
	            localSeekerDirection = transform.parent.InverseTransformDirection(commandLookDir);
	        }
	
	        float trackRate = !triggerUncaged ? seekerTrackRate : seekerScanTrackRate;
	
	        localSeekerDirection = Vector3.RotateTowards(Vector3.forward, localSeekerDirection, (float)Math.PI / 180f * gimbalFOV / 2f, 0f);
	        if (localSeekerDirection == Vector3.zero) localSeekerDirection = Vector3.forward;
	
	        Vector3 vector = transform.parent.InverseTransformDirection(transform.up);
	        if (localSeekerDirection == vector) vector = Vector3.forward;
	
	        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(localSeekerDirection, vector), trackRate * Time.fixedDeltaTime);
	    }
	
	    private void TrackHeat()
	    {
	        Vector3 forward = transform.forward;
	        if (HeatEmitter.emitters.Count == 0)
	        {
	            RotateSeekerHead(trackingTarget: false);
	            return;
	        }
	        Vector3 sumHeatPos = Vector3.zero;
	        float totalRecievedHeat = 0f;
	        Vector3 sumHeatVel = Vector3.zero;
	        float halfFov = seekerFOV / 2f;
	        float sqrSensitivity = sensitivity * sensitivity;
	        for (int i = 0; i < HeatEmitter.emitters.Count; i++)
	        {
	            HeatEmitter heatEmitter = HeatEmitter.emitters[i];
	            if (!heatEmitter) continue;
	            Vector3 heatPos = heatEmitter.transform.position;
	            Vector3 heatDir = heatPos - transform.position;
	
	            float distanceSq = heatDir.sqrMagnitude;
	            float viewAngle = Vector3.Angle(heatDir, forward);
	
	            if (heatEmitter.heat <= 0 || heatEmitter.isMissile) continue;
	            if (distanceSq < 10 || viewAngle > halfFov) continue;
	
	
	            float seenHeat = heatEmitter.heat * sqrSensitivity / distanceSq;
	
	            seenHeat /= Mathf.Clamp(4f * viewAngle / halfFov, 0.25f, 4f);
	            if (heatEmitter.isCountermeasure) seenHeat /= ccm;
	
	            if (seenHeat < minThreshold) continue;
	
	            // Only do vis check after confirming above heat threshold
	            var obscured = Map.instance.SimpleLineCast(transform.position, heatPos, out Vector3 hit);
	            if (obscured) continue;
	
	            sumHeatPos += seenHeat * heatPos;
	            sumHeatVel += seenHeat * heatEmitter.velocity;
	            totalRecievedHeat += seenHeat;
	        }
	
	        seenHeat = totalRecievedHeat;
	
	        if (totalRecievedHeat == 0)
	        {
	            lastTargetPosition = transform.position + transform.parent.forward * 1000f;
	            RotateSeekerHead(false);
	            return;
	        }
	
	        Vector3 avgHeatPos = sumHeatPos / totalRecievedHeat;
	        Vector3 avgHeatVel = sumHeatVel / totalRecievedHeat;
	        if (float.IsNaN(avgHeatPos.x) || float.IsNaN(avgHeatVel.x))
	        {
	            Logger.Info("[HSGE] " + $"NAN avgheatpos or vel");
	            lastTargetPosition = transform.position + transform.parent.forward * 1000f;
	            RotateSeekerHead(false);
	            return;
	        }
	
	        lastTargetPosition = avgHeatPos;
	        targetPosition = avgHeatPos;
	        targetVelocity = avgHeatVel;
	        Vector3 target = avgHeatPos - transform.position;
	        target = Vector3.RotateTowards(transform.parent.forward, target, gimbalFOV * (Mathf.PI / 180f) / 2f, 0f);
	        localSeekerDirection = transform.parent.InverseTransformDirection(target);
	        RotateSeekerHead(true);
	        seekerLock = Mathf.Clamp01(1f - Vector3.Angle(transform.forward, target) / seekerFOV);
	        lastParentForward = transform.parent.forward;
	    }
	
	    protected override void FixedUpdate()
	    {
	        TrackHeat();
	    }
	
	    private void DrawFovFrom(Transform from, float fov)
	    {
	        var forward = from.forward * 1000;
	        var left = Quaternion.AngleAxis(fov / 2, -from.right) * forward;
	        var right = Quaternion.AngleAxis(fov / 2, from.right) * forward;
	        var up = Quaternion.AngleAxis(fov / 2, from.up) * forward;
	        var down = Quaternion.AngleAxis(fov / 2, -from.up) * forward;
	
	        Gizmos.DrawLine(from.position, from.position + left);
	        Gizmos.DrawLine(from.position, from.position + right);
	        Gizmos.DrawLine(from.position, from.position + up);
	        Gizmos.DrawLine(from.position, from.position + down);
	    }
	
	}
	
}