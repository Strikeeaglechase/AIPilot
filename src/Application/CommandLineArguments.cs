using UnityGERunner;
using Coroutine;
﻿using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityGERunner.UnityApplication
{
	
	public class Options
	{
	    private static Options _instance;
	
	    public static Options instance
	    {
	        get
	        {
	            if (_instance != null) return _instance;
	            Logger.Info("[HSGE] " + string.Join(", ", Environment.GetCommandLineArgs()));
	            var res = Parser.Default.ParseArguments<Options>(Environment.GetCommandLineArgs());
	            var realErrors = res.Errors.Where(e => e.Tag != ErrorType.UnknownOptionError);
	
	            if (Environment.GetCommandLineArgs().Contains("--help"))
	            {
	                Environment.Exit(0);
	            }
	
	            if (realErrors.Count() == 0)
	            {
	                _instance = res.Value == null ? new Options() : res.Value;
	                return _instance;
	            }
	
	            foreach (var err in realErrors)
	            {
	                Logger.Info("[HSGE] " + err.Tag);
	                Logger.Info("[HSGE] " + err);
	            }
	            // Hard exit if we can
	#if HSGE
	            Environment.Exit(0);
	#endif
	
	            // Otherwise soft exit, which will likely cause issues
	            Logger.Error("[HSGE] " + $"CLI Parse error occurred, exiting");
	            // Application.Quit do not hard exit the application, so instead we return a default to attempt ot avoid a crash
	            GameEngine.instance.Quit();
	
	            _instance = new Options();
	            return _instance;
	        }
	    }
	
	    [Option("allied", HelpText = "Path to an AIPProvider DLL for allied team aircraft", Default = "")]
	    public string allied { get; set; } = "";
	
	    [Option("enemy", HelpText = "Path to an AIPProvider DLL for enemy team aircraft", Default = "")]
	    public string enemy { get; set; } = "";
	
	    [Option("debug-allied", HelpText = "Enable debugging for the allied team", Default = false)]
	    public bool debugAllied { get; set; } = false;
	
	    [Option("debug-enemy", HelpText = "Enable debugging for the enemy team", Default = false)]
	    public bool debugEnemy { get; set; } = false;
	
	    [Option("allied-count", HelpText = "Sets number of allied aircraft to spawn", Default = 1)]
	    public int alliedCount { get; set; } = 1;
	
	    [Option("enemy-count", HelpText = "Sets number of enemy aircraft to spawn", Default = 1)]
	    public int enemyCount { get; set; } = 1;
	
	    [Option("spawn-dist", HelpText = "Spawn distance between teams in meters", Default = 72000)]
	    public float spawnDist { get; set; } = 72000;
	
	    [Option("spawn-alt", HelpText = "Spawn altitude in meters", Default = 6000)]
	    public float spawnAlt { get; set; } = 6000;
	
	    [Option("max-time", HelpText = "Maximum simulation duration in seconds (sim time, not real time)", Default = 300)]
	    public float maxTime { get; set; } = 300;
	
	    [Option("no-map", HelpText = "Disable map loading", Default = false)]
	    public bool noMap { get; set; } = false;
	    [Option("map", HelpText = "Path to a directory containing the map to load", Default = "")]
	    public string map { get; set; } = "";
	    [Option("weapon-maxes", HelpText = "Sets limits to how many of each weapon type can be spawned. Format: \"WEAPON_NAME:COUNT,WEAPON_NAME:COUNT\"")]
	    public string weaponMaxes { get; set; } = "";
	
	    public Dictionary<string, int> WeaponCountLimits
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