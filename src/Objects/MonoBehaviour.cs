using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityGERunner.UnityApplication;

namespace UnityGERunner
{
    public class MonoBehaviour : GEObject
    {
        public static Dictionary<Type, List<MonoBehaviour>> objectTypes = new Dictionary<Type, List<MonoBehaviour>>();

        public Transform transform;
        public GameObject gameObject;
        private bool hasStarted = false;
        private bool hasAwaked = false;

        private bool _enabled = true;
        public bool enabled
        {
            get { return _enabled && gameObject.activeInHierarchy && !destroyed; }
            set
            {
                var previousState = _enabled;
                _enabled = value;

                if (previousState == false && _enabled) OnEnable();
                if (previousState == true && !_enabled) OnDisable();
            }
        }

        public bool isActiveAndEnabled => enabled;


        public void AttachToGameObject(GameObject go)
        {
            gameObject = go;
            transform = gameObject.transform;
        }

        public void RunAwake()
        {
            if (hasAwaked)
            {
                throw new Exception($"Awake called multiple times on {this}");
            }

            var ownType = this.GetType();
            if (!objectTypes.ContainsKey(ownType)) objectTypes.Add(ownType, new List<MonoBehaviour>());
            objectTypes[ownType].Add(this);

            Awake();
            if (enabled) OnEnable();
            hasAwaked = true;
        }

        public void RunUpdate()
        {
            if (!enabled) return;
            if (!hasStarted)
            {
                Start();
                hasStarted = true;
            }

            Update();
        }

        public void RunFixedUpdate()
        {
            if (!enabled) return;
            if (!hasStarted)
            {
                Start();
                hasStarted = true;
            }

            FixedUpdate();
        }

        public void RunCleanupDestroyed()
        {
            CleanupDestroyed();
        }

        public T GetComponent<T>() where T : MonoBehaviour
        {
            return gameObject.GetComponent<T>();
        }

        public bool TryGetComponent<T>(out T result) where T : MonoBehaviour
        {
            return gameObject.TryGetComponent<T>(out result);
        }

        public T GetComponentInParent<T>() where T : MonoBehaviour
        {
            return gameObject.GetComponentInParent<T>();
        }

        public T[] GetComponentsInChildren<T>() where T : MonoBehaviour
        {
            return gameObject.GetComponentsInChildren<T>();
        }

        public static T[] FindObjectsByType<T>(FindObjectsSortMode sortMode) where T : MonoBehaviour
        {
            return GameObject.FindObjectsByType<T>(sortMode);
        }

        public GameObject Instantiate(GameObject source, Vector3 pos, Quaternion rot)
        {
            return GameObject.Instantiate(source, pos, rot);
        }

        public GameObject Instantiate(GameObject source)
        {
            return GameObject.Instantiate(source);
        }

        public void Destroy(GameObject go)
        {
            GameObject.Destroy(go);
        }

        public override void Destroy()
        {
            if (enabled) OnDisable();
            OnDestroy();
            if (hasAwaked) objectTypes[this.GetType()].Remove(this);
            base.Destroy();
        }

        public override string ToString()
        {
            return GetType().Name;
        }

        protected virtual void Awake() { }
        protected virtual void Start() { }
        protected virtual void Update() { }
        protected virtual void FixedUpdate() { }
        protected virtual void OnDestroy() { }
        protected virtual void OnEnable() { }
        protected virtual void OnDisable() { }
        protected virtual void CleanupDestroyed() { }
    }
}
