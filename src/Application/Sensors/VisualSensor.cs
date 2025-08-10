using UnityGERunner;
using Coroutine;
using System.Collections.Generic;

namespace UnityGERunner.UnityApplication
{
	
	public enum VisualTargetType
	{
	    Aircraft,
	    Missile
	}
	
	public struct VisuallySpottedTarget
	{
	    public VisualTargetType type;
	    public NetVector direction;
	    public NetQuaternion orientation;
	    public float closure;
	    public int id;
	    public Team team;
	}
	
	public class VisualSensor : MonoBehaviour
	{
	    public Actor ourActor;
	
	    public float missileDetectRange = 5000;
	    public float aircraftDetectRange = 10000;
	
	    public List<VisuallySpottedTarget> spottedTargets = new List<VisuallySpottedTarget>();
	
	    protected override void FixedUpdate()
	    {
	        spottedTargets.Clear();
	        var units = FindObjectsByType<Actor>(FindObjectsSortMode.None);
	        foreach (var unit in units)
	        {
	            if (!unit.enabled) continue;
	            if (unit == ourActor) continue;
	            if (unit.isMissile && !unit.isMissileFired) continue;
	
	            var thresholdDist = unit.isMissile ? missileDetectRange : aircraftDetectRange;
	            var distSq = (transform.position - unit.transform.position).sqrMagnitude;
	            //Logger.Info("[HSGE] " + $"Dist threshold: {thresholdDist}, dist: {Mathf.Sqrt(distSq)}");
	            if (distSq < thresholdDist * thresholdDist) DetectTarget(unit);
	        }
	    }
	
	    public VisuallySpottedTarget[] GetState()
	    {
	        return spottedTargets.ToArray();
	    }
	
	    public void ReportDL(DatalinkController dl, int id)
	    {
	        foreach (var contact in spottedTargets) dl.Report(contact, id);
	    }
	
	    private void DetectTarget(Actor actor)
	    {
	        var relPos = (actor.transform.position - transform.position).normalized;
	        var relVel = (actor.velocity - ourActor.velocity);
	        float closureRate = -Vector3.Dot(relVel, relPos);
	
	        var target = new VisuallySpottedTarget
	        {
	            id = actor.entityId,
	            type = actor.isMissile ? VisualTargetType.Missile : VisualTargetType.Aircraft,
	            direction = relPos,
	            orientation = actor.transform.rotation,
	            closure = closureRate,
	            team = actor.team,
	        };
	        spottedTargets.Add(target);
	    }
	}
	
}