using UnityGERunner;
using Coroutine;
using Recorder;
using System.Collections.Generic;

namespace UnityGERunner.UnityApplication
{
	
	class Bullet
	{
	    public Vector3 position;
	    public Vector3 velocity;
	    public float firedAt;
	
	    public Bullet(Vector3 pos, Vector3 vel)
	    {
	        position = pos;
	        velocity = vel;
	        firedAt = Time.time;
	
	        GameRecorder.Event(new FireBulletEvent(pos, vel));
	    }
	
	    // NOTE: "Update" dosn't work because the Transpiler thinks its a monobehavior method
	    public void BUpdate(List<AIClient> possibleTargets)
	    {
	        position += velocity * Time.fixedDeltaTime;
	        velocity += Physics.gravity * Time.fixedDeltaTime;
	
	        foreach (var pt in possibleTargets)
	        {
	            var sqDist = (pt.transform.position - position).sqrMagnitude;
	            if (sqDist < pt.physicalRadius * pt.physicalRadius)
	            {
	                pt.OnDamage();
	                firedAt = 0; // By setting fired at to zero we will force the bullet to be culled instantly
	            }
	        }
	    }
	}
}