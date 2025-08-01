using System.Diagnostics;
using System.Reflection;

namespace UnityGERunner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("REEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
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