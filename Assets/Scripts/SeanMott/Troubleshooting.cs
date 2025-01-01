using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class Troubleshooting : MonoBehaviour
{
    //invokes the vs installer for the redistribution
    public void InvokeVCInstaller()
    {
        try
        {
            BootLoader.InvokeVSCppRedistribution();
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError(e);
            MainUI.instance.sfx.PlayOneShot(MainUI.instance.menu[4]);
            MessageUI.MessageBox(IntPtr.Zero, e.ToString(), "Download Failed!", 0);
        }
    }

    //redownloads the tools directory for the bootloader and vs installer
    public void GetLatestUpdater()
    {
        //if the tools directory exists, delete it
        if (Directory.Exists(System.Environment.CurrentDirectory + "/Tools"))
            Directory.Delete(System.Environment.CurrentDirectory + "/Tools", true);

        //get the latest bootloader
        BootLoader.GetBootLoader();
    }
}
