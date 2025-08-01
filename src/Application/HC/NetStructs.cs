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
	
	    public static implicit operator Vector3(NetVector v) => v.vec3;
	    public static implicit operator NetVector(Vector3 v) => new NetVector(v);
	}
	
	public struct NetQuaternion
	{
	    public float x;
	    public float y;
	    public float z;
	    public float w;
	
	    public NetQuaternion(float x, float y, float z, float w)
	    {
	        this.x = x;
	        this.y = y;
	        this.z = z;
	        this.w = w;
	    }
	
	    public NetQuaternion(Quaternion q)
	    {
	        this.x = q.x;
	        this.y = q.y;
	        this.z = q.z;
	        this.w = q.w;
	    }
	
	
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
	
	    public static implicit operator Quaternion(NetQuaternion q) => q.quat;
	    public static implicit operator NetQuaternion(Quaternion q) => new NetQuaternion(q);
	}
	
	public struct NetColor
	{
	    public float r;
	    public float g;
	    public float b;
	    public float a;
	
	    public NetColor(float r, float g, float b, float a)
	    {
	        this.r = r;
	        this.g = g;
	        this.b = b;
	        this.a = a;
	    }
	
	    public NetColor(Color c)
	    {
	        this.r = c.r;
	        this.g = c.g;
	        this.b = c.b;
	        this.a = c.a;
	    }
	
	
	    [JsonIgnore]
	    public Color col { get { return new Color(r, g, b, a); } }
	
	    public void WriteBytes(MemoryStream stream)
	    {
	        stream.Write(BitConverter.GetBytes(r));
	        stream.Write(BitConverter.GetBytes(g));
	        stream.Write(BitConverter.GetBytes(b));
	        stream.Write(BitConverter.GetBytes(a));
	    }
	
	    public static implicit operator Color(NetColor q) => q.col;
	    public static implicit operator NetColor(Color q) => new NetColor(q);
	}
}