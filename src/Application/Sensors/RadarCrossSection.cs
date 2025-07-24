using UnityGERunner;
using Coroutine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityGERunner.UnityApplication
{
	
	public interface IWeaponRCSProvider
	{
	    public float GetWeaponRCS();
	}
	
	public class RadarCrossSection : MonoBehaviour
	{
	    [Serializable]
	    public struct RadarReturn
	    {
	        public Vector3 normal;
	
	        public float returnValue;
	    }
	
	    public const float RCS_MULTIPLIER = 100f;
	    public const float DEFAULT_RCS = 40f;
	    public Transform referenceTf;
	
	    
	    public List<RadarReturn> returns = new List<RadarReturn>();
	
	    public float size;
	
	    public float overrideMultiplier = 1f;
	
	    //public HCPlayerEntity hcPlayerEntity;
	    private IWeaponRCSProvider weaponRCSProvider;
	
	
	    protected override void Awake()
	    {
	        if (!referenceTf) referenceTf = base.transform;
	    }
	
	    protected override void Start()
	    {
	        weaponRCSProvider = gameObject.GetComponentImplementing<IWeaponRCSProvider>();
	    }
	
	    public float GetCrossSection(Vector3 inDirection)
	    {
	        if (!referenceTf) referenceTf = base.transform;
	
	
	        inDirection = -referenceTf.InverseTransformDirection(inDirection).normalized;
	        float num = 0f;
	        float num2 = 0f;
	        float num3 = returns.Count;
	        for (int i = 0; (float)i < num3; i++)
	        {
	            float num4 = Vector3.Dot(inDirection, returns[i].normal);
	            if (num4 > 0f)
	            {
	                num4 = Mathf.Pow(num4, 15f);
	                num += returns[i].returnValue * num4;
	                num2 += num4;
	            }
	        }
	        float num5 = RCS_MULTIPLIER * overrideMultiplier * size * num / num2;
	
	        if (weaponRCSProvider != null) num5 += weaponRCSProvider.GetWeaponRCS();
	
	        return num5;
	    }
	
	    public float GetAverageCrossSection()
	    {
	        float avg = 0f;
	        if (returns != null && returns.Count > 0)
	        {
	            for (int i = 0; i < returns.Count; i++) avg += returns[i].returnValue;
	
	            avg /= (float)returns.Count;
	        }
	        return 100f * overrideMultiplier * size * avg;
	    }
	
	    
	    public void DebugAvgRCS()
	    {
	        Logger.Info("[HSGE] " + GetAverageCrossSection());
	    }
	
	}
	
}