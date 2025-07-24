using Coroutine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityGERunner.UnityApplication;
using UnityGERunner.UnityApplication.Prefabs;

namespace UnityGERunner
{
    public class GameEngine
    {
        public static GameEngine instance;
        public GameObject scene;

        private bool hasQuit = false;

        public static readonly Event FixedUpdateEvent = new Event();

        public GameEngine()
        {
            GameEngine.instance = this;
        }

        public void Init()
        {
            Logger.Info($"Starting HSGE");
            Time.lastUpdateTime = Time.time;

            scene = EnvScenePrefab.Create();
            scene.Awake();

            //controller = new Controller();
            //controller.Start();
        }

        public bool UpdateLoop()
        {
            if (hasQuit) return false;
            //controller.Update();


            //foreach (var entity in entities) entity.Update();
            //foreach (var entity in entities) entity.FixedUpdate();
            scene.Update();
            scene.FixedUpdate();
            CoroutineHandler.RaiseEvent(FixedUpdateEvent);
            scene.CleanupDestroyedBehaviors();


            //GEObject.CleanupDeadObjects();

            CoroutineHandler.Tick(Time.deltaTime);
            Time.Tick();

            return true;
        }

        public void Quit()
        {
            hasQuit = true;
        }

        public void SpawnGameObject(GameObject go)
        {
            go.transform.parent = scene.transform;
            go.Awake();
        }
    }
}
