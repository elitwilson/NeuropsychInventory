using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeuropsychInventory.Utilities {

    //Create AddRange extension method that works with IList
    public static class ExtensionMethods {
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> items) {
            if (list == null) throw new ArgumentNullException("list");
            if (items == null) throw new ArgumentNullException("items");
            foreach (T item in items) list.Add(item);
        }
    }
}