using System;

namespace Spectrum.UITest
{
    public class TestUtils
    {
        public static string GetApkDirectory(string apkDir)
        {
            var currentDir = System.Reflection.Assembly.GetExecutingAssembly().Location;
            return currentDir + apkDir;
        }
    }
}
