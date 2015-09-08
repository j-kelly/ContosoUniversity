namespace ContosoUniversity.Web.App.Tests
{
    using System;
    using System.Diagnostics;
    using System.Threading;

    public static class IisExpressHelper
    {
        private static Process _iisProcess;
        public static int Port { get; } = 8193;

        public static void StartIis()
        {
            if (_iisProcess == null)
            {
                var thread = new Thread(StartIisExpress) { IsBackground = true };
                thread.Start();
            }

        }
        private static void StartIisExpress()
        {
            var projectPath = $"{Environment.CurrentDirectory}\\..\\..\\..\\..\\ContosoUniversity.Web.App\\obj\\Publish";
            projectPath = System.IO.Path.GetFullPath(projectPath);

            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Normal,
                ErrorDialog = true,
                LoadUserProfile = true,
                CreateNoWindow = false,
                UseShellExecute = false,
                Arguments = string.Format("/path:\"{0}\" /port:{1}", projectPath, Port)
            };

            var programfiles = string.IsNullOrEmpty(startInfo.EnvironmentVariables["programfiles"])
                ? startInfo.EnvironmentVariables["programfiles(x86)"]
                : startInfo.EnvironmentVariables["programfiles"];

            startInfo.FileName = programfiles + "\\IIS Express\\iisexpress.exe";
            try
            {
                _iisProcess = new Process { StartInfo = startInfo };
                _iisProcess.Start();
                _iisProcess.WaitForExit();
            }
            catch
            {
                _iisProcess.CloseMainWindow();
                _iisProcess.Dispose();
            }
        }
        public static void StopIis()
        {
            if (_iisProcess != null)
            {
                _iisProcess.CloseMainWindow();
                _iisProcess.Dispose();
            }
        }
    }
}