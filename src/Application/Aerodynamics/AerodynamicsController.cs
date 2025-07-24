using UnityGERunner;
using Coroutine;
using System.Collections;
using System.Collections.Generic;

namespace UnityGERunner.UnityApplication
{
	
	public class AerodynamicsController : MonoBehaviour
	{
	    public Aerodynamics wingAero;
	
	    
	    public AnimationCurve atmosDensityCurve;
	
	    public SOCurve defaultDragMachCurve;
	
	    public SOCurve wingDragMachCurveZero;
	
	    public float sweepDragModifierX;
	
	    public float sweepDragModifierY;
	
	    public static AnimationCurve machAtAltCurve = new AnimationCurve(new Keyframe(0f, 340.3f, -0.003958875f, -0.003958875f, 0f, 0.333333f), new Keyframe(4572f, 322.2f, -0.003901572f, -0.003901572f, 0.333333f, 0.0559395f), new Keyframe(11000f, 295.1f, -0.004215931f, -0.004215931f, 0.333333f, 0f));
	
	    public static AerodynamicsController fetch { get; private set; }
	
	    public float AtmosPressureAtPosition(Vector3 worldPos)
	    {
	        return atmosDensityCurve.Evaluate(worldPos.y);
	    }
	
	    public float AtmosDensityAtPosition(Vector3 worldPos)
	    {
	        return 0.021f * AtmosPressureAtPosition(worldPos);
	    }
	
	    public float AtmosDensityAtAltitude(float altitude)
	    {
	        return 0.021f * atmosDensityCurve.Evaluate(altitude);
	    }
	
	    public float AtmosDensityAtPositionMetric(Vector3 worldPos)
	    {
	        return 1.225f * AtmosPressureAtPosition(worldPos);
	    }
	
	    public float DragMultiplierAtSpeed(float speed, float altitude)
	    {
	        float t = SpeedToMach(speed, altitude);
	        return defaultDragMachCurve.Evaluate(t);
	    }
	
	    public float DragMultiplierAtSpeed(float speed, Vector3 position)
	    {
	        return DragMultiplierAtSpeed(speed, position.y);
	    }
	
	    public float IndicatedAirspeed(float trueAirspeed, Vector3 position)
	    {
	        float altitude = position.y;
	        float num = AtmosPressureAtPosition(position);
	        float num2 = num * 1.225f;
	        num *= 101325f;
	        float num3 = num2 * trueAirspeed * trueAirspeed / 2f + num;
	        float num4 = Mathf.Sqrt(2f * (num3 - num) / 1.225f);
	        float num5 = 1f + 0.06792f * (altitude / 3048f);
	        return num4 * num5;
	    }
	
	    public float DragMultiplierAtSpeedAndSweep(float speed, float altitude, float sweep)
	    {
	        float num = Mathf.Lerp(1f, sweepDragModifierX, sweep / 90f);
	        float num2 = Mathf.Lerp(1f, sweepDragModifierY, sweep / 90f);
	        float num3 = SpeedToMach(speed, altitude);
	        return 1f + num2 * (wingDragMachCurveZero.Evaluate(num3 * num) - 1f);
	    }
	
	    protected override void Awake()
	    {
	        if ((bool)fetch)
	        {
	            GameObject.Destroy(fetch);
	        }
	        fetch = this;
	    }
	
	    public static float SpeedToMach(float ms, float altitude)
	    {
	        float num = machAtAltCurve.Evaluate(altitude);
	        return ms / num;
	    }
	}
}