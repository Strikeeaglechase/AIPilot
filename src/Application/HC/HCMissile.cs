using UnityGERunner;
using Coroutine;
using System.Collections;
using System.Collections.Generic;

namespace UnityGERunner.UnityApplication
{
	
	public class HCMissile : HCEntityBase
	{
	    public Vector3 velocity = Vector3.zero;
	    public Vector3 acceleration = Vector3.zero;
	
	    public bool fired = false;
	    public Color trailColor = Color.white;
	    private Vector3 prevPosition;
	
	    private WeaponStats.WeaponInfo info;
	    public float rcs => info.rcs;
	
	    protected override void Start()
	    {
	        var weaponInfo = LocalFightController.instance.weaponStats.weaponPrefabs.Find(p => p.path == path);
	        if (weaponInfo != null)
	        {
	            info = weaponInfo;
	        }
	        else
	        {
	            Logger.Error("[HSGE] " + $"Unable to resolve weapon stats for {path} ({entityId})");
	        }
	    }
	
	    protected override void FixedUpdate()
	    {
	        velocity += acceleration * Time.fixedDeltaTime;
	        transform.position += velocity * Time.fixedDeltaTime;
	    }
	
	    protected override void Update()
	    {
	        if (!fired) return;
	
	        if ((transform.position - prevPosition).sqrMagnitude > 100 * 100)
	        {
	            prevPosition = transform.position;
	            return;
	        }
	
	        Debug.DrawLine(transform.position, prevPosition, trailColor, 10);
	        prevPosition = transform.position;
	    }
	}
	
}