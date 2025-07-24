using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityGERunner
{
    public class Time
    {
        //public static float timeScale = 0; // Unused, make unity happy

        public static float time = 0;
        public static float deltaTime { get { return time - lastUpdateTime; } }
        public static float lastUpdateTime = 0;
        public static float fixedDeltaTime = 0.0111f;
        public static float timeStep = fixedDeltaTime;


        private static float lastLoggedT = 0;
        private static int ticks = 0;
        private static DateTime realTimeLastLogged = DateTime.Now;

        public static void Tick()
        {
            lastUpdateTime = time;
            time += timeStep;
            ticks++;

            //Logger.Info($"Time: {time}, ts: {timeStep}, fdt: {fixedDeltaTime}, llt: {lastLoggedT}");

            if (time - lastLoggedT > 10)
            {
                var simDelta = time - lastLoggedT;
                var realDelta = DateTime.Now - realTimeLastLogged;

                var simSecondsPerSecond = simDelta / realDelta.TotalSeconds;
                var simTicksPerSecond = ticks / realDelta.TotalSeconds;

                Logger.Info($"Sim time: {time:f0}s. Sim seconds/second: {simSecondsPerSecond:f2}, Ticks Per Second: {simTicksPerSecond:f0}");
                ticks = 0;
                lastLoggedT = time;
                realTimeLastLogged = DateTime.Now;
            }
        }
    }
}
