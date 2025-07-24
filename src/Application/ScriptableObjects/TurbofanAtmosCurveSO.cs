
using UnityGERunner;
using Coroutine;
using UnityGERunner.UnityApplication;
using UnityGERunner.UnityApplication.Prefabs;


namespace UnityGERunner.UnityApplication.ScriptableObjects
{
	public class TurbofanAtmosCurveSO : SOCurve
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
			behaviour11400000.curve = new AnimationCurve(new Keyframe(0f, 0f, 2.3234453f, 2.3234453f), new Keyframe(0.2f, 0.4f, 1.5f, 1.5f), new Keyframe(0.6f, 0.8f, 0.75f, 0.75f), new Keyframe(1f, 1f, 0.21137698f, 0.21137698f));
			behaviour11400000.enabled = true;
			
			
			return behaviour11400000;
		}
	}
}
	