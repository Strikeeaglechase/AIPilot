using UnityGERunner;
using Coroutine;
using UnityGERunner.UnityApplication;
﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Recorder
{

    public class EntityKinematicData
    {
        public NetVector position;
        public NetVector velocity;
        public NetVector rotation;
        public NetVector pyr;
        public int entityId;

        public void WriteBytes(MemoryStream stream)
        {
            position.WriteBytes(stream);
            velocity.WriteBytes(stream);
            rotation.WriteBytes(stream);
            pyr.WriteBytes(stream);
            stream.Write(BitConverter.GetBytes(entityId));
        }
    }

    public class RecordedFrame
    {
        public List<EntityKinematicData> motion = new List<EntityKinematicData>();
        public List<RecorderEvent> events = new List<RecorderEvent>();
        public List<string> logs = new List<string>();

        public ulong time;

        public RecordedFrame(float t)
        {
            time = (ulong)Mathf.Floor(t * 1000);
        }

        public MemoryStream GetBytes()
        {
            var memStream = new MemoryStream();
            memStream.Write(BitConverter.GetBytes(time));

            memStream.Write(BitConverter.GetBytes(motion.Count));
            foreach (var m in motion) m.WriteBytes(memStream);

            memStream.Write(BitConverter.GetBytes(events.Count));
            foreach (var e in events) e.WriteBytes(memStream);

            memStream.Write(BitConverter.GetBytes(logs.Count));
            foreach (var log in logs)
            {
                memStream.Write(BitConverter.GetBytes(log.Length));
                memStream.Write(UTF8Encoding.UTF8.GetBytes(log));
            }

            return memStream;
            //return memStream.ToArray();
        }
    }

    struct WrappedState
    {
        public OutboundState state;
        public int aiId;
    }
}
