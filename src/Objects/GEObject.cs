using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityGERunner
{
    public enum FindObjectsSortMode
    {
        None
    }

    public class GEObject
    {
        //public static List<GEObject> objects = new List<GEObject>();

        //public GEObject()
        //{
        //objects.Add(this);
        //}

        //public static void CleanupDeadObjects()
        //{
        //objects.CleanupDestroyed();
        //}

        public bool _destroyed = false;
        public bool destroyed
        {
            get { return _destroyed; }
            private set
            {
                _destroyed = value;
            }
        }

        public virtual void Destroy()
        {
            destroyed = true;
        }

        public static void Destroy(GEObject obj)
        {
            obj.Destroy();
        }

        //public static explicit operator bool(GEObject instance)
        //{
        //    return !instance.destroyed;
        //}

        public static implicit operator bool(GEObject instance)
        {
            return instance != null && !instance.destroyed;
        }

        private static bool Compare(GEObject x, GEObject y)
        {
            var xIsNull = (object)x == null || x.destroyed;
            var yIsNull = (object)y == null || y.destroyed;

            if (xIsNull && yIsNull) return true;
            if (xIsNull) return yIsNull;
            if (yIsNull) return xIsNull;

            return (object)x == (object)y;
        }

        public static bool operator ==(GEObject x, GEObject y)
        {
            return Compare(x, y);
        }

        public static bool operator !=(GEObject x, GEObject y)
        {
            return !Compare(x, y);
        }
    }
}
