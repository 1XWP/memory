using System;
using System.Threading;

namespace Assets.Scripts
{
    /// <summary>
    /// Static class for thread safe random
    /// </summary>
    public static class ThreadSafeRandom
    {
        [ThreadStatic]
        private static System.Random Local;

        public static System.Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new System.Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }
}