using UnityGERunner;
using Coroutine;
using Recorder;
using System.Collections;
using System.Collections.Generic;

namespace UnityGERunner.UnityApplication
{
	
	public class CMFlare : MonoBehaviour
	{
	    public float heatEmission;
	    public HeatEmitter heatEmitter;
	    public float gravFactor;
	    public Vector3 velocity;
	    public float drag = 0.2f;
	    public float flareLife = 7f;
	    private float finalEmission;
	
	    protected override void Start()
	    {
	        CoroutineHandler.Start(LifeRoutine());
	    }
	
	    private IEnumerator<Wait> LifeRoutine()
	    {
	        finalEmission = heatEmission;
	        yield return new Wait(flareLife);
	        finalEmission = 0f;
	
	        Destroy(gameObject);
	    }
	
	    // Update is called once per frame
	    protected override void FixedUpdate()
	    {
	        velocity += Time.fixedDeltaTime * gravFactor * Physics.gravity;
	        velocity += Time.fixedDeltaTime * drag * -velocity;
	        heatEmitter.velocity = velocity;
	        heatEmitter.AddHeat(finalEmission * Time.fixedDeltaTime);
	        transform.position += velocity * Time.fixedDeltaTime;
	    }
	}
	
}