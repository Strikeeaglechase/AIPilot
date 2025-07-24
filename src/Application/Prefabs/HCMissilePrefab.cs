
using UnityGERunner;
using Coroutine;
using UnityGERunner.UnityApplication;
using UnityGERunner.UnityApplication.ScriptableObjects;


namespace UnityGERunner.UnityApplication.Prefabs
{
	public class HCMissilePrefab : Prefab
	{
		public static GameObject Create()
		{
			// Component initializations
			var gameObject1081601098844916894 = new GameObject("HCMissile");
			var transform4148362922056435952 = new Transform();
			
			var behaviour6591563158095982089 = new HCMissile();
			
			// Component setups// Setup for GameObject 1081601098844916894
			gameObject1081601098844916894.SetActive(true);
			gameObject1081601098844916894.sourcePrefab = new HCMissilePrefab();
			gameObject1081601098844916894.AddComponents(transform4148362922056435952, behaviour6591563158095982089);
			
			
			// Setup for Transform 4148362922056435952
			// Children: 
			transform4148362922056435952.localPosition = new Vector3(0f, 0f, 0f);
			transform4148362922056435952.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform4148362922056435952.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for MonoBehaviour 6591563158095982089
			behaviour6591563158095982089.entityId = 0;
			behaviour6591563158095982089.path = "";
			behaviour6591563158095982089.ownerId = (ulong)0;
			behaviour6591563158095982089.manager = null;
			behaviour6591563158095982089.velocity = new Vector3(0f, 0f, 0f);
			behaviour6591563158095982089.acceleration = new Vector3(0f, 0f, 0f);
			behaviour6591563158095982089.fired = false;
			behaviour6591563158095982089.trailColor = new Color(1f, 1f, 1f, 1f);
			// Property "info" not found in MonoBehaviour 6591563158095982089
			behaviour6591563158095982089.enabled = true;
			
			
			gameObject1081601098844916894.gameObjectFileIdMap[1081601098844916894] = gameObject1081601098844916894;
			gameObject1081601098844916894.componentFileIdMap[4148362922056435952] = transform4148362922056435952;
			gameObject1081601098844916894.componentFileIdMap[6591563158095982089] = behaviour6591563158095982089;
			
			return gameObject1081601098844916894;
		}

		public override GameObject CreateInstance() 
		{
			return Create();
		}
	}
}