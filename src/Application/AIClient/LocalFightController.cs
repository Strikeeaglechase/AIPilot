using UnityGERunner;
using Coroutine;
using Recorder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace UnityGERunner.UnityApplication
{
	
	struct EquipData
	{
	    public string weaponPath;
	    public int hpCount;
	    public int[] allowedHardpoints;
	
	    public static Dictionary<string, EquipData> equips = new Dictionary<string, EquipData>
	    {
	        { "HPEquips/AFighter/af_amraam", new EquipData { weaponPath = "Weapons/Missiles/AIM-120", hpCount = 1, allowedHardpoints = new int[] { 4, 5, 6, 7 } } },
	        { "HPEquips/AFighter/af_amraamRail", new EquipData { weaponPath = "Weapons/Missiles/AIM-120", hpCount = 1, allowedHardpoints = new int[] { 1, 2, 3, 8, 9, 10, 11, 12 } } },
	        { "HPEquips/AFighter/af_amraamRailx2", new EquipData { weaponPath = "Weapons/Missiles/AIM-120", hpCount = 2, allowedHardpoints = new int[] { 1, 10, 11, 12 } } },
	        { "HPEquips/AFighter/fa26_iris-t-x1", new EquipData { weaponPath = "Weapons/Missiles/AIRS-T", hpCount = 1, allowedHardpoints = new int[] { 1, 2, 3, 8, 9, 10, 11, 12 } } },
	        { "HPEquips/AFighter/fa26_iris-t-x2", new EquipData { weaponPath = "Weapons/Missiles/AIRS-T", hpCount = 2, allowedHardpoints = new int[] { 1, 10 } } },
	        { "HPEquips/AFighter/fa26_iris-t-x3", new EquipData { weaponPath = "Weapons/Missiles/AIRS-T", hpCount = 3, allowedHardpoints = new int[] { 1, 10 } } },
	    };
	}
	
	public class LocalFightController : MonoBehaviour
	{
	    public Transform spawnCenterPoint;
	    public float spawnDistance = 72000f;
	    public float spawnAltitude = 6000;
	    public bool doSetupSpawns = false;
	
	    public static LocalFightController instance { get; private set; }
	    public WeaponStats weaponStats;
	
	    public string teamAAIPilotImplementationPath = "";
	    public string teamBAIPilotImplementationPath = "";
	    private Dictionary<Team, Type> aipImplementationCtors = new Dictionary<Team, Type>();
	
	    private IAIPProvider aipProvider;
	
	    public List<AIClient> alliedClients;
	    public List<AIClient> enemyClients;
	    private List<AIClient> clients;
	
	    public GameObject clientPrefab;
	
	    private int nextId = 1;
	
	    public DatalinkController alliedDl;
	    public DatalinkController enemyDl;
	
	
	    protected override void Awake()
	    {
	        if (instance != null)
	        {
	            Logger.Error("[HSGE] " + $"Duplicate LocalFightController created!");
	            Destroy(this);
	            return;
	        }
	
	        instance = this;
	
	        spawnDistance = Options.instance.spawnDist;
	        spawnAltitude = Options.instance.spawnAlt;
	
	        teamAAIPilotImplementationPath = Options.instance.allied;
	        teamBAIPilotImplementationPath = Options.instance.enemy;
	
	        while (alliedClients.Count() < Options.instance.alliedCount)
	        {
	            var newGo = Instantiate(clientPrefab);
	            alliedClients.Add(newGo.GetComponent<AIClient>());
	        }
	
	        while (enemyClients.Count() < Options.instance.enemyCount)
	        {
	            var newGo = Instantiate(clientPrefab);
	            enemyClients.Add(newGo.GetComponent<AIClient>());
	        }
	
	        clients = alliedClients.Concat(enemyClients).ToList();
	
	        foreach (var client in clients)
	        {
	            client.entityId = nextId;
	            client.actorId = nextId;
	            client.unitId = nextId;
	            nextId++;
	
	            var actor = client.GetComponent<Actor>();
	            actor.team = alliedClients.Contains(client) ? Team.Allied : Team.Enemy;
	
	            client.datalink = actor.team == Team.Allied ? alliedDl : enemyDl;
	        }
	    }
	
	    protected override void Start()
	    {
	        if (doSetupSpawns) SetupSpawnLocations();
	
	#if HSGE
	        ConfigureAndStartAIPs();
	#endif
	
	        var handlers = gameObject.GetComponentsInSceneImplementing<IVehicleReadyNotificationHandler>();
	        foreach (var handler in handlers) handler.OnVehicleReadyNotification();
	    }
	
	    protected override void FixedUpdate()
	    {
	        foreach (var client in clients) client.ReportAllDlInfo();
	        foreach (var client in clients) client.ExecuteAIP();
	
	        alliedDl.Clear();
	        enemyDl.Clear();
	    }
	
	    private void ConfigureAndStartAIPs()
	    {
	        LoadAIPImplementation(teamAAIPilotImplementationPath, Team.Allied);
	        LoadAIPImplementation(teamBAIPilotImplementationPath, Team.Enemy);
	
	        foreach (var client in clients)
	        {
	            if (!aipImplementationCtors.ContainsKey(client.team))
	            {
	                Logger.Warn("[HSGE] " + $"Cannot create AIP for {client} on team {client.team} as no AIPProvider ctor exists");
	                continue;
	            }
	            Logger.Info("[HSGE] " + $"Loading AIP for {client} on team {client.team} from {aipImplementationCtors[client.team]}");
	
	            //var flag = "--debug-" + client.team.ToString();
	            //var debugEnabled = cli.HasArg(flag);
	            var debugEnabled = client.team == Team.Allied ? Options.instance.debugAllied : Options.instance.debugEnemy;
	            //Logger.Info("[HSGE] " + $"Checking for {flag} to enable debugging: {debugEnabled}");
	            client.ConfigureAIP(aipImplementationCtors[client.team], spawnDistance, spawnCenterPoint.position, debugEnabled, Options.instance.alliedCount, Options.instance.enemyCount);
	        }
	    }
	
	    private void LoadAIPImplementation(string path, Team team)
	    {
	        if (path == null || path == string.Empty || !File.Exists(path))
	        {
	            Logger.Warn("[HSGE] " + $"No AIP Implementation for team {team} found at path: {path}");
	            return;
	        }
	
	        var bytes = File.ReadAllBytes(path);
	        var assemb = Assembly.Load(bytes);
	        var types = assemb.GetTypes();
	
	        foreach (var type in types)
	        {
	            if (!typeof(IAIPProvider).IsAssignableFrom(type)) continue;
	
	            aipImplementationCtors.Add(team, type);
	            break;
	        }
	    }
	
	    private void SetupSpawnLocations()
	    {
	
	        var spawnAngleRad = UnityGERunner.Random.Range(0, 360) * Mathf.Deg2Rad;
	
	        var x = Mathf.Cos(spawnAngleRad) * spawnDistance;
	        var z = Mathf.Sin(spawnAngleRad) * spawnDistance;
	
	        var x2 = Mathf.Cos(spawnAngleRad + Mathf.PI) * spawnDistance;
	        var z2 = Mathf.Sin(spawnAngleRad + Mathf.PI) * spawnDistance;
	
	        var vec = new Vector3(x, spawnAltitude, z) + spawnCenterPoint.position;
	        var vec2 = new Vector3(x2, spawnAltitude, z2) + spawnCenterPoint.position;
	
	        var look = Quaternion.LookRotation(vec);
	
	        Logger.Info("[HSGE] " + $"Spawn1: {vec}, spawn2: {vec2}");
	
	        foreach (var alliedClient in alliedClients)
	        {
	            alliedClient.transform.position = vec;
	            alliedClient.transform.LookAt(vec2, Vector3.up);
	        }
	        foreach (var enemyClient in enemyClients)
	
	        {
	            enemyClient.transform.position = vec2;
	            enemyClient.transform.LookAt(vec, Vector3.up);
	        }
	    }
	
	    public void RequestWeaponSpawn(EquipManager equipManager, SpawnAIPWeapon spawnRequest)
	    {
	        if (!enabled) return;
	        if (!EquipData.equips.ContainsKey(spawnRequest.path))
	        {
	            Logger.Warn("[HSGE] " + $"Attempted to spawn {spawnRequest.path} on hp {spawnRequest.hpIndex} however no equip data for that hardpoint");
	            return;
	        }
	
	
	        var equipData = EquipData.equips[spawnRequest.path];
	
	        if (!equipData.allowedHardpoints.Contains(spawnRequest.hpIndex))
	        {
	            Logger.Warn("[HSGE] " + $"Not allowed hardpoint. Attempted to spawn {spawnRequest.path} onto {spawnRequest.hpIndex}, however only valid hardpoints are: {string.Join(", ", equipData.allowedHardpoints)}");
	            return;
	        }
	
	        for (int i = 0; i < equipData.hpCount; i++)
	        {
	            equipManager.HandleWeaponSpawned(equipData.weaponPath, nextId++, -1, i);
	        }
	    }
	
	    private int GetAliveCount(List<AIClient> list, AIClient minus)
	    {
	        return list.Where(aic => aic != null && aic != minus).Count();
	    }
	
	    public void HandleDamageRequest(AIClient damagedClient)
	    {
	        Destroy(damagedClient.gameObject);
	
	        var alliedCount = GetAliveCount(alliedClients, damagedClient);
	        var enemyCount = GetAliveCount(enemyClients, damagedClient);
	
	        if (alliedCount == 0 && enemyCount == 0) Win(Team.Unknown);
	        else if (alliedCount == 0) Win(Team.Enemy);
	        else if (enemyCount == 0) Win(Team.Allied);
	    }
	
	    private void Win(Team team)
	    {
	        GameRecorder.Event(new WinEvent(team));
	        GameRecorder.EndSimulation();
	    }
	}
	
}