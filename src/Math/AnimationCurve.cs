using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityGERunner
{
    public struct Keyframe
    {
        public float time;
        public float value;
        public float inSlope;
        public float outSlope;

        public Keyframe(float time, float value, float inTangent, float outTangent, float inWeight, float outWeight)
        {
            this.time = time;
            this.value = value;
            this.inSlope = inTangent;
            this.outSlope = outTangent;
        }

        public Keyframe(float time, float value, float inSlope, float outSlope)
        {
            this.time = time;
            this.value = value;
            this.inSlope = inSlope;
            this.outSlope = outSlope;

            var test = new AnimationCurve(new Keyframe[] { this });
        }
    }

    public class AnimationCurve
    {
        private Keyframe[] keyframes;
        public AnimationCurve(params Keyframe[] keyframes)
        {
            this.keyframes = keyframes;
        }

        public float Evaluate(float t)
        {
            if (keyframes.Length == 0) return 0;

            if (t <= keyframes[0].time) return keyframes[0].value;
            if (t >= keyframes[keyframes.Length - 1].time) return keyframes[keyframes.Length - 1].value;

            for (int i = 0; i < keyframes.Length - 1; i++)
            {
                var kf0 = keyframes[i];
                var kf1 = keyframes[i + 1];

                if (t >= kf0.time && t <= kf1.time)
                {
                    return EvalCurve(kf0, kf1, t);
                }
            }

            return 0;
        }

        private float EvalCurve(Keyframe kf0, Keyframe kf1, float time)
        {
            var dt = kf1.time - kf0.time;
            var t = (time - kf0.time) / dt;

            var m0 = kf0.outSlope * dt;
            var m1 = kf1.inSlope * dt;

            var t2 = t * t;
            var t3 = t2 * t;

            var a = 2 * t3 - 3 * t2 + 1; // Cubic basis function
            var b = t3 - 2 * t2 + t; // Quadratic basis function
            var c = t3 - t2; // Linear basis function
            var d = -2 * t3 + 3 * t2; // Constant basis function

            return a * kf0.value + b * m0 + c * m1 + d * kf1.value;

        }
    }
}