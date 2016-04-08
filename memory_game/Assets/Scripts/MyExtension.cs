using System.Collections.Generic;

namespace Assets.Scripts
{   
    /// <summary>
    ///  Extension Class providing custom methods for Generics used in project
    /// </summary>
    static class MyExtensions
    {   
        /// <summary>
        /// Extension method for List 
        /// </summary>
        /// <typeparam name="T">Type of list elements</typeparam>
        /// <param name="list">List name</param>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}