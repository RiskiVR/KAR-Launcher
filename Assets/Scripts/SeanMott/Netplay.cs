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
	void BootClient()
	{
		//DirectoryInfo installDir = new DirectoryInfo(System.Environment.CurrentDirectory);
		DirectoryInfo installDir = new DirectoryInfo("C:/Users/rafal/Desktop/Boot test/KARNetplay");

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
			dolphin.StartInfo.WorkingDirectory = clientsFolder.FullName;
			dolphin.Start();
		}
		catch (Exception e)
		{
			UnityEngine.Debug.LogError(e);
			MainUI.instance.audioSource.PlayOneShot(MainUI.instance.menu[4]);
			MainUI.MessageUI.MessageBox(IntPtr.Zero, e.ToString(), "Download Failed!", 0);
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
		//FileInfo brotliEXE = KWStructure.GetSupportTool_Brotli_Windows(installDir);

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

			//generate Dolphin config
			string config = "[Analytics]\nID = 9fbc80be625d265e9c906466779b9cec\n[NetPlay]\nTraversalChoice = traversal\nChunkedUploadLimit = 0x00000bb8\nConnectPort = 0x0a42\nEnableChunkedUploadLimit = False\nHostCode = 00000000\nHostPort = 0x0a42\nIndexName = KAR\nIndexPassword = \nIndexRegion = NA\nNickname = Kirby\nUseIndex = True\nUseUPNP = False\n[Display]\nDisableScreenSaver = True\n[General]\nHotkeysRequireFocus = True\nISOPath0 = " +
			                installDir + "/ROMs\nISOPaths = 1\n[Interface]\nConfirmStop = True\nOnScreenDisplayMessages = True\nShowActiveTitle = True\nUseBuiltinTitleDatabase = True\nUsePanicHandlers = True\n[Core]\nAudioLatency = 20\nAudioStretch = False\nAudioStretchMaxLatency = 80\nDPL2Decoder = False\nDPL2Quality = 2\nDSPHLE = True\n[DSP]\nEnableJIT = False\nVolume = 100\nWASAPIDevice = ";
		
			DirectoryInfo configFolder = new DirectoryInfo(KWStructure.GenerateKWStructure_SubDirectory_Clients_User(installDir) + "/Config");
			configFolder.Create();

			System.IO.StreamWriter file = new System.IO.StreamWriter(
				configFolder.FullName + "/Dolphin.ini");
			file.Write(config);
			file.Close();

			//gets KARphin
			KWQICommonInstalls.GetLatest_KARphin(netplay);
		}
		catch (Exception e)
		{
			UnityEngine.Debug.LogError(e);
			MainUI.instance.audioSource.PlayOneShot(MainUI.instance.menu[4]);
			MainUI.MessageUI.MessageBox(IntPtr.Zero, e.ToString(), "Download Failed!", 0);
		}
	}

	//joins a match
	public void _on_join_match_pressed()
	{
		BootClient();
	}

	//hosts a match
	public void _on_host_match_pressed()
	{
		BootClient();
	}

	public void _set_karphin_client(int client)
	{
		currentClient = client;
	}
}