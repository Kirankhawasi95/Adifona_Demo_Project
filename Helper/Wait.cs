using System;
using System.Diagnostics;
using System.Threading;

namespace HorusUITest.Helper
{
    public static class Wait
    {
        public static void UntilTrue(Func<bool> condition, TimeSpan timeout, string exceptionMessage = null)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            if (condition.Invoke())
                return;
            while (watch.Elapsed < timeout)
            {
                Thread.Sleep(100);
                if (condition.Invoke())
                    return;
            }
            if (exceptionMessage != null)
                throw new TimeoutException(exceptionMessage);
            else
                throw new TimeoutException();
        }

        public static void UntilFalse(Func<bool> condition, TimeSpan timeout, string exceptionMessage = null)
        {
            UntilTrue(() => !condition(), timeout, exceptionMessage);
        }

        public static bool For(Func<bool> earlyReturnCondition, TimeSpan maximumDuration)
        {
            try
            {
                UntilTrue(earlyReturnCondition, maximumDuration);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
