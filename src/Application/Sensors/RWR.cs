using UnityGERunner;
using Coroutine;

namespace UnityGERunner.UnityApplication
{
	
	using System.Collections.Generic;
	using System.Linq;
	
	
	public struct RWRContact
	{
	    public float detectedTime;
	    public Vector3 position;
	    public float signalStrength;
	    public float distance;
	    public float txPower;
	    public int actorId;
	    public bool isLock;
	    public Team team;
	    public bool isMissile;
	}
	
	public struct StateRWRContact
	{
	    public float detectedTime;
	    public float signalStrength;
	    public int actorId;
	    public bool isLock;
	    public float bearing;
	    public Team team;
	    public bool isMissile;
	
	    private const float angularPrecision = 10f;
	
	    public StateRWRContact(Vector3 position, RWRContact contact)
	    {
	        this.detectedTime = contact.detectedTime;
	        this.signalStrength = contact.signalStrength;
	        this.actorId = contact.actorId;
	        this.isLock = contact.isLock;
	
	        var distanceBin = Mathf.Round(contact.distance / 1000f) * 1000f;
	        var sigStrength = contact.txPower / (distanceBin * distanceBin);
	        this.signalStrength = sigStrength;
	
	        var relPos = (position - contact.position).normalized;
	        var planerRelPos = Vector3.ProjectOnPlane(relPos, Vector3.up).normalized;
	        var angle = Mathf.Atan2(planerRelPos.x, planerRelPos.z) * Mathf.Rad2Deg + 180;
	        var lowFidelAngl = Mathf.Round(angle / angularPrecision) * angularPrecision;
	
	        this.bearing = lowFidelAngl;
	        this.team = contact.team;
	        this.isMissile = contact.isMissile;
	    }
	}
	
	public class RWR : MonoBehaviour
	{
	    public List<RWRContact> contacts = new List<RWRContact>();
	
	    public float persistTime = 0.5f;
	    public GameObject fakePingOn;
	
	    protected override void Start()
	    {
	
	    }
	
	    protected override void FixedUpdate()
	    {
	        contacts.RemoveAll(c => Time.time - c.detectedTime > persistTime);
	    }
	
	    public StateRWRContact[] GetState()
	    {
	        return contacts.Select(ct => new StateRWRContact(transform.position, ct)).ToArray();
	    }
	
	    public void HandlePing(Actor actor, int actorId, float signalStrength, float txPower, float frequency, NetVector globalPos, NetVector velocity, bool isLock)
	    {
	        var pos = !globalPos.isZero() ? new Vector3(globalPos.x, globalPos.y, globalPos.z) : actor.transform.position;
	
	        var existingContact = contacts.Find(c => c.actorId == actorId);
	        if (existingContact.actorId == actorId)
	        {
	            existingContact.signalStrength = signalStrength;
	            existingContact.position = pos;
	            existingContact.detectedTime = Time.time;
	            existingContact.isLock = isLock;
	            existingContact.txPower = txPower;
	            existingContact.distance = (transform.position - pos).magnitude;
	        }
	        else
	        {
	            var contact = new RWRContact
	            {
	                actorId = actorId,
	                position = pos,
	                signalStrength = signalStrength,
	                detectedTime = Time.time,
	                isLock = isLock,
	                txPower = txPower,
	                distance = (transform.position - pos).magnitude,
	                team = actor.team,
	                isMissile = actor.isMissile
	            };
	
	            contacts.Add(contact);
	        }
	    }
	}
	
}