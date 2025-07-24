using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityGERunner
{
    public enum RigidbodyInterpolation
    {
        Interpolate = 0
    }

    public class Rigidbody : MonoBehaviour
    {
        public float mass = 1;
        public float drag = 0;
        public float angularDamping = 0;
        public float angularDrag
        {
            get { return angularDamping; }
            set { angularDamping = value; }
        }

        public bool isKinematic = false;
        public bool useGravity = true;
        public RigidbodyInterpolation interpolation = RigidbodyInterpolation.Interpolate;
        public bool automaticCenterOfMass = false;

        public Vector3 centerOfMass;
        public Vector3 worldCenterOfMass
        {
            get
            {
                return transform.TransformPoint(centerOfMass);
            }
        }

        public Vector3 angularVelocity = Vector3.zero;
        public Vector3 linearVelocity = Vector3.zero;
        public Vector3 inertiaTensor = Vector3.one;
        public Vector3 position => gameObject.transform.position;
        public Quaternion rotation => gameObject.transform.rotation;

        protected override void FixedUpdate()
        {
            if (isKinematic) return;

            if (useGravity)
            {
                linearVelocity += Physics.gravity * Time.fixedDeltaTime;
            }

            transform.position += linearVelocity * Time.fixedDeltaTime;
            angularVelocity *= 1 - angularDamping * Time.fixedDeltaTime;
            transform.rotation *= Quaternion.Euler(angularVelocity * Time.fixedDeltaTime * Mathf.Rad2Deg);
        }

        public void MovePosition(Vector3 pos)
        {
            transform.position = pos;
        }

        public void MoveRotation(Quaternion rot)
        {
            transform.rotation = rot;
        }

        public Vector3 GetPointVelocity(Vector3 atPoint)
        {
            return linearVelocity;
        }

        public void AddForceAtPosition(Vector3 force, Vector3 position)
        {
            float dt = Time.fixedDeltaTime;

            linearVelocity += (force / mass) * dt;


            Vector3 r = position - transform.position;
            Vector3 torque = Vector3.Cross(r, force);

            AddRelativeTorque(transform.rotation * torque);
        }

        public void AddRelativeForce(Vector3 localForce)
        {
            float dt = Time.fixedDeltaTime;

            // Convert force from local space to world space
            Vector3 worldForce = rotation * localForce;

            // Apply force like a standard AddForce
            linearVelocity += (worldForce / mass) * dt;
        }

        public void AddRelativeTorque(Vector3 torque)
        {
            var localTorque = Quaternion.Inverse(transform.rotation) * torque;

            var invInerta = new Vector3(
                1f / inertiaTensor.x,
                1f / inertiaTensor.y,
                1f / inertiaTensor.z
                );

            Vector3 angularAccelLocal = new Vector3(
                localTorque.x * invInerta.x,
                localTorque.y * invInerta.y,
                localTorque.z * invInerta.z
            );


            Vector3 angularAcceleration = transform.rotation * angularAccelLocal;
            angularVelocity += angularAcceleration * Time.fixedDeltaTime;

            /*
            float dt = Time.fixedDeltaTime;

            Vector3 worldTorque = rotation * localTorque;
            Vector3 invInertiaLocal = new Vector3(
                1f / inertiaTensor.x,
                1f / inertiaTensor.y,
                1f / inertiaTensor.z
            );

            Vector3 torqueLocal = Quaternion.Inverse(rotation) * worldTorque;

            Vector3 angularAccelLocal = new Vector3(
                torqueLocal.x * invInertiaLocal.x,
                torqueLocal.y * invInertiaLocal.y,
                torqueLocal.z * invInertiaLocal.z
            );

            Vector3 angularAcceleration = rotation * angularAccelLocal;

            angularVelocity += angularAcceleration * dt;
            */
        }
    }
}
