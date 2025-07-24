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
	
	    public abstract SetupActions Start(SetupInfo info);
	    public abstract InboundState Update(OutboundState state);
	    public virtual void Stop() { }
	
	    public void Graph(string key, Vector3 value) { if (outputEnabled) GameRecorder.Graph(key, value); }
	    public void Graph(string key, NetVector value) { if (outputEnabled) GameRecorder.Graph(key, value); }
	    public void Graph(string key, float value) { if (outputEnabled) GameRecorder.Graph(key, value); }
	    public void Log(string message) { if (outputEnabled) GameRecorder.Log(message); }
	    public void DebugShape(DebugLine line) { if (outputEnabled) GameRecorder.DebugShape(line); }
	    public void DebugShape(DebugSphere sphere) { if (outputEnabled) GameRecorder.DebugShape(sphere); }
	    public void RemoveDebugShape(int shapeId) { if (outputEnabled) GameRecorder.RemoveDebugShape(shapeId); }
	    public float TestingValue(string name, float defaultValue, float minValue, float maxValue, float step = 0.1f) { return outputEnabled ? GameRecorder.TestingValue(name, defaultValue, minValue, maxValue, step) : defaultValue; }
	
	}
	
	public struct Kinematics
	{
	    public NetVector position;
	    public NetQuaternion rotation;
	    public NetVector velocity;
	    public NetVector angularVelocity;
	}
	
	public struct OutboundState
	{
	    public Kinematics kinematics;
	    public RadarState radar;
	    public StateRWRContact[] rwrContacts;
	    public VisuallySpottedTarget[] visualTargets;
	    public IRWeaponState ir;
	    public string[] weapons;
	
	    public int flareCount;
	    public int chaffCount;
	
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
	}
	
	public struct SetupInfo
	{
	    public Team team;
	    public int id;
	    public float spawnDist;
	    public NetVector mapCenterPoint;
	}
}