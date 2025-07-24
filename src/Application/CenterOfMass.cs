using UnityGERunner;
using Coroutine;

namespace UnityGERunner.UnityApplication
{
	
	
	public class CenterOfMass : MonoBehaviour
	{
	    protected override void OnEnable()
	    {
	        Rigidbody rb = GetComponentInParent<Rigidbody>();
	        rb.automaticCenterOfMass = false;
	        rb.centerOfMass = rb.transform.InverseTransformPoint(base.transform.position);
	    }
	}
	
}