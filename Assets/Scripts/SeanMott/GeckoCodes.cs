using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GeckoCodes : MonoBehaviour
{
    //gets the Hack Pack codes
    public void DownloadHPCodes()
    {
        DirectoryInfo installDir = new DirectoryInfo(System.Environment.CurrentDirectory);
        KWQICommonInstalls.GetLatest_GekkoCodes_HackPack(KWStructure.GenerateKWStructure_SubDirectory_Clients_User_GameSettings(installDir));
        KWQICommonInstalls.GetLatest_GekkoCodes_HackPack(new DirectoryInfo(KWStructure.GenerateKWStructure_Directory_NetplayClients(installDir).FullName + "/Legacy/User/GameSettings"));
    }

    //gets the Backside codes
    public static void DownloadBSCodes()
    {
        DirectoryInfo installDir = new DirectoryInfo(System.Environment.CurrentDirectory);
        KWQICommonInstalls.GetLatest_GekkoCodes_Backside(KWStructure.GenerateKWStructure_SubDirectory_Clients_User_GameSettings(installDir));
        KWQICommonInstalls.GetLatest_GekkoCodes_Backside(new DirectoryInfo(KWStructure.GenerateKWStructure_Directory_NetplayClients(installDir).FullName + "/Legacy/User/GameSettings"));
    }

    //gets the Vanilla JP codes
    public static void DownloadJPCodes()
    {
        DirectoryInfo installDir = new DirectoryInfo(System.Environment.CurrentDirectory);
        KWQICommonInstalls.GetLatest_GekkoCodes_JP(KWStructure.GenerateKWStructure_SubDirectory_Clients_User_GameSettings(installDir));
        KWQICommonInstalls.GetLatest_GekkoCodes_JP(new DirectoryInfo(KWStructure.GenerateKWStructure_Directory_NetplayClients(installDir).FullName + "/Legacy/User/GameSettings"));
    }

    //gets the Vanilla NA codes
    public static void DownloadNACodes()
    {
        DirectoryInfo installDir = new DirectoryInfo(System.Environment.CurrentDirectory);
        KWQICommonInstalls.GetLatest_GekkoCodes_NA(KWStructure.GenerateKWStructure_SubDirectory_Clients_User_GameSettings(installDir));
        KWQICommonInstalls.GetLatest_GekkoCodes_NA(new DirectoryInfo(KWStructure.GenerateKWStructure_Directory_NetplayClients(installDir).FullName + "/Legacy/User/GameSettings"));
    }
}
