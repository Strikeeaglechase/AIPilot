
using UnityGERunner;
using Coroutine;
using UnityGERunner.UnityApplication;
using UnityGERunner.UnityApplication.ScriptableObjects;


namespace UnityGERunner.UnityApplication.Prefabs
{
	public class AIClientPrefab : Prefab
	{
		public static GameObject Create()
		{
			// Component initializations
			var gameObject839487484599632978 = new GameObject("RadarTF");
			var transform625517006047086931 = new Transform();
			
			var gameObject952510700638765515 = new GameObject("Radar");
			var transform8428105453912267144 = new Transform();
			
			var behaviour3424778467738142397 = new Radar();
			var gameObject1298932314699798860 = new GameObject("AIClient");
			var transform7867794619607844773 = new Transform();
			
			var behaviour5527500078195339178 = new AIClient();
			var behaviour7870003077685781942 = new Actor();
			var behaviour4892452918045270120 = new KinematicPlane();
			var behaviour2881661582516595245 = new ModuleEngine();
			var behaviour4951117924955119384 = new SimpleDrag();
			var behaviour4778999080055250872 = new FuelTank();
			var behaviour7216977451890674701 = new VisualSensor();
			var behaviour1295623533689872749 = new MassUpdater();
			var rigidBody1302538070612963282 = new Rigidbody();
			
			var behaviour1992800865061777412 = new MonoBehaviour();
			var behaviour1756186139330704731 = new MonoBehaviour();
			var behaviour1695996403598227547 = new EquipManager();
			var behaviour3008035590580529071 = new RWR();
			var behaviour7782491234487686768 = new IRCountermeasure();
			var behaviour530053364128008802 = new ChaffCountermeasure();
			var behaviour5915429790337292566 = new RadarCrossSection();
			var behaviour3333124485101339158 = new TerrainCollider();
			var behaviour1303385942055963986 = new HeatEmitter();
			var gameObject1947470783785861658 = new GameObject("BodyParent");
			var transform5224939229950705443 = new Transform();
			
			var gameObject2894268027866959634 = new GameObject("AzimuthTf");
			var transform6689688638159823634 = new Transform();
			
			var gameObject4138221863959355245 = new GameObject("Main Camera");
			var transform5342821723292130903 = new Transform();
			
			var gameObject4786542654354445235 = new GameObject("ElevationTf");
			var transform185035937876871797 = new Transform();
			
			var gameObject6624719835676504098 = new GameObject("Model");
			var transform4683888422984510 = new Transform();
			
			var gameObject7755923712953595197 = new GameObject("CoM");
			var transform4466250847641149532 = new Transform();
			
			var behaviour7832233741879277281 = new CenterOfMass();
			
			// Component setups// Setup for GameObject 839487484599632978
			gameObject839487484599632978.SetActive(true);
			gameObject839487484599632978.sourcePrefab = new AIClientPrefab();
			gameObject839487484599632978.AddComponents(transform625517006047086931);
			
			
			// Setup for Transform 625517006047086931
			// Children: 
			transform625517006047086931.parent = transform6689688638159823634;
			transform625517006047086931.localPosition = new Vector3(0f, 0f, 0f);
			transform625517006047086931.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform625517006047086931.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for GameObject 952510700638765515
			gameObject952510700638765515.SetActive(true);
			gameObject952510700638765515.sourcePrefab = new AIClientPrefab();
			gameObject952510700638765515.AddComponents(transform8428105453912267144, behaviour3424778467738142397);
			
			
			// Setup for Transform 8428105453912267144
			// Children: 185035937876871797
			transform8428105453912267144.parent = transform7867794619607844773;
			transform8428105453912267144.localPosition = new Vector3(0f, 0.249f, 8.394f);
			transform8428105453912267144.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform8428105453912267144.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for MonoBehaviour 3424778467738142397
			behaviour3424778467738142397.rotationSpeed = 120f;
			behaviour3424778467738142397.rotationRange = 120f;
			behaviour3424778467738142397.verticalFov = 50f;
			behaviour3424778467738142397.lockFov = 120f;
			behaviour3424778467738142397.transmissionStrength = 125000f;
			behaviour3424778467738142397.receiverSensitivity = 500f;
			behaviour3424778467738142397.detectPersistTime = 0.5f;
			behaviour3424778467738142397.debugRadar = true;
			behaviour3424778467738142397.maxAzimuthOffset = -1f;
			behaviour3424778467738142397.maxElevationOffset = 30f;
			behaviour3424778467738142397.currentElevationAdjust = 0f;
			behaviour3424778467738142397.currentAzimuthAdjust = 0f;
			behaviour3424778467738142397.autolock = false;
			behaviour3424778467738142397.unlock = false;
			behaviour3424778467738142397.radarActorId = -1;
			behaviour3424778467738142397.radarFov = 120f;
			behaviour3424778467738142397.rotationTransform = transform625517006047086931;
			behaviour3424778467738142397.elevationTf = transform185035937876871797;
			behaviour3424778467738142397.azimuthTf = transform6689688638159823634;
			behaviour3424778467738142397.referenceForwardTf = transform8428105453912267144;
			behaviour3424778467738142397.twsedTargets = new();
			behaviour3424778467738142397.pdtTwsIdx = 0;
			behaviour3424778467738142397.initSttedTarget = null;
			behaviour3424778467738142397.enabled = true;
			
			
			// Setup for GameObject 1298932314699798860
			gameObject1298932314699798860.SetActive(true);
			gameObject1298932314699798860.sourcePrefab = new AIClientPrefab();
			gameObject1298932314699798860.AddComponents(transform7867794619607844773, behaviour5527500078195339178, behaviour7870003077685781942, behaviour4892452918045270120, behaviour2881661582516595245, behaviour4951117924955119384, behaviour4778999080055250872, behaviour7216977451890674701, behaviour1295623533689872749, rigidBody1302538070612963282, behaviour1992800865061777412, behaviour1756186139330704731, behaviour1695996403598227547, behaviour3008035590580529071, behaviour7782491234487686768, behaviour530053364128008802, behaviour5915429790337292566, behaviour3333124485101339158, behaviour1303385942055963986);
			
			
			// Setup for Transform 7867794619607844773
			// Children: 5224939229950705443, 4466250847641149532, 5342821723292130903, 8428105453912267144
			transform7867794619607844773.localPosition = new Vector3(27159.504f, 6000.003f, 10312.4f);
			transform7867794619607844773.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform7867794619607844773.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for MonoBehaviour 5527500078195339178
			behaviour5527500078195339178.entityId = 0;
			behaviour5527500078195339178.unitId = 0;
			behaviour5527500078195339178.actorId = 0;
			behaviour5527500078195339178.physicalRadius = 12.5f;
			behaviour5527500078195339178.actor = behaviour7870003077685781942;
			behaviour5527500078195339178.manager = null;
			behaviour5527500078195339178.radar = behaviour3424778467738142397;
			behaviour5527500078195339178.rb = rigidBody1302538070612963282;
			behaviour5527500078195339178.equipManager = behaviour1695996403598227547;
			behaviour5527500078195339178.rwr = behaviour3008035590580529071;
			behaviour5527500078195339178.visualTargetFinder = behaviour7216977451890674701;
			behaviour5527500078195339178.kp = behaviour4892452918045270120;
			behaviour5527500078195339178.engine = behaviour2881661582516595245;
			behaviour5527500078195339178.fuelTank = behaviour4778999080055250872;
			behaviour5527500078195339178.irCountermeasure = behaviour7782491234487686768;
			behaviour5527500078195339178.chaffCountermeasure = behaviour530053364128008802;
			behaviour5527500078195339178.datalink = null;
			behaviour5527500078195339178.enabled = true;
			
			
			// Setup for MonoBehaviour 7870003077685781942
			behaviour7870003077685781942.team = (Team)2;
			behaviour7870003077685781942.aiClient = null;
			behaviour7870003077685781942.enabled = true;
			
			
			// Setup for MonoBehaviour 4892452918045270120
			behaviour4892452918045270120.maxAoAcurve = new AnimationCurve(new Keyframe(0f, 9.786778f, 0.36750066f, 0.36750066f), new Keyframe(57.579475f, 34.87851f, 0f, 0f), new Keyframe(145.83153f, 29.552364f, -0.14893767f, -0.14893767f), new Keyframe(330f, 16.802605f, -0.0130549325f, -0.0130549325f));
			behaviour4892452918045270120.maxGCurve = new AnimationCurve(new Keyframe(0f, 0f, 0.0034654606f, 0.0034654606f), new Keyframe(91.31576f, 2.9108834f, 0.0333786f, 0.0333786f), new Keyframe(155.68416f, 7.249114f, 0.038599603f, 0.038599603f), new Keyframe(219.63521f, 9.159793f, 0.052452087f, 0.052452087f), new Keyframe(341.9569f, 13.61431f, -0.00066784304f, -0.00066784304f));
			behaviour4892452918045270120.rollRateCurve = new AnimationCurve(new Keyframe(0f, 194.75371f, 0f, 0f), new Keyframe(138.3144f, 226.88397f, 0.76293945f, 0.76293945f), new Keyframe(330f, 338.52728f, 0f, 0f));
			behaviour4892452918045270120.lerpCurve = new AnimationCurve(new Keyframe(0f, 1.284168f, 0f, 0f), new Keyframe(58.494534f, 1.3646439f, 0.004172325f, 0.004172325f), new Keyframe(102.34461f, 4.0628643f, 0.036363784f, 0.036363784f), new Keyframe(191.09071f, 7.3152823f, 0.03521778f, 0.03521778f), new Keyframe(330f, 9.9484215f, 0f, 0f));
			behaviour4892452918045270120.engine = behaviour2881661582516595245;
			behaviour4892452918045270120.input = new Vector3(0f, 0f, 0f);
			behaviour4892452918045270120.brake = 0f;
			behaviour4892452918045270120.flaps = 0f;
			behaviour4892452918045270120.flapsMultiplier = 1.7f;
			behaviour4892452918045270120.wingSweep = 38f;
			behaviour4892452918045270120.pitchLerpMult = 0.8f;
			behaviour4892452918045270120.rollLerpMult = 1.5f;
			behaviour4892452918045270120.yawGMult = 0.1f;
			behaviour4892452918045270120.yawAoAMult = 0.85f;
			behaviour4892452918045270120.dragArea = 0.2945789f;
			behaviour4892452918045270120.brakeDrag = 0.17f;
			behaviour4892452918045270120.fixedPoint = new Vector3(0f, 0f, 0f);
			behaviour4892452918045270120.rb = null;
			behaviour4892452918045270120.paused = false;
			behaviour4892452918045270120.debug = false;
			behaviour4892452918045270120.enabled = true;
			
			
			// Setup for MonoBehaviour 2881661582516595245
			behaviour2881661582516595245.entity = null;
			behaviour2881661582516595245.throttle = 1f;
			behaviour2881661582516595245.abThrustMult = 1.22f;
			behaviour2881661582516595245.maxThrust = 250f;
			behaviour2881661582516595245.autoAbThreshold = 0.75f;
			behaviour2881661582516595245.speedCurve = TurbofanVelocityCurveSO.instance;
			behaviour2881661582516595245.atmosCurve = TurbofanAtmosCurveSO.instance;
			behaviour2881661582516595245.resultThrust = 0f;
			behaviour2881661582516595245.thrustHeatMult = 25f;
			behaviour2881661582516595245.abHeatAdd = 1f;
			behaviour2881661582516595245.heatEmitter = behaviour1303385942055963986;
			behaviour2881661582516595245.fuelTank = behaviour4778999080055250872;
			behaviour2881661582516595245.fuelDrain = 3f;
			behaviour2881661582516595245.abDrainMult = 3f;
			behaviour2881661582516595245.enabled = true;
			
			
			// Setup for MonoBehaviour 4951117924955119384
			behaviour4951117924955119384.area = 0f;
			behaviour4951117924955119384.rb = rigidBody1302538070612963282;
			behaviour4951117924955119384.kp = behaviour4892452918045270120;
			behaviour4951117924955119384.enabled = true;
			
			
			// Setup for MonoBehaviour 4778999080055250872
			behaviour4778999080055250872.maxFuel = 7100f;
			behaviour4778999080055250872.currentFuel = 7100f;
			behaviour4778999080055250872.fuelDensity = 0.00084f;
			behaviour4778999080055250872.aiClient = behaviour5527500078195339178;
			behaviour4778999080055250872.enabled = true;
			
			
			// Setup for MonoBehaviour 7216977451890674701
			behaviour7216977451890674701.ourActor = behaviour7870003077685781942;
			behaviour7216977451890674701.missileDetectRange = 5000f;
			behaviour7216977451890674701.aircraftDetectRange = 10000f;
			behaviour7216977451890674701.enabled = true;
			
			
			// Setup for MonoBehaviour 1295623533689872749
			behaviour1295623533689872749.baseMass = 20f;
			behaviour1295623533689872749.updateInterval = -1f;
			behaviour1295623533689872749.enabled = true;
			
			
			// Setup for RigidBody 1302538070612963282
			rigidBody1302538070612963282.mass = 1f;
			rigidBody1302538070612963282.drag = 0f;
			rigidBody1302538070612963282.angularDrag = 0.05f;
			rigidBody1302538070612963282.centerOfMass = new Vector3(0f, 0.02978526f, -3.4902344f);
			rigidBody1302538070612963282.inertiaTensor = new Vector3(1f, 1f, 1f);
			rigidBody1302538070612963282.useGravity = true;
			rigidBody1302538070612963282.isKinematic = true;
			
			
			// Setup for MonoBehaviour 1992800865061777412
			// Script with GUID 62899f850307741f2a39c98a8b639597 not found for component 1992800865061777412
			
			// Setup for MonoBehaviour 1756186139330704731
			// Script with GUID 54dafd9bcd024cf40b15354c03b0b893 not found for component 1756186139330704731
			
			// Setup for MonoBehaviour 1695996403598227547
			behaviour1695996403598227547.equips = new(16);
			behaviour1695996403598227547.equips.Add("");
			behaviour1695996403598227547.equips.Add("");
			behaviour1695996403598227547.equips.Add("");
			behaviour1695996403598227547.equips.Add("");
			behaviour1695996403598227547.equips.Add("");
			behaviour1695996403598227547.equips.Add("");
			behaviour1695996403598227547.equips.Add("");
			behaviour1695996403598227547.equips.Add("");
			behaviour1695996403598227547.equips.Add("");
			behaviour1695996403598227547.equips.Add("");
			behaviour1695996403598227547.equips.Add("");
			behaviour1695996403598227547.equips.Add("");
			behaviour1695996403598227547.equips.Add("");
			behaviour1695996403598227547.equips.Add("");
			behaviour1695996403598227547.equips.Add("");
			behaviour1695996403598227547.equips.Add("");
			behaviour1695996403598227547.weapons = new();
			behaviour1695996403598227547.weaponStats = WeaponStatsSO.instance;
			behaviour1695996403598227547.radar = behaviour3424778467738142397;
			behaviour1695996403598227547.equipDrag = behaviour4951117924955119384;
			behaviour1695996403598227547.selectedWeapon = 0;
			behaviour1695996403598227547.tryFire = false;
			behaviour1695996403598227547.hasGun = false;
			behaviour1695996403598227547.bulletCount = 800;
			behaviour1695996403598227547.bulletSpeed = 1100f;
			behaviour1695996403598227547.dispersion = 0.1f;
			behaviour1695996403598227547.lifetime = 3f;
			behaviour1695996403598227547.enabled = true;
			
			
			// Setup for MonoBehaviour 3008035590580529071
			behaviour3008035590580529071.persistTime = 0.5f;
			behaviour3008035590580529071.fakePingOn = null;
			behaviour3008035590580529071.enabled = true;
			
			
			// Setup for MonoBehaviour 7782491234487686768
			behaviour7782491234487686768.flareLife = 7f;
			behaviour7782491234487686768.flarePrefab = FlarePrefab.Create();
			behaviour7782491234487686768.ejectSpeed = 45f;
			behaviour7782491234487686768.count = 120;
			behaviour7782491234487686768.fireFlare = false;
			behaviour7782491234487686768.enabled = true;
			
			
			// Setup for MonoBehaviour 530053364128008802
			behaviour530053364128008802.effectiveness = 2f;
			behaviour530053364128008802.effectDecay = 0.5f;
			behaviour530053364128008802.count = 120;
			behaviour530053364128008802.fireChaff = false;
			behaviour530053364128008802.enabled = true;
			
			
			// Setup for MonoBehaviour 5915429790337292566
			behaviour5915429790337292566.referenceTf = transform7867794619607844773;
			behaviour5915429790337292566.returns = new(18);
			var behaviour5915429790337292566returns0 = behaviour5915429790337292566.returns.ElementAtOrDefaultSafe(0);
			behaviour5915429790337292566returns0.normal = new Vector3(-0f, -0f, -1f);
			behaviour5915429790337292566returns0.returnValue = 0.0030040068f;
			behaviour5915429790337292566.returns.Add(behaviour5915429790337292566returns0);
			var behaviour5915429790337292566returns1 = behaviour5915429790337292566.returns.ElementAtOrDefaultSafe(1);
			behaviour5915429790337292566returns1.normal = new Vector3(-0.7071068f, -0f, -0.7071067f);
			behaviour5915429790337292566returns1.returnValue = 0.020103576f;
			behaviour5915429790337292566.returns.Add(behaviour5915429790337292566returns1);
			var behaviour5915429790337292566returns2 = behaviour5915429790337292566.returns.ElementAtOrDefaultSafe(2);
			behaviour5915429790337292566returns2.normal = new Vector3(-0.99999994f, -0f, -0.000000059604645f);
			behaviour5915429790337292566returns2.returnValue = 0.039612994f;
			behaviour5915429790337292566.returns.Add(behaviour5915429790337292566returns2);
			var behaviour5915429790337292566returns3 = behaviour5915429790337292566.returns.ElementAtOrDefaultSafe(3);
			behaviour5915429790337292566returns3.normal = new Vector3(-0.70710677f, -0f, 0.7071067f);
			behaviour5915429790337292566returns3.returnValue = 0.010492419f;
			behaviour5915429790337292566.returns.Add(behaviour5915429790337292566returns3);
			var behaviour5915429790337292566returns4 = behaviour5915429790337292566.returns.ElementAtOrDefaultSafe(4);
			behaviour5915429790337292566returns4.normal = new Vector3(0.00000008742278f, -0f, 1f);
			behaviour5915429790337292566returns4.returnValue = 0.0014441356f;
			behaviour5915429790337292566.returns.Add(behaviour5915429790337292566returns4);
			var behaviour5915429790337292566returns5 = behaviour5915429790337292566.returns.ElementAtOrDefaultSafe(5);
			behaviour5915429790337292566returns5.normal = new Vector3(0.7071069f, -0f, 0.7071067f);
			behaviour5915429790337292566returns5.returnValue = 0.010360913f;
			behaviour5915429790337292566.returns.Add(behaviour5915429790337292566returns5);
			var behaviour5915429790337292566returns6 = behaviour5915429790337292566.returns.ElementAtOrDefaultSafe(6);
			behaviour5915429790337292566returns6.normal = new Vector3(0.99999994f, -0f, -0.000000059604645f);
			behaviour5915429790337292566returns6.returnValue = 0.039312672f;
			behaviour5915429790337292566.returns.Add(behaviour5915429790337292566returns6);
			var behaviour5915429790337292566returns7 = behaviour5915429790337292566.returns.ElementAtOrDefaultSafe(7);
			behaviour5915429790337292566returns7.normal = new Vector3(0.7071066f, -0f, -0.707107f);
			behaviour5915429790337292566returns7.returnValue = 0.02006913f;
			behaviour5915429790337292566.returns.Add(behaviour5915429790337292566returns7);
			var behaviour5915429790337292566returns8 = behaviour5915429790337292566.returns.ElementAtOrDefaultSafe(8);
			behaviour5915429790337292566returns8.normal = new Vector3(-0f, 0.7071069f, -0.70710677f);
			behaviour5915429790337292566returns8.returnValue = 0.09480216f;
			behaviour5915429790337292566.returns.Add(behaviour5915429790337292566returns8);
			var behaviour5915429790337292566returns9 = behaviour5915429790337292566.returns.ElementAtOrDefaultSafe(9);
			behaviour5915429790337292566returns9.normal = new Vector3(-0f, 1.0000001f, 0.00000014751397f);
			behaviour5915429790337292566returns9.returnValue = 0.23637128f;
			behaviour5915429790337292566.returns.Add(behaviour5915429790337292566returns9);
			var behaviour5915429790337292566returns10 = behaviour5915429790337292566.returns.ElementAtOrDefaultSafe(10);
			behaviour5915429790337292566returns10.normal = new Vector3(-0f, 0.70710677f, 0.7071068f);
			behaviour5915429790337292566returns10.returnValue = 0.04430033f;
			behaviour5915429790337292566.returns.Add(behaviour5915429790337292566returns10);
			var behaviour5915429790337292566returns11 = behaviour5915429790337292566.returns.ElementAtOrDefaultSafe(11);
			behaviour5915429790337292566returns11.normal = new Vector3(-0f, -0.7071069f, 0.7071067f);
			behaviour5915429790337292566returns11.returnValue = 0.03837302f;
			behaviour5915429790337292566.returns.Add(behaviour5915429790337292566returns11);
			var behaviour5915429790337292566returns12 = behaviour5915429790337292566.returns.ElementAtOrDefaultSafe(12);
			behaviour5915429790337292566returns12.normal = new Vector3(-0f, -1f, -0.00000018966082f);
			behaviour5915429790337292566returns12.returnValue = 0.23594964f;
			behaviour5915429790337292566.returns.Add(behaviour5915429790337292566returns12);
			var behaviour5915429790337292566returns13 = behaviour5915429790337292566.returns.ElementAtOrDefaultSafe(13);
			behaviour5915429790337292566returns13.normal = new Vector3(-0f, -0.7071067f, -0.7071068f);
			behaviour5915429790337292566returns13.returnValue = 0.07296312f;
			behaviour5915429790337292566.returns.Add(behaviour5915429790337292566returns13);
			var behaviour5915429790337292566returns14 = behaviour5915429790337292566.returns.ElementAtOrDefaultSafe(14);
			behaviour5915429790337292566returns14.normal = new Vector3(-0.70710677f, -0.7071069f, -0f);
			behaviour5915429790337292566returns14.returnValue = 0.08872227f;
			behaviour5915429790337292566.returns.Add(behaviour5915429790337292566returns14);
			var behaviour5915429790337292566returns15 = behaviour5915429790337292566.returns.ElementAtOrDefaultSafe(15);
			behaviour5915429790337292566returns15.normal = new Vector3(0.70710677f, -0.7071068f, -0f);
			behaviour5915429790337292566returns15.returnValue = 0.09132164f;
			behaviour5915429790337292566.returns.Add(behaviour5915429790337292566returns15);
			var behaviour5915429790337292566returns16 = behaviour5915429790337292566.returns.ElementAtOrDefaultSafe(16);
			behaviour5915429790337292566returns16.normal = new Vector3(0.7071067f, 0.7071069f, -0f);
			behaviour5915429790337292566returns16.returnValue = 0.11092063f;
			behaviour5915429790337292566.returns.Add(behaviour5915429790337292566returns16);
			var behaviour5915429790337292566returns17 = behaviour5915429790337292566.returns.ElementAtOrDefaultSafe(17);
			behaviour5915429790337292566returns17.normal = new Vector3(-0.707107f, 0.7071066f, -0f);
			behaviour5915429790337292566returns17.returnValue = 0.108485356f;
			behaviour5915429790337292566.returns.Add(behaviour5915429790337292566returns17);
			behaviour5915429790337292566.size = 10.3865f;
			behaviour5915429790337292566.overrideMultiplier = 1f;
			behaviour5915429790337292566.enabled = true;
			
			
			// Setup for MonoBehaviour 3333124485101339158
			
			behaviour3333124485101339158.enabled = true;
			
			
			// Setup for MonoBehaviour 1303385942055963986
			behaviour1303385942055963986.isMissile = false;
			behaviour1303385942055963986.isCountermeasure = false;
			behaviour1303385942055963986.heat = 0f;
			behaviour1303385942055963986.cooldownRate = 100f;
			behaviour1303385942055963986.enabled = true;
			
			
			// Setup for GameObject 1947470783785861658
			gameObject1947470783785861658.SetActive(true);
			gameObject1947470783785861658.sourcePrefab = new AIClientPrefab();
			gameObject1947470783785861658.AddComponents(transform5224939229950705443);
			
			
			// Setup for Transform 5224939229950705443
			// Children: 4683888422984510
			transform5224939229950705443.parent = transform7867794619607844773;
			transform5224939229950705443.localPosition = new Vector3(0f, 0.23f, -3.87f);
			transform5224939229950705443.localRotation = new Quaternion(0f, 1f, 0f, 0f);
			transform5224939229950705443.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for GameObject 2894268027866959634
			gameObject2894268027866959634.SetActive(true);
			gameObject2894268027866959634.sourcePrefab = new AIClientPrefab();
			gameObject2894268027866959634.AddComponents(transform6689688638159823634);
			
			
			// Setup for Transform 6689688638159823634
			// Children: 625517006047086931
			transform6689688638159823634.parent = transform185035937876871797;
			transform6689688638159823634.localPosition = new Vector3(0f, 0f, 0f);
			transform6689688638159823634.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform6689688638159823634.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for GameObject 4138221863959355245
			gameObject4138221863959355245.SetActive(true);
			gameObject4138221863959355245.sourcePrefab = new AIClientPrefab();
			gameObject4138221863959355245.AddComponents(transform5342821723292130903);
			
			
			// Setup for Transform 5342821723292130903
			// Children: 
			transform5342821723292130903.parent = transform7867794619607844773;
			transform5342821723292130903.localPosition = new Vector3(0f, 11.4f, -48.8f);
			transform5342821723292130903.localRotation = new Quaternion(-0.030538544f, 0f, 0f, 0.99953365f);
			transform5342821723292130903.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for GameObject 4786542654354445235
			gameObject4786542654354445235.SetActive(true);
			gameObject4786542654354445235.sourcePrefab = new AIClientPrefab();
			gameObject4786542654354445235.AddComponents(transform185035937876871797);
			
			
			// Setup for Transform 185035937876871797
			// Children: 6689688638159823634
			transform185035937876871797.parent = transform8428105453912267144;
			transform185035937876871797.localPosition = new Vector3(0f, 0f, 0f);
			transform185035937876871797.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform185035937876871797.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for GameObject 6624719835676504098
			gameObject6624719835676504098.SetActive(true);
			gameObject6624719835676504098.sourcePrefab = new AIClientPrefab();
			gameObject6624719835676504098.AddComponents(transform4683888422984510);
			
			
			// Setup for Transform 4683888422984510
			// Children: 
			transform4683888422984510.parent = transform5224939229950705443;
			transform4683888422984510.localPosition = new Vector3(0f, 0f, 0f);
			transform4683888422984510.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform4683888422984510.scale = new Vector3(2.379832f, 2.379832f, 2.379832f);
			
			
			// Setup for GameObject 7755923712953595197
			gameObject7755923712953595197.SetActive(true);
			gameObject7755923712953595197.sourcePrefab = new AIClientPrefab();
			gameObject7755923712953595197.AddComponents(transform4466250847641149532, behaviour7832233741879277281);
			
			
			// Setup for Transform 4466250847641149532
			// Children: 
			transform4466250847641149532.parent = transform7867794619607844773;
			transform4466250847641149532.localPosition = new Vector3(0f, 0.03f, -3.49f);
			transform4466250847641149532.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform4466250847641149532.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for MonoBehaviour 7832233741879277281
			
			behaviour7832233741879277281.enabled = true;
			
			
			gameObject1298932314699798860.gameObjectFileIdMap[839487484599632978] = gameObject839487484599632978;
			gameObject1298932314699798860.componentFileIdMap[625517006047086931] = transform625517006047086931;
			gameObject1298932314699798860.gameObjectFileIdMap[952510700638765515] = gameObject952510700638765515;
			gameObject1298932314699798860.componentFileIdMap[8428105453912267144] = transform8428105453912267144;
			gameObject1298932314699798860.componentFileIdMap[3424778467738142397] = behaviour3424778467738142397;
			gameObject1298932314699798860.gameObjectFileIdMap[1298932314699798860] = gameObject1298932314699798860;
			gameObject1298932314699798860.componentFileIdMap[7867794619607844773] = transform7867794619607844773;
			gameObject1298932314699798860.componentFileIdMap[5527500078195339178] = behaviour5527500078195339178;
			gameObject1298932314699798860.componentFileIdMap[7870003077685781942] = behaviour7870003077685781942;
			gameObject1298932314699798860.componentFileIdMap[4892452918045270120] = behaviour4892452918045270120;
			gameObject1298932314699798860.componentFileIdMap[2881661582516595245] = behaviour2881661582516595245;
			gameObject1298932314699798860.componentFileIdMap[4951117924955119384] = behaviour4951117924955119384;
			gameObject1298932314699798860.componentFileIdMap[4778999080055250872] = behaviour4778999080055250872;
			gameObject1298932314699798860.componentFileIdMap[7216977451890674701] = behaviour7216977451890674701;
			gameObject1298932314699798860.componentFileIdMap[1295623533689872749] = behaviour1295623533689872749;
			gameObject1298932314699798860.componentFileIdMap[1302538070612963282] = rigidBody1302538070612963282;
			gameObject1298932314699798860.componentFileIdMap[1992800865061777412] = behaviour1992800865061777412;
			gameObject1298932314699798860.componentFileIdMap[1756186139330704731] = behaviour1756186139330704731;
			gameObject1298932314699798860.componentFileIdMap[1695996403598227547] = behaviour1695996403598227547;
			gameObject1298932314699798860.componentFileIdMap[3008035590580529071] = behaviour3008035590580529071;
			gameObject1298932314699798860.componentFileIdMap[7782491234487686768] = behaviour7782491234487686768;
			gameObject1298932314699798860.componentFileIdMap[530053364128008802] = behaviour530053364128008802;
			gameObject1298932314699798860.componentFileIdMap[5915429790337292566] = behaviour5915429790337292566;
			gameObject1298932314699798860.componentFileIdMap[3333124485101339158] = behaviour3333124485101339158;
			gameObject1298932314699798860.componentFileIdMap[1303385942055963986] = behaviour1303385942055963986;
			gameObject1298932314699798860.gameObjectFileIdMap[1947470783785861658] = gameObject1947470783785861658;
			gameObject1298932314699798860.componentFileIdMap[5224939229950705443] = transform5224939229950705443;
			gameObject1298932314699798860.gameObjectFileIdMap[2894268027866959634] = gameObject2894268027866959634;
			gameObject1298932314699798860.componentFileIdMap[6689688638159823634] = transform6689688638159823634;
			gameObject1298932314699798860.gameObjectFileIdMap[4138221863959355245] = gameObject4138221863959355245;
			gameObject1298932314699798860.componentFileIdMap[5342821723292130903] = transform5342821723292130903;
			gameObject1298932314699798860.gameObjectFileIdMap[4786542654354445235] = gameObject4786542654354445235;
			gameObject1298932314699798860.componentFileIdMap[185035937876871797] = transform185035937876871797;
			gameObject1298932314699798860.gameObjectFileIdMap[6624719835676504098] = gameObject6624719835676504098;
			gameObject1298932314699798860.componentFileIdMap[4683888422984510] = transform4683888422984510;
			gameObject1298932314699798860.gameObjectFileIdMap[7755923712953595197] = gameObject7755923712953595197;
			gameObject1298932314699798860.componentFileIdMap[4466250847641149532] = transform4466250847641149532;
			gameObject1298932314699798860.componentFileIdMap[7832233741879277281] = behaviour7832233741879277281;
			
			return gameObject1298932314699798860;
		}

		public override GameObject CreateInstance() 
		{
			return Create();
		}
	}
}