using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class Netplay : MonoBehaviour
{
	//the index array for which Client to launch
	public static string[] clientNames = {
		"KARphin",
		"KARphinDev"
	};
	public static int currentClient = 0;

    //attempts to boot the chosen client
    void BootClient(string state)
	{
        //checks if the client exists
        DirectoryInfo clientsFolder = new DirectoryInfo(Path.Combine(System.Environment.CurrentDirectory, "Clients"));
        FileInfo client = new FileInfo(Path.Combine(clientsFolder.FullName, "KARphin.exe"));

        try
		{
			//gets the correct client
			switch (clientNames[currentClient])
			{
				case "KARphin":
					client = new FileInfo(Path.Combine(clientsFolder.FullName, "KARphin.exe"));
					if (!client.Exists) //if it doesn't exist we download it
					{
                        if (!client.Exists)
                        {
                            System.Console.WriteLine($"{client.FullName}");
                            System.Console.WriteLine($"{clientNames[currentClient]} does not exist, can not boot.");
                            return;
                        }
                    }
					
                    break;

                case "KARphinDev":
                    client = new FileInfo(Path.Combine(clientsFolder.FullName, "KARphinDev.exe"));
                    if (!client.Exists) //if it doesn't exist we download it
                    {
                        if (!client.Exists)
                        {
                            System.Console.WriteLine($"{client.FullName}");
                            System.Console.WriteLine($"{clientNames[currentClient]} does not exist, can not boot.");
                            return;
                        }
                    }
                    break;
            }

            //writes the boot mode
            File.WriteAllText(Path.Combine(System.Environment.CurrentDirectory, "Clients", "Boot.state"), state);

            //boots the client
            var dolphin = new Process();
			dolphin.StartInfo.FileName = client.FullName;
            dolphin.StartInfo.ArgumentList.Add("-u");
            dolphin.StartInfo.ArgumentList.Add(Path.Combine(clientsFolder.FullName + "StarDust_Player_Settings"));
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

    //boots client for configuring
    public void _on_configure_pressed()
    {
        BootClient("None");
    }

    //joins a match
    public void _on_join_match_pressed()
	{
        BootClient("Lobby");
	}

	//hosts a match
	public void _on_host_match_pressed()
	{
        BootClient("Host");
    }

	public void _set_karphin_client(int client)
	{
		currentClient = client;
	}
}