using UnityGERunner;
using Coroutine;
using System.Collections;
using System.Collections.Generic;

namespace UnityGERunner.UnityApplication
{
	
	public interface IMassObject
	{
	    float GetMass();
	}
	
	public class MassUpdater : MonoBehaviour
	{
	    public float baseMass;
	
	    private Rigidbody rb;
	
	    private List<MonoBehaviour> massObjects = new List<MonoBehaviour>();
	
	    public float updateInterval = -1f;
	
	    protected override void Awake()
	    {
	        rb = GetComponent<Rigidbody>();
	    }
	
	    protected override void Start()
	    {
	        UpdateMassObjects();
	    }
	
	    protected override void OnEnable()
	    {
	        if (updateInterval > 0f)
	        {
	            CoroutineHandler.Start(IntervalUpdateRoutine());
	        }
	        else
	        {
	            CoroutineHandler.Start(ConstantUpdateRoutine());
	        }
	    }
	
	    private IEnumerator<Wait> ConstantUpdateRoutine()
	    {
	        yield return new Wait(0);
	        while (base.enabled)
	        {
	            UpdateMass();
	            yield return new Wait(0);
	        }
	    }
	
	    private IEnumerator<Wait> IntervalUpdateRoutine()
	    {
	        Wait wait = new Wait(updateInterval);
	        yield return new Wait(0);
	        yield return new Wait(Random.Range(0f, updateInterval));
	        while (base.enabled)
	        {
	            UpdateMass();
	            yield return wait;
	        }
	    }
	
	    public void UpdateMassObjects()
	    {
	        massObjects.Clear();
	        IMassObject[] componentsInChildrenImplementing = gameObject.GetComponentsInChildrenImplementing<IMassObject>();
	        for (int i = 0; i < componentsInChildrenImplementing.Length; i++)
	        {
	            MonoBehaviour component = (MonoBehaviour)componentsInChildrenImplementing[i];
	            if ((bool)component)
	            {
	                massObjects.Add(component);
	            }
	        }
	        UpdateMass();
	    }
	
	    public void RemoveMassObject(IMassObject o)
	    {
	        massObjects.Remove((MonoBehaviour)o);
	    }
	
	    private void UpdateMass()
	    {
	        float num = baseMass;
	        int count = massObjects.Count;
	        bool flag = false;
	        for (int i = 0; i < count; i++)
	        {
	            if (massObjects[i] != null)
	            {
	                num += ((IMassObject)massObjects[i]).GetMass();
	            }
	            else
	            {
	                flag = true;
	            }
	        }
	        if (flag)
	        {
	            massObjects.RemoveAll((MonoBehaviour x) => x == null);
	        }
	        if (!rb)
	        {
	            rb = GetComponent<Rigidbody>();
	        }
	        rb.mass = num;
	        // Logger.Info("[HSGE] " + $"Total vehicle mass: {num}");
	    }
	
	    
	    public void DebugPrintMassObjects()
	    {
	        Logger.Info("[HSGE] " + "Mass Objects: ");
	        foreach (MonoBehaviour massObject in massObjects)
	        {
	            if (massObject != null)
	            {
	                Logger.Info("[HSGE] " + massObject.gameObject.name + " (" + ((IMassObject)massObject).GetMass() + "t)");
	            }
	            else
	            {
	                Logger.Info("[HSGE] " + "null");
	            }
	        }
	        UpdateMass();
	        Logger.Info("[HSGE] " + $" = TOTAL: {rb.mass}t");
	    }
	}
	
}