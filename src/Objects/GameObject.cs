using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityGERunner
{
    public class GameObject : GEObject
    {
        public Transform transform;
        public List<MonoBehaviour> components = new List<MonoBehaviour>();
        public string name = "";

        public Dictionary<ulong, MonoBehaviour> componentFileIdMap = new Dictionary<ulong, MonoBehaviour>();
        public Dictionary<ulong, GameObject> gameObjectFileIdMap = new Dictionary<ulong, GameObject>();

        private bool _activeInHierarchy = true;
        private bool hasAwaked = false;
        public bool activeInHierarchy
        {
            get
            {
                return !destroyed && _activeInHierarchy;
            }
            set
            {
                _activeInHierarchy = value;
            }
        }

        public Prefab sourcePrefab;

        public GameObject parent
        {
            get
            {
                return transform.parent?.gameObject;
            }
            set
            {
                transform.parent = value.transform;
            }
        }

        public List<GameObject> children
        {
            get
            {
                return transform.children.Select(c => c.gameObject).ToList();
            }
        }

        public GameObject() : base() { }
        public GameObject(string name) : base() { this.name = name; }

        public void Awake()
        {
            if (hasAwaked || !activeInHierarchy) return;

            hasAwaked = true;
            foreach (var child in children) child.Awake();
            foreach (var comp in components) comp.RunAwake();
        }

        public void Update()
        {
            if (!activeInHierarchy) return;

            foreach (var child in children) child.Update();
            foreach (var comp in components)
            {
                comp.RunUpdate();
            }
        }

        public void FixedUpdate()
        {
            if (!activeInHierarchy) return;

            foreach (var child in children) child.FixedUpdate();
            foreach (var comp in components)
            {
                comp.RunFixedUpdate();
            }
        }

        public void CleanupDestroyedBehaviors()
        {
            components.CleanupDestroyed();
            foreach (var comp in components) comp.RunCleanupDestroyed();
        }

        public void SetActive(bool newActiveState)
        {
            if (activeInHierarchy == newActiveState) return;

            if (newActiveState)
            {
                Logger.Info($"Setting {this} to active");
                activeInHierarchy = true;
                Awake();
            }
            else
            {
                Logger.Info($"Setting {this} to inactive");
                activeInHierarchy = false;
            }
        }

        public static void Destroy(GameObject go)
        {
            foreach (var child in go.children) Destroy(child);
            foreach (var comp in go.components) comp.Destroy();

            GEObject.Destroy(go);
        }

        public T AddComponent<T>() where T : MonoBehaviour
        {
            return AddComponent(typeof(T)) as T;
        }

        public object AddComponent(Type componentType)
        {
            var component = Activator.CreateInstance(componentType);
            if (component is MonoBehaviour monoB)
            {
                AddComponent(monoB);
                return monoB;
            }
            else
            {
                Logger.Error($"Unable to add component {componentType.Name} to {this} as it is not a MonoBehaviour");
                return null;
            }
        }

        public void AddComponent(MonoBehaviour component)
        {
            components.Add(component);
            component.AttachToGameObject(this);

            if (hasAwaked) component.RunAwake();

            if (component is Transform tf)
            {
                if (transform != null)
                {
                    Logger.Error($"Multiple transforms added to single game object: {this}");
                    return;
                }

                transform = tf;
            }
        }

        public void AddComponents(params MonoBehaviour[] monoBehaviours)
        {
            foreach (var monoBehaviour in monoBehaviours) AddComponent(monoBehaviour);
        }

        public T GetComponent<T>() where T : MonoBehaviour
        {
            foreach (var comp in components)
            {
                if (comp is T t) return t;
            }
            return null;
        }

        public bool TryGetComponent<T>(out T result) where T : MonoBehaviour
        {
            var comp = GetComponent<T>();
            result = comp;
            return comp != null;
        }

        public T[] GetComponents<T>() where T : MonoBehaviour
        {
            return components.Where(c => c is T).Select(t => (T)t).ToArray();
        }

        public T GetComponentInParent<T>() where T : MonoBehaviour
        {
            if (TryGetComponent<T>(out var ownComp)) return ownComp;

            if (parent == null) return null;

            return parent.GetComponentInParent<T>();
        }

        public T[] GetComponentsInChildren<T>(bool includeInactive = true) where T : MonoBehaviour
        {
            List<T> components = GetComponents<T>().ToList();
            foreach (var child in children)
            {
                var childComps = child.GetComponentsInChildren<T>();
                foreach (var childComp in childComps)
                {
                    components.Add(childComp);
                }
            }

            return components.ToArray();
        }

        public T[] GetComponentsInParent<T>() where T : MonoBehaviour
        {
            List<T> components = GetComponents<T>().ToList();

            if (parent != null)
            {
                var parentComps = parent.GetComponentsInParent<T>();
                foreach (var parentComp in parentComps) components.Add(parentComp);
            }

            return components.ToArray();
        }

        public static T[] FindObjectsByType<T>(FindObjectsSortMode sortMode) where T : MonoBehaviour
        {
            //var byOld = GameEngine.instance.scene.GetComponentsInChildren<T>();
            var byNew = MonoBehaviour.objectTypes.GetValueOrDefault(typeof(T))?.Where(o => o.enabled).OfType<T>().ToArray() ?? new T[0];

            //if (byNew.Length != byOld.Length)
            //{
            //    Logger.Error($"FindObjectsByType does not work!");
            //}

            return byNew;
        }

        public override string ToString()
        {
            if (name != "") return name;
            return "GameObject";
        }

        public static GameObject Instantiate(GameObject source, Vector3 pos, Quaternion rot)
        {
            var go = Instantiate(source);
            go.transform.position = pos;
            go.transform.rotation = rot;

            return go;
        }

        public static GameObject Instantiate(GameObject source)
        {
            if (source.sourcePrefab == null)
            {
                Logger.Error($"Attempt to Instantiate GameObject that dosn't have a source prefab: {source}");
                return null;
            }

            var go = source.sourcePrefab.CreateInstance();
            go.name += " (Clone)";

            GameEngine.instance.SpawnGameObject(go);

            return go;
        }
    }
}
