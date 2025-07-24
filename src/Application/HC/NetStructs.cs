using UnityGERunner;
using Coroutine;
﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace UnityGERunner.UnityApplication
{
	
	
	public struct NetVector
	{
	    public float x;
	    public float y;
	    public float z;
	
	    public NetVector(float x, float y, float z)
	    {
	        this.x = x;
	        this.y = y;
	        this.z = z;
	    }
	
	    public NetVector(Vector3 v)
	    {
	        this.x = v.x;
	        this.y = v.y;
	        this.z = v.z;
	    }
	
	    [JsonIgnore]
	    public Vector3 vec3 { get { return new Vector3(x, y, z); } }
	
	    public static NetVector From(Vector3 v)
	    {
	        return new NetVector { x = v.x, y = v.y, z = v.z };
	    }
	
	    public bool isZero()
	    {
	        return x == 0 && y == 0 && z == 0;
	    }
	
	    public void WriteBytes(MemoryStream stream)
	    {
	        stream.Write(BitConverter.GetBytes(x));
	        stream.Write(BitConverter.GetBytes(y));
	        stream.Write(BitConverter.GetBytes(z));
	    }
	}
	
	public struct NetQuaternion
	{
	    public float x;
	    public float y;
	    public float z;
	    public float w;
	
	    [JsonIgnore]
	    public Quaternion quat { get { return new Quaternion(x, y, z, w); } }
	    public static NetQuaternion From(Quaternion q)
	    {
	        return new NetQuaternion { x = q.x, y = q.y, z = q.z, w = q.w };
	    }
	
	    public void WriteBytes(MemoryStream stream)
	    {
	        stream.Write(BitConverter.GetBytes(x));
	        stream.Write(BitConverter.GetBytes(y));
	        stream.Write(BitConverter.GetBytes(z));
	        stream.Write(BitConverter.GetBytes(w));
	    }
	}
	
	public struct NetColor
	{
	    public float r;
	    public float g;
	    public float b;
	    public float a;
	
	    [JsonIgnore]
	    public Color color { get { return new Color(r, g, b, a); } }
	
	    public NetColor(Color c)
	    {
	        r = c.r;
	        g = c.g;
	        b = c.b;
	        a = c.a;
	    }
	
	    public void WriteBytes(MemoryStream stream)
	    {
	        stream.Write(BitConverter.GetBytes(r));
	        stream.Write(BitConverter.GetBytes(g));
	        stream.Write(BitConverter.GetBytes(b));
	        stream.Write(BitConverter.GetBytes(a));
	    }
	}
}