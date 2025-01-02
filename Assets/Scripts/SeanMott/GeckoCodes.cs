using System.IO.Compression;
using System.IO;
using System.Net;
using System;
using UnityEngine;
using UnityEditor.PackageManager;
using UnityEditor.Search;

public class GeckoCodes : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //validates the gecko code directory
    static string ValidateGeckoCodeDirectory()
    {
        //generates the gecko code directory
        string clientsDir = Path.Combine(System.Environment.CurrentDirectory, "Clients");
        if (!Directory.Exists(clientsDir))
            Directory.CreateDirectory(clientsDir);
        string geckoDir = Path.Combine(clientsDir, "StarDust_Player_Settings");
        if (!Directory.Exists(geckoDir))
            Directory.CreateDirectory(geckoDir);
        geckoDir = Path.Combine(geckoDir, "GameSettings");
        if (!Directory.Exists(geckoDir))
            Directory.CreateDirectory(geckoDir);
        return geckoDir;
    }

    //downloads the HP Codes
    public void DownloadHPCodes()
    {
        //attempts a download
        using (WebClient client = new WebClient())
        {
            try
            {
                client.DownloadFile("https://github.com/KARWorkshop/KAR-Gecko-ASM/releases/download/stardust/KHPE01.ini",
                    Path.Combine(ValidateGeckoCodeDirectory(), "KHPE01.ini"));
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
