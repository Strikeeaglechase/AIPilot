
using UnityGERunner;
using Coroutine;
using UnityGERunner.UnityApplication;
using UnityGERunner.UnityApplication.ScriptableObjects;


namespace UnityGERunner.UnityApplication.Prefabs
{
	public class MarkerPrefab : Prefab
	{
		public static GameObject Create()
		{
			// Component initializations
			var gameObject2244553412172195907 = new GameObject("ChevronLeft");
			var transform2244553412172195908 = new Transform();
			
			var behaviour2244553412172195909 = new MonoBehaviour();
			var gameObject2244553412464856331 = new GameObject("ChevronRight");
			var transform2244553412464856332 = new Transform();
			
			var behaviour2244553412464856333 = new MonoBehaviour();
			var gameObject2244553412557758999 = new GameObject("Name");
			var transform2244553412557759000 = new Transform();
			
			var behaviour2244553412557759001 = new MonoBehaviour();
			var gameObject2244553412930542032 = new GameObject("Marker");
			var transform2244553412930542033 = new Transform();
			
			var behaviour2244553412930542035 = new MonoBehaviour();
			var behaviour2244553412930542034 = new MonoBehaviour();
			
			// Component setups// Setup for GameObject 2244553412172195907
			gameObject2244553412172195907.SetActive(true);
			gameObject2244553412172195907.sourcePrefab = new MarkerPrefab();
			gameObject2244553412172195907.AddComponents(transform2244553412172195908, behaviour2244553412172195909);
			
			
			// Setup for Transform 2244553412172195908
			// Children: 
			transform2244553412172195908.parent = transform2244553412930542033;
			transform2244553412172195908.localPosition = new Vector3(0f, 0f, 0f);
			transform2244553412172195908.localRotation = new Quaternion(0f, 0f, 0.70710677f, 0.7071068f);
			transform2244553412172195908.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for MonoBehaviour 2244553412172195909
			// Script with GUID fe87c0e1cc204ed48ad3b37840f39efc not found for component 2244553412172195909
			
			// Setup for GameObject 2244553412464856331
			gameObject2244553412464856331.SetActive(true);
			gameObject2244553412464856331.sourcePrefab = new MarkerPrefab();
			gameObject2244553412464856331.AddComponents(transform2244553412464856332, behaviour2244553412464856333);
			
			
			// Setup for Transform 2244553412464856332
			// Children: 
			transform2244553412464856332.parent = transform2244553412930542033;
			transform2244553412464856332.localPosition = new Vector3(0f, 0f, 0f);
			transform2244553412464856332.localRotation = new Quaternion(0f, 0f, 0.7071066f, -0.707107f);
			transform2244553412464856332.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for MonoBehaviour 2244553412464856333
			// Script with GUID fe87c0e1cc204ed48ad3b37840f39efc not found for component 2244553412464856333
			
			// Setup for GameObject 2244553412557758999
			gameObject2244553412557758999.SetActive(true);
			gameObject2244553412557758999.sourcePrefab = new MarkerPrefab();
			gameObject2244553412557758999.AddComponents(transform2244553412557759000, behaviour2244553412557759001);
			
			
			// Setup for Transform 2244553412557759000
			// Children: 
			transform2244553412557759000.parent = transform2244553412930542033;
			transform2244553412557759000.localPosition = new Vector3(0f, 0f, 0f);
			transform2244553412557759000.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform2244553412557759000.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for MonoBehaviour 2244553412557759001
			// Script with GUID f4688fdb7df04437aeb418b961361dc5 not found for component 2244553412557759001
			
			// Setup for GameObject 2244553412930542032
			gameObject2244553412930542032.SetActive(true);
			gameObject2244553412930542032.sourcePrefab = new MarkerPrefab();
			gameObject2244553412930542032.AddComponents(transform2244553412930542033, behaviour2244553412930542035, behaviour2244553412930542034);
			
			
			// Setup for Transform 2244553412930542033
			// Children: 2244553412172195908, 2244553412464856332, 2244553412557759000
			transform2244553412930542033.localPosition = new Vector3(0f, 0f, 0f);
			transform2244553412930542033.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform2244553412930542033.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for MonoBehaviour 2244553412930542035
			// Script with GUID fe87c0e1cc204ed48ad3b37840f39efc not found for component 2244553412930542035
			
			// Setup for MonoBehaviour 2244553412930542034
			// Script with GUID 2ce1142bdb347cf46948121b0cb23770 not found for component 2244553412930542034
			
			gameObject2244553412930542032.gameObjectFileIdMap[2244553412172195907] = gameObject2244553412172195907;
			gameObject2244553412930542032.componentFileIdMap[2244553412172195908] = transform2244553412172195908;
			gameObject2244553412930542032.componentFileIdMap[2244553412172195909] = behaviour2244553412172195909;
			gameObject2244553412930542032.gameObjectFileIdMap[2244553412464856331] = gameObject2244553412464856331;
			gameObject2244553412930542032.componentFileIdMap[2244553412464856332] = transform2244553412464856332;
			gameObject2244553412930542032.componentFileIdMap[2244553412464856333] = behaviour2244553412464856333;
			gameObject2244553412930542032.gameObjectFileIdMap[2244553412557758999] = gameObject2244553412557758999;
			gameObject2244553412930542032.componentFileIdMap[2244553412557759000] = transform2244553412557759000;
			gameObject2244553412930542032.componentFileIdMap[2244553412557759001] = behaviour2244553412557759001;
			gameObject2244553412930542032.gameObjectFileIdMap[2244553412930542032] = gameObject2244553412930542032;
			gameObject2244553412930542032.componentFileIdMap[2244553412930542033] = transform2244553412930542033;
			gameObject2244553412930542032.componentFileIdMap[2244553412930542035] = behaviour2244553412930542035;
			gameObject2244553412930542032.componentFileIdMap[2244553412930542034] = behaviour2244553412930542034;
			
			return gameObject2244553412930542032;
		}

		public override GameObject CreateInstance() 
		{
			return Create();
		}
	}
}