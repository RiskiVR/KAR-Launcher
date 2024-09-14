using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class Troubleshooting : MonoBehaviour
{
    public void GetLatestUpdater()
    {
        DirectoryInfo installDir = new DirectoryInfo(System.Environment.CurrentDirectory);
        
        try
        {
            //checks for old Updater with a space, delete it
            FileInfo oldUpdater = new FileInfo(installDir.FullName + "/KAR Updater.exe");
            if(oldUpdater.Exists)
            {
                oldUpdater.Delete();
                DirectoryInfo oldData = new DirectoryInfo(installDir.FullName + "/KAR Updater_Data");
                if(oldData.Exists) oldData.Delete(true);
            }

            //downloads
            KWQICommonInstalls.GetLatest_KARUpdater(installDir);
		
            //runs the Updater
            var updater = new System.Diagnostics.Process();
            updater.StartInfo.FileName = installDir.FullName + "/KAR Updater.exe";
            updater.StartInfo.WorkingDirectory = installDir.FullName;
            updater.Start();
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError(e);
            MainUI.instance.sfx.PlayOneShot(MainUI.instance.menu[4]);
            MessageUI.MessageBox(IntPtr.Zero, e.ToString(), "Download Failed!", 0);
        }
    }
    
    public void InvokeVCInstaller()
    {
        DirectoryInfo installDir = new DirectoryInfo(System.Environment.CurrentDirectory);
        
        try
        {
            var installer = new Process();
            installer.StartInfo.FileName = KWStructure.GenerateKWStructure_Directory_Tools(installDir).FullName + "/Windows/VC_redist.x64.exe";
            installer.StartInfo.WorkingDirectory = installDir.FullName;
            installer.Start();
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError(e);
            MainUI.instance.sfx.PlayOneShot(MainUI.instance.menu[4]);
            MessageUI.MessageBox(IntPtr.Zero, e.ToString(), "Download Failed!", 0);
        }
    }
}
