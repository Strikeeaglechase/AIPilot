
using UnityGERunner;
using Coroutine;
using UnityGERunner.UnityApplication;
using UnityGERunner.UnityApplication.Prefabs;


namespace UnityGERunner.UnityApplication.ScriptableObjects
{
	public class WingDragMachCurveZeroSO : SOCurve
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
			behaviour11400000.curve = new AnimationCurve(new Keyframe(0f, 1f, -0.07434126f, -0.07434126f), new Keyframe(0.7f, 1f, 0.076345526f, 0.076345526f), new Keyframe(1f, 2.0014796f, -0.09308129f, -0.09308129f), new Keyframe(1.8158038f, 1.217366f, -0.16727163f, -0.16727163f), new Keyframe(5.195239f, 1.0016707f, -0.09260967f, -0.09260967f));
			behaviour11400000.enabled = true;
			
			
			return behaviour11400000;
		}
	}
}
	