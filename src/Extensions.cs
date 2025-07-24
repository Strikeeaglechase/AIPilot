using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityGERunner
{
    public static class Extensions
    {
        public static void CleanupDestroyed<T>(this List<T> list) where T : GEObject
        {
            list.RemoveAll(o => o.destroyed);
        }

        public static T ElementAtOrDefaultSafe<T>(this IEnumerable<T> source, int index)
        {
            var result = source.ElementAtOrDefault(index);
            if (result != null) return result;

            return (T)Activator.CreateInstance(typeof(T));
        }
    }
}
