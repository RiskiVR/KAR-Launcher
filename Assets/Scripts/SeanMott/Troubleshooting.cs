using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class Troubleshooting : MonoBehaviour
{
    public void InvokeVCInstaller()
    {
        //DirectoryInfo installDir = new DirectoryInfo(System.Environment.CurrentDirectory);
        
        try
        {
           //LaunchBootLoader.Invoke_UpdateVSDistribution();

           // var installer = new Process();
           // installer.StartInfo.FileName = KWStructure.GenerateKWStructure_Directory_Tools(installDir).FullName + "/Windows/VC_redist.x64.exe";
           // installer.StartInfo.WorkingDirectory = installDir.FullName;
           // installer.Start();
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError(e);
            MainUI.instance.sfx.PlayOneShot(MainUI.instance.menu[4]);
            MessageUI.MessageBox(IntPtr.Zero, e.ToString(), "Download Failed!", 0);
        }
    }
}
