using UnityGERunner;
using Coroutine;
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace UnityGERunner.UnityApplication
{
	
	public class AirbaseTerrainMod
	{
	    private Vector3 center = new Vector3(288.73f, 0, -122.06f);
	    private Vector3 extents = new Vector3(905.4f, 0, 1795.76f);
	
	    private float radius = 579.7f;
	    public Vector3 position;
	    public Quaternion rotation;
	
	    public AirbaseTerrainMod(Node node)
	    {
	        var type = node.GetValue<string>("prefab");
	        switch(type)
	        {
	            case "airbase1":
	                center = new Vector3(288.73f, 0, -122.06f);
	                extents = new Vector3(905.4f, 0, 1795.76f);
	                break;
	            case "airbase2":
	                center = new Vector3(72, 0, 26.42f);
	                extents = new Vector3(791.2f, 0, 1534.7f);
	                break;
	                
	            default:
	                Logger.Error("[HSGE] " + $"Unknown airbase type {type}");
	                return;
	        }
	
	        position = node.GetValue<Vector3>("globalPos");
	        rotation = Quaternion.Euler(node.GetValue<Vector3>("rotation"));
	    }
	
	    public void Apply(float[,] heights)
	    {
	        var quad = GetQuadPoints();
	
	        for (int x = 0; x < heights.GetLength(0); x++)
	        {
	            for (int z = 0; z < heights.GetLength(1); z++)
	            {
	                var worldPoint = new Vector2(x * Map.metersPerPixel, z * Map.metersPerPixel);
	                var distance = DistanceToQuad(worldPoint, quad);
	                if (distance < 0)
	                {
	                    heights[z, x] = position.y;
	                }
	                else
	                {
	                    float t = Mathf.Clamp01(distance / radius);
	                    heights[z, x] = Mathf.Lerp(position.y, heights[z, x], t);
	                }
	            }
	        }
	    }
	
	    private Vector2[] GetQuadPoints()
	    {
	        var a = position + Rotate(new Vector3(-extents.x / 2, 0, -extents.z / 2));
	        var b = position + Rotate(new Vector3(-extents.x / 2, 0, extents.z / 2));
	        var c = position + Rotate(new Vector3(extents.x / 2, 0, extents.z / 2));
	        var d = position + Rotate(new Vector3(extents.x / 2, 0, -extents.z / 2));
	
	        return new Vector3[] { a, b, c, d }.Select(v => new Vector2(v.x, v.z)).ToArray();
	    }
	
	    private Vector3 Rotate(Vector3 pt)
	    {
	        return center + (rotation * pt);
	    }
	
	    private float DistanceToQuad(Vector2 point, Vector2[] quad)
	    {
	        return DistanceToQuad(point, quad[0], quad[1], quad[2], quad[3]);
	    }
	    private float DistanceToQuad(Vector2 point, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
	    {
	        var lines = new Vector2[][] { new Vector2[] { p0, p1 }, new Vector2[] { p1, p2 }, new Vector2[] { p2, p3 }, new Vector2[] { p3, p0 } };
	
	        float minDistance = float.MaxValue;
	        bool allPerp = true;
	
	        foreach (var line in lines)
	        {
	
	            FindDistanceToSegment(point, line[0], line[1], out Vector2 closest, out bool isPerp);
	            var distance = (closest - point).magnitude;
	            if (distance < minDistance) minDistance = distance;
	            if (!isPerp) allPerp = false;
	        }
	
	        return minDistance * (allPerp ? -1 : 1);
	    }
	
	    private float FindDistanceToSegment(Vector2 pt, Vector2 p1, Vector2 p2, out Vector2 closest, out bool isPerp)
	    {
	        isPerp = false;
	        float dx = p2.x - p1.x;
	        float dy = p2.y - p1.y;
	        if ((dx == 0) && (dy == 0))
	        {
	            dx = pt.x - p1.x;
	            dy = pt.y - p1.y;
	            closest = p1;
	            return Mathf.Sqrt(dx * dx + dy * dy);
	        }
	
	        // Calculate the t that minimizes the distance.
	        float t = ((pt.x - p1.x) * dx + (pt.y - p1.y) * dy) /
	            (dx * dx + dy * dy);
	
	        // See if this represents one of the segment's
	        // end points or a point in the middle.
	        if (t < 0)
	        {
	            closest = new Vector2(p1.x, p1.y);
	            dx = pt.x - p1.x;
	            dy = pt.y - p1.y;
	        }
	        else if (t > 1)
	        {
	            closest = new Vector2(p2.x, p2.y);
	            dx = pt.x - p2.x;
	            dy = pt.y - p2.y;
	        }
	        else
	        {
	            closest = new Vector2(p1.x + t * dx, p1.y + t * dy);
	            dx = pt.x - p1.x + t * dx;
	            dy = pt.y - p1.y + t * dy;
	            isPerp = true;
	        }
	
	        return Mathf.Sqrt(dx * dx + dy * dy);
	    }
	}
}