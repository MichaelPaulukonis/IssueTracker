using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueTracker.Utils
{
    public static class ExtensionMethods
    {
        public static void SafeForEach<T>(this IEnumerable<T> target, Action<T> action)
        {
            if (target == null) throw new ArgumentNullException("Target", "The IEnumearble<T> passed to the method was null.");
            if (action == null) throw new ArgumentNullException("Action", "The Action passed to the method was null.");
            List<T> temp = target.ToList();
            for (int i = temp.Count - 1; i >= 0; i--)
            {
                if (temp[i] == null) throw new NullReferenceException(string.Format("The Item at index {0} was null.", i));
            }
            temp.ForEach(action);
        }

        public static double PercentOf(this double numerator, double denominator)
        {
            var temp = numerator / denominator;
            if (Double.NaN.CompareTo(temp) == 0) return 0;
            return temp;
        }
    }
}