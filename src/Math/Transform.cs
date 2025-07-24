using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityGERunner
{
    public class Transform : MonoBehaviour
    {
        private Transform _parent;

        public Transform parent
        {
            get { return _parent; }
            set
            {
                GetGlobalTransform(out var worldPosition, out var worldRotation);

                if (_parent != null) _parent.children.Remove(this);

                if (value == null) _parent = GameEngine.instance.scene.transform;
                else _parent = value;

                _parent.children.Add(this);

                position = worldPosition;
                rotation = worldRotation;

                //var delta = (worldPosition - position).sqrMagnitude;
                //if (delta > 1 || float.IsNaN(delta))
                //    Logger.Info($"{worldPosition} -> {position}");
            }
        }

        public List<Transform> children = new List<Transform>();

        public Vector3 position
        {
            get
            {
                GetGlobalTransform(out var pos, out var rot);
                return pos;
            }
            set
            {
                if (parent != null) localPosition = parent.InverseTransformPoint(value);
                else localPosition = value;
            }
        }
        public Quaternion rotation
        {
            get
            {
                GetGlobalTransform(out var pos, out var rot);
                return rot;
            }
            set
            {
                if (parent != null) localRotation = parent.InverseTransformRotation(value);
                else localRotation = value;

                //localRotation = InverseTransformRotation(value);
            }
        }
        public Vector3 scale = Vector3.one;

        public Vector3 localPosition = Vector3.zero;
        public Quaternion localRotation = Quaternion.identity;
        public Vector3 localEulerAngles
        {
            get { return localRotation.eulerAngles; }
            set { localRotation = Quaternion.Euler(value); }
        }

        public Vector3 eulerAngles
        {
            get
            {
                return rotation.eulerAngles;
            }
            set
            {
                rotation = Quaternion.Euler(value);
            }
        }

        public Vector3 right
        {
            get
            {
                return rotation * Vector3.right;
            }
        }

        public Vector3 up
        {
            get
            {
                return rotation * Vector3.up;
            }

        }

        public Vector3 forward
        {
            get
            {
                return rotation * Vector3.forward;
            }
        }

        protected override void CleanupDestroyed()
        {
            children.CleanupDestroyed();
        }

        public void SetParent(Transform parent)
        {
            this.parent = parent;
        }

        public void GetGlobalTransform(out Vector3 worldPosition, out Quaternion worldRotation)
        {
            if (parent == null)
            {
                worldPosition = localPosition;
                worldRotation = localRotation;
            }
            else
            {
                // Recursively get parent world transform
                parent.GetGlobalTransform(out Vector3 parentPos, out Quaternion parentRot);

                // Apply rotation and scale to local position
                Vector3 scaledLocalPos = Vector3.Scale(localPosition, parent.scale); // apply parent scale
                Vector3 rotatedLocalPos = parentRot * scaledLocalPos;

                worldPosition = parentPos + rotatedLocalPos;
                worldRotation = parentRot * localRotation;
            }
        }

        public Vector3 TransformPoint(Vector3 localPoint)
        {
            // Apply scale
            Vector3 scaled = new Vector3(
                localPoint.x * scale.x,
                localPoint.y * scale.y,
                localPoint.z * scale.z
            );

            // Apply rotation
            Vector3 rotated = rotation * scaled;

            // Apply translation
            return position + rotated;
        }

        public Vector3 InverseTransformPoint(Vector3 worldPosition)
        {
            // Offset from parent position
            Vector3 offset = worldPosition - position;

            // Undo rotation
            Quaternion invRotation = Quaternion.Inverse(rotation);
            Vector3 unrotated = invRotation * offset;

            // Undo scale
            Vector3 localPosition = new Vector3(
                unrotated.x / scale.x,
                unrotated.y / scale.y,
                unrotated.z / scale.z
            );

            return localPosition;
        }

        public Quaternion InverseTransformRotation(Quaternion worldRotation)
        {
            // Undo parent rotation
            return Quaternion.Inverse(rotation) * worldRotation;
        }

        public Vector3 InverseTransformDirection(Vector3 worldDirection)
        {
            // Only inverse of rotation is needed for directions
            Quaternion inverseRotation = Quaternion.Inverse(rotation);
            return inverseRotation * worldDirection;
        }

        public Vector3 TransformVector(Vector3 localVector)
        {
            // Apply local scale
            Vector3 scaled = new Vector3(
                localVector.x * scale.x,
                localVector.y * scale.y,
                localVector.z * scale.z
            );

            // Then apply rotation
            return rotation * scaled;
        }

        public Vector3 InverseTransformVector(Vector3 worldVector)
        {
            // Undo rotation
            Vector3 unrotated = Quaternion.Inverse(rotation) * worldVector;

            // Undo scale
            return new Vector3(
                unrotated.x / scale.x,
                unrotated.y / scale.y,
                unrotated.z / scale.z
            );
        }

        public void LookAt(Vector3 target)
        {
            LookAt(target, Vector3.up);
        }

        public void LookAt(Vector3 target, Vector3 up)
        {
            Vector3 forward = (target - position).normalized;

            if (forward.sqrMagnitude < 1e-6f)
                return; // Target is too close; cannot lookAt

            rotation = Quaternion.LookRotation(forward, up);
        }

        public Transform Clone()
        {
            var newTransform = new Transform();
            newTransform.position = new Vector3(position.x, position.y, position.z);
            newTransform.rotation = new Quaternion(rotation.x, rotation.y, rotation.z, rotation.w);
            return newTransform;
        }
    }
}
