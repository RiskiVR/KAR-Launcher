using System;
using System.IO.Compression;
using System.IO;
using System.Net;
using UnityEngine;

//handles the bootloader and updating various aspects outside the launcher
public class BootLoader
{
    //checks for the bootloader and if not updates it
    static public FileInfo GetBootLoader()
    {
        //gets the bootloader and check if it is missing
        string bootloaderFP = System.Environment.CurrentDirectory + "/Tools/Bootloader.exe";
        bool needsNewBootLoader = !File.Exists(bootloaderFP);

        //if we need a new bootloader
        if(needsNewBootLoader)
        {
            //attempts a download
            using (WebClient client = new WebClient())
            {
                try
                {
                    string zipFP = System.Environment.CurrentDirectory + "/Tools.zip";
                    client.DownloadFile("https://github.com/KARWorkshop/KARBootUpdater/releases/latest/download/Bootloader_Win.zip",
                        zipFP);

                    //unzips the package
                    ZipFile.ExtractToDirectory(zipFP, System.Environment.CurrentDirectory + "/Tools");

                    //deletes the package
                    File.Delete(zipFP);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                    return new FileInfo("");
                }
            }
        }

        return new FileInfo(bootloaderFP);
    }

    //updates the launcher
    public static void UpdateLauncher()
    {
        FileInfo bootloader = GetBootLoader();

        System.Diagnostics.Process process = new System.Diagnostics.Process();
        process.StartInfo.FileName = bootloader.FullName;
        process.StartInfo.WorkingDirectory = System.Environment.CurrentDirectory;
        process.StartInfo.Arguments = "-ver " + Application.version + " -installDir " + System.Environment.CurrentDirectory + " -boot -launcher";
        process.Start();
        process.WaitForExit();
    }

    //performs a fresh install of KARphin
    public static void PerformFreshInstall()
    {
        FileInfo bootloader = GetBootLoader();

        System.Diagnostics.Process process = new System.Diagnostics.Process();
        process.StartInfo.FileName = bootloader.FullName;
        process.StartInfo.WorkingDirectory = System.Environment.CurrentDirectory;
        process.StartInfo.Arguments = "-installDir " + System.Environment.CurrentDirectory + " -fresh";
        process.Start();
        process.WaitForExit();
    }

    //checks and installs visual C++ redistribution if needed
}
