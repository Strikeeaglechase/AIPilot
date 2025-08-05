using System.Diagnostics;
using System.Reflection;

namespace UnityGERunner
{
    internal class Program
    {
        private static void Test()
        {
            var speed1 = 100;
            var speed2 = 50;
            var radarPos = new Vector3(0, 0, 0);
            var targetPos = new Vector3(0, 0, 100);
            var norm = (targetPos - radarPos).normalized;
            for (int i = 0; i < 360; i += 1)
            {
                var vel1 = new Vector3(Mathf.Cos((i + 90) * Mathf.Deg2Rad), 0, Mathf.Sin((i + 90) * Mathf.Deg2Rad)).normalized * speed1;
                var vel2 = new Vector3(Mathf.Cos((i + 90) * Mathf.Deg2Rad), 0, Mathf.Sin((i + 90) * Mathf.Deg2Rad)).normalized * speed2;

                var notch1 = Mathf.Abs(Vector3.Dot(Vector3.ClampMagnitude(vel1 / 100f, 1.5f), norm));
                notch1 = Mathf.Clamp(notch1, 0.0125f, 1.5f);

                var notch2 = Mathf.Abs(Vector3.Dot(Vector3.ClampMagnitude(vel2 / 100f, 1.5f), norm));
                notch2 = Mathf.Clamp(notch2, 0.0125f, 1.5f);

                Console.WriteLine($"{i},{notch1},{notch2}");
            }
        }

        static void Main(string[] args)
        {
            GameEngine engine = new GameEngine();

            engine.Init();

            int t = 0;

            while (true)
            {
                var keepRunning = engine.UpdateLoop();
                if (!keepRunning) break;

                // Setting sim durration and speed
#if DEBUG && false
                if (t++ % 50 == 0) Thread.Sleep(1);
#endif
                //if (Time.time > 100) break;
            }

            var autoVtgr = Environment.GetCommandLineArgs().Any(a => a == "--auto-vtgr");
            if (!autoVtgr) return;

            Logger.Warn($"VTGR proc start");

            var startInfo = new ProcessStartInfo
            {
                FileName = "C:\\Program Files\\nodejs\\node.exe",
                Arguments = "C:\\Users\\strik\\Desktop\\Programs\\CSharp\\UnityGERunner\\UnityTranspiler\\dist\\recordingToVtgr\\recordingToVtgr.js",
                UseShellExecute = false,
                RedirectStandardError = false,
                RedirectStandardOutput = false,
            };
            var process = new Process { StartInfo = startInfo };
            process.Start();
        }
    }
}