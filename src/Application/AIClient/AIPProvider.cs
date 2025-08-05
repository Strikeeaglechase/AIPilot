using UnityGERunner;
using Coroutine;
﻿using Recorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityGERunner.UnityApplication
{
	
	
	public abstract class IAIPProvider
	{
	    public bool outputEnabled = false;
	    public string __aipName = "";
	
	    public abstract SetupActions Start(SetupInfo info);
	    public abstract InboundState Update(OutboundState state);
	    public virtual void Stop() { }
	
	    public void Graph(string key, Vector3 value) { if (outputEnabled) GameRecorder.Graph(key, value); }
	    public void Graph(string key, NetVector value) { if (outputEnabled) GameRecorder.Graph(key, value); }
	    public void Graph(string key, float value) { if (outputEnabled) GameRecorder.Graph(key, value); }
	    public void Log(string message) { if (outputEnabled) GameRecorder.Log(__aipName, message); }
	    public void DebugShape(DebugLine line) { if (outputEnabled) GameRecorder.DebugShape(line); }
	    public void DebugShape(DebugSphere sphere) { if (outputEnabled) GameRecorder.DebugShape(sphere); }
	    public void RemoveDebugShape(int shapeId) { if (outputEnabled) GameRecorder.RemoveDebugShape(shapeId); }
	    public float TestingValue(string name, float defaultValue, float minValue, float maxValue, float step = 0.1f) { return outputEnabled ? GameRecorder.TestingValue(name, defaultValue, minValue, maxValue, step) : defaultValue; }
	    public bool Linecast(NetVector a, NetVector b, out NetVector hitPoint)
	    {
	        var res = Map.instance.SimpleLineCast(a.vec3, b.vec3, out var hpv3);
	        hitPoint = hpv3;
	        return res;
	    }
	    public float HeightAt(NetVector pt) { return Map.instance.GetHeightAtSubpoint(pt); }
	}
	
	public struct Kinematics
	{
	    public NetVector position;
	    public NetQuaternion rotation;
	    public NetVector velocity;
	}
	
	public struct OutboundState
	{
	    public Kinematics kinematics;
	    public RadarState radar;
	    public StateRWRContact[] rwrContacts;
	    public VisuallySpottedTarget[] visualTargets;
	    public IRWeaponState ir;
	    public DatalinkState datalink;
	
	    public string[] weapons;
	    public int selectedWeapon;
	
	    public int flareCount;
	    public int chaffCount;
	
	    public float fuel;
	    public float time;
	}
	
	public enum InboundAction
	{
	    RadarState,
	    RadarSTT,
	    RadarStopSTT,
	    RadarTWS,
	    RadarDropTWS,
	    RadarSetPDT,
	    RadarElevation,
	    RadarAzimuth,
	    RadarFov,
	    Flare,
	    Chaff,
	    ChaffFlare,
	    SelectHardpoint,
	    Fire,
	    SetUncage
	}
	
	public struct InboundState
	{
	    public NetVector pyr;
	    public float throttle;
	
	    public NetVector irLookDir;
	
	    public int[] events;
	}
	
	public struct SetupActions
	{
	    public string[] hardpoints;
	    public string name;
	    public float fuel;
	}
	
	public struct SetupInfo
	{
	    public Team team;
	    public int id;
	    public float spawnDist;
	    public NetVector mapCenterPoint;
	    public string mapId;
	    public string mapPath;
	    public int alliedSpawns;
	    public int enemySpawns;
	    public Dictionary<string, int> weaponRestrictions;
	}
}