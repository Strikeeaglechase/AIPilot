using UnityGERunner;
using Coroutine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;



namespace UnityGERunner.UnityApplication
{
	
	public static class ComponentExtensions
	{
	    public static T GetComponentImplementing<T>(this GameObject go)
	    {
	        MonoBehaviour[] components = go.GetComponents<MonoBehaviour>();
	        foreach (MonoBehaviour component in components)
	        {
	            if (typeof(T).IsAssignableFrom(component.GetType()))
	            {
	                return (T)(object)component;
	            }
	        }
	        return default(T);
	    }
	
	    public static T[] GetComponentsImplementing<T>(this GameObject go)
	    {
	        return (from x in go.GetComponents<MonoBehaviour>()
	                where typeof(T).IsAssignableFrom(x.GetType())
	                select x).Cast<T>().ToArray();
	    }
	
	    public static T[] GetComponentsInChildrenImplementing<T>(this GameObject go, bool includeInactive = false)
	    {
	        return (from x in go.GetComponentsInChildren<MonoBehaviour>(includeInactive)
	                where (bool)x && typeof(T).IsAssignableFrom(x.GetType())
	                select x).Cast<T>().ToArray();
	    }
	
	
	    public static T[] GetComponentsInSceneImplementing<T>(this GameObject go)
	    {
	#if HSGE
	        var results = new List<T>();
	        foreach (var t in MonoBehaviour.objectTypes)
	        {
	            if (t.Key.IsAssignableTo(typeof(T)))
	            {
	                foreach (var component in t.Value.OfType<T>()) results.Add(component);
	            }
	        }
	        return results.ToArray();
	#else
	        return GameObject.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<T>().ToArray();
	#endif
	    }
	}
}