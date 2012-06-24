using System;
using System.Threading;

namespace Guidelines.Core.Threading
{
    public class ThreadLauncher
    {
        public static Thread Launch(Action handler, bool wait = false)
        {
            return Launch(a => handler(), 1, wait);
        }

        public static Thread Launch<T>(Action<T> handler, T args, bool wait = false)
        {
            var worker = new Thread(() => RunHandler(handler, args));
            worker.Start();

            if (wait)
            {
                worker.Join();
            }

            return worker;
        }

        public static void RunHandler<T>(Action<T> handler, T args)
        {
            try
            {
                handler(args);
            }
            catch (Exception ex)
            {
				LogPortal.Error(ex.Message, ex);
            }
        }
    }
}
