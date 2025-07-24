
using UnityGERunner;
using Coroutine;
using UnityGERunner.UnityApplication;
using UnityGERunner.UnityApplication.Prefabs;


namespace UnityGERunner.UnityApplication.ScriptableObjects
{
	public class Wing_AeroSO : Aerodynamics
	{
		private static Aerodynamics _instance;
		public static Aerodynamics instance 
		{
			get {
				if (_instance == null) _instance = Create();
				return _instance;
			}
		}

		private static Aerodynamics Create()
		{
			var behaviour11400000 = new Aerodynamics();
			behaviour11400000.liftCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 0f), new Keyframe(10.147114f, 0.657073f, 0.117135905f, 0.117135905f), new Keyframe(19.87395f, 1.6f, 0f, 0f), new Keyframe(25.926586f, 0.9872913f, -0.10027962f, -0.10027962f), new Keyframe(47.12334f, 0.19928709f, -0.01105581f, -0.01105581f), new Keyframe(90f, 0f, -0.002525132f, -0.002525132f));
			behaviour11400000.dragCurve = new AnimationCurve(new Keyframe(0f, 0.094851f, 0.000996615f, 0.000996615f), new Keyframe(10f, 0.13597842f, 0.009086366f, 0.009086366f), new Keyframe(20f, 0.2859784f, 0.025214357f, 0.025214357f), new Keyframe(24.320389f, 0.50500023f, 0.04416491f, 0.04416491f), new Keyframe(47.050488f, 1.245956f, 0.011091748f, 0.011091748f), new Keyframe(90f, 2.1607416f, 0f, 0f));
			behaviour11400000.buffetCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 0f), new Keyframe(5f, 0f, 0f, 0f), new Keyframe(11.282368f, 0.01412947f, 0.0042978507f, 0.0042978507f), new Keyframe(13.602881f, 0.10339088f, 0.04316064f, 0.04316064f), new Keyframe(20.847225f, 1.0044786f, 0.03274375f, 0.03274375f), new Keyframe(50f, 0.72789115f, -0.019399447f, -0.019399447f), new Keyframe(90f, 0f, -0.01819728f, -0.01819728f));
			behaviour11400000.buffetMagnitude = 0.001041177f;
			behaviour11400000.buffetTimeFactor = 0.00017f;
			behaviour11400000.perpLiftVector = false;
			behaviour11400000.mirroredCurves = true;
			behaviour11400000.enabled = true;
			
			
			return behaviour11400000;
		}
	}
}
	