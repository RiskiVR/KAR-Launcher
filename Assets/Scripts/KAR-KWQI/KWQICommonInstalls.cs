/*
0.3.0 implementation of how the KAR Workshop Quick Install format.

The spec for the project and latest version can be found at the Github.
https://github.com/SeanMott/KAR-KWQI

*/

//defines a class for common downloads of the core packages

using System;
using System.IO;
using System.IO.Compression;
using System.Net;

class KWQICommonInstalls
{
    //installs the latest KARphin
    public static bool GetLatest_KARphin(DirectoryInfo installTarget)
    {
        //downloads the latest KARphin
        FileInfo archive = KWQIWebClient.Download_Archive_Windows(installTarget,
        "https://github.com/SeanMott/KARphin_Modern/releases/latest/download/KARphin.zip",
        "KARphin");

        //unpacks it
        DirectoryInfo uncompressed = new DirectoryInfo(installTarget.FullName + "/KARphin");
        ZipFile.ExtractToDirectory(archive.FullName, uncompressed.FullName, true);

        //moves the contents into the target directory
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);

        //clean up
        archive.Delete();
        uncompressed.Delete(true);

        return true;
    }

    //installs the latest KARphin Dev
    public static bool GetLatest_KARphinDev(DirectoryInfo installTarget)
    {
        //downloads the latest KARphin
        FileInfo archive = KWQIWebClient.Download_Archive_Windows(installTarget,
        "https://github.com/SeanMott/KARphin_Modern/releases/latest/download/KARphinDev.zip",
        "KARphinDev");

        //unpacks it
        DirectoryInfo uncompressed = new DirectoryInfo(installTarget.FullName + "/KARphinDev");
        ZipFile.ExtractToDirectory(archive.FullName, uncompressed.FullName, true);
        
        //moves the contents into the target directory
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);

        //clean up
        archive.Delete();
        uncompressed.Delete(true);

        return true;
    }

    //installs the latest Hack Pack Gekko Codes
    public static bool GetLatest_GekkoCodes_HackPack(DirectoryInfo installTarget, bool isKARphinModern)
    {
        //downloads the latest Legacy codes
        if (!isKARphinModern)
        {
            KWQIWebClient.Download_GekkoCodes_Windows(installTarget,
                "https://github.com/KARWorkshop/KAR-Gecko-ASM/releases/download/L1.0/KHPE01.ini",
                "KHPE01");
        }

        //modern codes
        else
        {
            KWQIWebClient.Download_GekkoCodes_Windows(installTarget,
                "https://github.com/KARWorkshop/KAR-Gecko-ASM/releases/download/K1.0/KHPE01.ini",
                "KHPE01");
        }
        
        return true;
    }

    //installs the latest Backside Gekko Codes
    public static bool GetLatest_GekkoCodes_Backside(DirectoryInfo installTarget, bool isKARphinModern)
    {
        //downloads the latest Legacy
        if (!isKARphinModern)
        {
            KWQIWebClient.Download_GekkoCodes_Windows(installTarget,
                "https://github.com/KARWorkshop/KAR-Gecko-ASM/releases/download/L1.0/KBSE01.ini",
                "KBSE01");
        }
        else
        {
            KWQIWebClient.Download_GekkoCodes_Windows(installTarget,
                "https://github.com/KARWorkshop/KAR-Gecko-ASM/releases/download/K1.0/KBSE01.ini",
                "KBSE01");
        }

        return true;
    }

    //installs the latest Ignition KAR Gekko Codes
    public static bool GetLatest_GekkoCodes_Ignition(DirectoryInfo installTarget)
    {
        //downloads the latest KARphin
        KWQIWebClient.Download_GekkoCodes_Windows(installTarget,
            "https://github.com/KARWorkshop/KAR-Gecko-ASM/releases/download/K1.0/SLAV01.ini",
            "SLAV01");
        return true;
    }

    //installs the latest JP KAR Gekko Codes
    public static bool GetLatest_GekkoCodes_JP(DirectoryInfo installTarget)
    {
        //downloads the latest KARphin
        KWQIWebClient.Download_GekkoCodes_Windows(installTarget,
            "https://github.com/KARWorkshop/KAR-Gecko-ASM/releases/download/K1.0/GKYP01.ini",
			"GKYP01");
        return true;
    }

    //installs the latest NA KAR Gekko Codes
    public static bool GetLatest_GekkoCodes_NA(DirectoryInfo installTarget)
    {
        //downloads the latest KARphin
        KWQIWebClient.Download_GekkoCodes_Windows(installTarget,
            "https://github.com/KARWorkshop/KAR-Gecko-ASM/releases/download/K1.0/GKYE01.ini",
			"GKYE01");
        return true;
    }

    //installs the latest Skin Packs
    public static bool GetLatest_SkinPacks(DirectoryInfo installTarget)
    {
        //downloads the latest KARphin
        FileInfo archive = KWQIWebClient.Download_Archive_Windows(installTarget,
            "https://github.com/KARWorkshop/Skin-Packs/releases/latest/download/SkinPacks_OldB2Method.zip",
            "SkinPacks");

        //unpacks it
        DirectoryInfo uncompressed = new DirectoryInfo(installTarget.FullName + "/SkinPacks");
        ZipFile.ExtractToDirectory(archive.FullName, uncompressed.FullName, true);

        //moves the contents into the target directory
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);

        //clean up
        archive.Delete();
        uncompressed.Delete(true);

        return true;
    }

    //installs the latest KARDont
    public static bool GetLatest_KARDont(DirectoryInfo installTarget)
    {
        //downloads the latest KARphin
        FileInfo archive = KWQIWebClient.Download_Archive_Windows(installTarget,
            "https://github.com/SeanMott/KARDont/releases/latest/download/KARDont.zip",
            "KARDont");

        //unpacks it
        DirectoryInfo uncompressed = new DirectoryInfo(installTarget.FullName);
        ZipFile.ExtractToDirectory(archive.FullName, uncompressed.FullName, true);

        //clean up
        archive.Delete();

        return true;
    }

    //installs the latest Client Deps
    public static bool GetLatest_ClientDeps(DirectoryInfo installTarget)
    {
        //downloads the latest KARphin
        FileInfo archive = KWQIWebClient.Download_Archive_Windows(installTarget,
            "https://github.com/KARWorkshop/ClientDeps/releases/latest/download/ClientDeps.zip",
			"ClientDeps");

        //unpacks it
        DirectoryInfo uncompressed = new DirectoryInfo(installTarget.FullName + "/ClientDeps");
        ZipFile.ExtractToDirectory(archive.FullName, uncompressed.FullName, true);

        //moves the contents into the target directory
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);

        //clean up
        archive.Delete();
        uncompressed.Delete(true);

        return true;
    }

    //installs the latest KAR Updater
    public static void GetLatest_KARUpdater(DirectoryInfo installTarget)
    {
        //downloads the latest KARphin
        FileInfo archive = KWQIWebClient.Download_Archive_Windows(installTarget,
			"https://github.com/RiskiVR/KAR-Updater/releases/latest/download/KARUpdater.zip",
			"KARUpdater");

        //unpacks it
        DirectoryInfo uncompressed = new DirectoryInfo(installTarget.FullName + "/KARUpdater");
        ZipFile.ExtractToDirectory(archive.FullName, uncompressed.FullName, true);

        //moves the contents into the target directory
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);

        //clean up
        archive.Delete();
        uncompressed.Delete(true);
    }

    //installs the latest KAR Launcher
    public static bool GetLatest_KARLauncher(DirectoryInfo installTarget)
    {
        //downloads the latest KARphin
        FileInfo archive = KWQIWebClient.Download_Archive_Windows(installTarget,
            "https://github.com/RiskiVR/KAR-Launcher/releases/latest/download/KARLauncher.zip",
            "KARLauncher");

        //unpacks it
        DirectoryInfo uncompressed = new DirectoryInfo(installTarget.FullName + "/KARLauncher");
        ZipFile.ExtractToDirectory(archive.FullName, uncompressed.FullName, true);

        //moves the contents into the target directory
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);

        //clean up
        archive.Delete();
        uncompressed.Delete(true);
        return true;
    }

    //installs the latest Boot Updater
    public static bool GetLatest_BootUpdater(DirectoryInfo installTarget)
    {
        //downloads the latest KARphin
        FileInfo archive = KWQIWebClient.Download_Archive_Windows(installTarget,
            "https://github.com/KARWorkshop/KARBootUpdater/releases/latest/download/KAR_BootUpdate.zip",
            "KARBootUpater");

        //unpacks it
        DirectoryInfo uncompressed = new DirectoryInfo(installTarget.FullName + "/KARBootUpdater");
        ZipFile.ExtractToDirectory(archive.FullName, uncompressed.FullName, true);

        //moves the contents into the target directory
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);

        //clean up
        archive.Delete();
        uncompressed.Delete(true);
        return true;
    }

    //installs the latest Tools
    public static bool GetLatest_Tools(DirectoryInfo installTarget)
    {
        //downloads the latest KARphin
        FileInfo archive = KWQIWebClient.Download_Archive_Windows(installTarget,
            "https://github.com/KARWorkshop/Tools/releases/latest/download/Tools.zip",
			"Tools");

        //unpacks it
        DirectoryInfo uncompressed = new DirectoryInfo(installTarget.FullName + "/Tools");
        ZipFile.ExtractToDirectory(archive.FullName, uncompressed.FullName, true);

        //moves the contents into the target directory
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);

        //clean up
        archive.Delete();
        uncompressed.Delete(true);

        return true;
    }
}