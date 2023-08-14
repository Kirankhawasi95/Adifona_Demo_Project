using System;
using System.Collections.Generic;
using System.IO;

namespace HorusUITest.Helper
{
    public static class EnvironmentUtils
    {
		private static string GetFirstLine(string text)
		{
			return text.Split('\n')[0];
		}

		public static string LocatePathToAdb()
		{
			return LocatePathToAndroidHome() + "/platform-tools/adb";
		}

		private static string SearchForPathToAdb()
		{
			string SearchAt(string root)
			{
				string result = CommandUtils.ExecuteBash($"find {root} -type f -name adb");
				return GetFirstLine(result);
			}

			List<string> placesToSearch = new List<string>();
			placesToSearch.Add("~/Library/Android");
			placesToSearch.Add("~/Library/Developer/Xamarin");
			placesToSearch.Add("~/Library/Developer");
			placesToSearch.Add("~/Library");
			foreach (var p in placesToSearch)
			{
				string path = SearchAt(p);
				if (path.EndsWith("adb"))
					return path;
			}
			throw new Exception("Unable to locate the adb executable.");
		}

		public static string LocatePathToAndroidHome()
		{
			List<string> directories = new List<string>();
			string user = Environment.UserName;
			directories.Add($"/Users/{user}/Library/Android/sdk");
			directories.Add($"/Users/{user}/Library/Developer/Xamarin/android-sdk-macosx");
			foreach (var d in directories)
			{
				if (Directory.Exists(d))
					return d;
			}

			try
			{
				string path = SearchForPathToAdb();
				path = path.Replace("/platform-tools/adb", string.Empty);
				return path;
			}
			catch (Exception e)
			{
				throw new Exception("Unable to locate ANDROID_HOME.", e);
			}
		}

		public static string LocatePathToJavaHome()
		{
			string path = CommandUtils.ExecuteBash($"find /Library/java/JavaVirtualMachines -type d -name Home");
			foreach (var p in path.Split('\n'))
			{
				if (p.EndsWith("Contents/Home"))
					return p;
			}
			throw new Exception("Unable to locate JAVA_HOME.");
		}
	}
}
