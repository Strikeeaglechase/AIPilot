using UnityGERunner;
using Coroutine;
using BigGustave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UnityGERunner.UnityApplication
{
	
	
	public class Map
	{
	    public const float maxHeight = 6000;
	    public const float minHeight = 80;
	
	    public const float metersPerPixel = 153.6f;
	
	    private static Map _instance;
	    public string mapPath = "";
	    public string mapId = "invalid";
	
	    private List<Png> heightmapImages = new List<Png>();
	
	
	    public float[,] heightmap;
	    private float[,] highestOfQuad;
	
	    public int width;
	    public int height;
	
	    public List<Vector3> airbases = new List<Vector3>();
	
	    public Node mapVtm;
	    public Node missionVts;
	
	    public int __iid = 0;
	
	
	    private List<AirbaseTerrainMod> terrainMods = new List<AirbaseTerrainMod>();
	
	    public static Map instance
	    {
	        get
	        {
	            if (_instance == null) _instance = new Map();
	            return _instance;
	        }
	    }
	
	    private Map()
	    {
	        if (Options.instance.noMap)
	        {
	            Logger.Info("[HSGE] " + $"No map flag, skipping map load");
	            return;
	        }
	
	        mapPath = GameEngine.dataPath + "/Resources/Map/";
	        if (Options.instance.map != string.Empty)
	        {
	            mapPath = Options.instance.map;
	        }
	        Logger.Info("[HSGE] " + $"Map load path: {Path.GetFullPath(mapPath)}");
	
	        __iid = new System.Random().Next(100000);
	
	        if (!LoadFromIndexed())
	        {
	            var singleHeightFile = TryLoadImage(mapPath + "height.png");
	            if (singleHeightFile == null) throw new Exception("Unable to load any heightmap files");
	            heightmapImages.Add(singleHeightFile);
	        }
	
	        Logger.Info("[HSGE] " + $"Loaded {heightmapImages.Count} heightmap images");
	
	        width = heightmapImages[0].Width;
	        height = heightmapImages[0].Height;
	        heightmap = new float[height, width];
	        highestOfQuad = new float[height, width];
	
	        LoadVTMFile();
	#if !HSGE
	        //LoadAirbases();
	#endif
	        ComputeTerrainHeights();
	    }
	
	    private Png TryLoadImage(string path)
	    {
	        Logger.Info("[HSGE] " + $"Checking to load {path}");
	        if (File.Exists(path)) return Png.Open(path);
	        return null;
	    }
	
	    private string TryLoadTextFile(string path)
	    {
	        if (File.Exists(path)) return File.ReadAllText(path);
	        return null;
	    }
	
	    private void LoadVTMFile()
	    {
	        var dirFiles = Directory.GetFiles(mapPath);
	
	        string[] vtmFileName = Directory.GetFiles(mapPath, "?*.vtm");
	        string[] vtsFileName = Directory.GetFiles(mapPath, "?*.vts");
	
	        if (vtmFileName.Length == 0)
	        {
	            Logger.Error("[HSGE] " + $"No VTM file found within {mapPath}");
	            return;
	        }
	
	        if (vtsFileName.Length == 0)
	        {
	            Logger.Error("[HSGE] " + $"No VTS file found within {mapPath}");
	            return;
	        }
	
	
	        mapVtm = ParseVTFile(vtmFileName[0]);
	        missionVts = ParseVTFile(vtsFileName[0]);
	
	        mapId = mapVtm.GetValue<string>("mapID");
	
	        Logger.Info("[HSGE] " + $"Loaded map and mission VT files!");
	    }
	
	    private Node ParseVTFile(string path)
	    {
	        Logger.Info("[HSGE] " + $"Loading VT file: {path}");
	        var file = TryLoadTextFile(path);
	        if (file == null) throw new Exception($"Unable to load file {path} for ParseVTFile");
	
	        var cleaned = file.Split('\n').Select(l => l.Trim()).ToList();
	        return ConfigNodeHandler.Parse(cleaned);
	    }
	
	    private void LoadAirbases()
	    {
	        var staticPrefabsNode = mapVtm.GetNode("StaticPrefabs");
	        var prefabs = staticPrefabsNode.GetNodes("StaticPrefab");
	        string[] airbasePrefabs = { "airbase1", "airbase2" };
	
	        foreach (var prefab in prefabs)
	        {
	            if (!airbasePrefabs.Contains(prefab.GetValue<string>("prefab"))) continue;
	            var tMod = new AirbaseTerrainMod(prefab);
	            terrainMods.Add(tMod);
	
	            GameObject airbaseMarker = new GameObject("Airbase");
	            airbaseMarker.transform.position = tMod.position;
	            airbaseMarker.transform.rotation = tMod.rotation;
	#if !HSGE
	            airbaseMarker.AddComponent<AirbaseBounds>();
	#endif
	            airbases.Add(tMod.position);
	        }
	    }
	
	    private bool LoadFromIndexed()
	    {
	        var loadedAtLeastOne = false;
	        for (int i = 0; i < 4; i++)
	        {
	            var image = TryLoadImage(mapPath + "height" + i + ".png");
	            if (image != null)
	            {
	                heightmapImages.Add(image);
	                loadedAtLeastOne = true;
	            }
	        }
	
	        return loadedAtLeastOne;
	    }
	
	    private void ComputeTerrainHeights()
	    {
	        foreach (var image in heightmapImages)
	        {
	            for (int y = 0; y < height; y++)
	            {
	                for (int x = 0; x < width; x++) heightmap[y, x] += image.GetPixel(x, (height - 1) - y).R;
	            }
	        }
	
	        Logger.Info("[HSGE] " + $"Loading height from {heightmapImages.Count} images");
	
	        // Set height correctly
	        for (int y = 0; y < height; y++)
	        {
	            for (int x = 0; x < width; x++)
	            {
	                float height = heightmap[y, x];
	                heightmap[y, x] = ((maxHeight + minHeight) / (float)(heightmapImages.Count * 255)) * height - minHeight;
	            }
	        }
	
	        // Apply terrain mods
	        foreach (var mod in terrainMods)
	        {
	            mod.Apply(heightmap);
	        }
	
	        // Compute heights of quad
	        for (int y = 0; y < height; y++)
	        {
	            for (int x = 0; x < width; x++)
	            {
	                var a = heightmap[y, x];
	                if (x + 1 == width || y + 1 == height)
	                {
	                    highestOfQuad[y, x] = a;
	                    continue;
	                }
	
	                var b = heightmap[y + 1, x];
	                var c = heightmap[y, x + 1];
	                var d = heightmap[y + 1, x + 1];
	
	                highestOfQuad[y, x] = Mathf.Max(a, b, c, d);
	            }
	        }
	    }
	
	    public float GetHeightAtPoint(Transform point)
	    {
	        var x = (int)(point.position.x / metersPerPixel);
	        var y = (int)(point.position.z / metersPerPixel);
	
	        if (x < 0 || y < 0 || y >= height || x >= width) return 0;
	
	        return heightmap[y, x];
	    }
	
	    public Vector3 GetPoint(int x, int y)
	    {
	        return new Vector3(x * metersPerPixel, heightmap[y, x], y * metersPerPixel);
	    }
	
	    public Vector3 GetPoint(float x, float y)
	    {
	        return new Vector3(x * metersPerPixel, heightmap[(int)y, (int)x], y * metersPerPixel);
	    }
	
	    private float TriArea(Vector2 a, Vector2 b, Vector2 c)
	    {
	        return Mathf.Abs(a.x * (b.y - c.y) + b.x * (c.y - a.y) + c.x * (a.y - b.y)) / 2;
	    }
	
	    private bool PointInTriangle(Vector2 p, Vector2 a, Vector2 b, Vector2 c)
	    {
	        var area = TriArea(a, b, c);
	        var area1 = TriArea(p, b, c);
	        var area2 = TriArea(a, p, c);
	        var area3 = TriArea(a, b, p);
	
	        return Mathf.Abs(area - (area1 + area2 + area3)) < 0.01f;
	    }
	
	    private Vector2 XZ(Vector3 inp) => new Vector2(inp.x, inp.z);
	
	    private bool PlaneRaycast(Vector3 origin, Vector3 direction, Vector3 normal, float planeDist, out float enter)
	    {
	        float num = Vector3.Dot(direction, normal);
	        float num2 = 0f - Vector3.Dot(origin, normal) - planeDist;
	        if (Mathf.Approximately(num, 0f))
	        {
	            enter = 0f;
	            return false;
	        }
	
	        enter = num2 / num;
	        return enter > 0f;
	    }
	
	    public float GetHeightAtSubpoint(Vector3 position)
	    {
	        var xLower = Mathf.Floor(position.x / metersPerPixel);
	        var yLower = Mathf.Floor(position.z / metersPerPixel);
	
	        var xUpper = Mathf.Ceil(position.x / metersPerPixel);
	        var yUpper = Mathf.Ceil(position.z / metersPerPixel);
	
	        if (xLower < 0 || yLower < 0 || yLower >= height || xLower >= width) return 0;
	        if (xUpper < 0 || yUpper < 0 || yUpper >= height || xUpper >= width) return 0;
	
	        Vector2 point = new Vector2(position.x, position.z);
	
	        Vector3 cornerA = GetPoint(xLower, yLower);
	        Vector3 cornerB = GetPoint(xUpper, yLower);
	        Vector3 cornerC = GetPoint(xLower, yUpper);
	        Vector3 cornerD = GetPoint(xUpper, yUpper);
	
	        var isInUpperTri = PointInTriangle(point, XZ(cornerA), XZ(cornerB), XZ(cornerD));
	        var isInLowerTri = PointInTriangle(point, XZ(cornerA), XZ(cornerD), XZ(cornerC));
	
	        Vector3 p1;
	        Vector3 p2;
	        Vector3 p3;
	        if (isInUpperTri)
	        {
	            p1 = cornerA;
	            p2 = cornerD;
	            p3 = cornerB;
	        }
	        else
	        {
	            p1 = cornerA;
	            p2 = cornerC;
	            p3 = cornerD;
	        }
	
	        var a = p2 - p1;
	        var b = p3 - p1;
	        var normal = Vector3.Normalize(Vector3.Cross(a, b));
	        float planeDist = 0f - Vector3.Dot(normal, p1);
	
	        if (PlaneRaycast(position, Vector3.down, normal, planeDist, out float dist))
	        {
	            return (position + Vector3.down * dist).y;
	        }
	
	        if (PlaneRaycast(position, Vector3.up, normal, planeDist, out float dist2))
	        {
	            return (position + Vector3.up * dist2).y;
	        }
	        return 0f;
	    }
	
	    public bool SimpleLineCast(Vector3 pt1, Vector3 pt2, out Vector3 hit, bool debug = false)
	    {
	        float stepSize = 100f;
	        Vector3 stepVec = (pt2 - pt1).normalized * stepSize;
	        float distToTravel = (pt1 - pt2).magnitude;
	        float distTraveled = 0;
	
	        while (distTraveled < distToTravel)
	        {
	            var prevPt = pt1;
	            if ((pt1 - pt2).sqrMagnitude < stepSize * stepSize)
	            {
	                pt1 = pt2;
	            }
	            else
	            {
	                pt1 += stepVec;
	            }
	            distTraveled += 100f;
	
	            if (debug)
	            {
	                Gizmos.color = Color.gray;
	                Gizmos.DrawLine(pt1, prevPt);
	            }
	
	            var cellX = Mathf.FloorToInt(pt1.x / metersPerPixel);
	            var cellY = Mathf.FloorToInt(pt1.z / metersPerPixel);
	
	            if (cellX < 0 || cellY < 0 || cellX >= width || cellY >= height)
	            {
	                hit = Vector3.zero;
	                return false;
	            }
	
	            var highestPointInQuad = highestOfQuad[cellY, cellX];
	            if (highestPointInQuad > pt1.y)
	            {
	                if (debug) Gizmos.color = Color.blue;
	
	                var subStepCount = 10;
	                for (int i = 0; i < subStepCount; i++)
	                {
	                    var subPt = prevPt + (stepVec / subStepCount) * i;
	                    // Gizmos.DrawSphere(subPt, 1f);
	
	                    bool breakAfterCheck = false;
	                    if ((subPt - pt2).sqrMagnitude < (stepSize / 10) * (stepSize / 10))
	                    {
	                        subPt = pt2;
	                        breakAfterCheck = true;
	                    }
	
	                    var subHighestPointInQuad = GetHeightAtSubpoint(subPt);
	                    if (subHighestPointInQuad > subPt.y)
	                    {
	                        subPt.y = subHighestPointInQuad;
	                        if (debug)
	                        {
	                            Gizmos.color = Color.red;
	                            Gizmos.DrawSphere(subPt, 1f);
	                        }
	
	                        hit = subPt;
	                        return true;
	                    }
	
	                    if (breakAfterCheck)
	                    {
	                        hit = Vector3.zero;
	                        return false;
	                    }
	                }
	
	                if (debug) Gizmos.color = Color.yellow;
	            }
	            else
	            {
	                if (debug) Gizmos.color = Color.green;
	            }
	
	            if (debug) Gizmos.DrawSphere(pt1, 2.5f);
	        }
	
	        hit = Vector3.zero;
	        return false;
	    }
	
	    public void Linecast(Vector3 pt1, Vector3 pt2, out Vector3 hitPoint)
	    {
	        Vector2 startPoint = XZ(pt1) / metersPerPixel;
	        Vector2 endPoint = XZ(pt2) / metersPerPixel;
	        float yslope = (pt2.y - pt1.y) / (pt2 - pt1).magnitude;
	
	        Vector3 dir3 = (pt2 - pt1).normalized;
	        Vector2 dir = XZ(dir3).normalized;
	        Vector2 unitStepSize = new Vector2(Mathf.Sqrt(1 + (dir.y / dir.x) * (dir.y / dir.x)), Mathf.Sqrt(1 + (dir.x / dir.y) * (dir.x / dir.y)));
	        Vector2 currentChunk = new Vector2(Mathf.Floor(startPoint.x), Mathf.Floor(startPoint.y));
	        Vector2 lengths;
	        Vector2 step;
	
	        if (dir.x < 0)
	        {
	            step.x = -1;
	            lengths.x = (startPoint.x - currentChunk.x) * unitStepSize.x;
	        }
	        else
	        {
	            step.x = 1;
	            lengths.x = ((currentChunk.x + 1) - startPoint.x) * unitStepSize.x;
	        }
	
	        if (dir.y < 0)
	        {
	            step.y = -1;
	            lengths.y = (startPoint.y - currentChunk.y) * unitStepSize.y;
	        }
	        else
	        {
	            step.y = 1;
	            lengths.y = ((currentChunk.y + 1) - startPoint.y) * unitStepSize.y;
	        }
	
	
	        float distance = 0;
	        float distMax = (startPoint - endPoint).magnitude;
	        bool hitGround = false;
	
	        while (distance < distMax)
	        {
	            if (lengths.x < lengths.y)
	            {
	                currentChunk.x += step.x;
	                distance = lengths.x;
	                lengths.x += unitStepSize.x;
	            }
	            else
	            {
	                currentChunk.y += step.y;
	                distance = lengths.y;
	                lengths.y += unitStepSize.y;
	            }
	
	            var currentPoint = pt1.y + yslope * (distance * metersPerPixel);
	            // Logger.Info("[HSGE] " + $"y: {currentChunk.y}, x: {currentChunk.x}, w: {width}, h: {height}, l: {highestOfQuad.Length}, r: {highestOfQuad.Rank}");
	            if (currentChunk.y >= height || currentChunk.x >= width | currentChunk.y < 0 || currentChunk.x < 0) break;
	
	            var worldHeight = highestOfQuad[(int)currentChunk.y, (int)currentChunk.x];
	
	            var worldPoint = new Vector3(currentChunk.x * metersPerPixel, currentPoint, currentChunk.y * metersPerPixel);
	            var hPoint = new Vector3(currentChunk.x * metersPerPixel, worldHeight, currentChunk.y * metersPerPixel);
	
	            Gizmos.color = Color.blue;
	            Gizmos.DrawSphere(worldPoint, 5);
	            Gizmos.color = Color.red;
	            Gizmos.DrawSphere(hPoint, 5);
	
	            if (currentPoint < worldHeight)
	            {
	                hitGround = true;
	                break;
	            }
	        }
	
	        if (hitGround) Gizmos.color = Color.red;
	        else Gizmos.color = Color.white;
	        Gizmos.DrawLine(pt1, pt1 + dir3 * distance * metersPerPixel);
	
	        hitPoint = pt1 + dir3 * distance * metersPerPixel;
	
	        //return hitGround;
	    }
	
	    private Vector2 LineIntersection(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
	    {
	        // Check if none of the lines are of length 0
	        if ((x1 == x2 && y1 == y2) || (x3 == x4 && y3 == y4))
	        {
	            return Vector2.zero;
	        }
	
	        var denominator = ((y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1));
	
	        // Lines are parallel
	        if (denominator == 0)
	        {
	            return Vector2.zero;
	        }
	        var ua = ((x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3)) / denominator;
	        var ub = ((x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3)) / denominator;
	
	        if (ua < 0 || ua > 1 || ub < 0 || ub > 1)
	        {
	            return Vector2.zero;
	
	        }
	        var x = x1 + ua * (x2 - x1);
	        var y = y1 + ua * (y2 - y1);
	
	        return new Vector2(x, y);
	    }
	}
}