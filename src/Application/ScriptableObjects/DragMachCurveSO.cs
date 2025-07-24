
using UnityGERunner;
using Coroutine;
using UnityGERunner.UnityApplication;
using UnityGERunner.UnityApplication.Prefabs;


namespace UnityGERunner.UnityApplication.ScriptableObjects
{
	public class DragMachCurveSO : SOCurve
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
			behaviour11400000.curve = new AnimationCurve(new Keyframe(0f, 1f, 0f, 0f), new Keyframe(0.5093591f, 0.976165f, 0f, 0f), new Keyframe(1.0187182f, 1.333f, 1.5667598f, 1.5667598f), new Keyframe(1.4262054f, 1.833f, 0f, 0f), new Keyframe(2.5467956f, 1.1666666f, -0.4750811f, -0.4750811f), new Keyframe(5.093591f, 0.6666667f, 0f, 0f), new Keyframe(8.659105f, 0.8666667f, 0f, 0f));
			behaviour11400000.enabled = true;
			
			
			return behaviour11400000;
		}
	}
}
	