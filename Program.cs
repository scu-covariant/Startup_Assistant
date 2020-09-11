using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;

namespace Startup_Assistant
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1 && args[0] == "--admin")
            {
                string currentPath = "\"" + Path.Combine(System.Environment.CurrentDirectory, "Wallpaper_Assistant.exe") + "\"";
                RegistryKey rk = Registry.LocalMachine;
                RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                rk2.SetValue("WallpaperAssistant", currentPath);
                rk2.Close();
                rk.Close();
            }
            else
            {
                System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
                System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
                if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        WorkingDirectory = System.Environment.CurrentDirectory,
                        FileName = Process.GetCurrentProcess().MainModule.FileName,
                        Arguments = "--admin"
                    };
                    Process.Start(startInfo);
                }
                else
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        UseShellExecute = true,
                        WorkingDirectory = System.Environment.CurrentDirectory,
                        FileName = Process.GetCurrentProcess().MainModule.FileName,
                        Arguments = "--admin",
                        Verb = "runas"
                    };
                    Process.Start(startInfo);
                }
                Environment.Exit(0);
            }
        }
    }
}