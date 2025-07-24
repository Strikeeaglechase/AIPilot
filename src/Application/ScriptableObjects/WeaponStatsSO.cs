
using UnityGERunner;
using Coroutine;
using UnityGERunner.UnityApplication;
using UnityGERunner.UnityApplication.Prefabs;


namespace UnityGERunner.UnityApplication.ScriptableObjects
{
	public class WeaponStatsSO : WeaponStats
	{
		private static WeaponStats _instance;
		public static WeaponStats instance 
		{
			get {
				if (_instance == null) _instance = Create();
				return _instance;
			}
		}

		private static WeaponStats Create()
		{
			var behaviour11400000 = new WeaponStats();
			behaviour11400000.weaponPrefabs = new(10);
			var behaviour11400000weaponPrefabs0 = behaviour11400000.weaponPrefabs.ElementAtOrDefaultSafe(0);
			behaviour11400000weaponPrefabs0.prefab = null;
			behaviour11400000weaponPrefabs0.path = "Weapons/Missiles/AIM-54";
			behaviour11400000weaponPrefabs0.rcs = 6.426766f;
			behaviour11400000weaponPrefabs0.mass = 0.45f;
			behaviour11400000weaponPrefabs0.drag = 0.0036036253f;
			behaviour11400000.weaponPrefabs.Add(behaviour11400000weaponPrefabs0);
			var behaviour11400000weaponPrefabs1 = behaviour11400000.weaponPrefabs.ElementAtOrDefaultSafe(1);
			behaviour11400000weaponPrefabs1.prefab = AIM_120Prefab.Create();
			behaviour11400000weaponPrefabs1.path = "Weapons/Missiles/AIM-120";
			behaviour11400000weaponPrefabs1.rcs = 2.9871655f;
			behaviour11400000weaponPrefabs1.mass = 0.152f;
			behaviour11400000weaponPrefabs1.drag = 0.001711764f;
			behaviour11400000.weaponPrefabs.Add(behaviour11400000weaponPrefabs1);
			var behaviour11400000weaponPrefabs2 = behaviour11400000.weaponPrefabs.ElementAtOrDefaultSafe(2);
			behaviour11400000weaponPrefabs2.prefab = null;
			behaviour11400000weaponPrefabs2.path = "Weapons/Missiles/AIM-120D";
			behaviour11400000weaponPrefabs2.rcs = 1.8737452f;
			behaviour11400000weaponPrefabs2.mass = 0.152f;
			behaviour11400000weaponPrefabs2.drag = 0.0012517873f;
			behaviour11400000.weaponPrefabs.Add(behaviour11400000weaponPrefabs2);
			var behaviour11400000weaponPrefabs3 = behaviour11400000.weaponPrefabs.ElementAtOrDefaultSafe(3);
			behaviour11400000weaponPrefabs3.prefab = null;
			behaviour11400000weaponPrefabs3.path = "Weapons/Missiles/AIM-7";
			behaviour11400000weaponPrefabs3.rcs = 3.4659991f;
			behaviour11400000weaponPrefabs3.mass = 0.152f;
			behaviour11400000weaponPrefabs3.drag = 0.0024220145f;
			behaviour11400000.weaponPrefabs.Add(behaviour11400000weaponPrefabs3);
			var behaviour11400000weaponPrefabs4 = behaviour11400000.weaponPrefabs.ElementAtOrDefaultSafe(4);
			behaviour11400000weaponPrefabs4.prefab = null;
			behaviour11400000weaponPrefabs4.path = "Weapons/Missiles/AIM-9+";
			behaviour11400000weaponPrefabs4.rcs = 2.4963102f;
			behaviour11400000weaponPrefabs4.mass = 0.0853f;
			behaviour11400000weaponPrefabs4.drag = 0.0019996932f;
			behaviour11400000.weaponPrefabs.Add(behaviour11400000weaponPrefabs4);
			var behaviour11400000weaponPrefabs5 = behaviour11400000.weaponPrefabs.ElementAtOrDefaultSafe(5);
			behaviour11400000weaponPrefabs5.prefab = null;
			behaviour11400000weaponPrefabs5.path = "Weapons/Missiles/AIM-9";
			behaviour11400000weaponPrefabs5.rcs = 2.4963102f;
			behaviour11400000weaponPrefabs5.mass = 0.0853f;
			behaviour11400000weaponPrefabs5.drag = 0.0019996932f;
			behaviour11400000.weaponPrefabs.Add(behaviour11400000weaponPrefabs5);
			var behaviour11400000weaponPrefabs6 = behaviour11400000.weaponPrefabs.ElementAtOrDefaultSafe(6);
			behaviour11400000weaponPrefabs6.prefab = null;
			behaviour11400000weaponPrefabs6.path = "Weapons/Missiles/AIM-9E";
			behaviour11400000weaponPrefabs6.rcs = 2.4963102f;
			behaviour11400000weaponPrefabs6.mass = 0.0853f;
			behaviour11400000weaponPrefabs6.drag = 0.0019996932f;
			behaviour11400000.weaponPrefabs.Add(behaviour11400000weaponPrefabs6);
			var behaviour11400000weaponPrefabs7 = behaviour11400000.weaponPrefabs.ElementAtOrDefaultSafe(7);
			behaviour11400000weaponPrefabs7.prefab = AIRS_TPrefab.Create();
			behaviour11400000weaponPrefabs7.path = "Weapons/Missiles/AIRS-T";
			behaviour11400000weaponPrefabs7.rcs = 3.2354915f;
			behaviour11400000weaponPrefabs7.mass = 0.088f;
			behaviour11400000weaponPrefabs7.drag = 0.0024967163f;
			behaviour11400000.weaponPrefabs.Add(behaviour11400000weaponPrefabs7);
			var behaviour11400000weaponPrefabs8 = behaviour11400000.weaponPrefabs.ElementAtOrDefaultSafe(8);
			behaviour11400000weaponPrefabs8.prefab = null;
			behaviour11400000weaponPrefabs8.path = "Weapons/Missiles/HARM";
			behaviour11400000weaponPrefabs8.rcs = 4.0033174f;
			behaviour11400000weaponPrefabs8.mass = 0.355f;
			behaviour11400000weaponPrefabs8.drag = 0.006251625f;
			behaviour11400000.weaponPrefabs.Add(behaviour11400000weaponPrefabs8);
			var behaviour11400000weaponPrefabs9 = behaviour11400000.weaponPrefabs.ElementAtOrDefaultSafe(9);
			behaviour11400000weaponPrefabs9.prefab = null;
			behaviour11400000weaponPrefabs9.path = "Weapons/Missiles/SideARM";
			behaviour11400000weaponPrefabs9.rcs = 2.4963102f;
			behaviour11400000weaponPrefabs9.mass = 0.09383f;
			behaviour11400000weaponPrefabs9.drag = 0.0019996932f;
			behaviour11400000.weaponPrefabs.Add(behaviour11400000weaponPrefabs9);
			behaviour11400000.enabled = true;
			
			
			return behaviour11400000;
		}
	}
}
	