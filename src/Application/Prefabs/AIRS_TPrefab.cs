
using UnityGERunner;
using Coroutine;
using UnityGERunner.UnityApplication;
using UnityGERunner.UnityApplication.ScriptableObjects;


namespace UnityGERunner.UnityApplication.Prefabs
{
	public class AIRS_TPrefab : Prefab
	{
		public static GameObject Create()
		{
			// Component initializations
			var gameObject6580821834537331402 = new GameObject("AIRS-T");
			var transform6580821834537331397 = new Transform();
			
			var behaviour7420062136455188480 = new Actor();
			var behaviour6580821834537331396 = new Missile();
			var behaviour6580821834537331403 = new SimpleDrag();
			var behaviour6018144878263016147 = new TerrainCollider();
			var gameObject6580821834557621782 = new GameObject("SeekerParent");
			var transform6580821834557621783 = new Transform();
			
			var gameObject6580821834721912257 = new GameObject("wing");
			var transform6580821834721912258 = new Transform();
			
			var behaviour6580821834721912259 = new OmniWing();
			var gameObject6580821835014368563 = new GameObject("Seeker");
			var transform6580821835014368556 = new Transform();
			
			var behaviour6580821835014368557 = new HeatSeeker();
			var gameObject6580821835107238351 = new GameObject("airst");
			var transform6580821835107238344 = new Transform();
			
			
			// Component setups// Setup for GameObject 6580821834537331402
			gameObject6580821834537331402.SetActive(true);
			gameObject6580821834537331402.sourcePrefab = new AIRS_TPrefab();
			gameObject6580821834537331402.AddComponents(transform6580821834537331397, behaviour7420062136455188480, behaviour6580821834537331396, behaviour6580821834537331403, behaviour6018144878263016147);
			
			
			// Setup for Transform 6580821834537331397
			// Children: 6580821835107238344, 6580821834721912258, 6580821834557621783
			transform6580821834537331397.localPosition = new Vector3(0f, 0f, 0f);
			transform6580821834537331397.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform6580821834537331397.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for MonoBehaviour 7420062136455188480
			behaviour7420062136455188480.team = (Team)2;
			behaviour7420062136455188480.enabled = true;
			
			
			// Setup for MonoBehaviour 6580821834537331396
			behaviour6580821834537331396.mass = 0.088f;
			behaviour6580821834537331396.angularDrag = 27f;
			behaviour6580821834537331396.guidanceMode = (MissileGuidanceMode)1;
			behaviour6580821834537331396.leadTimeMultiplier = 1f;
			behaviour6580821834537331396.minGuidanceSimSpeed = 200f;
			behaviour6580821834537331396.boostThrust = 20f;
			behaviour6580821834537331396.boostTime = 2.2f;
			behaviour6580821834537331396.cruiseThrust = 2f;
			behaviour6580821834537331396.cruiseTime = 6f;
			behaviour6580821834537331396.thrustDelay = 0f;
			behaviour6580821834537331396.initialKick = 10f;
			behaviour6580821834537331396.maxTorque = 11f;
			behaviour6580821834537331396.maxTorqueSpeed = 300f;
			behaviour6580821834537331396.torqueRampUpRate = 2f;
			behaviour6580821834537331396.minTorqueSpeed = 100f;
			behaviour6580821834537331396.torqueToPrograde = 0f;
			behaviour6580821834537331396.maxAoA = 50f;
			behaviour6580821834537331396.steerMult = 6f;
			behaviour6580821834537331396.steerIntegral = 0f;
			behaviour6580821834537331396.maxSteerIntegralAccum = 0f;
			behaviour6580821834537331396.maxLeadTime = 4f;
			behaviour6580821834537331396.squareInput = true;
			behaviour6580821834537331396.maxBallisticOffset = 10f;
			behaviour6580821834537331396.minBallisticCalcSpeed = 0f;
			behaviour6580821834537331396.warheadRadius = 15f;
			behaviour6580821834537331396.pitbullRange = 0f;
			behaviour6580821834537331396.aamSearchRadar = null;
			behaviour6580821834537331396.aamSearchFov = 0f;
			behaviour6580821834537331396.commandRadarTarget = null;
			behaviour6580821834537331396.commandRadar = null;
			behaviour6580821834537331396.heatSeeker = behaviour6580821835014368557;
			behaviour6580821834537331396.simpleDrag = behaviour6580821834537331403;
			behaviour6580821834537331396.debugMissile = false;
			behaviour6580821834537331396.limitMaxTorque = true;
			behaviour6580821834537331396.aeroTorqueMult = 1f;
			behaviour6580821834537331396.trailColor = new Color(1f, 1f, 1f, 1f);
			behaviour6580821834537331396.entityId = 0;
			behaviour6580821834537331396.weaponPath = "Weapons/Missiles/AIRS-T";
			behaviour6580821834537331396.hpIndex = 0;
			behaviour6580821834537331396.enabled = true;
			
			
			// Setup for MonoBehaviour 6580821834537331403
			behaviour6580821834537331403.area = 0.0003f;
			behaviour6580821834537331403.rb = null;
			behaviour6580821834537331403.enabled = false;
			
			
			// Setup for MonoBehaviour 6018144878263016147
			
			behaviour6018144878263016147.enabled = true;
			
			
			// Setup for GameObject 6580821834557621782
			gameObject6580821834557621782.SetActive(true);
			gameObject6580821834557621782.sourcePrefab = new AIRS_TPrefab();
			gameObject6580821834557621782.AddComponents(transform6580821834557621783);
			
			
			// Setup for Transform 6580821834557621783
			// Children: 6580821835014368556
			transform6580821834557621783.parent = transform6580821834537331397;
			transform6580821834557621783.localPosition = new Vector3(0f, 0f, 1.792f);
			transform6580821834557621783.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform6580821834557621783.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for GameObject 6580821834721912257
			gameObject6580821834721912257.SetActive(true);
			gameObject6580821834721912257.sourcePrefab = new AIRS_TPrefab();
			gameObject6580821834721912257.AddComponents(transform6580821834721912258, behaviour6580821834721912259);
			
			
			// Setup for Transform 6580821834721912258
			// Children: 
			transform6580821834721912258.parent = transform6580821834537331397;
			transform6580821834721912258.localPosition = new Vector3(0f, 0f, -0.5f);
			transform6580821834721912258.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform6580821834721912258.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for MonoBehaviour 6580821834721912259
			behaviour6580821834721912259.rb = null;
			behaviour6580821834721912259.aeroProfile = Wing_AeroSO.instance;
			behaviour6580821834721912259.liftCoefficient = 0.45f;
			behaviour6580821834721912259.dragCoefficient = 0.1f;
			behaviour6580821834721912259.liftArea = 0.04f;
			behaviour6580821834721912259.enabled = false;
			
			
			// Setup for GameObject 6580821835014368563
			gameObject6580821835014368563.SetActive(true);
			gameObject6580821835014368563.sourcePrefab = new AIRS_TPrefab();
			gameObject6580821835014368563.AddComponents(transform6580821835014368556, behaviour6580821835014368557);
			
			
			// Setup for Transform 6580821835014368556
			// Children: 
			transform6580821835014368556.parent = transform6580821834557621783;
			transform6580821835014368556.localPosition = new Vector3(0f, 0f, 0f);
			transform6580821835014368556.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform6580821835014368556.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for MonoBehaviour 6580821835014368557
			behaviour6580821835014368557.gimbalFOV = 190f;
			behaviour6580821835014368557.seekerFOV = 6f;
			behaviour6580821835014368557.seekerScanTrackRate = 10f;
			behaviour6580821835014368557.seekerTrackRate = 20f;
			behaviour6580821835014368557.sensitivity = 10000f;
			behaviour6580821835014368557.minThreshold = 900f;
			behaviour6580821835014368557.ccm = 2f;
			behaviour6580821835014368557.triggerUncaged = false;
			behaviour6580821835014368557.commandLookDir = new Vector3(0f, 0f, 0f);
			behaviour6580821835014368557.uncagedScanSpeed = 720f;
			behaviour6580821835014368557.uncagedScanFraction = 0.15f;
			behaviour6580821835014368557.targetPosition = new Vector3(0f, 0f, 0f);
			behaviour6580821835014368557.targetVelocity = new Vector3(0f, 0f, 0f);
			behaviour6580821835014368557.seekerLock = 0f;
			behaviour6580821835014368557.enabled = true;
			
			
			// Setup for GameObject 6580821835107238351
			gameObject6580821835107238351.SetActive(true);
			gameObject6580821835107238351.sourcePrefab = new AIRS_TPrefab();
			gameObject6580821835107238351.AddComponents(transform6580821835107238344);
			
			
			// Setup for Transform 6580821835107238344
			// Children: 
			transform6580821835107238344.parent = transform6580821834537331397;
			transform6580821835107238344.localPosition = new Vector3(0f, 0f, 0f);
			transform6580821835107238344.localRotation = new Quaternion(0.2705982f, 0.65328145f, 0.65328145f, 0.2705982f);
			transform6580821835107238344.scale = new Vector3(1f, 1f, 1f);
			
			
			gameObject6580821834537331402.gameObjectFileIdMap[6580821834537331402] = gameObject6580821834537331402;
			gameObject6580821834537331402.componentFileIdMap[6580821834537331397] = transform6580821834537331397;
			gameObject6580821834537331402.componentFileIdMap[7420062136455188480] = behaviour7420062136455188480;
			gameObject6580821834537331402.componentFileIdMap[6580821834537331396] = behaviour6580821834537331396;
			gameObject6580821834537331402.componentFileIdMap[6580821834537331403] = behaviour6580821834537331403;
			gameObject6580821834537331402.componentFileIdMap[6018144878263016147] = behaviour6018144878263016147;
			gameObject6580821834537331402.gameObjectFileIdMap[6580821834557621782] = gameObject6580821834557621782;
			gameObject6580821834537331402.componentFileIdMap[6580821834557621783] = transform6580821834557621783;
			gameObject6580821834537331402.gameObjectFileIdMap[6580821834721912257] = gameObject6580821834721912257;
			gameObject6580821834537331402.componentFileIdMap[6580821834721912258] = transform6580821834721912258;
			gameObject6580821834537331402.componentFileIdMap[6580821834721912259] = behaviour6580821834721912259;
			gameObject6580821834537331402.gameObjectFileIdMap[6580821835014368563] = gameObject6580821835014368563;
			gameObject6580821834537331402.componentFileIdMap[6580821835014368556] = transform6580821835014368556;
			gameObject6580821834537331402.componentFileIdMap[6580821835014368557] = behaviour6580821835014368557;
			gameObject6580821834537331402.gameObjectFileIdMap[6580821835107238351] = gameObject6580821835107238351;
			gameObject6580821834537331402.componentFileIdMap[6580821835107238344] = transform6580821835107238344;
			
			return gameObject6580821834537331402;
		}

		public override GameObject CreateInstance() 
		{
			return Create();
		}
	}
}