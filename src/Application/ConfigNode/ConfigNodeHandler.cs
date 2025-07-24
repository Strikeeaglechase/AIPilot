using UnityGERunner;
using Coroutine;
﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityGERunner.UnityApplication
{
	
	public class ConfigNodeHandler
	{
	    private class Reader<T>
	    {
	        private List<T> data;
	        private int head = 0;
	        public Reader(List<T> data)
	        {
	            this.data = data;
	        }
	
	        public T Read()
	        {
	            return data[head++];
	        }
	
	        public bool Eof()
	        {
	            return head == data.Count;
	        }
	    }
	
	
	    public static Node Parse(List<string> lines)
	    {
	        var reader = new Reader<string>(lines);
	        return Parse(reader.Read(), reader);
	    }
	
	    private static Node Parse(string name, Reader<string> reader)
	    {
	
	        reader.Read(); // Skip opening {
	
	        var node = new Node(name);
	        while (!reader.Eof())
	        {
	            var next = reader.Read();
	            if (next == "Briefing") return node; // The parsing dosn't work for briefing stuff, so uh we don't parse
	            if (next == "}") return node;
	
	            if (next.Contains("="))
	            {
	                var valueName = next.Substring(0, next.IndexOf(" ="));
	                var value = ParseValue(next.Substring(next.IndexOf("= ") + 2));
	                // Logger.Info("[HSGE] " + $"{next.Substring(next.IndexOf("= ") + 2)} -> {value} ({value?.GetType()})");
	                node.SetValue(valueName, value);
	            }
	            else
	            {
	                // Logger.Info("[HSGE] " + $" Parse into {next} ({next.Length})");
	                node.AddNode(Parse(next, reader));
	            }
	        }
	
	        throw new Exception($"Unable to parse {name}, unexpected eof");
	    }
	
	    private static object ParseValue(string input)
	    {
	        if (input == "True") return true;
	        if (input == "False") return false;
	        if (input == "null") return null;
	        if (input == "") return "";
	        if (IsNumric(input)) return float.Parse(input);
	        if (input[0] == '(') return Vector(input);
	        if (input.Contains(";") && IsNumric(input.Split(';')[0])) return input.Split(';').Select(v => float.Parse(v));
	        if (input.Contains(";")) return input.Substring(0, input.Length - 1).Split(';');
	        return input;
	    }
	
	    private static Vector3 Vector(string input)
	    {
	        var values = input.Substring(1, input.Length - 2).Split(',').Select(v => v.Trim()).ToArray();
	        if (values.Length == 1) return new Vector3(float.Parse(values[0]), 0, 0);
	        if (values.Length == 2) return new Vector3(float.Parse(values[0]), float.Parse(values[1]), 0);
	        if (values.Length == 3) return new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
	
	        Logger.Error("[HSGE] " + $"Invalid number of arguments for vector parse: {input}");
	
	        return new Vector3(0, 0, 0);
	    }
	
	    private static bool IsNumric(string input)
	    {
	        var validNumbers = "1234567890.-E";
	        return input.All(c => validNumbers.Contains(c));
	    }
	}
	
	public class Node
	{
	    public string name;
	    private Dictionary<string, object> values = new Dictionary<string, object>();
	    private List<Node> nodes = new List<Node>();
	    public Node(string name)
	    {
	        this.name = name;
	    }
	
	    public Node AddNode(Node node)
	    {
	        nodes.Add(node);
	        return this;
	    }
	
	    public Node GetNode(string name)
	    {
	        return nodes.Find(n => n.name == name);
	    }
	
	    public List<Node> GetNodes(string name, bool recursive = true)
	    {
	        List<Node> matching = new List<Node>();
	        nodes.ForEach(sn =>
	        {
	            if (recursive) sn.GetNodes(name).ForEach(n => matching.Add(n));
	            if (sn.name == name) matching.Add(sn);
	        });
	
	        return matching;
	    }
	
	    public int GetInt(string name) => Convert.ToInt32(values[name]);
	    public string GetString(string name) => Convert.ToString(values[name]);
	    public bool GetBool(string name) => Convert.ToBoolean(values[name]);
	    public float GetFloat(string name) => Convert.ToSingle(values[name]);
	
	    public T GetValue<T>(string name)
	    {
	        return (T)values[name];
	    }
	
	    public bool TryGetValue<T>(string name, out T value)
	    {
	        try
	        {
	            if (values.ContainsKey(name))
	            {
	                value = (T)values[name];
	                return true;
	            }
	            value = default;
	            return false;
	        }
	        catch (Exception e)
	        {
	            Logger.Error("[HSGE] " + e.Message);
	            value = default;
	            return false;
	        }
	    }
	
	    public Node SetValue(string name, object value)
	    {
	        if (values.ContainsKey(name)) values[name] = value;
	        else values.Add(name, value);
	        return this;
	    }
	
	    public override string ToString()
	    {
	        return ToString("");
	    }
	
	    public string ToString(string pad)
	    {
	        var result = $"{pad}Node {name}";
	        foreach (var key in values.Keys)
	        {
	            result += $"\n{pad}  {key} = {values[key]}";
	        }
	
	        foreach (var node in nodes)
	        {
	            result += $"\n{pad}  {node.ToString(pad + "  ")}";
	        }
	
	        return result;
	    }
	}
	
}