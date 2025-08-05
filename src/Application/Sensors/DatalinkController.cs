using UnityGERunner;
using Coroutine;
using System.Collections.Generic;
using System.Linq;

namespace UnityGERunner.UnityApplication
{
	
	public struct RadarDLData
	{
	    public StateTargetData data;
	    public int contributedBy;
	}
	
	public struct VisualDLData
	{
	    public VisuallySpottedTarget data;
	    public int contributedBy;
	}
	
	public struct FriendlyData
	{
	    public int id;
	    public NetVector position;
	    public NetVector velocity;
	}
	
	public struct DatalinkState
	{
	    public RadarDLData[] radar;
	    public VisualDLData[] visual;
	    public FriendlyData[] friendlies;
	}
	
	
	public class DatalinkController : MonoBehaviour
	{
	    public Team team;
	    private List<RadarDLData> radarDlData = new List<RadarDLData>();
	    private List<VisualDLData> visualDLData = new List<VisualDLData>();
	    private List<FriendlyData> friendlyData = new List<FriendlyData>();
	
	    public void Report(StateTargetData data, int reportedBy)
	    {
	        radarDlData.Add(new RadarDLData { data = data, contributedBy = reportedBy });
	    }
	
	    public void Report(VisuallySpottedTarget data, int reportedBy)
	    {
	        visualDLData.Add(new VisualDLData { data = data, contributedBy = reportedBy });
	    }
	
	    public void ReportMyself(int id, NetVector position, NetVector velocity)
	    {
	        friendlyData.Add(new FriendlyData { id = id, position = position, velocity = velocity });
	    }
	
	    public DatalinkState GetState()
	    {
	        return new DatalinkState
	        {
	            radar = radarDlData.ToArray(),
	            visual = visualDLData.ToArray(),
	            friendlies = friendlyData.ToArray()
	        };
	    }
	
	    public void Clear()
	    {
	        radarDlData.Clear();
	        visualDLData.Clear();
	        friendlyData.Clear();
	    }
	}
	
}