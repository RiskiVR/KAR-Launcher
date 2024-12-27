using System;
using System.IO.Compression;
using System.IO;
using System.Net;
using UnityEngine;

//handles the bootloader and updating various aspects outside the launcher
public class BootLoader : MonoBehaviour
{
    //checks for the bootloader and if not updates it
    static public FileInfo GetBootLoader()
    {
        //gets the bootloader and check if it is missing
        string bootloaderFP = System.Environment.CurrentDirectory + "/Tools/Bootloader.exe";
        bool needsNewBootLoader = !File.Exists(bootloaderFP);

        //check the version
        if(!needsNewBootLoader)
        {
            //invoke bootloader to check it's own versioning
        }

        //if we need a new bootloader
        if(needsNewBootLoader)
        {
            //attempts a download
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.DownloadFile("https://github.com/KARWorkshop/KARBootUpdater/releases/latest/download/Bootloader_Win.zip",
                        System.Environment.CurrentDirectory + "/Tools.zip");

                    //unzips the package
                    ZipFile.ExtractToDirectory(System.Environment.CurrentDirectory + "/Tools.zip", System.Environment.CurrentDirectory + "/Tools");

                    //deletes the package
                    File.Delete(System.Environment.CurrentDirectory + "/Tools.zip");

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

    //performs a fresh install of KARphin

    //checks and installs visual C++ redistribution if needed

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
