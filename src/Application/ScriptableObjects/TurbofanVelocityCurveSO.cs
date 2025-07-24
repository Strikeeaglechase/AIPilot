
using UnityGERunner;
using Coroutine;
using UnityGERunner.UnityApplication;
using UnityGERunner.UnityApplication.Prefabs;


namespace UnityGERunner.UnityApplication.ScriptableObjects
{
	public class TurbofanVelocityCurveSO : SOCurve
	{
		private static SOCurve _instance;
		public static SOCurve instance 
		{
			get {
				if (_instance == null) _instance = Create();
				return _instance;
			}
		}

		private static SOCurve Create()
		{
			var behaviour11400000 = new SOCurve();
			behaviour11400000.curve = new AnimationCurve(new Keyframe(0f, 1f, 0f, 0f), new Keyframe(269.41064f, 1.1750208f, 0.0022197238f, 0.0022197238f), new Keyframe(535.5579f, 2f, 0f, 0f), new Keyframe(943.25f, 0f, 0f, 0f));
			behaviour11400000.enabled = true;
			
			
			return behaviour11400000;
		}
	}
}
	