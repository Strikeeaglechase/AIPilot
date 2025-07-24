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
	
	    private void DetectTarget(Actor actor)
	    {
	        //var localPos = transform.InverseTransformPoint(actor.transform.position);
	        //var localDir = (transform.position - localPos).normalized;
	
	        var relPos = (actor.transform.position - transform.position).normalized;
	
	        //Debug.DrawLine(transform.position, transform.position + globalLineDir * 50_000, Color.yellow);
	        //var theta = Mathf.Atan2(localDir.z, localDir.x);
	        //var phi = Mathf.Asin(localDir.y);
	
	        var target = new VisuallySpottedTarget
	        {
	            id = actor.entityId,
	            type = actor.isMissile ? VisualTargetType.Missile : VisualTargetType.Aircraft,
	            direction = NetVector.From(relPos),
	            team = actor.team,
	        };
	        spottedTargets.Add(target);
	    }
	}
	
}