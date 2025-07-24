using UnityGERunner;
using Coroutine;
using Recorder;
using System.Collections;
using System.Collections.Generic;

namespace UnityGERunner.UnityApplication
{
	
	public class OmniWing : MonoBehaviour
	{
	    public Rigidbody rb;
	    private Transform wingTransform;
	    public Aerodynamics aeroProfile;
	
	    public float liftCoefficient;
	    public float dragCoefficient;
	    public float liftArea;
	
	    private float liftConstant;
	    private float dragConstant;
	
	    private Vector3 rbOffset;
	
	
	    protected override void Start()
	    {
	        wingTransform = transform;
	        if (aeroProfile == null) aeroProfile = AerodynamicsController.fetch.wingAero;
	        UpdateConstants();
	    }
	
	    protected override void FixedUpdate()
	    {
	        if (!rb || rb.isKinematic) return;
	        Vector3 position = rb.position + rb.transform.TransformVector(rbOffset);
	        Vector3 airflow = rb.GetPointVelocity(position);
	        float airspeedSq = airflow.sqrMagnitude;
	        if (airspeedSq < 0f) return;
	
	        float atmoDensity = AerodynamicsController.fetch.AtmosDensityAtPosition(position);
	        Vector3 projectedAirflow = Vector3.Project(airflow, wingTransform.forward);
	        float airspeed = Mathf.Sqrt(airspeedSq);
	        float aoa = Vector3.Angle(projectedAirflow, airflow);
	        float aeroForce = atmoDensity * airspeedSq;
	        float liftForce = liftConstant * aeroForce * aeroProfile.liftCurve.Evaluate(aoa);
	        float dragForce = dragConstant * aeroForce * aeroProfile.dragCurve.Evaluate(aoa) * AerodynamicsController.fetch.DragMultiplierAtSpeed(airspeed, position);
	        //Logger.Info("[HSGE] " + $"Drag: {dragForce}");
	        Vector3 vector3 = -Vector3.ProjectOnPlane(airflow, projectedAirflow).normalized;
	        Vector3 force = (0f - dragForce) * airflow / airspeed + vector3 * liftForce;
	        if (float.IsNaN(force.x))
	        {
	
	            Logger.Warn("[HSGE] " + $"OmniWing force is NaN");
	            return;
	        }
	        rb.AddForceAtPosition(force, position);
	    }
	
	    public float CalculateDragMagAtSpeed(float speed)
	    {
	        return 0.5f * dragCoefficient * liftArea * 0.021f * speed * speed * aeroProfile.dragCurve.Evaluate(0f);
	    }
	
	    
	    private void UpdateConstants()
	    {
	        liftConstant = 0.5f * liftCoefficient * liftArea;
	        dragConstant = 0.5f * dragCoefficient * liftArea;
	    }
	
	    public void SetParentRigidbody(Rigidbody rb)
	    {
	        this.rb = rb;
	        rbOffset = rb.transform.InverseTransformPoint(base.transform.position);
	        //Logger.Info("[HSGE] " + $"RBOffset: {rbOffset}, tfp: {transform.position}, rbp: {rb.transform.position}");
	    }
	}
	
}