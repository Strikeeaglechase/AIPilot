using UnityGERunner;
using Coroutine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityGERunner.UnityApplication
{
	
	
	public class WeaponStats : ScriptableObject
	{
	    [Serializable]
	    public class WeaponInfo
	    {
	        public GameObject prefab;
	        public string path;
	        public float rcs;
	        public float mass;
	        public float drag;
	    }
	
	    public List<WeaponInfo> weaponPrefabs = new List<WeaponInfo>();
	
	}
	
}