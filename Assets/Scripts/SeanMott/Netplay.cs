using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;

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

		//validate the Replay folder, for storing the game replays exists
		DirectoryInfo replayDir = new DirectoryInfo(installDir + "/Replays");
		if (!replayDir.Exists)
			replayDir.Create();

		try
		{
			//checks if the client exists
			DirectoryInfo clientsFolder = new DirectoryInfo(""); //KWStructure.GenerateKWStructure_Directory_NetplayClients(installDir);

			//gets the correct client
			FileInfo client = new FileInfo(clientsFolder.FullName);
			switch (clientNames[currentClient])
			{
				case "KARphin":
					client = new FileInfo(clientsFolder.FullName + "/KARphin.exe");
					if (!client.Exists) //if it doesn't exist we download it
					{
                       // KWQICommonInstalls.GetLatest_KARphin(KWStructure.GenerateKWStructure_Directory_NetplayClients(installDir));

                        client = new FileInfo(clientsFolder.FullName + "/KARphin.exe");
                        if (!client.Exists)
                        {
                            System.Console.WriteLine($"{client.FullName}");
                            System.Console.WriteLine($"{clientNames[currentClient]} does not exist, can not boot.");
                            return;
                        }
                    }
					
                    break;

                case "KARphinDev":
                    client = new FileInfo(clientsFolder.FullName + "/KARphinDev.exe");
                    if (!client.Exists) //if it doesn't exist we download it
                    {
                       // KWQICommonInstalls.GetLatest_KARphinDev(KWStructure.GenerateKWStructure_Directory_NetplayClients(installDir));

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
            dolphin.StartInfo.Arguments = " -u \"" + installDir + "/Clients/User\" " + args;
            dolphin.StartInfo.WorkingDirectory = clientsFolder.FullName;

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

	//resets a client folder
	public static void ResetClientFolder()
	{
        try
        {
            BootLoader.PerformFreshInstall();  
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError(e);
            MainUI.instance.sfx.PlayOneShot(MainUI.instance.menu[4]);
            MessageUI.MessageBox(IntPtr.Zero, e.ToString(), "Download Failed!", 0);
        }
    }

	//resets the client data
	public void _on_reset_client_pressed()
	{
		ResetClientFolder();
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