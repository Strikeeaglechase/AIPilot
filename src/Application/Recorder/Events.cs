using UnityGERunner;
using Coroutine;
using UnityGERunner.UnityApplication;
﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Recorder
{
    public enum RecorderEventType
    {
        EntityInit,
        EntityDelete,
        Chaff,
        Flare,
        Win,
        DebugLine,
        DebugSphere,
        RemoveDebugShape
    }

    public abstract class RecorderEvent
    {
        public RecorderEventType type;

        public virtual void WriteBytes(MemoryStream stream)
        {
            stream.WriteByte((byte)type);
        }
    }

    public class EntityInit : RecorderEvent
    {
        public int entityId;
        public string path;
        public string name;
        public Team team;

        public EntityInit(int entityId, string path, string name, Team team)
        {
            type = RecorderEventType.EntityInit;
            this.entityId = entityId;
            this.path = path;
            this.name = name;
            this.team = team;
        }

        public override void WriteBytes(MemoryStream stream)
        {
            base.WriteBytes(stream);
            stream.Write(BitConverter.GetBytes(entityId));
            stream.WriteByte((byte)team);

            stream.WriteByte((byte)path.Length);
            stream.Write(UTF8Encoding.UTF8.GetBytes(path));

            stream.WriteByte((byte)name.Length);
            stream.Write(UTF8Encoding.UTF8.GetBytes(name));
        }
    }

    public class EntityDelete : RecorderEvent
    {
        public int entityId;

        public EntityDelete(int entityId)
        {
            type = RecorderEventType.EntityDelete;
            this.entityId = entityId;
        }

        public override void WriteBytes(MemoryStream stream)
        {
            base.WriteBytes(stream);
            stream.Write(BitConverter.GetBytes(entityId));
        }
    }

    public class FlareEvent : RecorderEvent
    {
        public int entityId;

        public FlareEvent(int entityId)
        {
            type = RecorderEventType.Flare;
            this.entityId = entityId;
        }

        public override void WriteBytes(MemoryStream stream)
        {
            base.WriteBytes(stream);
            stream.Write(BitConverter.GetBytes(entityId));
        }
    }

    public class ChaffEvent : RecorderEvent
    {
        public int entityId;

        public ChaffEvent(int entityId)
        {
            type = RecorderEventType.Chaff;
            this.entityId = entityId;
        }

        public override void WriteBytes(MemoryStream stream)
        {
            base.WriteBytes(stream);
            stream.Write(BitConverter.GetBytes(entityId));
        }
    }

    public class WinEvent : RecorderEvent
    {
        public Team team;

        public WinEvent(Team team)
        {
            type = RecorderEventType.Win;
            this.team = team;
        }

        public override void WriteBytes(MemoryStream stream)
        {
            base.WriteBytes(stream);
            stream.WriteByte((byte)team);
        }
    }

    public class DebugLine : RecorderEvent
    {
        public NetVector? start;
        public NetVector? end;
        public NetColor? color;
        public int id;

        public DebugLine(int id)
        {
            type = RecorderEventType.DebugLine;
            this.id = id;
        }

        public override void WriteBytes(MemoryStream stream)
        {
            base.WriteBytes(stream);
            stream.Write(BitConverter.GetBytes(id));

            stream.WriteByte(start.HasValue ? (byte)1 : (byte)0);
            if (start.HasValue) start.Value.WriteBytes(stream);

            stream.WriteByte(end.HasValue ? (byte)1 : (byte)0);
            if (end.HasValue) end.Value.WriteBytes(stream);

            stream.WriteByte(color.HasValue ? (byte)1 : (byte)0);
            if (color.HasValue) color.Value.WriteBytes(stream);
        }
    }

    public class DebugSphere : RecorderEvent
    {
        public NetVector? pos;
        public int? size;
        public NetColor? color;
        public int id;

        public DebugSphere(int id)
        {
            type = RecorderEventType.DebugLine;
            this.id = id;
        }

        public override void WriteBytes(MemoryStream stream)
        {
            base.WriteBytes(stream);
            stream.Write(BitConverter.GetBytes(id));

            stream.WriteByte(pos.HasValue ? (byte)1 : (byte)0);
            if (pos.HasValue) pos.Value.WriteBytes(stream);

            stream.WriteByte(size.HasValue ? (byte)1 : (byte)0);
            if (size.HasValue) stream.Write(BitConverter.GetBytes(size.Value));

            stream.WriteByte(color.HasValue ? (byte)1 : (byte)0);
            if (color.HasValue) color.Value.WriteBytes(stream);
        }

    }

    public class RemoveDebugShape : RecorderEvent
    {
        public int id;

        public RemoveDebugShape(int id)
        {
            type = RecorderEventType.RemoveDebugShape;
            this.id = id;
        }

        public override void WriteBytes(MemoryStream stream)
        {
            base.WriteBytes(stream);
            stream.Write(BitConverter.GetBytes(id));
        }
    }
}
