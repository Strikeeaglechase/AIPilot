
using UnityGERunner;
using Coroutine;
using UnityGERunner.UnityApplication;
using UnityGERunner.UnityApplication.ScriptableObjects;


namespace UnityGERunner.UnityApplication.Prefabs
{
	public class RwrIconPrefab : Prefab
	{
		public static GameObject Create()
		{
			// Component initializations
			var gameObject5180965533954448914 = new GameObject("LockIcon");
			var transform5009513880562784155 = new Transform();
			
			var behaviour506393006303115061 = new MonoBehaviour();
			var gameObject6454488802272903307 = new GameObject("RwrIcon");
			var transform1017180039392130732 = new Transform();
			
			var behaviour1990456712281727132 = new MonoBehaviour();
			var gameObject7328965286170265994 = new GameObject("Symbol");
			var transform184517675910563284 = new Transform();
			
			var behaviour2606653700707663950 = new MonoBehaviour();
			
			// Component setups// Setup for GameObject 5180965533954448914
			gameObject5180965533954448914.SetActive(true);
			gameObject5180965533954448914.sourcePrefab = new RwrIconPrefab();
			gameObject5180965533954448914.AddComponents(transform5009513880562784155, behaviour506393006303115061);
			
			
			// Setup for Transform 5009513880562784155
			// Children: 
			transform5009513880562784155.parent = transform1017180039392130732;
			transform5009513880562784155.localPosition = new Vector3(0f, 0f, 0f);
			transform5009513880562784155.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform5009513880562784155.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for MonoBehaviour 506393006303115061
			// Script with GUID fe87c0e1cc204ed48ad3b37840f39efc not found for component 506393006303115061
			
			// Setup for GameObject 6454488802272903307
			gameObject6454488802272903307.SetActive(true);
			gameObject6454488802272903307.sourcePrefab = new RwrIconPrefab();
			gameObject6454488802272903307.AddComponents(transform1017180039392130732, behaviour1990456712281727132);
			
			
			// Setup for Transform 1017180039392130732
			// Children: 184517675910563284, 5009513880562784155
			transform1017180039392130732.localPosition = new Vector3(0f, 0f, 0f);
			transform1017180039392130732.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform1017180039392130732.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for MonoBehaviour 1990456712281727132
			// Script with GUID 622ce425e3aee50499a53a439ba3f6a0 not found for component 1990456712281727132
			
			// Setup for GameObject 7328965286170265994
			gameObject7328965286170265994.SetActive(true);
			gameObject7328965286170265994.sourcePrefab = new RwrIconPrefab();
			gameObject7328965286170265994.AddComponents(transform184517675910563284, behaviour2606653700707663950);
			
			
			// Setup for Transform 184517675910563284
			// Children: 
			transform184517675910563284.parent = transform1017180039392130732;
			transform184517675910563284.localPosition = new Vector3(0f, 0f, 0f);
			transform184517675910563284.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform184517675910563284.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for MonoBehaviour 2606653700707663950
			// Script with GUID f4688fdb7df04437aeb418b961361dc5 not found for component 2606653700707663950
			
			gameObject6454488802272903307.gameObjectFileIdMap[5180965533954448914] = gameObject5180965533954448914;
			gameObject6454488802272903307.componentFileIdMap[5009513880562784155] = transform5009513880562784155;
			gameObject6454488802272903307.componentFileIdMap[506393006303115061] = behaviour506393006303115061;
			gameObject6454488802272903307.gameObjectFileIdMap[6454488802272903307] = gameObject6454488802272903307;
			gameObject6454488802272903307.componentFileIdMap[1017180039392130732] = transform1017180039392130732;
			gameObject6454488802272903307.componentFileIdMap[1990456712281727132] = behaviour1990456712281727132;
			gameObject6454488802272903307.gameObjectFileIdMap[7328965286170265994] = gameObject7328965286170265994;
			gameObject6454488802272903307.componentFileIdMap[184517675910563284] = transform184517675910563284;
			gameObject6454488802272903307.componentFileIdMap[2606653700707663950] = behaviour2606653700707663950;
			
			return gameObject6454488802272903307;
		}

		public override GameObject CreateInstance() 
		{
			return Create();
		}
	}
}