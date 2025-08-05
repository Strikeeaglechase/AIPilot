using UnityGERunner;
using Coroutine;
using System.Collections;
using System.Collections.Generic;

namespace UnityGERunner.UnityApplication
{
	
	public class SimpleDrag : MonoBehaviour
	{
	    public float area;
	    public Rigidbody rb;
	    public KinematicPlane kp;
	
	    private float CalculateDragForceMagnitude(float spd)
	    {
	        return 0.5f * AerodynamicsController.fetch.AtmosDensityAtPosition(transform.position) * area * spd * AerodynamicsController.fetch.DragMultiplierAtSpeed(spd, transform.position);
	    }
	
	
	    protected override void FixedUpdate()
	    {
	        if (!rb) return;
	        if (area <= 0f) return;
	
	        Vector3 com = rb.worldCenterOfMass;
	        Vector3 airflow = rb.GetPointVelocity(com);
	        float dragForce = CalculateDragForceMagnitude(airflow.magnitude);
	        Vector3 force = -airflow * dragForce;
	
	        if (float.IsNaN(force.x)) return;
	
	        if (!rb.isKinematic) rb.AddForceAtPosition(force, com);
	        if (kp != null) kp.AddForce(force);
	    }
	}
	
}