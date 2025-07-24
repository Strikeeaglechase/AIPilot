using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UnityGERunner
{
    public struct Quaternion
    {
        public float x;

        public float y;

        public float z;

        public float w;

        private static readonly Quaternion identityQuaternion = new Quaternion(0f, 0f, 0f, 1f);

        public const float kEpsilon = 1E-06f;

        public float this[int index]
        {
            get
            {
                return index switch
                {
                    0 => x,
                    1 => y,
                    2 => z,
                    3 => w,
                    _ => throw new IndexOutOfRangeException("Invalid Quaternion index!"),
                };
            }
            set
            {
                switch (index)
                {
                    case 0:
                        x = value;
                        break;
                    case 1:
                        y = value;
                        break;
                    case 2:
                        z = value;
                        break;
                    case 3:
                        w = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Quaternion index!");
                }
            }
        }

        public static Quaternion identity => identityQuaternion;

        public Vector3 eulerAngles
        {
            get
            {
                /*
                Vector3 angles = Vector3.zero;

                float sinr_cosp = 2 * (w * x + y * z);
                float cosr_cosp = 1 - 2 * (x * x + y * y);
                angles.x = Mathf.Atan2(sinr_cosp, cosr_cosp);

                float sinp = 2 * (w * y - z * x);
                if (Mathf.Abs(sinp) >= 1)
                {
                    angles.y = (float)Math.CopySign(Math.PI / 2, (float)sinp);
                }
                else
                {
                    angles.y = Mathf.Asin(sinp);
                }

                float siny_cosp = 2 * (w * z + x * y);
                float cosy_cosp = 1 - 2 * (y * y + z * z);
                angles.z = Mathf.Atan2(siny_cosp, cosy_cosp);

                return angles * 57.2958f;
                */

                float yaw, pitch, roll;
                z = -z;
                float w2 = w * w;
                float x2 = x * x;
                float y2 = y * y;
                float z2 = z * z;
                float unitLength = w2 + x2 + y2 + z2; // Normalised == 1, otherwise correction divisor.
                float abcd = 2 * (w * x + y * z);

                if (abcd > (1.0f - Mathf.Epsilon) * unitLength)
                {
                    roll = 2 * Mathf.Atan2(y, w);
                    pitch = Mathf.PI / 2;
                    yaw = 0;
                }
                else if (abcd < (-1.0 + Mathf.Epsilon) * unitLength)
                {
                    roll = -2 * Mathf.Atan2(y, w);
                    pitch = -Mathf.PI / 2;
                    yaw = 0;
                }
                else
                {
                    float adbc = 2 * (w * z - x * y);
                    float acbd = 2 * (w * y - x * z);
                    roll = Mathf.Atan2(adbc, 1 - 2 * (z2 + x2));
                    pitch = Mathf.Asin(abcd / unitLength);
                    yaw = Mathf.Atan2(acbd, 1 - 2 * (y2 + x2));
                }

                return new Vector3(pitch, yaw, -roll) * Mathf.Rad2Deg;
            }
            set
            {
                Vector3 radAngles = value * 0.0174533f;

                float cy = Mathf.Cos(radAngles.z * 0.5f);
                float sy = Mathf.Sin(radAngles.z * 0.5f);
                float cp = Mathf.Cos(radAngles.y * 0.5f);
                float sp = Mathf.Sin(radAngles.y * 0.5f);
                float cr = Mathf.Cos(radAngles.x * 0.5f);
                float sr = Mathf.Sin(radAngles.x * 0.5f);

                w = cr * cp * cy + sr * sp * sy;
                x = sr * cp * cy - cr * sp * sy;
                y = cr * sp * cy + sr * cp * sy;
                z = cr * cp * sy - sr * sp * cy;
            }
        }
        public static Quaternion Euler(float x, float y, float z)
        {
            return Euler(new Vector3(x, y, z));
        }

        public static Quaternion Euler(Vector3 angles)
        {
            Quaternion q = new Quaternion();
            q.eulerAngles = angles;
            return q;
        }

        public Quaternion normalized => Normalize(this);

        public Quaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public void Set(float newX, float newY, float newZ, float newW)
        {
            x = newX;
            y = newY;
            z = newZ;
            w = newW;
        }

        public static Quaternion operator *(Quaternion lhs, Quaternion rhs)
        {
            return new Quaternion(lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y, lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z, lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x, lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);
        }

        public static Vector3 operator *(Quaternion rotation, Vector3 point)
        {
            float num = rotation.x * 2f;
            float num2 = rotation.y * 2f;
            float num3 = rotation.z * 2f;
            float num4 = rotation.x * num;
            float num5 = rotation.y * num2;
            float num6 = rotation.z * num3;
            float num7 = rotation.x * num2;
            float num8 = rotation.x * num3;
            float num9 = rotation.y * num3;
            float num10 = rotation.w * num;
            float num11 = rotation.w * num2;
            float num12 = rotation.w * num3;
            Vector3 result = default(Vector3);
            result.x = (1f - (num5 + num6)) * point.x + (num7 - num12) * point.y + (num8 + num11) * point.z;
            result.y = (num7 + num12) * point.x + (1f - (num4 + num6)) * point.y + (num9 - num10) * point.z;
            result.z = (num8 - num11) * point.x + (num9 + num10) * point.y + (1f - (num4 + num5)) * point.z;
            return result;
        }

        private static bool IsEqualUsingDot(float dot)
        {
            return dot > 0.999999f;
        }

        public static bool operator ==(Quaternion lhs, Quaternion rhs)
        {
            return IsEqualUsingDot(Dot(lhs, rhs));
        }

        public static bool operator !=(Quaternion lhs, Quaternion rhs)
        {
            return !(lhs == rhs);
        }

        public static float Dot(Quaternion a, Quaternion b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        }


        public static float Angle(Quaternion a, Quaternion b)
        {
            float num = Dot(a, b);
            return IsEqualUsingDot(num) ? 0f : (Mathf.Acos(Mathf.Min(Mathf.Abs(num), 1f)) * 2f * 57.29578f);
        }

        private static Vector3 Internal_MakePositive(Vector3 euler)
        {
            float num = -0.005729578f;
            float num2 = 360f + num;
            if (euler.x < num)
            {
                euler.x += 360f;
            }
            else if (euler.x > num2)
            {
                euler.x -= 360f;
            }
            if (euler.y < num)
            {
                euler.y += 360f;
            }
            else if (euler.y > num2)
            {
                euler.y -= 360f;
            }
            if (euler.z < num)
            {
                euler.z += 360f;
            }
            else if (euler.z > num2)
            {
                euler.z -= 360f;
            }
            return euler;
        }

        public static Quaternion Normalize(Quaternion q)
        {
            float num = Mathf.Sqrt(Dot(q, q));
            if (num < Mathf.Epsilon)
            {
                return identity;
            }
            return new Quaternion(q.x / num, q.y / num, q.z / num, q.w / num);
        }

        public void Normalize()
        {
            this = Normalize(this);
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2) ^ (w.GetHashCode() >> 1);
        }

        public override bool Equals(object other)
        {
            if (!(other is Quaternion))
            {
                return false;
            }
            return Equals((Quaternion)other);
        }

        public bool Equals(Quaternion other)
        {
            return x.Equals(other.x) && y.Equals(other.y) && z.Equals(other.z) && w.Equals(other.w);
        }

        public void SetLookRotation(Vector3 view)
        {
            SetLookRotation(view, Vector3.up);
        }

        public void SetLookRotation(Vector3 view, Vector3 up)
        {
            this = LookRotation(view, up);
        }

        public static Quaternion LookRotation(Vector3 forward)
        {
            return LookRotation(forward, Vector3.up);
        }

        public static Quaternion LookRotation(Vector3 forward, Vector3 up)
        {
            forward.Normalize();

            Vector3 vector = Vector3.Normalize(forward);
            Vector3 vector2 = Vector3.Normalize(Vector3.Cross(up, vector));
            Vector3 vector3 = Vector3.Cross(vector, vector2);
            var m00 = vector2.x;
            var m01 = vector2.y;
            var m02 = vector2.z;
            var m10 = vector3.x;
            var m11 = vector3.y;
            var m12 = vector3.z;
            var m20 = vector.x;
            var m21 = vector.y;
            var m22 = vector.z;


            float num8 = (m00 + m11) + m22;
            var quaternion = new Quaternion();
            if (num8 > 0f)
            {
                var num = (float)Math.Sqrt(num8 + 1f);
                quaternion.w = num * 0.5f;
                num = 0.5f / num;
                quaternion.x = (m12 - m21) * num;
                quaternion.y = (m20 - m02) * num;
                quaternion.z = (m01 - m10) * num;
                return quaternion;
            }
            if ((m00 >= m11) && (m00 >= m22))
            {
                var num7 = (float)Math.Sqrt(((1f + m00) - m11) - m22);
                var num4 = 0.5f / num7;
                quaternion.x = 0.5f * num7;
                quaternion.y = (m01 + m10) * num4;
                quaternion.z = (m02 + m20) * num4;
                quaternion.w = (m12 - m21) * num4;
                return quaternion;
            }
            if (m11 > m22)
            {
                var num6 = (float)Math.Sqrt(((1f + m11) - m00) - m22);
                var num3 = 0.5f / num6;
                quaternion.x = (m10 + m01) * num3;
                quaternion.y = 0.5f * num6;
                quaternion.z = (m21 + m12) * num3;
                quaternion.w = (m20 - m02) * num3;
                return quaternion;
            }
            var num5 = (float)Math.Sqrt(((1f + m22) - m00) - m11);
            var num2 = 0.5f / num5;
            quaternion.x = (m20 + m02) * num2;
            quaternion.y = (m21 + m12) * num2;
            quaternion.z = 0.5f * num5;
            quaternion.w = (m01 - m10) * num2;
            return quaternion;
        }

        public static Quaternion FromToRotation(Vector3 fromDirection, Vector3 toDirection)
        {
            Vector3 from = fromDirection.normalized;
            Vector3 to = toDirection.normalized;

            float dot = Vector3.Dot(from, to);

            if (dot >= 1.0f)
            {
                // Vectors are the same
                return Quaternion.identity;
            }

            if (dot <= -1.0f)
            {
                // Vectors are opposite
                Vector3 orthogonal = Vector3.FindOrthogonal(from);
                return AngleAxis(180.0f, orthogonal);
            }

            Vector3 axis = Vector3.Cross(from, to);
            float s = Mathf.Sqrt((1 + dot) * 2);
            float invs = 1 / s;

            return new Quaternion(
                axis.x * invs,
                axis.y * invs,
                axis.z * invs,
                s * 0.5f
            );
        }


        public static Quaternion AngleAxis(float angle, Vector3 axis)
        {
            angle = angle * Mathf.Deg2Rad;
            axis = axis.normalized;
            var halfAngle = angle / 2;
            var s = Mathf.Sin(halfAngle);

            return new Quaternion(
                axis.x * s,
                axis.y * s,
                axis.z * s,
                Mathf.Cos(halfAngle)
                );
        }

        public void ToAngleAxis(out float angle, out Vector3 axis)
        {
            if (w > 1.0f) this = Normalize(this);

            angle = 2.0f * Mathf.Acos(w);
            float sinHalfAngle = Mathf.Sqrt(1.0f - w * w);

            if (sinHalfAngle < 0.0001f)
            {
                // If it's very close to zero, direction is not important
                axis = new Vector3(1, 0, 0);
            }
            else
            {
                axis = new Vector3(x, y, z) / sinHalfAngle;
                axis = axis.normalized;
            }

            angle = angle * Mathf.Rad2Deg; // Convert to degrees
        }


        public static Quaternion Slerp(Quaternion a, Quaternion b, float t)
        {
            // Clamp t to [0, 1]
            t = Math.Clamp(t, 0f, 1f);

            // Compute dot product
            float dot = a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;

            // If dot < 0, use the short path by negating b
            if (dot < 0f)
            {
                dot = -dot;
                b = new Quaternion(-b.x, -b.y, -b.z, -b.w);
            }

            const float DOT_THRESHOLD = 0.9995f;
            if (dot > DOT_THRESHOLD)
            {
                // If very close, use linear interpolation to avoid divide-by-zero
                Quaternion result = new Quaternion(
                    a.x + t * (b.x - a.x),
                    a.y + t * (b.y - a.y),
                    a.z + t * (b.z - a.z),
                    a.w + t * (b.w - a.w)
                );
                return Normalize(result);
            }

            // Compute angle and sin values
            float theta_0 = (float)Math.Acos(dot);          // initial angle
            float sin_theta_0 = (float)Math.Sin(theta_0);

            float theta = theta_0 * t;                       // interpolated angle
            float sin_theta = (float)Math.Sin(theta);

            float s0 = (float)Math.Cos(theta) - dot * sin_theta / sin_theta_0;
            float s1 = sin_theta / sin_theta_0;

            return new Quaternion(
                s0 * a.x + s1 * b.x,
                s0 * a.y + s1 * b.y,
                s0 * a.z + s1 * b.z,
                s0 * a.w + s1 * b.w
            );
        }

        public static Quaternion Inverse(Quaternion q)
        {
            float norm = q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w;
            if (norm < 1e-6f)
                return new Quaternion(0, 0, 0, 1); // identity fallback for zero-length quaternion

            // Inverse of a unit quaternion is its conjugate
            Quaternion conjugate = new Quaternion(-q.x, -q.y, -q.z, q.w);
            return new Quaternion(
                conjugate.x / norm,
                conjugate.y / norm,
                conjugate.z / norm,
                conjugate.w / norm
            );
        }

        public override string ToString()
        {
            return $"({x}, {y}, {z}, {w})";
        }

    }
}
