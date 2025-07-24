using UnityGERunner;
using Coroutine;
using UnityGERunner.UnityApplication;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;



namespace Recorder
{
    class TestingValueDef
    {
        public string name;
        public float value;
        public float minValue;
        public float maxValue;
        public float step;
    }

    public class GameRecorder : MonoBehaviour
    {
        public static GameRecorder instance { get; private set; }

        public float maxSimTime = 60;
        public string outputPath = "./recording.json";
        public string graphOutputPath = "./graphs.json";
        public string logOutputPath = "./aip.log";
        public string stateOutputPath = "./state.json";
        public string testingValuesOutputPath = "./rvt.json";

        private bool hasClosedStreams = false;
        private bool hasEnded = false;

        private RecordedFrame currentFrame;
        private FileStream writeStream;
        private FileStream logWriteStream;
        private FileStream stateStream;

        private Dictionary<string, List<float>> graphData = new Dictionary<string, List<float>>();
        private List<TestingValueDef> testingValues = new List<TestingValueDef>();


        public static void Record(Actor actor)
        {
            var entityData = new EntityKinematicData
            {
                position = NetVector.From(actor.transform.position),
                velocity = NetVector.From(actor.velocity),
                rotation = NetVector.From(actor.transform.rotation.eulerAngles),
                entityId = actor.entityId,
            };

            instance.currentFrame.motion.Add(entityData);
        }

        public static void Record(Actor actor, NetVector pyr)
        {
            var entityData = new EntityKinematicData
            {
                position = NetVector.From(actor.transform.position),
                velocity = NetVector.From(actor.velocity),
                rotation = NetVector.From(actor.transform.rotation.eulerAngles),
                pyr = pyr,
                entityId = actor.entityId,
            };

            instance.currentFrame.motion.Add(entityData);
        }

        public static void InitEntity(int entityId, string path, string name)
        {
            var initEvent = new EntityInit(entityId, path, name);

            Event(initEvent);
        }

        public static void DeleteEntity(int entityId)
        {
            Event(new EntityDelete(entityId));
        }

        public static void Event(RecorderEvent evt)
        {
            instance.currentFrame.events.Add(evt);
        }

        private void CreateNewFrame()
        {
            if (currentFrame != null && !hasClosedStreams)
            {
                var frameStr = JsonConvert.SerializeObject(currentFrame) + "\n";
                var bytes = UTF8Encoding.UTF8.GetBytes(frameStr);
                writeStream.Write(bytes, 0, bytes.Length);
                //var frameBytes = currentFrame.GetBytes();
                //frameBytes.CopyTo(writeStream);
            }

            currentFrame = new RecordedFrame(Time.time);

            foreach (var key in graphData.Keys)
            {
                graphData[key].Add(0);
            }
        }

        private void InitGraphDataFor(string key)
        {
            var frameCount = graphData["time"].Count;
            var newGraphData = new List<float>(new float[frameCount]);
            graphData.Add(key, newGraphData);
        }

        public static void Graph(string key, Vector3 value)
        {
            Graph(key + ".x", value.x);
            Graph(key + ".y", value.y);
            Graph(key + ".z", value.z);
        }

        public static void Graph(string key, NetVector value)
        {
            Graph(key + ".x", value.x);
            Graph(key + ".y", value.y);
            Graph(key + ".z", value.z);
        }

        public static void Graph(string key, float value)
        {
            if (!instance.graphData.ContainsKey(key)) instance.InitGraphDataFor(key);

            var idx = instance.graphData[key].Count - 1;
            instance.graphData[key][idx] = value;
        }

        public static void Log(string value)
        {
            instance.currentFrame.logs.Add(value);
            var maybeSpace = value.StartsWith('[') ? "" : " ";
            Logger.Info("[HSGE] " + $"[AIP]{maybeSpace}{value}");

            if (instance.hasClosedStreams) return;
            instance.logWriteStream.Write(UTF8Encoding.UTF8.GetBytes(value + "\n"));
        }

        public static void DebugShape(DebugLine line) { Event(line); }

        public static void DebugShape(DebugSphere sphere) { Event(sphere); }

        public static void RemoveDebugShape(int shapeId)
        {
            Event(new RemoveDebugShape(shapeId));
        }

        public static void RecordState(OutboundState state)
        {
            if (instance.hasClosedStreams) return;
            var stateStr = JsonConvert.SerializeObject(state) + "\n";
            var bytes = UTF8Encoding.UTF8.GetBytes(stateStr);
            instance.stateStream.Write(bytes, 0, bytes.Length);
        }

        public static float TestingValue(string name, float defaultValue, float minValue, float maxValue, float step = 0.1f)
        {
            var existing = instance.testingValues.Find(tv => tv.name == name);
            if (existing != null)
            {
                existing.minValue = minValue;
                existing.maxValue = maxValue;
                existing.step = step;
                return existing.value;
            }
            else
            {
                var newTv = new TestingValueDef
                {
                    name = name,
                    value = defaultValue,
                    minValue = minValue,
                    maxValue = maxValue,
                    step = step,
                };

                instance.testingValues.Add(newTv);
                return newTv.value;
            }
        }

        private void DumpGraphs()
        {
            File.WriteAllText(graphOutputPath, JsonConvert.SerializeObject(graphData));

            List<List<string>> csv = new List<List<string>>();
            List<string> header = new List<string>();
            foreach (var graphName in graphData.Keys) header.Add(graphName);
            csv.Add(header);

            for (int i = 0; i < graphData["time"].Count; i++)
            {
                List<string> row = new List<string>();
                foreach (var graphName in graphData.Keys) row.Add(graphData[graphName][i].ToString());

                csv.Add(row);
            }

            var csvOut = graphOutputPath.Replace(".json", ".csv");
            //string csvString = "";
            //foreach (var row in csv)
            //{
            //    foreach (var entry in row)
            //    {
            //        csvString += entry + ",";
            //    }
            //    csvString += "\n";
            //}
            var csvString = string.Join("\n", csv.Select(row => string.Join(",", row)));
            File.WriteAllText(csvOut, csvString);
        }

        public static void EndSimulation()
        {
            Logger.Info("[HSGE] " + $"Recorder ending simulation");
            instance.hasEnded = true;

            instance.DumpGraphs();

            var testingValuesStr = JsonConvert.SerializeObject(instance.testingValues, Formatting.Indented);
            File.WriteAllText(instance.testingValuesOutputPath, testingValuesStr);

            if (instance.currentFrame != null)
            {
                var frameStr = JsonConvert.SerializeObject(instance.currentFrame) + "\n";
                var bytes = UTF8Encoding.UTF8.GetBytes(frameStr);
                instance.writeStream.Write(bytes, 0, bytes.Length);
            }

            instance.hasClosedStreams = true;
            instance.writeStream.Close();
            instance.logWriteStream.Close();
            instance.stateStream.Close();



#if UNITY_EDITOR
            //UnityEditor.EditorApplication.isPlaying = false;
            Time.timeScale = 0;
#else
        GameEngine.instance.Quit();
#endif
        }

        protected override void Awake()
        {
            instance = this;
            writeStream = File.Create(outputPath);
            logWriteStream = File.Create(logOutputPath);
            stateStream = File.Create(stateOutputPath);

            Logger.Info("[HSGE] " + $"Recording file output: {Path.GetFullPath(outputPath)}");

            graphData.Add("time", new List<float>());
            CreateNewFrame();

            if (File.Exists(testingValuesOutputPath))
            {
                var testingValuesStr = File.ReadAllText(testingValuesOutputPath);
                testingValues = JsonConvert.DeserializeObject<List<TestingValueDef>>(testingValuesStr);

                foreach (var tv in testingValues)
                {
                    Logger.Info("[HSGE] " + $"Loaded existing testing value: {tv.name}={tv.value} ({tv.minValue}-{tv.maxValue})");
                }
            }
        }

        protected override void FixedUpdate()
        {
            if (hasEnded) return;

            CreateNewFrame();
            var tIdx = graphData["time"].Count - 1;
            graphData["time"][tIdx] = Time.time;

            if (Time.time > maxSimTime)
            {
                EndSimulation();
            }
        }
    }
}