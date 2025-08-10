
using UnityGERunner;
using Coroutine;
using UnityGERunner.UnityApplication;
using UnityGERunner.UnityApplication.ScriptableObjects;


namespace UnityGERunner.UnityApplication.Prefabs
{
	public class AircraftPrefab : Prefab
	{
		public static GameObject Create()
		{
			// Component initializations
			var gameObject3738062472738147320 = new GameObject("Model");
			var transform3738062472738147321 = new Transform();
			
			var gameObject3738062474184576475 = new GameObject("BodyParent");
			var transform3738062474184576472 = new Transform();
			
			var gameObject3738062474209517590 = new GameObject("Aircraft");
			var transform3738062474209517589 = new Transform();
			
			var behaviour7210895779002417747 = new Actor();
			var behaviour3738062474209517588 = new HCPlayerEntity();
			var behaviour3738062474209517591 = new MonoBehaviour();
			var behaviour442573906786450286 = new RadarCrossSection();
			var behaviour5005441903793897913 = new IRCountermeasure();
			var behaviour5005441903793897918 = new ChaffCountermeasure();
			var gameObject5005441903226408491 = new GameObject("LeftEngine");
			var transform5005441903226408488 = new Transform();
			
			var behaviour5005441903226408494 = new MonoBehaviour();
			var behaviour5005441903226408489 = new HeatEmitter();
			var gameObject5005441903954840346 = new GameObject("RightEngine");
			var transform5005441903954840347 = new Transform();
			
			var behaviour5005441903954840345 = new MonoBehaviour();
			var behaviour5005441903954840344 = new HeatEmitter();
			
			// Component setups// Setup for GameObject 3738062472738147320
			gameObject3738062472738147320.SetActive(true);
			gameObject3738062472738147320.sourcePrefab = new AircraftPrefab();
			gameObject3738062472738147320.AddComponents(transform3738062472738147321);
			
			
			// Setup for Transform 3738062472738147321
			// Children: 
			transform3738062472738147321.parent = transform3738062474184576472;
			transform3738062472738147321.localPosition = new Vector3(0f, 0f, 0f);
			transform3738062472738147321.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform3738062472738147321.scale = new Vector3(2.379832f, 2.379832f, 2.379832f);
			
			
			// Setup for GameObject 3738062474184576475
			gameObject3738062474184576475.SetActive(true);
			gameObject3738062474184576475.sourcePrefab = new AircraftPrefab();
			gameObject3738062474184576475.AddComponents(transform3738062474184576472);
			
			
			// Setup for Transform 3738062474184576472
			// Children: 3738062472738147321
			transform3738062474184576472.parent = transform3738062474209517589;
			transform3738062474184576472.localPosition = new Vector3(0f, 0.32f, -3.81f);
			transform3738062474184576472.localRotation = new Quaternion(1.1263973e-14f, 1f, 0f, 2.0861624e-7f);
			transform3738062474184576472.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for GameObject 3738062474209517590
			gameObject3738062474209517590.SetActive(true);
			gameObject3738062474209517590.sourcePrefab = new AircraftPrefab();
			gameObject3738062474209517590.AddComponents(transform3738062474209517589, behaviour7210895779002417747, behaviour3738062474209517588, behaviour3738062474209517591, behaviour442573906786450286, behaviour5005441903793897913, behaviour5005441903793897918);
			
			
			// Setup for Transform 3738062474209517589
			// Children: 3738062474184576472, 5005441903226408488, 5005441903954840347
			transform3738062474209517589.localPosition = new Vector3(27475f, 1325f, 14664f);
			transform3738062474209517589.localRotation = new Quaternion(0f, -1f, 0f, 0f);
			transform3738062474209517589.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for MonoBehaviour 7210895779002417747
			behaviour7210895779002417747.team = (Team)0;
			// Property "isSpawned" not found in MonoBehaviour 7210895779002417747
			behaviour7210895779002417747.enabled = true;
			
			
			// Setup for MonoBehaviour 3738062474209517588
			behaviour3738062474209517588.entityId = 0;
			behaviour3738062474209517588.path = "";
			behaviour3738062474209517588.ownerId = (ulong)0;
			behaviour3738062474209517588.manager = null;
			behaviour3738062474209517588.velocity = new Vector3(0f, 0f, 0f);
			behaviour3738062474209517588.acceleration = new Vector3(0f, 0f, 0f);
			behaviour3738062474209517588.throttle = 0f;
			behaviour3738062474209517588.rwrSensitivity = 1100f;
			behaviour3738062474209517588.missiles = new();
			behaviour3738062474209517588.enabled = true;
			
			
			// Setup for MonoBehaviour 3738062474209517591
			// Script with GUID dfcc4c78f30689b4080592e0cdb521b1 not found for component 3738062474209517591
			
			// Setup for MonoBehaviour 442573906786450286
			behaviour442573906786450286.referenceTf = transform3738062474209517589;
			behaviour442573906786450286.returns = new(18);
			var behaviour442573906786450286returns0 = behaviour442573906786450286.returns.ElementAtOrDefaultSafe(0);
			behaviour442573906786450286returns0.normal = new Vector3(-0f, -0f, -1f);
			behaviour442573906786450286returns0.returnValue = 0.0030040068f;
			behaviour442573906786450286.returns.Add(behaviour442573906786450286returns0);
			var behaviour442573906786450286returns1 = behaviour442573906786450286.returns.ElementAtOrDefaultSafe(1);
			behaviour442573906786450286returns1.normal = new Vector3(-0.7071068f, -0f, -0.7071067f);
			behaviour442573906786450286returns1.returnValue = 0.020103576f;
			behaviour442573906786450286.returns.Add(behaviour442573906786450286returns1);
			var behaviour442573906786450286returns2 = behaviour442573906786450286.returns.ElementAtOrDefaultSafe(2);
			behaviour442573906786450286returns2.normal = new Vector3(-0.99999994f, -0f, -0.000000059604645f);
			behaviour442573906786450286returns2.returnValue = 0.039612994f;
			behaviour442573906786450286.returns.Add(behaviour442573906786450286returns2);
			var behaviour442573906786450286returns3 = behaviour442573906786450286.returns.ElementAtOrDefaultSafe(3);
			behaviour442573906786450286returns3.normal = new Vector3(-0.70710677f, -0f, 0.7071067f);
			behaviour442573906786450286returns3.returnValue = 0.010492419f;
			behaviour442573906786450286.returns.Add(behaviour442573906786450286returns3);
			var behaviour442573906786450286returns4 = behaviour442573906786450286.returns.ElementAtOrDefaultSafe(4);
			behaviour442573906786450286returns4.normal = new Vector3(0.00000008742278f, -0f, 1f);
			behaviour442573906786450286returns4.returnValue = 0.0014441356f;
			behaviour442573906786450286.returns.Add(behaviour442573906786450286returns4);
			var behaviour442573906786450286returns5 = behaviour442573906786450286.returns.ElementAtOrDefaultSafe(5);
			behaviour442573906786450286returns5.normal = new Vector3(0.7071069f, -0f, 0.7071067f);
			behaviour442573906786450286returns5.returnValue = 0.010360913f;
			behaviour442573906786450286.returns.Add(behaviour442573906786450286returns5);
			var behaviour442573906786450286returns6 = behaviour442573906786450286.returns.ElementAtOrDefaultSafe(6);
			behaviour442573906786450286returns6.normal = new Vector3(0.99999994f, -0f, -0.000000059604645f);
			behaviour442573906786450286returns6.returnValue = 0.039312672f;
			behaviour442573906786450286.returns.Add(behaviour442573906786450286returns6);
			var behaviour442573906786450286returns7 = behaviour442573906786450286.returns.ElementAtOrDefaultSafe(7);
			behaviour442573906786450286returns7.normal = new Vector3(0.7071066f, -0f, -0.707107f);
			behaviour442573906786450286returns7.returnValue = 0.02006913f;
			behaviour442573906786450286.returns.Add(behaviour442573906786450286returns7);
			var behaviour442573906786450286returns8 = behaviour442573906786450286.returns.ElementAtOrDefaultSafe(8);
			behaviour442573906786450286returns8.normal = new Vector3(-0f, 0.7071069f, -0.70710677f);
			behaviour442573906786450286returns8.returnValue = 0.09480216f;
			behaviour442573906786450286.returns.Add(behaviour442573906786450286returns8);
			var behaviour442573906786450286returns9 = behaviour442573906786450286.returns.ElementAtOrDefaultSafe(9);
			behaviour442573906786450286returns9.normal = new Vector3(-0f, 1.0000001f, 0.00000014751397f);
			behaviour442573906786450286returns9.returnValue = 0.23637128f;
			behaviour442573906786450286.returns.Add(behaviour442573906786450286returns9);
			var behaviour442573906786450286returns10 = behaviour442573906786450286.returns.ElementAtOrDefaultSafe(10);
			behaviour442573906786450286returns10.normal = new Vector3(-0f, 0.70710677f, 0.7071068f);
			behaviour442573906786450286returns10.returnValue = 0.04430033f;
			behaviour442573906786450286.returns.Add(behaviour442573906786450286returns10);
			var behaviour442573906786450286returns11 = behaviour442573906786450286.returns.ElementAtOrDefaultSafe(11);
			behaviour442573906786450286returns11.normal = new Vector3(-0f, -0.7071069f, 0.7071067f);
			behaviour442573906786450286returns11.returnValue = 0.03837302f;
			behaviour442573906786450286.returns.Add(behaviour442573906786450286returns11);
			var behaviour442573906786450286returns12 = behaviour442573906786450286.returns.ElementAtOrDefaultSafe(12);
			behaviour442573906786450286returns12.normal = new Vector3(-0f, -1f, -0.00000018966082f);
			behaviour442573906786450286returns12.returnValue = 0.23594964f;
			behaviour442573906786450286.returns.Add(behaviour442573906786450286returns12);
			var behaviour442573906786450286returns13 = behaviour442573906786450286.returns.ElementAtOrDefaultSafe(13);
			behaviour442573906786450286returns13.normal = new Vector3(-0f, -0.7071067f, -0.7071068f);
			behaviour442573906786450286returns13.returnValue = 0.07296312f;
			behaviour442573906786450286.returns.Add(behaviour442573906786450286returns13);
			var behaviour442573906786450286returns14 = behaviour442573906786450286.returns.ElementAtOrDefaultSafe(14);
			behaviour442573906786450286returns14.normal = new Vector3(-0.70710677f, -0.7071069f, -0f);
			behaviour442573906786450286returns14.returnValue = 0.08872227f;
			behaviour442573906786450286.returns.Add(behaviour442573906786450286returns14);
			var behaviour442573906786450286returns15 = behaviour442573906786450286.returns.ElementAtOrDefaultSafe(15);
			behaviour442573906786450286returns15.normal = new Vector3(0.70710677f, -0.7071068f, -0f);
			behaviour442573906786450286returns15.returnValue = 0.09132164f;
			behaviour442573906786450286.returns.Add(behaviour442573906786450286returns15);
			var behaviour442573906786450286returns16 = behaviour442573906786450286.returns.ElementAtOrDefaultSafe(16);
			behaviour442573906786450286returns16.normal = new Vector3(0.7071067f, 0.7071069f, -0f);
			behaviour442573906786450286returns16.returnValue = 0.11092063f;
			behaviour442573906786450286.returns.Add(behaviour442573906786450286returns16);
			var behaviour442573906786450286returns17 = behaviour442573906786450286.returns.ElementAtOrDefaultSafe(17);
			behaviour442573906786450286returns17.normal = new Vector3(-0.707107f, 0.7071066f, -0f);
			behaviour442573906786450286returns17.returnValue = 0.108485356f;
			behaviour442573906786450286.returns.Add(behaviour442573906786450286returns17);
			behaviour442573906786450286.size = 10.3865f;
			behaviour442573906786450286.overrideMultiplier = 1f;
			// Property "hcPlayerEntity" not found in MonoBehaviour 442573906786450286
			behaviour442573906786450286.enabled = true;
			
			
			// Setup for MonoBehaviour 5005441903793897913
			behaviour5005441903793897913.flareLife = 7f;
			behaviour5005441903793897913.flarePrefab = FlarePrefab.Create();
			behaviour5005441903793897913.ejectSpeed = 45f;
			behaviour5005441903793897913.count = 120;
			behaviour5005441903793897913.fireFlare = false;
			behaviour5005441903793897913.enabled = true;
			
			
			// Setup for MonoBehaviour 5005441903793897918
			behaviour5005441903793897918.effectiveness = 2f;
			behaviour5005441903793897918.effectDecay = 0.5f;
			behaviour5005441903793897918.count = 120;
			behaviour5005441903793897918.fireChaff = false;
			behaviour5005441903793897918.enabled = true;
			
			
			// Setup for GameObject 5005441903226408491
			gameObject5005441903226408491.SetActive(true);
			gameObject5005441903226408491.sourcePrefab = new AircraftPrefab();
			gameObject5005441903226408491.AddComponents(transform5005441903226408488, behaviour5005441903226408494, behaviour5005441903226408489);
			
			
			// Setup for Transform 5005441903226408488
			// Children: 
			transform5005441903226408488.parent = transform3738062474209517589;
			transform5005441903226408488.localPosition = new Vector3(0f, 0f, 0f);
			transform5005441903226408488.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform5005441903226408488.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for MonoBehaviour 5005441903226408494
			// Script with GUID b135e8b0dd26494448eca17fdc4c2196 not found for component 5005441903226408494
			
			// Setup for MonoBehaviour 5005441903226408489
			behaviour5005441903226408489.isMissile = false;
			behaviour5005441903226408489.isCountermeasure = false;
			behaviour5005441903226408489.heat = 0f;
			behaviour5005441903226408489.cooldownRate = 100f;
			behaviour5005441903226408489.enabled = true;
			
			
			// Setup for GameObject 5005441903954840346
			gameObject5005441903954840346.SetActive(true);
			gameObject5005441903954840346.sourcePrefab = new AircraftPrefab();
			gameObject5005441903954840346.AddComponents(transform5005441903954840347, behaviour5005441903954840345, behaviour5005441903954840344);
			
			
			// Setup for Transform 5005441903954840347
			// Children: 
			transform5005441903954840347.parent = transform3738062474209517589;
			transform5005441903954840347.localPosition = new Vector3(0f, 0f, 0f);
			transform5005441903954840347.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform5005441903954840347.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for MonoBehaviour 5005441903954840345
			// Script with GUID b135e8b0dd26494448eca17fdc4c2196 not found for component 5005441903954840345
			
			// Setup for MonoBehaviour 5005441903954840344
			behaviour5005441903954840344.isMissile = false;
			behaviour5005441903954840344.isCountermeasure = false;
			behaviour5005441903954840344.heat = 0f;
			behaviour5005441903954840344.cooldownRate = 100f;
			behaviour5005441903954840344.enabled = true;
			
			
			gameObject3738062474209517590.gameObjectFileIdMap[3738062472738147320] = gameObject3738062472738147320;
			gameObject3738062474209517590.componentFileIdMap[3738062472738147321] = transform3738062472738147321;
			gameObject3738062474209517590.gameObjectFileIdMap[3738062474184576475] = gameObject3738062474184576475;
			gameObject3738062474209517590.componentFileIdMap[3738062474184576472] = transform3738062474184576472;
			gameObject3738062474209517590.gameObjectFileIdMap[3738062474209517590] = gameObject3738062474209517590;
			gameObject3738062474209517590.componentFileIdMap[3738062474209517589] = transform3738062474209517589;
			gameObject3738062474209517590.componentFileIdMap[7210895779002417747] = behaviour7210895779002417747;
			gameObject3738062474209517590.componentFileIdMap[3738062474209517588] = behaviour3738062474209517588;
			gameObject3738062474209517590.componentFileIdMap[3738062474209517591] = behaviour3738062474209517591;
			gameObject3738062474209517590.componentFileIdMap[442573906786450286] = behaviour442573906786450286;
			gameObject3738062474209517590.componentFileIdMap[5005441903793897913] = behaviour5005441903793897913;
			gameObject3738062474209517590.componentFileIdMap[5005441903793897918] = behaviour5005441903793897918;
			gameObject3738062474209517590.gameObjectFileIdMap[5005441903226408491] = gameObject5005441903226408491;
			gameObject3738062474209517590.componentFileIdMap[5005441903226408488] = transform5005441903226408488;
			gameObject3738062474209517590.componentFileIdMap[5005441903226408494] = behaviour5005441903226408494;
			gameObject3738062474209517590.componentFileIdMap[5005441903226408489] = behaviour5005441903226408489;
			gameObject3738062474209517590.gameObjectFileIdMap[5005441903954840346] = gameObject5005441903954840346;
			gameObject3738062474209517590.componentFileIdMap[5005441903954840347] = transform5005441903954840347;
			gameObject3738062474209517590.componentFileIdMap[5005441903954840345] = behaviour5005441903954840345;
			gameObject3738062474209517590.componentFileIdMap[5005441903954840344] = behaviour5005441903954840344;
			
			return gameObject3738062474209517590;
		}

		public override GameObject CreateInstance() 
		{
			return Create();
		}
	}
}