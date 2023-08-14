using System;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;

namespace HorusUITest.Helper
{
    public static class Output
    {
        public static void Immediately(string text, bool mirrorToDebug = true)
        {
            string message = $"*** {DateTime.Now.ToString("HH:mm:ss")} {text}";
            TestContext.Progress.WriteLine(message);
            if (mirrorToDebug)
                Debug.WriteLine(message);
        }

        public static void TestStep(string text, int depth = 1, bool mirrorToDebug = true)
        {
            StringBuilder message = new StringBuilder();
            for (int i = 0; i < depth; i++)
            {
                message.Append("   ");
            }
            message.Append("Test step: ").Append(text);
            Immediately(message.ToString(), mirrorToDebug);
        }
    }
}
