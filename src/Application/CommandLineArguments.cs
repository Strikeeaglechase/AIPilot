using UnityGERunner;
using Coroutine;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace UnityGERunner.UnityApplication
{
	
	public class CommandLineArguments
	{
	    private string[] args;
	
	    public CommandLineArguments()
	    {
	        args = Environment.GetCommandLineArgs().Select(a => a.ToLower()).ToArray();
	    }
	
	    public float GetArg(string name, float defaultValue = 0)
	    {
	        var sArg = GetArgWithParam(name);
	        if (sArg == null) return defaultValue;
	        return float.Parse(sArg);
	    }
	
	    public bool GetArg(string name, bool defaultValue = false)
	    {
	        var sArg = GetArgWithParam(name);
	        if (sArg == null) return defaultValue;
	        return bool.Parse(sArg);
	    }
	
	    public string GetArg(string name, string defaultValue = "")
	    {
	        var sArg = GetArgWithParam(name);
	        if (sArg == null) return defaultValue;
	        return sArg;
	    }
	
	    private string GetArgWithParam(string arg)
	    {
	        var idx = Array.IndexOf(args, arg.ToLower());
	
	        if (idx == -1) return null;
	        if (idx + 1 >= args.Length) return null;
	
	        return args[idx + 1];
	    }
	
	    public bool HasArg(string arg)
	    {
	        return Array.IndexOf(args, arg.ToLower()) != -1;
	    }
	}
	
}