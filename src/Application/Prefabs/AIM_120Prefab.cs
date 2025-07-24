
using UnityGERunner;
using Coroutine;
using UnityGERunner.UnityApplication;
using UnityGERunner.UnityApplication.ScriptableObjects;


namespace UnityGERunner.UnityApplication.Prefabs
{
	public class AIM_120Prefab : Prefab
	{
		public static GameObject Create()
		{
			// Component initializations
			var gameObject8917532810680964955 = new GameObject("radarTf");
			var transform8917532810680964948 = new Transform();
			
			var gameObject8917532810710937216 = new GameObject("wing");
			var transform8917532810710937217 = new Transform();
			
			var behaviour8917532810710937218 = new OmniWing();
			var gameObject8917532811213071787 = new GameObject("Radar");
			var transform8917532811213071780 = new Transform();
			
			var behaviour8917532811213071781 = new Radar();
			var gameObject8917532811642993045 = new GameObject("missile");
			var transform8917532811642993046 = new Transform();
			
			var gameObject8917532811999056233 = new GameObject("AIM-120");
			var transform8917532811999056234 = new Transform();
			
			var behaviour575318572188242511 = new Actor();
			var behaviour8917532811999056229 = new Missile();
			var behaviour8917532811999056228 = new SimpleDrag();
			var behaviour8917532811999056235 = new RadarCrossSection();
			var behaviour7612473891945616326 = new TerrainCollider();
			
			// Component setups// Setup for GameObject 8917532810680964955
			gameObject8917532810680964955.SetActive(true);
			gameObject8917532810680964955.sourcePrefab = new AIM_120Prefab();
			gameObject8917532810680964955.AddComponents(transform8917532810680964948);
			
			
			// Setup for Transform 8917532810680964948
			// Children: 
			transform8917532810680964948.parent = transform8917532811213071780;
			transform8917532810680964948.localPosition = new Vector3(0f, 0f, 0f);
			transform8917532810680964948.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform8917532810680964948.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for GameObject 8917532810710937216
			gameObject8917532810710937216.SetActive(true);
			gameObject8917532810710937216.sourcePrefab = new AIM_120Prefab();
			gameObject8917532810710937216.AddComponents(transform8917532810710937217, behaviour8917532810710937218);
			
			
			// Setup for Transform 8917532810710937217
			// Children: 
			transform8917532810710937217.parent = transform8917532811999056234;
			transform8917532810710937217.localPosition = new Vector3(0f, 0f, -0.625f);
			transform8917532810710937217.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform8917532810710937217.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for MonoBehaviour 8917532810710937218
			behaviour8917532810710937218.rb = null;
			behaviour8917532810710937218.aeroProfile = Wing_AeroSO.instance;
			behaviour8917532810710937218.liftCoefficient = 0.39f;
			behaviour8917532810710937218.dragCoefficient = 0.05f;
			behaviour8917532810710937218.liftArea = 0.14f;
			behaviour8917532810710937218.enabled = false;
			
			
			// Setup for GameObject 8917532811213071787
			gameObject8917532811213071787.SetActive(true);
			gameObject8917532811213071787.sourcePrefab = new AIM_120Prefab();
			gameObject8917532811213071787.AddComponents(transform8917532811213071780, behaviour8917532811213071781);
			
			
			// Setup for Transform 8917532811213071780
			// Children: 8917532810680964948
			transform8917532811213071780.parent = transform8917532811999056234;
			transform8917532811213071780.localPosition = new Vector3(0f, 0f, 1.7412109f);
			transform8917532811213071780.localRotation = new Quaternion(1.7763568e-15f, -1.1368684e-13f, -3.8369308e-12f, 1f);
			transform8917532811213071780.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for MonoBehaviour 8917532811213071781
			behaviour8917532811213071781.rotationSpeed = 30f;
			behaviour8917532811213071781.rotationRange = 7f;
			behaviour8917532811213071781.verticalFov = 7f;
			behaviour8917532811213071781.lockFov = 100f;
			behaviour8917532811213071781.transmissionStrength = 5000f;
			behaviour8917532811213071781.receiverSensitivity = 500f;
			behaviour8917532811213071781.detectPersistTime = 0.5f;
			behaviour8917532811213071781.debugRadar = false;
			behaviour8917532811213071781.maxAzimuthOffset = -1f;
			behaviour8917532811213071781.maxElevationOffset = 30f;
			behaviour8917532811213071781.currentElevationAdjust = 0f;
			behaviour8917532811213071781.currentAzimuthAdjust = 0f;
			behaviour8917532811213071781.autolock = false;
			behaviour8917532811213071781.unlock = false;
			behaviour8917532811213071781.radarActorId = -1;
			behaviour8917532811213071781.radarFov = 120f;
			behaviour8917532811213071781.rotationTransform = transform8917532810680964948;
			behaviour8917532811213071781.elevationTf = null;
			behaviour8917532811213071781.azimuthTf = null;
			behaviour8917532811213071781.referenceForwardTf = transform8917532811999056234;
			behaviour8917532811213071781.twsedTargets = new();
			behaviour8917532811213071781.pdtTwsIdx = 0;
			behaviour8917532811213071781.initSttedTarget = null;
			behaviour8917532811213071781.enabled = false;
			
			
			// Setup for GameObject 8917532811642993045
			gameObject8917532811642993045.SetActive(true);
			gameObject8917532811642993045.sourcePrefab = new AIM_120Prefab();
			gameObject8917532811642993045.AddComponents(transform8917532811642993046);
			
			
			// Setup for Transform 8917532811642993046
			// Children: 
			transform8917532811642993046.parent = transform8917532811999056234;
			transform8917532811642993046.localPosition = new Vector3(0f, 0f, 0f);
			transform8917532811642993046.localRotation = new Quaternion(-0.7071068f, 0f, 0f, 0.7071068f);
			transform8917532811642993046.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for GameObject 8917532811999056233
			gameObject8917532811999056233.SetActive(true);
			gameObject8917532811999056233.sourcePrefab = new AIM_120Prefab();
			gameObject8917532811999056233.AddComponents(transform8917532811999056234, behaviour575318572188242511, behaviour8917532811999056229, behaviour8917532811999056228, behaviour8917532811999056235, behaviour7612473891945616326);
			
			
			// Setup for Transform 8917532811999056234
			// Children: 8917532811642993046, 8917532810710937217, 8917532811213071780
			transform8917532811999056234.localPosition = new Vector3(0f, 0f, 0f);
			transform8917532811999056234.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform8917532811999056234.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for MonoBehaviour 575318572188242511
			behaviour575318572188242511.team = (Team)2;
			behaviour575318572188242511.enabled = true;
			
			
			// Setup for MonoBehaviour 8917532811999056229
			behaviour8917532811999056229.mass = 0.152f;
			behaviour8917532811999056229.angularDrag = 10f;
			behaviour8917532811999056229.guidanceMode = (MissileGuidanceMode)0;
			behaviour8917532811999056229.leadTimeMultiplier = 1f;
			behaviour8917532811999056229.minGuidanceSimSpeed = 200f;
			behaviour8917532811999056229.boostThrust = 22f;
			behaviour8917532811999056229.boostTime = 3.8f;
			behaviour8917532811999056229.cruiseThrust = 5f;
			behaviour8917532811999056229.cruiseTime = 18f;
			behaviour8917532811999056229.thrustDelay = 0.5f;
			behaviour8917532811999056229.initialKick = 0f;
			behaviour8917532811999056229.maxTorque = 10f;
			behaviour8917532811999056229.maxTorqueSpeed = 525f;
			behaviour8917532811999056229.torqueRampUpRate = 0.4f;
			behaviour8917532811999056229.minTorqueSpeed = 100f;
			behaviour8917532811999056229.torqueToPrograde = 0f;
			behaviour8917532811999056229.maxAoA = 30f;
			behaviour8917532811999056229.steerMult = 5f;
			behaviour8917532811999056229.steerIntegral = 0f;
			behaviour8917532811999056229.maxSteerIntegralAccum = 0f;
			behaviour8917532811999056229.maxLeadTime = 15f;
			behaviour8917532811999056229.squareInput = false;
			behaviour8917532811999056229.maxBallisticOffset = 40f;
			behaviour8917532811999056229.minBallisticCalcSpeed = 0f;
			behaviour8917532811999056229.warheadRadius = 20f;
			behaviour8917532811999056229.pitbullRange = 12000f;
			behaviour8917532811999056229.aamSearchRadar = behaviour8917532811213071781;
			behaviour8917532811999056229.aamSearchFov = 173f;
			behaviour8917532811999056229.commandRadarTarget = null;
			behaviour8917532811999056229.commandRadar = null;
			behaviour8917532811999056229.heatSeeker = null;
			behaviour8917532811999056229.simpleDrag = behaviour8917532811999056228;
			behaviour8917532811999056229.debugMissile = false;
			behaviour8917532811999056229.limitMaxTorque = true;
			behaviour8917532811999056229.aeroTorqueMult = 1f;
			behaviour8917532811999056229.trailColor = new Color(1f, 1f, 1f, 1f);
			behaviour8917532811999056229.entityId = 0;
			behaviour8917532811999056229.weaponPath = "Weapons/Missiles/AIM-120";
			behaviour8917532811999056229.hpIndex = 0;
			behaviour8917532811999056229.enabled = true;
			
			
			// Setup for MonoBehaviour 8917532811999056228
			behaviour8917532811999056228.area = 0.0001f;
			behaviour8917532811999056228.rb = null;
			behaviour8917532811999056228.enabled = false;
			
			
			// Setup for MonoBehaviour 8917532811999056235
			behaviour8917532811999056235.referenceTf = transform8917532811999056234;
			behaviour8917532811999056235.returns = new(18);
			var behaviour8917532811999056235returns0 = behaviour8917532811999056235.returns.ElementAtOrDefaultSafe(0);
			behaviour8917532811999056235returns0.normal = new Vector3(-0f, -0f, -1f);
			behaviour8917532811999056235returns0.returnValue = 0.0013601262f;
			behaviour8917532811999056235.returns.Add(behaviour8917532811999056235returns0);
			var behaviour8917532811999056235returns1 = behaviour8917532811999056235.returns.ElementAtOrDefaultSafe(1);
			behaviour8917532811999056235returns1.normal = new Vector3(-0.7071068f, -0f, -0.7071067f);
			behaviour8917532811999056235returns1.returnValue = 0.008295479f;
			behaviour8917532811999056235.returns.Add(behaviour8917532811999056235returns1);
			var behaviour8917532811999056235returns2 = behaviour8917532811999056235.returns.ElementAtOrDefaultSafe(2);
			behaviour8917532811999056235returns2.normal = new Vector3(-0.99999994f, -0f, -0.000000059604645f);
			behaviour8917532811999056235returns2.returnValue = 0.024461519f;
			behaviour8917532811999056235.returns.Add(behaviour8917532811999056235returns2);
			var behaviour8917532811999056235returns3 = behaviour8917532811999056235.returns.ElementAtOrDefaultSafe(3);
			behaviour8917532811999056235returns3.normal = new Vector3(-0.70710677f, -0f, 0.7071067f);
			behaviour8917532811999056235returns3.returnValue = 0.0060864724f;
			behaviour8917532811999056235.returns.Add(behaviour8917532811999056235returns3);
			var behaviour8917532811999056235returns4 = behaviour8917532811999056235.returns.ElementAtOrDefaultSafe(4);
			behaviour8917532811999056235returns4.normal = new Vector3(0.00000008742278f, -0f, 1f);
			behaviour8917532811999056235returns4.returnValue = 0.000056367775f;
			behaviour8917532811999056235.returns.Add(behaviour8917532811999056235returns4);
			var behaviour8917532811999056235returns5 = behaviour8917532811999056235.returns.ElementAtOrDefaultSafe(5);
			behaviour8917532811999056235returns5.normal = new Vector3(0.7071069f, -0f, 0.7071067f);
			behaviour8917532811999056235returns5.returnValue = 0.00608653f;
			behaviour8917532811999056235.returns.Add(behaviour8917532811999056235returns5);
			var behaviour8917532811999056235returns6 = behaviour8917532811999056235.returns.ElementAtOrDefaultSafe(6);
			behaviour8917532811999056235returns6.normal = new Vector3(0.99999994f, -0f, -0.000000059604645f);
			behaviour8917532811999056235returns6.returnValue = 0.024461394f;
			behaviour8917532811999056235.returns.Add(behaviour8917532811999056235returns6);
			var behaviour8917532811999056235returns7 = behaviour8917532811999056235.returns.ElementAtOrDefaultSafe(7);
			behaviour8917532811999056235returns7.normal = new Vector3(0.7071066f, -0f, -0.707107f);
			behaviour8917532811999056235returns7.returnValue = 0.008295548f;
			behaviour8917532811999056235.returns.Add(behaviour8917532811999056235returns7);
			var behaviour8917532811999056235returns8 = behaviour8917532811999056235.returns.ElementAtOrDefaultSafe(8);
			behaviour8917532811999056235returns8.normal = new Vector3(-0f, 0.7071069f, -0.70710677f);
			behaviour8917532811999056235returns8.returnValue = 0.008270826f;
			behaviour8917532811999056235.returns.Add(behaviour8917532811999056235returns8);
			var behaviour8917532811999056235returns9 = behaviour8917532811999056235.returns.ElementAtOrDefaultSafe(9);
			behaviour8917532811999056235returns9.normal = new Vector3(-0f, 1.0000001f, 0.00000014751397f);
			behaviour8917532811999056235returns9.returnValue = 0.024451567f;
			behaviour8917532811999056235.returns.Add(behaviour8917532811999056235returns9);
			var behaviour8917532811999056235returns10 = behaviour8917532811999056235.returns.ElementAtOrDefaultSafe(10);
			behaviour8917532811999056235returns10.normal = new Vector3(-0f, 0.70710677f, 0.7071068f);
			behaviour8917532811999056235returns10.returnValue = 0.0060738395f;
			behaviour8917532811999056235.returns.Add(behaviour8917532811999056235returns10);
			var behaviour8917532811999056235returns11 = behaviour8917532811999056235.returns.ElementAtOrDefaultSafe(11);
			behaviour8917532811999056235returns11.normal = new Vector3(-0f, -0.7071069f, 0.7071067f);
			behaviour8917532811999056235returns11.returnValue = 0.0060208593f;
			behaviour8917532811999056235.returns.Add(behaviour8917532811999056235returns11);
			var behaviour8917532811999056235returns12 = behaviour8917532811999056235.returns.ElementAtOrDefaultSafe(12);
			behaviour8917532811999056235returns12.normal = new Vector3(-0f, -1f, -0.00000018966082f);
			behaviour8917532811999056235returns12.returnValue = 0.024121568f;
			behaviour8917532811999056235.returns.Add(behaviour8917532811999056235returns12);
			var behaviour8917532811999056235returns13 = behaviour8917532811999056235.returns.ElementAtOrDefaultSafe(13);
			behaviour8917532811999056235returns13.normal = new Vector3(-0f, -0.7071067f, -0.7071068f);
			behaviour8917532811999056235returns13.returnValue = 0.008157481f;
			behaviour8917532811999056235.returns.Add(behaviour8917532811999056235returns13);
			var behaviour8917532811999056235returns14 = behaviour8917532811999056235.returns.ElementAtOrDefaultSafe(14);
			behaviour8917532811999056235returns14.normal = new Vector3(-0.70710677f, -0.7071069f, -0f);
			behaviour8917532811999056235returns14.returnValue = 0.035740502f;
			behaviour8917532811999056235.returns.Add(behaviour8917532811999056235returns14);
			var behaviour8917532811999056235returns15 = behaviour8917532811999056235.returns.ElementAtOrDefaultSafe(15);
			behaviour8917532811999056235returns15.normal = new Vector3(0.70710677f, -0.7071068f, -0f);
			behaviour8917532811999056235returns15.returnValue = 0.03574044f;
			behaviour8917532811999056235.returns.Add(behaviour8917532811999056235returns15);
			var behaviour8917532811999056235returns16 = behaviour8917532811999056235.returns.ElementAtOrDefaultSafe(16);
			behaviour8917532811999056235returns16.normal = new Vector3(0.7071067f, 0.7071069f, -0f);
			behaviour8917532811999056235returns16.returnValue = 0.035527688f;
			behaviour8917532811999056235.returns.Add(behaviour8917532811999056235returns16);
			var behaviour8917532811999056235returns17 = behaviour8917532811999056235.returns.ElementAtOrDefaultSafe(17);
			behaviour8917532811999056235returns17.normal = new Vector3(-0.707107f, 0.7071066f, -0f);
			behaviour8917532811999056235returns17.returnValue = 0.035527684f;
			behaviour8917532811999056235.returns.Add(behaviour8917532811999056235returns17);
			behaviour8917532811999056235.size = 1.799883f;
			behaviour8917532811999056235.overrideMultiplier = 1f;
			behaviour8917532811999056235.enabled = true;
			
			
			// Setup for MonoBehaviour 7612473891945616326
			
			behaviour7612473891945616326.enabled = true;
			
			
			gameObject8917532811999056233.gameObjectFileIdMap[8917532810680964955] = gameObject8917532810680964955;
			gameObject8917532811999056233.componentFileIdMap[8917532810680964948] = transform8917532810680964948;
			gameObject8917532811999056233.gameObjectFileIdMap[8917532810710937216] = gameObject8917532810710937216;
			gameObject8917532811999056233.componentFileIdMap[8917532810710937217] = transform8917532810710937217;
			gameObject8917532811999056233.componentFileIdMap[8917532810710937218] = behaviour8917532810710937218;
			gameObject8917532811999056233.gameObjectFileIdMap[8917532811213071787] = gameObject8917532811213071787;
			gameObject8917532811999056233.componentFileIdMap[8917532811213071780] = transform8917532811213071780;
			gameObject8917532811999056233.componentFileIdMap[8917532811213071781] = behaviour8917532811213071781;
			gameObject8917532811999056233.gameObjectFileIdMap[8917532811642993045] = gameObject8917532811642993045;
			gameObject8917532811999056233.componentFileIdMap[8917532811642993046] = transform8917532811642993046;
			gameObject8917532811999056233.gameObjectFileIdMap[8917532811999056233] = gameObject8917532811999056233;
			gameObject8917532811999056233.componentFileIdMap[8917532811999056234] = transform8917532811999056234;
			gameObject8917532811999056233.componentFileIdMap[575318572188242511] = behaviour575318572188242511;
			gameObject8917532811999056233.componentFileIdMap[8917532811999056229] = behaviour8917532811999056229;
			gameObject8917532811999056233.componentFileIdMap[8917532811999056228] = behaviour8917532811999056228;
			gameObject8917532811999056233.componentFileIdMap[8917532811999056235] = behaviour8917532811999056235;
			gameObject8917532811999056233.componentFileIdMap[7612473891945616326] = behaviour7612473891945616326;
			
			return gameObject8917532811999056233;
		}

		public override GameObject CreateInstance() 
		{
			return Create();
		}
	}
}