using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityGERunner
{
    public class Random
    {
        //public static readonly Random Instance = new Random();
        private static readonly System.Random rand = new System.Random();

        public static Vector3 onUnitSphere
        {
            get
            {
                var u = (rand.NextSingle() - 0.5f) * 2;
                var t = rand.NextSingle() * Mathf.PI * 2;
                var f = Mathf.Sqrt(1 - u * u);

                return new Vector3(
                    f * Mathf.Cos(t),
                    f * Mathf.Sin(t),
                    u
                );
            }
        }

        public static int Range(int min, int max)
        {
            return rand.Next(min, max);
        }

        public static float Range(float min, float max)
        {
            return rand.NextSingle() * (max - min) + min;
        }
    }
}
