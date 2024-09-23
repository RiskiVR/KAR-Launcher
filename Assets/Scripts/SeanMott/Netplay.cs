using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Netplay : MonoBehaviour
{
	//the index array for which Client to launch
	public static string[] clientNames = {
		"KARphin",
		"KARphin_Legacy",
		"KARphinDev"
	};
	public static int currentClient = 0;

	//attempts to boot the chosen client
	void BootClient(string args = "")
	{
		DirectoryInfo installDir = new DirectoryInfo(System.Environment.CurrentDirectory);

  //      //validate the Replay folder, for storing the game replays exists
  //      DirectoryInfo replayDir = new DirectoryInfo(installDir + "/Replays");
		//if (!replayDir.Exists)
		//	replayDir.Create();

        try
		{
			//checks if the client exists
			DirectoryInfo clientsFolder = KWStructure.GenerateKWStructure_Directory_NetplayClients(installDir);

			//gets the correct client
			FileInfo client = new FileInfo(clientsFolder.FullName);
			switch (clientNames[currentClient])
			{
				case "KARphin":
					client = new FileInfo(clientsFolder.FullName + "/KARphin.exe");
					if (!client.Exists) //if it doesn't exist we download it
					{
                        KWQICommonInstalls.GetLatest_KARphin(KWStructure.GenerateKWStructure_Directory_NetplayClients(installDir));

                        client = new FileInfo(clientsFolder.FullName + "/KARphin.exe");
                        if (!client.Exists)
                        {
                            System.Console.WriteLine($"{client.FullName}");
                            System.Console.WriteLine($"{clientNames[currentClient]} does not exist, can not boot.");
                            return;
                        }
                    }
					
                    break;

                case "KARphin_Legacy":
                    client = new FileInfo(clientsFolder.FullName + "/Legacy/KARphin_Legacy.exe");
                    if (!client.Exists) //if it doesn't exist we download it
                    {
                        System.Console.WriteLine($"{client.FullName}");
                        System.Console.WriteLine($"{clientNames[currentClient]} does not exist, can not boot.");
                        return;
                    }
                    break;

                case "KARphinDev":
                    client = new FileInfo(clientsFolder.FullName + "/KARphinDev.exe");
                    if (!client.Exists) //if it doesn't exist we download it
                    {
                        KWQICommonInstalls.GetLatest_KARphinDev(KWStructure.GenerateKWStructure_Directory_NetplayClients(installDir));

                        client = new FileInfo(clientsFolder.FullName + "/KARphinDev.exe");
                        if (!client.Exists)
                        {
                            System.Console.WriteLine($"{client.FullName}");
                            System.Console.WriteLine($"{clientNames[currentClient]} does not exist, can not boot.");
                            return;
                        }
                    }
                    break;
            }

			//boots the client
			var dolphin = new Process();
			dolphin.StartInfo.FileName = client.FullName;
			dolphin.StartInfo.Arguments = (clientNames[currentClient] == "KARphin_Legacy" ? "" : args);
			dolphin.StartInfo.WorkingDirectory = (clientNames[currentClient] == "KARphin_Legacy" ? clientsFolder.FullName + "/Legacy" : clientsFolder.FullName);

            dolphin.Start();
		}
		catch (Exception e)
		{
			UnityEngine.Debug.LogError(e);
			MainUI.instance.sfx.PlayOneShot(MainUI.instance.menu[4]);
			MessageUI.MessageBox(IntPtr.Zero, e.ToString(), "Download Failed!", 0);
		}
	}

	//boots client for configuring
	public void _on_configure_pressed()
	{
		BootClient();
	}


	//resets the client data
	public void _on_reset_client_pressed()
	{
		DirectoryInfo installDir = new DirectoryInfo(System.Environment.CurrentDirectory);

		try
		{
			//nukes the whole User folder
			DirectoryInfo netplay = KWStructure.GenerateKWStructure_Directory_NetplayClients(installDir);
			if (netplay.Exists)
			{
				netplay.Delete(true);
				netplay = KWStructure.GenerateKWStructure_Directory_NetplayClients(installDir);
			}

			//gets the client deps
			KWQICommonInstalls.GetLatest_ClientDeps(netplay);

			//gets the Gekko Codes
			KWQICommonInstalls.GetLatest_GekkoCodes_Backside(KWStructure.GenerateKWStructure_SubDirectory_Clients_User_GameSettings(installDir));
			KWQICommonInstalls.GetLatest_GekkoCodes_HackPack(KWStructure.GenerateKWStructure_SubDirectory_Clients_User_GameSettings(installDir));

			//gets KARphin
			KWQICommonInstalls.GetLatest_KARphin(netplay);
		}
		catch (Exception e)
		{
			UnityEngine.Debug.LogError(e);
			MainUI.instance.sfx.PlayOneShot(MainUI.instance.menu[4]);
			MessageUI.MessageBox(IntPtr.Zero, e.ToString(), "Download Failed!", 0);
		}
	}

	//joins a match
	public void _on_join_match_pressed()
	{
		BootClient("--netBrowser f");
	}

	//hosts a match
	public void _on_host_match_pressed()
	{
		BootClient("--netHost f");
	}

	public void _set_karphin_client(int client)
	{
		currentClient = client;
	}
}