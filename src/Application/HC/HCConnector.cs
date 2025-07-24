using UnityGERunner;
using Coroutine;
using System.Collections;
using System.Collections.Generic;

#if !HSGE
using NativeWebSocket;
#else
using WebSocketClient;
#endif
namespace UnityGERunner.UnityApplication
{
	
	using System.Threading;
	using System.Text;
	using Newtonsoft.Json.Linq;
	using Newtonsoft.Json;
	using System.Security.Cryptography;
	using Decompress.V5;
	using System;
	
	public class ClientId
	{
	    public string id;
	    public string type;
	}
	
	public class RPC
	{
	    public string className;
	    public string method;
	    public string id;
	    public object[] args;
	
	    private int curArgIdx = 0;
	
	    public T arg<T>(int index)
	    {
	        var selectedArg = args[index];
	        return (T)Convert.ChangeType(selectedArg, typeof(T));
	    }
	
	    public T arg<T>()
	    {
	        var selectedArg = args[curArgIdx++];
	        return (T)Convert.ChangeType(selectedArg, typeof(T));
	    }
	
	    public override string ToString()
	    {
	        return $"{className}.{method}({string.Join(", ", args)})";
	    }
	}
	
	public abstract class CommandPacket
	{
	    public string type;
	}
	
	public class ConfigureAIP : CommandPacket { }
	
	public class KinematicsPacket : CommandPacket
	{
	    public NetVector position;
	    public NetVector velocity;
	    public NetVector acceleration;
	    public NetVector rotation;
	}
	
	public class AIPData : KinematicsPacket
	{
	    public NetVector pyr;
	    public float throttle;
	}
	
	public class AIPMissileData : KinematicsPacket
	{
	    public int entityId;
	    public bool detonate;
	}
	
	public class SpawnAIPWeapon : CommandPacket
	{
	    public string path;
	    public int hpIndex;
	}
	
	public class RWRPing : CommandPacket
	{
	    public string rwrTargetOwnerId;
	    public int actorId;
	    public float signalStrength;
	    public float frequency;
	    public NetVector position;
	    public NetVector velocity;
	    public bool isLock;
	}
	
	public class AIPKillEntity : CommandPacket
	{
	    public int entityId;
	}
	
	public class UseCountermeasure : CommandPacket
	{
	    // 0=flare
	    // 1=chaff
	    // 2=both
	    public int cmsType;
	}
	
	public class WrappedOutboundPacket
	{
	    public enum OutType
	    {
	        RPC,
	        CommandPacket
	    }
	
	    public OutType type;
	    public object packet;
	}
	
	public interface IVehicleReadyNotificationHandler
	{
	    void OnVehicleReadyNotification();
	}
	
	public class HCConnector : MonoBehaviour
	{
	    private WebSocket client;
	    public string clientId;
	    public HCManager manager;
	
	    public bool doWsReconnect = false;
	
	    public static HCConnector instance
	    {
	        get;
	        private set;
	    }
	
	    protected override void Awake()
	    {
	        if (instance != null)
	        {
	            Logger.Error("[HSGE] " + $"Duplicate HCConnector created!");
	            Destroy(this);
	            return;
	        }
	
	        instance = this;
	    }
	
	    protected override void Start()
	    {
	        ConfigureWS();
	    }
	
	    private async void ConfigureWS()
	    {
	        if (client != null)
	        {
	            await client.Close();
	        }
	
	        Logger.Info("[HSGE] " + $"Setting up HC WS");
	        client = new WebSocket("ws://localhost:8010");
	
	        client.OnOpen += () =>
	        {
	            Logger.Info("[HSGE] " + $"WS Client opened!");
	            client.SendText("autosub");
	        };
	
	        client.OnClose += (closeCode) =>
	        {
	            Logger.Info("[HSGE] " + $"WS Client closed: {closeCode}");
	            if (doWsReconnect) CoroutineHandler.Start(ConfigureWSAfterDelay());
	        };
	
	        client.OnMessage += (message) =>
	        {
	            HandlerMessage(message);
	        };
	
	        await client.Connect();
	    }
	
	    private IEnumerator<Wait> ConfigureWSAfterDelay()
	    {
	        yield return new Wait(1f);
	
	        ConfigureWS();
	    }
	
	    private void HandlerMessage(byte[] bytes)
	    {
	        var isJsonMessage = bytes[0] == 123; // Check for initial '{'
	
	        if (isJsonMessage)
	        {
	            string message = Encoding.UTF8.GetString(bytes);
	            var jobj = JObject.Parse(message);
	            // Logger.Info("[HSGE] " + jobj.ContainsKey("type"));
	            // Logger.Info("[HSGE] " + jobj["type"].ToString());
	            if (jobj.ContainsKey("type") && jobj["type"].ToString() == "assignId")
	            {
	                var cid = JsonConvert.DeserializeObject<ClientId>(message);
	                // Logger.Info("[HSGE] " + $"Parsing CID: {cid}");
	                if (cid == null || cid.type != "assignId")
	                {
	                    Logger.Info("[HSGE] " + $"Invalid client ID message: {message}");
	                    return;
	                }
	
	                clientId = cid.id;
	                Logger.Info("[HSGE] " + $"Received clientId: {clientId}");
	
	                SendCommandPacket(new ConfigureAIP());
	
	                // var handlers = gameObject.GetComponentsInChildrenImplementing<IVehicleReadyNotificationHandler>();
	            }
	            else
	            {
	                var rpc = JsonConvert.DeserializeObject<RPC>(message);
	                if (rpc == null)
	                {
	                    Logger.Info("[HSGE] " + $"Unable to parse RPC: {message}");
	                    return;
	                }
	
	                manager.HandleRPC(rpc);
	            }
	        }
	        else
	        {
	            var decompressor = new DecompressorV5(bytes);
	            var rpcs = decompressor.DecompressRPCPackets();
	            foreach (var rpc in rpcs)
	            {
	                manager.HandleRPC(rpc);
	            }
	        }
	    }
	
	    protected override void Update()
	    {
	#if !UNITY_WEBGL || UNITY_EDITOR
	        client.DispatchMessageQueue();
	#endif
	    }
	
	    public void SendRPC(RPC rpc)
	    {
	#if DO_NETWORK
	        Send(new WrappedOutboundPacket { type = WrappedOutboundPacket.OutType.RPC, packet = rpc });
	#endif
	    }
	
	    public void SendCommandPacket(CommandPacket commandPacket)
	    {
	#if DO_NETWORK
	        commandPacket.type = commandPacket.GetType().Name;
	        Send(new WrappedOutboundPacket { type = WrappedOutboundPacket.OutType.CommandPacket, packet = commandPacket });
	#endif
	    }
	
	    private void Send(WrappedOutboundPacket wop)
	    {
	        if (clientId == null || clientId.Length == 0)
	        {
	            // Logger.Info("[HSGE] " + $"Not sending packet because clientId is empty. Message: {message}");
	            return;
	        }
	
	        var message = JsonConvert.SerializeObject(wop);
	        client.SendText(message);
	    }
	}
	
}