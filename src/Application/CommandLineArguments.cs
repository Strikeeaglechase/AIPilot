using UnityGERunner;
using Coroutine;
﻿using CommandLine;
using CommandLine.Text;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityGERunner.UnityApplication
{
	
	
	class OptionHelpAttribute : Attribute
	{
	    public string helpText;
	    public OptionHelpAttribute(string helpText) { this.helpText = helpText; }
	}
	
	class Options
	{
	    private static Options _instance;
	
	    public static Options instance
	    {
	        get
	        {
	            if (_instance == null) LoadOptions();
	            return _instance;
	        }
	    }
	
	    public static void LoadOptions()
	    {
	        _instance = new Options();
	
	        var cli = Environment.GetCommandLineArgs();
	        if (cli.Length == 1 || cli[1] == "--help")
	        {
	            PrintHelp();
	#if HSGE
	            Environment.Exit(0);
	#endif
	            return;
	        }
	
	        var optionsFile = cli[1];
	        if (!File.Exists(optionsFile))
	        {
	            Logger.Error("[HSGE] " + $"Unable to resolve options file: {optionsFile}");
	#if HSGE
	            Environment.Exit(0);
	#endif
	        }
	
	        Logger.Info("[HSGE] " + $"Loading options from {optionsFile}");
	        var config = File.ReadAllText(optionsFile);
	        var jo = JObject.Parse(config);
	
	        var fields = _instance.GetType().GetFields();
	        foreach (var field in fields)
	        {
	            if (jo.ContainsKey(field.Name))
	            {
	                Logger.Info("[HSGE] " + $"Option set:     {field.Name} = {jo[field.Name]}");
	                if (jo[field.Name].Type == JTokenType.Array)
	                {
	                    var value = (JArray)jo[field.Name];
	                    var rawValue = value.Values().Select(v => ((JValue)v).Value).ToArray();
	
	                    switch (field.FieldType.Name)
	                    {
	                        case "String[]":
	                            var strArr = Array.ConvertAll<object, string>(rawValue, (obj) => obj?.ToString() ?? string.Empty);
	                            field.SetValue(_instance, strArr);
	                            break;
	                        case "UInt64[]":
	                            var ulongArr = Array.ConvertAll<object, UInt64>(rawValue, (obj) => (UInt64)(long)obj);
	                            field.SetValue(_instance, ulongArr);
	                            break;
	                        default:
	                            Logger.Error("[HSGE] " + $"Unhandled feild type {field.FieldType.Name} on option {field.Name}");
	                            return;
	                    }
	                }
	                else
	                {
	                    var value = (JValue)jo[field.Name];
	                    object rawValue = value.Value;
	
	                    if (value.Type == JTokenType.Integer) rawValue = (int)(long)value.Value;
	
	                    field.SetValue(_instance, rawValue);
	                }
	            }
	            else
	            {
	                Logger.Warn("[HSGE] " + $"Option default: {field.Name} = {field.GetValue(_instance)}");
	            }
	        }
	    }
	
	    private static void PrintHelp()
	    {
	        string helpResult = "AI Pilot\n  A tool to enable the creation, testing, and deployment of AI for VTOL VR\n\n";
	        helpResult += "Usage: AIPilot.exe [CONFIG_FILE]\nWhere CONFIG_FILE is a filepath to a json document containing any of the following options:\n\n";
	
	        var fields = _instance.GetType().GetFields();
	
	        var reqPad = fields.Select(f => f.Name.Length).Max();
	        foreach (var field in fields)
	        {
	            var attr = (OptionHelpAttribute)Attribute.GetCustomAttribute(field, typeof(OptionHelpAttribute));
	            var paramHelp = attr != null ? attr.helpText : "";
	
	            helpResult += " " + field.Name.PadRight(reqPad + 4) + " " + paramHelp + "\n";
	        }
	
	        Logger.Info("[HSGE] " + helpResult);
	    }
	
	    [OptionHelp("Path to an AIPProvider DLL for allied team aircraft")]
	    public string allied = "";
	
	    [OptionHelp("Path to an AIPProvider DLL for enemy team aircraft")]
	    public string enemy = "";
	
	    [OptionHelp("Enable debugging for the allied team")]
	    public bool debugAllied = false;
	
	    [OptionHelp("Enable debugging for the enemy team")]
	    public bool debugEnemy = false;
	
	    [OptionHelp("Sets number of allied aircraft to spawn")]
	    public int alliedCount = 1;
	
	    [OptionHelp("Sets number of enemy aircraft to spawn")]
	    public int enemyCount = 1;
	
	    [OptionHelp("Spawn distance between teams in meters")]
	    public float spawnDist = 72000;
	
	    [OptionHelp("Spawn altitude in meters")]
	    public float spawnAlt = 6000;
	
	    [OptionHelp("Maximum simulation duration in seconds (sim time, not real time)")]
	    public float maxTime = 300;
	
	    [OptionHelp("Disable map loading")]
	    public bool noMap = false;
	
	    [OptionHelp("Path to a directory containing the map to load")]
	    public string map = "";
	
	    [OptionHelp("Sets limits to how many of each weapon type can be spawned. Format: \"WEAPON_NAME:COUNT,WEAPON_NAME:COUNT\"")]
	    public string weaponMaxes = "";
	
	    [OptionHelp("List of arguments to pass into the SetupInfo call to the Allied AIP")]
	    public string[] alliedArgs = new string[0];
	
	    [OptionHelp("List of arguments to pass into the SetupInfo call to the Enemy AIP")]
	    public string[] enemyArgs = new string[0];
	
	    public static string Allied => instance.allied;
	    public static string Enemy => instance.enemy;
	    public static bool DebugAllied => instance.debugAllied;
	    public static bool DebugEnemy => instance.debugEnemy;
	    public static int AlliedCount => instance.alliedCount;
	    public static int EnemyCount => instance.enemyCount;
	    public static float SpawnDist => instance.spawnDist;
	    public static float SpawnAlt => instance.spawnAlt;
	    public static float MaxTime => instance.maxTime;
	    public static bool NoMap => instance.noMap;
	    public static string Map => instance.map;
	    public static string WeaponMaxes => instance.weaponMaxes;
	    public static string[] AlliedArgs => instance.alliedArgs;
	    public static string[] EnemyArgs => instance.enemyArgs;
	
	    public static Dictionary<string, int> WeaponCountLimits
	    {
	        get
	        {
	            Dictionary<string, int> weaponMaxes = new Dictionary<string, int>();
	            var wepOpts = Options.instance.weaponMaxes.Split(",");
	            foreach (var opt in wepOpts)
	            {
	                string[] parts = opt.Split(":");
	                if (parts.Count() != 2) continue;
	
	                weaponMaxes[parts[0]] = int.Parse(parts[1]);
	            }
	
	            return weaponMaxes;
	        }
	    }
	}
	
}