using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityGERunner
{
    public struct Vector3
    {

        public const float kEpsilon = 1E-05f;

        public const float kEpsilonNormalSqrt = 1E-15f;

        public float x;

        public float y;

        public float z;

        private static readonly Vector3 zeroVector = new Vector3(0f, 0f, 0f);

        private static readonly Vector3 oneVector = new Vector3(1f, 1f, 1f);

        private static readonly Vector3 upVector = new Vector3(0f, 1f, 0f);

        private static readonly Vector3 downVector = new Vector3(0f, -1f, 0f);

        private static readonly Vector3 leftVector = new Vector3(-1f, 0f, 0f);

        private static readonly Vector3 rightVector = new Vector3(1f, 0f, 0f);

        private static readonly Vector3 forwardVector = new Vector3(0f, 0f, 1f);

        private static readonly Vector3 backVector = new Vector3(0f, 0f, -1f);

        private static readonly Vector3 positiveInfinityVector = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

        private static readonly Vector3 negativeInfinityVector = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);

        public Vector3 normalized => Normalize(this);

        public float magnitude => (float)Math.Sqrt(x * x + y * y + z * z);

        public float sqrMagnitude => x * x + y * y + z * z;

        public static Vector3 zero => zeroVector;

        public static Vector3 one => oneVector;

        public static Vector3 forward => forwardVector;

        public static Vector3 back => backVector;

        public static Vector3 up => upVector;

        public static Vector3 down => downVector;

        public static Vector3 left => leftVector;

        public static Vector3 right => rightVector;

        public static Vector3 positiveInfinity => positiveInfinityVector;

        public static Vector3 negativeInfinity => negativeInfinityVector;

        public static Vector3 LerpUnclamped(Vector3 a, Vector3 b, float t)
        {
            return new Vector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
        }

        public static Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
        {
            float num = target.x - current.x;
            float num2 = target.y - current.y;
            float num3 = target.z - current.z;
            float num4 = num * num + num2 * num2 + num3 * num3;
            if (num4 == 0f || (maxDistanceDelta >= 0f && num4 <= maxDistanceDelta * maxDistanceDelta))
            {
                return target;
            }
            float num5 = (float)Math.Sqrt(num4);
            return new Vector3(current.x + num / num5 * maxDistanceDelta, current.y + num2 / num5 * maxDistanceDelta, current.z + num3 / num5 * maxDistanceDelta);
        }


        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3(float x, float y)
        {
            this.x = x;
            this.y = y;
            z = 0f;
        }

        public Vector3(float x, float y, double v) : this(x, y)
        {
        }

        public void Set(float newX, float newY, float newZ)
        {
            x = newX;
            y = newY;
            z = newZ;
        }

        public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
        {
            t = Mathf.Clamp01(t);
            return new Vector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
        }

        public static Vector3 Scale(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public void Scale(Vector3 scale)
        {
            x *= scale.x;
            y *= scale.y;
            z *= scale.z;
        }

        public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2);
        }

        public override bool Equals(object other)
        {
            if (!(other is Vector3))
            {
                return false;
            }
            return Equals((Vector3)other);
        }

        public bool Equals(Vector3 other)
        {
            return x == other.x && y == other.y && z == other.z;
        }

        public static Vector3 Reflect(Vector3 inDirection, Vector3 inNormal)
        {
            float num = -2f * Dot(inNormal, inDirection);
            return new Vector3(num * inNormal.x + inDirection.x, num * inNormal.y + inDirection.y, num * inNormal.z + inDirection.z);
        }

        public static Vector3 Normalize(Vector3 value)
        {
            float num = Magnitude(value);
            if (num > 1E-05f)
            {
                return value / num;
            }
            return zero;
        }

        public void Normalize()
        {
            float num = Magnitude(this);
            if (num > 1E-05f)
            {
                this.Set(x / num, y / num, z / num);
            }
            else
            {
                this.Set(0, 0, 0);
            }
        }

        public static float Dot(Vector3 lhs, Vector3 rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
        }


        public static float Distance(Vector3 a, Vector3 b)
        {
            float num = a.x - b.x;
            float num2 = a.y - b.y;
            float num3 = a.z - b.z;
            return (float)Math.Sqrt(num * num + num2 * num2 + num3 * num3);
        }

        public static Vector3 ClampMagnitude(Vector3 vector, float maxLength)
        {
            float num = vector.sqrMagnitude;
            if (num > maxLength * maxLength)
            {
                float num2 = (float)Math.Sqrt(num);
                float num3 = vector.x / num2;
                float num4 = vector.y / num2;
                float num5 = vector.z / num2;
                return new Vector3(num3 * maxLength, num4 * maxLength, num5 * maxLength);
            }
            return vector;
        }

        public static float Magnitude(Vector3 vector)
        {
            return (float)Math.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
        }

        public static float SqrMagnitude(Vector3 vector)
        {
            return vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
        }

        public static float Angle(Vector3 from, Vector3 to)
        {
            float num = (float)Math.Sqrt(from.sqrMagnitude * to.sqrMagnitude);
            if (num < 1E-15f)
            {
                return 0f;
            }

            float num2 = Mathf.Clamp(Dot(from, to) / num, -1f, 1f);
            return Mathf.Acos(num2) * Mathf.Rad2Deg;
        }

        public static float MoveTowards(float current, float target, float maxDelta)
        {
            if (Math.Abs(target - current) <= maxDelta)
                return target;
            return current + Math.Sign(target - current) * maxDelta;
        }



        public static Vector3 RotateAroundAxis(Vector3 v, Vector3 axis, float angle)
        {
            float cos = Mathf.Cos(angle);
            float sin = Mathf.Sin(angle);
            return v * cos + Cross(axis, v) * sin + axis * Dot(axis, v) * (1 - cos);
        }


        public static Vector3 RotateTowards(Vector3 current, Vector3 target, float maxRadiansDelta, float maxMagnitudeDelta)
        {
            float currentMag = current.magnitude;
            float targetMag = target.magnitude;

            // First adjust magnitude
            float newMag = MoveTowards(currentMag, targetMag, maxMagnitudeDelta);

            // Normalize the vectors
            Vector3 from = current.normalized;
            Vector3 to = target.normalized;

            // Angle between them
            float angle = Angle(from, to) * Mathf.Deg2Rad;

            if (angle == 0f)
                return to * newMag;

            float t = Math.Min(1f, maxRadiansDelta / angle);
            Vector3 axis = Cross(from, to);
            if (axis.magnitude < 1e-6f) // if from and to are colinear
            {
                // Return either to or from based on direction
                return Lerp(from, to, t) * newMag;
            }

            axis = axis.normalized;

            return RotateAroundAxis(from, axis, angle * t) * newMag;
        }

        public static Vector3 Slerp(Vector3 a, Vector3 b, float t)
        {
            t = Mathf.Clamp01(t);

            float magA = a.magnitude;
            float magB = b.magnitude;

            // Normalize input vectors
            Vector3 from = magA > 1e-6f ? a / magA : Vector3.zero;
            Vector3 to = magB > 1e-6f ? b / magB : Vector3.zero;

            // Dot product
            float dot = Dot(from, to);
            dot = Math.Clamp(dot, -1f, 1f);

            float theta = (float)Math.Acos(dot);

            // If angle is very small, use linear interpolation
            if (theta < 1e-5f)
                return Lerp(a, b, t);

            float sinTheta = (float)Math.Sin(theta);
            float w1 = (float)Math.Sin((1 - t) * theta) / sinTheta;
            float w2 = (float)Math.Sin(t * theta) / sinTheta;

            // Interpolate direction, then interpolate magnitude
            Vector3 direction = (w1 * from + w2 * to).normalized;
            float magnitude = Mathf.Lerp(magA, magB, t);

            return direction * magnitude;
        }

        public static Vector3 ProjectOnPlane(Vector3 vector, Vector3 planeNormal)
        {
            float num = Dot(planeNormal, planeNormal);
            if (num < Mathf.Epsilon)
            {
                return vector;
            }

            float num2 = Dot(vector, planeNormal);
            return new Vector3(vector.x - planeNormal.x * num2 / num, vector.y - planeNormal.y * num2 / num, vector.z - planeNormal.z * num2 / num);
        }

        public static Vector3 Project(Vector3 vector, Vector3 onNormal)
        {
            float num = Dot(onNormal, onNormal);
            if (num < Mathf.Epsilon)
            {
                return zero;
            }

            float num2 = Dot(vector, onNormal);
            return new Vector3(onNormal.x * num2 / num, onNormal.y * num2 / num, onNormal.z * num2 / num);
        }

        public static Vector3 FindOrthogonal(Vector3 v)
        {
            if (Mathf.Abs(v.x) < Mathf.Abs(v.y) && Mathf.Abs(v.x) < Mathf.Abs(v.z))
                return new Vector3(0, -v.z, v.y);
            else if (Mathf.Abs(v.y) < Mathf.Abs(v.z))
                return new Vector3(-v.z, 0, v.x);
            else
                return new Vector3(-v.y, v.x, 0);
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }
        public static Vector3 operator +(Vector3 a, float b)
        {
            return new Vector3(a.x + b, a.y + b, a.z + b);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vector3 operator -(Vector3 a)
        {
            return new Vector3(0f - a.x, 0f - a.y, 0f - a.z);
        }

        public static Vector3 operator *(Vector3 a, float d)
        {
            return new Vector3(a.x * d, a.y * d, a.z * d);
        }

        public static Vector3 operator *(float d, Vector3 a)
        {
            return new Vector3(a.x * d, a.y * d, a.z * d);
        }
        public static Vector3 operator *(Vector3 d, Vector3 a)
        {
            return new Vector3(a.x * d.x, a.y * d.y, a.z * d.z);
        }

        public static Vector3 operator /(Vector3 a, float d)
        {
            return new Vector3(a.x / d, a.y / d, a.z / d);
        }

        public static bool operator ==(Vector3 lhs, Vector3 rhs)
        {
            float num = lhs.x - rhs.x;
            float num2 = lhs.y - rhs.y;
            float num3 = lhs.z - rhs.z;
            float num4 = num * num + num2 * num2 + num3 * num3;
            return num4 < 9.99999944E-11f;
        }

        public static bool operator !=(Vector3 lhs, Vector3 rhs)
        {
            return !(lhs == rhs);
        }

        public override string ToString()
        {
            return $"({x}, {y}, {z})";
        }
    }
}
