
using UnityGERunner;
using Coroutine;
using UnityGERunner.UnityApplication;
using UnityGERunner.UnityApplication.ScriptableObjects;


namespace UnityGERunner.UnityApplication.Prefabs
{
	public class FlarePrefab : Prefab
	{
		public static GameObject Create()
		{
			// Component initializations
			var gameObject5640875105050760880 = new GameObject("Flare");
			var transform5640875105050760885 = new Transform();
			
			var behaviour5640875105050760886 = new CMFlare();
			var behaviour5640875105050760887 = new HeatEmitter();
			
			// Component setups// Setup for GameObject 5640875105050760880
			gameObject5640875105050760880.SetActive(true);
			gameObject5640875105050760880.sourcePrefab = new FlarePrefab();
			gameObject5640875105050760880.AddComponents(transform5640875105050760885, behaviour5640875105050760886, behaviour5640875105050760887);
			
			
			// Setup for Transform 5640875105050760885
			// Children: 
			transform5640875105050760885.localPosition = new Vector3(27182.102f, 1203.5891f, 10536.759f);
			transform5640875105050760885.localRotation = new Quaternion(0f, 0f, 0f, 1f);
			transform5640875105050760885.scale = new Vector3(1f, 1f, 1f);
			
			
			// Setup for MonoBehaviour 5640875105050760886
			behaviour5640875105050760886.heatEmission = 1400f;
			behaviour5640875105050760886.heatEmitter = behaviour5640875105050760887;
			behaviour5640875105050760886.gravFactor = 0.55f;
			behaviour5640875105050760886.velocity = new Vector3(0f, 0f, 0f);
			behaviour5640875105050760886.drag = 0.83f;
			behaviour5640875105050760886.flareLife = 7f;
			behaviour5640875105050760886.enabled = true;
			
			
			// Setup for MonoBehaviour 5640875105050760887
			behaviour5640875105050760887.isMissile = false;
			behaviour5640875105050760887.isCountermeasure = true;
			behaviour5640875105050760887.heat = 0f;
			behaviour5640875105050760887.cooldownRate = 100f;
			behaviour5640875105050760887.enabled = true;
			
			
			gameObject5640875105050760880.gameObjectFileIdMap[5640875105050760880] = gameObject5640875105050760880;
			gameObject5640875105050760880.componentFileIdMap[5640875105050760885] = transform5640875105050760885;
			gameObject5640875105050760880.componentFileIdMap[5640875105050760886] = behaviour5640875105050760886;
			gameObject5640875105050760880.componentFileIdMap[5640875105050760887] = behaviour5640875105050760887;
			
			return gameObject5640875105050760880;
		}

		public override GameObject CreateInstance() 
		{
			return Create();
		}
	}
}