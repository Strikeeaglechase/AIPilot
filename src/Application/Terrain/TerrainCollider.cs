using UnityGERunner;
using Coroutine;

namespace UnityGERunner.UnityApplication
{
	
	public class TerrainCollider : MonoBehaviour
	{
	    protected override void FixedUpdate()
	    {
	        var height = Map.instance.GetHeightAtSubpoint(transform.position);
	        if (transform.position.y <= height)
	        {
	            var damageRec = gameObject.GetComponentsImplementing<IDamageReceiver>();
	            foreach (var dr in damageRec) dr.OnDamage();
	        }
	    }
	}
	
}