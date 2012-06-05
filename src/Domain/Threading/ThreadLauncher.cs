using System;
using System.Reflection;
using System.Threading;
using log4net;

namespace Guidelines.Domain.Threading
{
    public class ThreadLauncher
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

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
                lock (Log)
                {
                    Log.Error(ex.Message, ex);
                }
            }
        }
    }
}
