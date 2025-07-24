
using UnityGERunner;
using Coroutine;
using UnityGERunner.UnityApplication;
using UnityGERunner.UnityApplication.ScriptableObjects;
using Recorder;

namespace UnityGERunner.UnityApplication.Prefabs
{
	public class EnvScenePrefab : Prefab
	{
		public static GameObject Create()
		{
			// Component initializations
			var gameObject9367691 = new GameObject("LeftStack");
			var transform9367692 = new Transform();
			
			var behaviour9367693 = new MonoBehaviour();
			var gameObject11743762 = new GameObject("RadarLock");
			var transform11743763 = new Transform();
			
			var behaviour11743764 = new MonoBehaviour();
			var gameObject47798556 = new GameObject("RWRDisplay");
			var transform47798557 = new Transform();
			
			var behaviour47798558 = new MonoBehaviour();
			var behaviour47798560 = new MonoBehaviour();
			var gameObject262980278 = new GameObject("SpawnCenter");
			var transform262980279 = new Transform();
			
			var gameObject265828803 = new GameObject("BodyParent");
			var transform265828804 = new Transform();
			
			var gameObject363287742 = new GameObject("EventSystem");
			var behaviour363287743 = new MonoBehaviour();
			var behaviour363287744 = new MonoBehaviour();
			var transform363287745 = new Transform();
			
			var pfInst608333639 = AIClientPrefab.Create();
			var gameObject612834648 = new GameObject("HUDMarkers");
			var transform612834649 = new Transform();
			
			var behaviour612834651 = new MonoBehaviour();
			
			
			var gameObject796391537 = new GameObject("Image");
			var transform796391538 = new Transform();
			
			var behaviour796391539 = new MonoBehaviour();
			var gameObject942087939 = new GameObject("Directional Light");
			var transform942087941 = new Transform();
			
			var gameObject1045590941 = new GameObject("Speed");
			var transform1045590942 = new Transform();
			
			var behaviour1045590943 = new MonoBehaviour();
			var gameObject1117093109 = new GameObject("LeftStack");
			var transform1117093110 = new Transform();
			
			var behaviour1117093111 = new MonoBehaviour();
			var gameObject1261272100 = new GameObject("Model");
			var transform1261272101 = new Transform();
			
			var gameObject1311220498 = new GameObject("Speed");
			var transform1311220499 = new Transform();
			
			var behaviour1311220500 = new MonoBehaviour();
			var gameObject1311617375 = new GameObject("Map");
			var behaviour1311617376 = new MonoBehaviour();
			var transform1311617377 = new Transform();
			
			var pfInst1339571764 = AIClientPrefab.Create();
			
			
			
			
			var gameObject1363819661 = new GameObject("CoM");
			var transform1363819662 = new Transform();
			
			var gameObject1447504070 = new GameObject("ASF-58");
			var rigidBody1447504071 = new Rigidbody();
			
			var behaviour1447504072 = new MassUpdater();
			var behaviour1447504073 = new ModuleEngine();
			var behaviour1447504074 = new KinematicPlane();
			var behaviour1447504075 = new Actor();
			var transform1447504076 = new Transform();
			
			var gameObject1717733297 = new GameObject("Main Camera");
			var transform1717733298 = new Transform();
			
			var gameObject1859288641 = new GameObject("Canvas");
			var transform1859288642 = new Transform();
			
			var behaviour1859288644 = new MonoBehaviour();
			var behaviour1859288646 = new MonoBehaviour();
			var gameObject1941113518 = new GameObject("World");
			var transform1941113519 = new Transform();
			
			var behaviour1941113520 = new AerodynamicsController();
			var behaviour1941113521 = new HCConnector();
			var behaviour1941113522 = new HCManager();
			var behaviour1941113523 = new GameRecorder();
			var behaviour1941113524 = new LocalFightController();
			var behaviour1941113525 = new MonoBehaviour();
			var gameObject2033369362 = new GameObject("Canvas");
			var transform2033369363 = new Transform();
			
			var behaviour2033369364 = new MonoBehaviour();
			var behaviour2033369365 = new MonoBehaviour();
			var behaviour2033369366 = new MonoBehaviour();
			var gameObject2114172169 = new GameObject("MissileHolder");
			var transform2114172170 = new Transform();
			
			var pfInst5005441902897201914 = AircraftPrefab.Create();
			
			
			var gameObject619946745 = new GameObject("env");
			var transform662565500 = new Transform();
			
			
			// Component setups// Setup for GameObject 9367691
			gameObject9367691.SetActive(true);
			gameObject9367691.sourcePrefab = new EnvScenePrefab();
			gameObject9367691.AddComponents(transform9367692, behaviour9367693);
			
			
			// Setup for Transform 9367692
			// Children: 
			transform9367692.parent = transform2033369363;
			transform9367692.localPosition = new Vector3(0f, 0f, 0f);
			transform9367692.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform9367692.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for MonoBehaviour 9367693
			// Script with GUID f4688fdb7df04437aeb418b961361dc5 not found for component 9367693
			
			// Setup for GameObject 11743762
			gameObject11743762.SetActive(true);
			gameObject11743762.sourcePrefab = new EnvScenePrefab();
			gameObject11743762.AddComponents(transform11743763, behaviour11743764);
			
			
			// Setup for Transform 11743763
			// Children: 
			transform11743763.parent = transform1859288642;
			transform11743763.localPosition = new Vector3(0f, 0f, 0f);
			transform11743763.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform11743763.scale = new Vector3(4.17f, 4.17f, 1f);
			
			
			// Setup for MonoBehaviour 11743764
			// Script with GUID fe87c0e1cc204ed48ad3b37840f39efc not found for component 11743764
			
			// Setup for GameObject 47798556
			gameObject47798556.SetActive(true);
			gameObject47798556.sourcePrefab = new EnvScenePrefab();
			gameObject47798556.AddComponents(transform47798557, behaviour47798558, behaviour47798560);
			
			
			// Setup for Transform 47798557
			// Children: 796391538
			transform47798557.parent = transform1859288642;
			transform47798557.localPosition = new Vector3(0f, 0f, 0f);
			transform47798557.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform47798557.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for MonoBehaviour 47798558
			// Script with GUID fe87c0e1cc204ed48ad3b37840f39efc not found for component 47798558
			
			// Setup for MonoBehaviour 47798560
			// Script with GUID cb7aa4676791eeb479417024f71cb799 not found for component 47798560
			
			// Setup for GameObject 262980278
			gameObject262980278.SetActive(true);
			gameObject262980278.sourcePrefab = new EnvScenePrefab();
			gameObject262980278.AddComponents(transform262980279);
			
			
			// Setup for Transform 262980279
			// Children: 
			transform262980279.parent = transform662565500;
			transform262980279.localPosition = new Vector3(96199f, 0f, 98981f);
			transform262980279.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform262980279.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for GameObject 265828803
			gameObject265828803.SetActive(true);
			gameObject265828803.sourcePrefab = new EnvScenePrefab();
			gameObject265828803.AddComponents(transform265828804);
			
			
			// Setup for Transform 265828804
			// Children: 1261272101
			transform265828804.parent = transform1447504076;
			transform265828804.localPosition = new Vector3(0f, 0.23f, -3.87f);
			transform265828804.localRotation = new Quaternion(0f, 1f, 0f, 0f);
			transform265828804.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for GameObject 363287742
			gameObject363287742.SetActive(true);
			gameObject363287742.sourcePrefab = new EnvScenePrefab();
			gameObject363287742.AddComponents(transform363287745, behaviour363287744, behaviour363287743);
			
			
			// Setup for MonoBehaviour 363287743
			// Script with GUID 4f231c4fb786f3946a6b90b886c48677 not found for component 363287743
			
			// Setup for MonoBehaviour 363287744
			// Script with GUID 76c392e42b5098c458856cdf6ecaaaa1 not found for component 363287744
			
			// Setup for Transform 363287745
			// Children: 
			transform363287745.parent = transform662565500;
			transform363287745.localPosition = new Vector3(0f, 0f, 0f);
			transform363287745.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform363287745.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for PrefabInstance 608333639
			((Transform)pfInst608333639.componentFileIdMap[7867794619607844773]).parent = transform662565500;
			pfInst608333639.name = "TargetClient";
			((Rigidbody)pfInst608333639.componentFileIdMap[1302538070612963282]).centerOfMass.x = 0.0003258884f; // Inferred float type
			((Rigidbody)pfInst608333639.componentFileIdMap[1302538070612963282]).centerOfMass.y = 0.029785156f; // Inferred float type
			((Rigidbody)pfInst608333639.componentFileIdMap[1302538070612963282]).centerOfMass.z = -3.488481f; // Inferred float type
			((EquipManager)pfInst608333639.componentFileIdMap[1695996403598227547]).tryFire = false; // bool type
			((Radar)pfInst608333639.componentFileIdMap[3424778467738142397]).unlock = false; // bool type
			((Radar)pfInst608333639.componentFileIdMap[3424778467738142397]).autolock = false; // bool type
			((GameObject)pfInst608333639.gameObjectFileIdMap[4138221863959355245]).activeInHierarchy = false; // Inferred bool type
			((KinematicPlane)pfInst608333639.componentFileIdMap[4892452918045270120]).debug = false; // bool type
			((KinematicPlane)pfInst608333639.componentFileIdMap[4892452918045270120]).paused = false; // bool type
			((Transform)pfInst608333639.componentFileIdMap[7867794619607844773]).localPosition = new Vector3(28689f, ((Transform)pfInst608333639.componentFileIdMap[7867794619607844773]).localPosition.y, ((Transform)pfInst608333639.componentFileIdMap[7867794619607844773]).localPosition.z);
			((Transform)pfInst608333639.componentFileIdMap[7867794619607844773]).localPosition = new Vector3(((Transform)pfInst608333639.componentFileIdMap[7867794619607844773]).localPosition.x, 6029.888f, ((Transform)pfInst608333639.componentFileIdMap[7867794619607844773]).localPosition.z);
			((Transform)pfInst608333639.componentFileIdMap[7867794619607844773]).localPosition = new Vector3(((Transform)pfInst608333639.componentFileIdMap[7867794619607844773]).localPosition.x, ((Transform)pfInst608333639.componentFileIdMap[7867794619607844773]).localPosition.y, 34983f);
			((Transform)pfInst608333639.componentFileIdMap[7867794619607844773]).localRotation = new Quaternion(((Transform)pfInst608333639.componentFileIdMap[7867794619607844773]).localRotation.x, ((Transform)pfInst608333639.componentFileIdMap[7867794619607844773]).localRotation.y, ((Transform)pfInst608333639.componentFileIdMap[7867794619607844773]).localRotation.z, -0.062877156f);
			((Transform)pfInst608333639.componentFileIdMap[7867794619607844773]).localRotation = new Quaternion(-0f, ((Transform)pfInst608333639.componentFileIdMap[7867794619607844773]).localRotation.y, ((Transform)pfInst608333639.componentFileIdMap[7867794619607844773]).localRotation.z, ((Transform)pfInst608333639.componentFileIdMap[7867794619607844773]).localRotation.w);
			((Transform)pfInst608333639.componentFileIdMap[7867794619607844773]).localRotation = new Quaternion(((Transform)pfInst608333639.componentFileIdMap[7867794619607844773]).localRotation.x, 0.9980213f, ((Transform)pfInst608333639.componentFileIdMap[7867794619607844773]).localRotation.z, ((Transform)pfInst608333639.componentFileIdMap[7867794619607844773]).localRotation.w);
			((Transform)pfInst608333639.componentFileIdMap[7867794619607844773]).localRotation = new Quaternion(((Transform)pfInst608333639.componentFileIdMap[7867794619607844773]).localRotation.x, ((Transform)pfInst608333639.componentFileIdMap[7867794619607844773]).localRotation.y, -0f, ((Transform)pfInst608333639.componentFileIdMap[7867794619607844773]).localRotation.w);
			
			
			
			
			// Setup for GameObject 612834648
			gameObject612834648.SetActive(true);
			gameObject612834648.sourcePrefab = new EnvScenePrefab();
			gameObject612834648.AddComponents(transform612834649, behaviour612834651);
			
			
			// Setup for Transform 612834649
			// Children: 
			transform612834649.parent = transform1859288642;
			transform612834649.localPosition = new Vector3(0f, 0f, 0f);
			transform612834649.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform612834649.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for MonoBehaviour 612834651
			// Script with GUID 437a09559ff367d4cb2ba93a8ab55b38 not found for component 612834651
			
			// Setup for GameObject 796391537
			gameObject796391537.SetActive(true);
			gameObject796391537.sourcePrefab = new EnvScenePrefab();
			gameObject796391537.AddComponents(transform796391538, behaviour796391539);
			
			
			// Setup for Transform 796391538
			// Children: 
			transform796391538.parent = transform47798557;
			transform796391538.localPosition = new Vector3(0f, 0f, 0f);
			transform796391538.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform796391538.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for MonoBehaviour 796391539
			// Script with GUID fe87c0e1cc204ed48ad3b37840f39efc not found for component 796391539
			
			// Setup for GameObject 942087939
			gameObject942087939.SetActive(true);
			gameObject942087939.sourcePrefab = new EnvScenePrefab();
			gameObject942087939.AddComponents(transform942087941);
			
			
			// Setup for Transform 942087941
			// Children: 
			transform942087941.parent = transform662565500;
			transform942087941.localPosition = new Vector3(1.98f, 7.73f, -3.44f);
			transform942087941.localRotation = new Quaternion(-0.22590484f, 0.21742448f, -0.23241222f, -0.9206945f);
			transform942087941.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for GameObject 1045590941
			gameObject1045590941.SetActive(true);
			gameObject1045590941.sourcePrefab = new EnvScenePrefab();
			gameObject1045590941.AddComponents(transform1045590942, behaviour1045590943);
			
			
			// Setup for Transform 1045590942
			// Children: 
			transform1045590942.parent = transform2033369363;
			transform1045590942.localPosition = new Vector3(0f, 0f, 0f);
			transform1045590942.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform1045590942.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for MonoBehaviour 1045590943
			// Script with GUID f4688fdb7df04437aeb418b961361dc5 not found for component 1045590943
			
			// Setup for GameObject 1117093109
			gameObject1117093109.SetActive(true);
			gameObject1117093109.sourcePrefab = new EnvScenePrefab();
			gameObject1117093109.AddComponents(transform1117093110, behaviour1117093111);
			
			
			// Setup for Transform 1117093110
			// Children: 
			transform1117093110.parent = transform1859288642;
			transform1117093110.localPosition = new Vector3(0f, 0f, 0f);
			transform1117093110.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform1117093110.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for MonoBehaviour 1117093111
			// Script with GUID f4688fdb7df04437aeb418b961361dc5 not found for component 1117093111
			
			// Setup for GameObject 1261272100
			gameObject1261272100.SetActive(true);
			gameObject1261272100.sourcePrefab = new EnvScenePrefab();
			gameObject1261272100.AddComponents(transform1261272101);
			
			
			// Setup for Transform 1261272101
			// Children: 
			transform1261272101.parent = transform265828804;
			transform1261272101.localPosition = new Vector3(0f, 0f, 0f);
			transform1261272101.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform1261272101.scale = new Vector3(2.379832f, 2.379832f, 2.379832f);
			
			
			// Setup for GameObject 1311220498
			gameObject1311220498.SetActive(true);
			gameObject1311220498.sourcePrefab = new EnvScenePrefab();
			gameObject1311220498.AddComponents(transform1311220499, behaviour1311220500);
			
			
			// Setup for Transform 1311220499
			// Children: 
			transform1311220499.parent = transform1859288642;
			transform1311220499.localPosition = new Vector3(0f, 0f, 0f);
			transform1311220499.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform1311220499.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for MonoBehaviour 1311220500
			// Script with GUID f4688fdb7df04437aeb418b961361dc5 not found for component 1311220500
			
			// Setup for GameObject 1311617375
			gameObject1311617375.SetActive(true);
			gameObject1311617375.sourcePrefab = new EnvScenePrefab();
			gameObject1311617375.AddComponents(transform1311617377, behaviour1311617376);
			
			
			// Setup for MonoBehaviour 1311617376
			// Script with GUID afb30d11ecdc0c74e83db51cfb6559d9 not found for component 1311617376
			
			// Setup for Transform 1311617377
			// Children: 
			transform1311617377.parent = transform662565500;
			transform1311617377.localPosition = new Vector3(0f, 0f, 0f);
			transform1311617377.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform1311617377.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for PrefabInstance 1339571764
			((Transform)pfInst1339571764.componentFileIdMap[7867794619607844773]).parent = transform662565500;
			pfInst1339571764.name = "AIClient";
			((Rigidbody)pfInst1339571764.componentFileIdMap[1302538070612963282]).centerOfMass.x = 0f; // Inferred float type
			((Rigidbody)pfInst1339571764.componentFileIdMap[1302538070612963282]).centerOfMass.y = 0.02978526f; // Inferred float type
			((Rigidbody)pfInst1339571764.componentFileIdMap[1302538070612963282]).centerOfMass.z = -3.4902344f; // Inferred float type
			((EquipManager)pfInst1339571764.componentFileIdMap[1695996403598227547]).tryFire = false; // bool type
			
			((RWR)pfInst1339571764.componentFileIdMap[3008035590580529071]).persistTime = 0.5f; // float type
			((Radar)pfInst1339571764.componentFileIdMap[3424778467738142397]).autolock = false; // bool type
			((Radar)pfInst1339571764.componentFileIdMap[3424778467738142397]).radarFov = 100f; // float type
			((Radar)pfInst1339571764.componentFileIdMap[3424778467738142397]).currentAzimuthAdjust = 90f; // float type
			((Radar)pfInst1339571764.componentFileIdMap[3424778467738142397]).currentElevationAdjust = 0f; // float type
			((KinematicPlane)pfInst1339571764.componentFileIdMap[4892452918045270120]).paused = false; // bool type
			((Transform)pfInst1339571764.componentFileIdMap[7867794619607844773]).localPosition = new Vector3(27159.504f, ((Transform)pfInst1339571764.componentFileIdMap[7867794619607844773]).localPosition.y, ((Transform)pfInst1339571764.componentFileIdMap[7867794619607844773]).localPosition.z);
			((Transform)pfInst1339571764.componentFileIdMap[7867794619607844773]).localPosition = new Vector3(((Transform)pfInst1339571764.componentFileIdMap[7867794619607844773]).localPosition.x, 6000.28f, ((Transform)pfInst1339571764.componentFileIdMap[7867794619607844773]).localPosition.z);
			((Transform)pfInst1339571764.componentFileIdMap[7867794619607844773]).localPosition = new Vector3(((Transform)pfInst1339571764.componentFileIdMap[7867794619607844773]).localPosition.x, ((Transform)pfInst1339571764.componentFileIdMap[7867794619607844773]).localPosition.y, 21709.525f);
			((Transform)pfInst1339571764.componentFileIdMap[7867794619607844773]).localRotation = new Quaternion(((Transform)pfInst1339571764.componentFileIdMap[7867794619607844773]).localRotation.x, ((Transform)pfInst1339571764.componentFileIdMap[7867794619607844773]).localRotation.y, ((Transform)pfInst1339571764.componentFileIdMap[7867794619607844773]).localRotation.z, 1f);
			((Transform)pfInst1339571764.componentFileIdMap[7867794619607844773]).localRotation = new Quaternion(-0.000000014901161f, ((Transform)pfInst1339571764.componentFileIdMap[7867794619607844773]).localRotation.y, ((Transform)pfInst1339571764.componentFileIdMap[7867794619607844773]).localRotation.z, ((Transform)pfInst1339571764.componentFileIdMap[7867794619607844773]).localRotation.w);
			((Transform)pfInst1339571764.componentFileIdMap[7867794619607844773]).localRotation = new Quaternion(((Transform)pfInst1339571764.componentFileIdMap[7867794619607844773]).localRotation.x, -0f, ((Transform)pfInst1339571764.componentFileIdMap[7867794619607844773]).localRotation.z, ((Transform)pfInst1339571764.componentFileIdMap[7867794619607844773]).localRotation.w);
			((Transform)pfInst1339571764.componentFileIdMap[7867794619607844773]).localRotation = new Quaternion(((Transform)pfInst1339571764.componentFileIdMap[7867794619607844773]).localRotation.x, ((Transform)pfInst1339571764.componentFileIdMap[7867794619607844773]).localRotation.y, -0f, ((Transform)pfInst1339571764.componentFileIdMap[7867794619607844773]).localRotation.w);
			
			
			
			
			// Setup for GameObject 1363819661
			gameObject1363819661.SetActive(true);
			gameObject1363819661.sourcePrefab = new EnvScenePrefab();
			gameObject1363819661.AddComponents(transform1363819662);
			
			
			// Setup for Transform 1363819662
			// Children: 
			transform1363819662.parent = transform1447504076;
			transform1363819662.localPosition = new Vector3(0f, 0.03f, -3.49f);
			transform1363819662.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform1363819662.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for GameObject 1447504070
			gameObject1447504070.SetActive(false);
			gameObject1447504070.sourcePrefab = new EnvScenePrefab();
			gameObject1447504070.AddComponents(transform1447504076, behaviour1447504075, behaviour1447504074, behaviour1447504073, behaviour1447504072, rigidBody1447504071);
			
			
			// Setup for RigidBody 1447504071
			rigidBody1447504071.mass = 1f;
			rigidBody1447504071.drag = 0f;
			rigidBody1447504071.angularDrag = 0.05f;
			rigidBody1447504071.centerOfMass = new Vector3(0f, 0f, 0f);
			rigidBody1447504071.inertiaTensor = new Vector3(1f, 1f, 1f);
			rigidBody1447504071.useGravity = true;
			rigidBody1447504071.isKinematic = true;
			
			
			// Setup for MonoBehaviour 1447504072
			behaviour1447504072.baseMass = 17f;
			behaviour1447504072.updateInterval = -1f;
			behaviour1447504072.enabled = true;
			
			
			// Setup for MonoBehaviour 1447504073
			behaviour1447504073.entity = null;
			behaviour1447504073.throttle = 1f;
			behaviour1447504073.abThrustMult = 1.34f;
			behaviour1447504073.maxThrust = 260f;
			behaviour1447504073.autoAbThreshold = 0.75f;
			behaviour1447504073.speedCurve = TurbofanVelocityCurveSO.instance;
			behaviour1447504073.atmosCurve = TurbofanAtmosCurveSO.instance;
			behaviour1447504073.thrustHeatMult = 25f;
			behaviour1447504073.abHeatAdd = 1f;
			behaviour1447504073.heatEmitter = null;
			behaviour1447504073.resultThrust = 0f;
			behaviour1447504073.enabled = true;
			
			
			// Setup for MonoBehaviour 1447504074
			behaviour1447504074.maxAoAcurve = new AnimationCurve(new Keyframe(0f, 9.950487f, 0.4992255f, 0.4992255f), new Keyframe(141.62473f, 28.527775f, -0.047778506f, -0.047778506f), new Keyframe(330f, 16.409803f, -0.0130549325f, -0.0130549325f));
			behaviour1447504074.maxGCurve = new AnimationCurve(new Keyframe(0f, 0f, 0.0034654606f, 0.0034654606f), new Keyframe(87.43936f, 2.3799484f, 0.05388886f, 0.05388886f), new Keyframe(346.14432f, 9.991627f, 3.5082007e-8f, 3.5082007e-8f));
			behaviour1447504074.rollRateCurve = new AnimationCurve(new Keyframe(0f, 107.245544f, 0f, 0f), new Keyframe(375.20358f, 245.1213f, 0f, 0f));
			behaviour1447504074.lerpCurve = new AnimationCurve(new Keyframe(0f, 2.848402f, -0.0025400529f, -0.0025400529f), new Keyframe(200f, 6.358963f, 0.048973184f, 0.048973184f), new Keyframe(330f, 9.9484215f, 0f, 0f));
			behaviour1447504074.engine = null;
			behaviour1447504074.input = new Vector3(0f, 0f, 0f);
			behaviour1447504074.brake = 0f;
			behaviour1447504074.flaps = 0f;
			behaviour1447504074.flapsMultiplier = 1.5f;
			behaviour1447504074.wingSweep = 20f;
			behaviour1447504074.pitchLerpMult = 0.8f;
			behaviour1447504074.rollLerpMult = 1.5f;
			behaviour1447504074.yawGMult = 0.08f;
			behaviour1447504074.yawAoAMult = 0.45f;
			behaviour1447504074.dragArea = 0.18f;
			behaviour1447504074.brakeDrag = 0.29f;
			behaviour1447504074.fixedPoint = new Vector3(0f, 0f, 0f);
			behaviour1447504074.rb = null;
			behaviour1447504074.paused = false;
			behaviour1447504074.debug = false;
			behaviour1447504074.enabled = true;
			
			
			// Setup for MonoBehaviour 1447504075
			behaviour1447504075.team = (Team)0;
			behaviour1447504075.enabled = true;
			
			
			// Setup for Transform 1447504076
			// Children: 265828804, 1363819662, 1717733298
			transform1447504076.parent = transform662565500;
			transform1447504076.localPosition = new Vector3(23f, 0f, 0f);
			transform1447504076.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform1447504076.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for GameObject 1717733297
			gameObject1717733297.SetActive(true);
			gameObject1717733297.sourcePrefab = new EnvScenePrefab();
			gameObject1717733297.AddComponents(transform1717733298);
			
			
			// Setup for Transform 1717733298
			// Children: 2033369363
			transform1717733298.parent = transform1447504076;
			transform1717733298.localPosition = new Vector3(-0.5f, 8.9f, -30.5f);
			transform1717733298.localRotation = new Quaternion(0.12152836f, 0f, 0f, 0.992588f);
			transform1717733298.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for GameObject 1859288641
			gameObject1859288641.SetActive(true);
			gameObject1859288641.sourcePrefab = new EnvScenePrefab();
			gameObject1859288641.AddComponents(transform1859288642, behaviour1859288644, behaviour1859288646);
			
			
			// Setup for Transform 1859288642
			// Children: 1117093110, 1311220499, 612834649, 11743763, 47798557
			transform1859288642.parent = transform662565500;
			transform1859288642.localPosition = new Vector3(0f, 0f, 0f);
			transform1859288642.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform1859288642.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for MonoBehaviour 1859288644
			// Script with GUID 0cd44c1031e13a943bb63640046fad76 not found for component 1859288644
			
			// Setup for MonoBehaviour 1859288646
			// Script with GUID 689280b9a998eb34a843a65fce0ec382 not found for component 1859288646
			
			// Setup for GameObject 1941113518
			gameObject1941113518.SetActive(true);
			gameObject1941113518.sourcePrefab = new EnvScenePrefab();
			gameObject1941113518.AddComponents(transform1941113519, behaviour1941113520, behaviour1941113521, behaviour1941113522, behaviour1941113524, behaviour1941113523, behaviour1941113525);
			
			
			// Setup for Transform 1941113519
			// Children: 
			transform1941113519.parent = transform662565500;
			transform1941113519.localPosition = new Vector3(0f, 0f, 0f);
			transform1941113519.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform1941113519.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for MonoBehaviour 1941113520
			behaviour1941113520.wingAero = Wing_AeroSO.instance;
			behaviour1941113520.atmosDensityCurve = new AnimationCurve(new Keyframe(0f, 1f, -0.00010767791f, -0.00010767791f), new Keyframe(2136f, 0.77f, -0.000094025076f, -0.000094025076f), new Keyframe(4500f, 0.58f, -0.00007161469f, -0.00007161469f), new Keyframe(8000f, 0.36f, -0.00005309524f, -0.00005309524f), new Keyframe(11000f, 0.23f, -0.000035219513f, -0.000035219513f), new Keyframe(15058.188f, 0.12f, -0.000016841554f, -0.000016841554f), new Keyframe(32000f, 0.008566692f, -0.0000012097864f, -0.0000012097864f), new Keyframe(45720f, 0.001556584f, -1.6992908e-7f, -1.6992908e-7f), new Keyframe(60960f, 0.00019307832f, 0f, 0f));
			behaviour1941113520.defaultDragMachCurve = DragMachCurveSO.instance;
			behaviour1941113520.wingDragMachCurveZero = WingDragMachCurveZeroSO.instance;
			behaviour1941113520.sweepDragModifierX = 0.3f;
			behaviour1941113520.sweepDragModifierY = 0.27f;
			behaviour1941113520.enabled = true;
			
			
			// Setup for MonoBehaviour 1941113521
			behaviour1941113521.clientId = "";
			behaviour1941113521.manager = behaviour1941113522;
			behaviour1941113521.doWsReconnect = false;
			behaviour1941113521.enabled = false;
			
			
			// Setup for MonoBehaviour 1941113522
			behaviour1941113522.connector = behaviour1941113521;
			behaviour1941113522.playerEntityPrefab = AircraftPrefab.Create();
			behaviour1941113522.missilePrefab = HCMissilePrefab.Create();
			behaviour1941113522.missileParent = transform2114172170;
			behaviour1941113522.equipManager = ((EquipManager)pfInst1339571764.componentFileIdMap[1695996403598227547]);
			behaviour1941113522.aiClient = ((AIClient)pfInst1339571764.componentFileIdMap[5527500078195339178]);
			behaviour1941113522.rwr = ((RWR)pfInst1339571764.componentFileIdMap[3008035590580529071]);
			behaviour1941113522.enabled = false;
			
			
			// Setup for MonoBehaviour 1941113523
			behaviour1941113523.maxSimTime = 300f;
			behaviour1941113523.outputPath = "./recording.json";
			behaviour1941113523.graphOutputPath = "./graphs.json";
			behaviour1941113523.logOutputPath = "./aip.log";
			behaviour1941113523.stateOutputPath = "./state.json";
			behaviour1941113523.testingValuesOutputPath = "./rvt.json";
			behaviour1941113523.enabled = true;
			
			
			// Setup for MonoBehaviour 1941113524
			behaviour1941113524.spawnCenterPoint = transform262980279;
			behaviour1941113524.spawnDistance = 72000f;
			behaviour1941113524.spawnAltitude = 6000f;
			behaviour1941113524.doSetupSpawns = false;
			behaviour1941113524.weaponStats = WeaponStatsSO.instance;
			behaviour1941113524.teamAAIPilotImplementationPath = "";
			behaviour1941113524.teamBAIPilotImplementationPath = "";
			behaviour1941113524.alliedClients = new(1);
			behaviour1941113524.alliedClients.Add(((AIClient)pfInst1339571764.componentFileIdMap[5527500078195339178]));
			behaviour1941113524.enemyClients = new(1);
			behaviour1941113524.enemyClients.Add(((AIClient)pfInst608333639.componentFileIdMap[5527500078195339178]));
			behaviour1941113524.enabled = true;
			
			
			// Setup for MonoBehaviour 1941113525
			// Script with GUID b9135f7c3f7d9634d9dbc84dd13c0fed not found for component 1941113525
			
			// Setup for GameObject 2033369362
			gameObject2033369362.SetActive(true);
			gameObject2033369362.sourcePrefab = new EnvScenePrefab();
			gameObject2033369362.AddComponents(transform2033369363, behaviour2033369366, behaviour2033369365, behaviour2033369364);
			
			
			// Setup for Transform 2033369363
			// Children: 9367692, 1045590942
			transform2033369363.parent = transform1717733298;
			transform2033369363.localPosition = new Vector3(0f, 0f, 0f);
			transform2033369363.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform2033369363.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for MonoBehaviour 2033369364
			// Script with GUID 689280b9a998eb34a843a65fce0ec382 not found for component 2033369364
			
			// Setup for MonoBehaviour 2033369365
			// Script with GUID dc42784cf147c0c48a680349fa168899 not found for component 2033369365
			
			// Setup for MonoBehaviour 2033369366
			// Script with GUID 0cd44c1031e13a943bb63640046fad76 not found for component 2033369366
			
			// Setup for GameObject 2114172169
			gameObject2114172169.SetActive(true);
			gameObject2114172169.sourcePrefab = new EnvScenePrefab();
			gameObject2114172169.AddComponents(transform2114172170);
			
			
			// Setup for Transform 2114172170
			// Children: 
			transform2114172170.parent = transform662565500;
			transform2114172170.localPosition = new Vector3(0f, 0f, 0f);
			transform2114172170.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform2114172170.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for PrefabInstance 5005441902897201914
			((Transform)pfInst5005441902897201914.componentFileIdMap[3738062474209517589]).parent = transform662565500;
			((RadarCrossSection)pfInst5005441902897201914.componentFileIdMap[442573906786450286]).size = 10.3865f; // float type
			((HCPlayerEntity)pfInst5005441902897201914.componentFileIdMap[3738062474209517588]).path = "Vehicles/FA-26B"; // string type
			((HCPlayerEntity)pfInst5005441902897201914.componentFileIdMap[3738062474209517588]).entityId = 1859; // int type
			((Transform)pfInst5005441902897201914.componentFileIdMap[3738062474209517589]).localPosition = new Vector3(27475f, ((Transform)pfInst5005441902897201914.componentFileIdMap[3738062474209517589]).localPosition.y, ((Transform)pfInst5005441902897201914.componentFileIdMap[3738062474209517589]).localPosition.z);
			((Transform)pfInst5005441902897201914.componentFileIdMap[3738062474209517589]).localPosition = new Vector3(((Transform)pfInst5005441902897201914.componentFileIdMap[3738062474209517589]).localPosition.x, 6000f, ((Transform)pfInst5005441902897201914.componentFileIdMap[3738062474209517589]).localPosition.z);
			((Transform)pfInst5005441902897201914.componentFileIdMap[3738062474209517589]).localPosition = new Vector3(((Transform)pfInst5005441902897201914.componentFileIdMap[3738062474209517589]).localPosition.x, ((Transform)pfInst5005441902897201914.componentFileIdMap[3738062474209517589]).localPosition.y, 20300f);
			((Transform)pfInst5005441902897201914.componentFileIdMap[3738062474209517589]).localRotation = new Quaternion(((Transform)pfInst5005441902897201914.componentFileIdMap[3738062474209517589]).localRotation.x, ((Transform)pfInst5005441902897201914.componentFileIdMap[3738062474209517589]).localRotation.y, ((Transform)pfInst5005441902897201914.componentFileIdMap[3738062474209517589]).localRotation.z, 0.0000020968089f);
			((Transform)pfInst5005441902897201914.componentFileIdMap[3738062474209517589]).localRotation = new Quaternion(0.0000020968027f, ((Transform)pfInst5005441902897201914.componentFileIdMap[3738062474209517589]).localRotation.y, ((Transform)pfInst5005441902897201914.componentFileIdMap[3738062474209517589]).localRotation.z, ((Transform)pfInst5005441902897201914.componentFileIdMap[3738062474209517589]).localRotation.w);
			((Transform)pfInst5005441902897201914.componentFileIdMap[3738062474209517589]).localRotation = new Quaternion(((Transform)pfInst5005441902897201914.componentFileIdMap[3738062474209517589]).localRotation.x, -0.70710784f, ((Transform)pfInst5005441902897201914.componentFileIdMap[3738062474209517589]).localRotation.z, ((Transform)pfInst5005441902897201914.componentFileIdMap[3738062474209517589]).localRotation.w);
			((Transform)pfInst5005441902897201914.componentFileIdMap[3738062474209517589]).localRotation = new Quaternion(((Transform)pfInst5005441902897201914.componentFileIdMap[3738062474209517589]).localRotation.x, ((Transform)pfInst5005441902897201914.componentFileIdMap[3738062474209517589]).localRotation.y, -0.70710576f, ((Transform)pfInst5005441902897201914.componentFileIdMap[3738062474209517589]).localRotation.w);
			
			
			
			pfInst5005441902897201914.name = "Aircraft1";
			((GameObject)pfInst5005441902897201914.gameObjectFileIdMap[3738062474209517590]).activeInHierarchy = false; // Inferred bool type
			
			// Setup for GameObject 619946745
			gameObject619946745.SetActive(true);
			gameObject619946745.sourcePrefab = new EnvScenePrefab();
			gameObject619946745.AddComponents(transform662565500);
			
			
			// Setup for Transform 662565500
			// Children: 942087941, 5005441902897201914, 1311617377, 1941113519, 1339571764, 608333639, 363287745, 1447504076, 1859288642, 2114172170, 262980279
			transform662565500.localPosition = new Vector3(0f, 0f, 0f);
			transform662565500.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform662565500.scale = new Vector3(1f, 1f, 1f);
			
			
			gameObject619946745.gameObjectFileIdMap[9367691] = gameObject9367691;
			gameObject619946745.componentFileIdMap[9367692] = transform9367692;
			gameObject619946745.componentFileIdMap[9367693] = behaviour9367693;
			gameObject619946745.gameObjectFileIdMap[11743762] = gameObject11743762;
			gameObject619946745.componentFileIdMap[11743763] = transform11743763;
			gameObject619946745.componentFileIdMap[11743764] = behaviour11743764;
			gameObject619946745.gameObjectFileIdMap[47798556] = gameObject47798556;
			gameObject619946745.componentFileIdMap[47798557] = transform47798557;
			gameObject619946745.componentFileIdMap[47798558] = behaviour47798558;
			gameObject619946745.componentFileIdMap[47798560] = behaviour47798560;
			gameObject619946745.gameObjectFileIdMap[262980278] = gameObject262980278;
			gameObject619946745.componentFileIdMap[262980279] = transform262980279;
			gameObject619946745.gameObjectFileIdMap[265828803] = gameObject265828803;
			gameObject619946745.componentFileIdMap[265828804] = transform265828804;
			gameObject619946745.gameObjectFileIdMap[363287742] = gameObject363287742;
			gameObject619946745.componentFileIdMap[363287743] = behaviour363287743;
			gameObject619946745.componentFileIdMap[363287744] = behaviour363287744;
			gameObject619946745.componentFileIdMap[363287745] = transform363287745;
			gameObject619946745.gameObjectFileIdMap[608333639] = pfInst608333639;
			gameObject619946745.gameObjectFileIdMap[612834648] = gameObject612834648;
			gameObject619946745.componentFileIdMap[612834649] = transform612834649;
			gameObject619946745.componentFileIdMap[612834651] = behaviour612834651;
			gameObject619946745.componentFileIdMap[613013775] = ((AIClient)pfInst608333639.componentFileIdMap[5527500078195339178]);
			gameObject619946745.componentFileIdMap[755765158] = ((Radar)pfInst1339571764.componentFileIdMap[3424778467738142397]);
			gameObject619946745.gameObjectFileIdMap[796391537] = gameObject796391537;
			gameObject619946745.componentFileIdMap[796391538] = transform796391538;
			gameObject619946745.componentFileIdMap[796391539] = behaviour796391539;
			gameObject619946745.gameObjectFileIdMap[942087939] = gameObject942087939;
			gameObject619946745.componentFileIdMap[942087941] = transform942087941;
			gameObject619946745.gameObjectFileIdMap[1045590941] = gameObject1045590941;
			gameObject619946745.componentFileIdMap[1045590942] = transform1045590942;
			gameObject619946745.componentFileIdMap[1045590943] = behaviour1045590943;
			gameObject619946745.gameObjectFileIdMap[1117093109] = gameObject1117093109;
			gameObject619946745.componentFileIdMap[1117093110] = transform1117093110;
			gameObject619946745.componentFileIdMap[1117093111] = behaviour1117093111;
			gameObject619946745.gameObjectFileIdMap[1261272100] = gameObject1261272100;
			gameObject619946745.componentFileIdMap[1261272101] = transform1261272101;
			gameObject619946745.gameObjectFileIdMap[1311220498] = gameObject1311220498;
			gameObject619946745.componentFileIdMap[1311220499] = transform1311220499;
			gameObject619946745.componentFileIdMap[1311220500] = behaviour1311220500;
			gameObject619946745.gameObjectFileIdMap[1311617375] = gameObject1311617375;
			gameObject619946745.componentFileIdMap[1311617376] = behaviour1311617376;
			gameObject619946745.componentFileIdMap[1311617377] = transform1311617377;
			gameObject619946745.gameObjectFileIdMap[1339571764] = pfInst1339571764;
			gameObject619946745.componentFileIdMap[1339571765] = ((RWR)pfInst1339571764.componentFileIdMap[3008035590580529071]);
			gameObject619946745.componentFileIdMap[1339571766] = ((EquipManager)pfInst1339571764.componentFileIdMap[1695996403598227547]);
			gameObject619946745.componentFileIdMap[1339571767] = ((AIClient)pfInst1339571764.componentFileIdMap[5527500078195339178]);
			gameObject619946745.componentFileIdMap[1339571776] = ((KinematicPlane)pfInst1339571764.componentFileIdMap[4892452918045270120]);
			gameObject619946745.gameObjectFileIdMap[1363819661] = gameObject1363819661;
			gameObject619946745.componentFileIdMap[1363819662] = transform1363819662;
			gameObject619946745.gameObjectFileIdMap[1447504070] = gameObject1447504070;
			gameObject619946745.componentFileIdMap[1447504071] = rigidBody1447504071;
			gameObject619946745.componentFileIdMap[1447504072] = behaviour1447504072;
			gameObject619946745.componentFileIdMap[1447504073] = behaviour1447504073;
			gameObject619946745.componentFileIdMap[1447504074] = behaviour1447504074;
			gameObject619946745.componentFileIdMap[1447504075] = behaviour1447504075;
			gameObject619946745.componentFileIdMap[1447504076] = transform1447504076;
			gameObject619946745.gameObjectFileIdMap[1717733297] = gameObject1717733297;
			gameObject619946745.componentFileIdMap[1717733298] = transform1717733298;
			gameObject619946745.gameObjectFileIdMap[1859288641] = gameObject1859288641;
			gameObject619946745.componentFileIdMap[1859288642] = transform1859288642;
			gameObject619946745.componentFileIdMap[1859288644] = behaviour1859288644;
			gameObject619946745.componentFileIdMap[1859288646] = behaviour1859288646;
			gameObject619946745.gameObjectFileIdMap[1941113518] = gameObject1941113518;
			gameObject619946745.componentFileIdMap[1941113519] = transform1941113519;
			gameObject619946745.componentFileIdMap[1941113520] = behaviour1941113520;
			gameObject619946745.componentFileIdMap[1941113521] = behaviour1941113521;
			gameObject619946745.componentFileIdMap[1941113522] = behaviour1941113522;
			gameObject619946745.componentFileIdMap[1941113523] = behaviour1941113523;
			gameObject619946745.componentFileIdMap[1941113524] = behaviour1941113524;
			gameObject619946745.componentFileIdMap[1941113525] = behaviour1941113525;
			gameObject619946745.gameObjectFileIdMap[2033369362] = gameObject2033369362;
			gameObject619946745.componentFileIdMap[2033369363] = transform2033369363;
			gameObject619946745.componentFileIdMap[2033369364] = behaviour2033369364;
			gameObject619946745.componentFileIdMap[2033369365] = behaviour2033369365;
			gameObject619946745.componentFileIdMap[2033369366] = behaviour2033369366;
			gameObject619946745.gameObjectFileIdMap[2114172169] = gameObject2114172169;
			gameObject619946745.componentFileIdMap[2114172170] = transform2114172170;
			gameObject619946745.gameObjectFileIdMap[5005441902897201914] = pfInst5005441902897201914;
			gameObject619946745.componentFileIdMap[7210895780769036230] = ((Transform)pfInst5005441902897201914.componentFileIdMap[3738062474209517589]);
			gameObject619946745.gameObjectFileIdMap[9223372036854775807] = gameObject619946745;
			gameObject619946745.gameObjectFileIdMap[619946745] = gameObject619946745;
			gameObject619946745.componentFileIdMap[662565500] = transform662565500;
			
			return gameObject619946745;
		}

		public override GameObject CreateInstance() 
		{
			return Create();
		}
	}
}