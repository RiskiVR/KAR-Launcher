using System;
using System.Collections;
using System.IO;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

//automatically downloads the latest KARphin if it is out of date
public class AutoDownload : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI headerText;
	[SerializeField] private GameObject buttons;
	void Awake()
	{
		if (Directory.Exists("Clients")) RunCoroutines();
		else
		{
			headerText.text = "There is no KARphin client installed. Set up KARphin?";
			buttons.SetActive(true);
		}
    }

	public void RunCoroutines()
	{
		//performs a boot updater check
		StartCoroutine(BootDL());
		
		//performs a launcher check
		StartCoroutine(LauncherDL());

		//performs a KARphin update
		StartCoroutine(KARphinDL());
	}
	
	//checks if the boot updater exists
	IEnumerator BootDL()
	{
		headerText.text = "Checking for Boot Updater updates...";
		yield return new WaitForSeconds(0.01f);
		DirectoryInfo installDir = new DirectoryInfo(System.Environment.CurrentDirectory);
		KWQICommonInstalls.GetLatest_BootUpdater(installDir);
	}
	
	//checks that the launcher is at the latest
	IEnumerator LauncherDL()
	{
		headerText.text = "Checking for Launcher updates...";
		yield return new WaitForSeconds(0.01f);
		System.Diagnostics.Process updater = new System.Diagnostics.Process();
		updater.StartInfo.FileName = new DirectoryInfo(System.Environment.CurrentDirectory).FullName + "/KAR_BootUpdate.exe";
		updater.StartInfo.WorkingDirectory = new DirectoryInfo(System.Environment.CurrentDirectory).FullName;
		updater.StartInfo.Arguments = "-launcher " + Application.version;
		updater.Start();
		updater.WaitForExit();
	}

	//performs a download of KARphin
	IEnumerator KARphinDL()
	{
		headerText.text = "Checking for KARphin updates...";
		yield return new WaitForSeconds(0.01f);
		//the URL to the build date file for KARphin
		string fileUrl = "https://github.com/SeanMott/KARphin_Modern/releases/download/latest/new_KARphinBuild.txt";

		//gets the file
		using (WebClient client = new WebClient())
		{
			try
			{
				client.DownloadFile(fileUrl, "new_KARphinBuild.txt");
				Debug.Log("Downloaded the new build date data");
			}
			catch (Exception ex)
			{
				Debug.Log("An error occurred: " + ex.Message);
				MessageUI.MessageBox(IntPtr.Zero, ex.ToString(), "Error!", 0);
			}
		}

		//checks if a previous build data file exits
		string currentBuild = "";
		if(File.Exists("KARphinBuild.txt"))
			currentBuild = File.ReadAllText("KARphinBuild.txt");

		//check the date of the new file and the old file
		//if the new date is more new, get the new KARphin
		string newBuildDate = File.ReadAllText("new_KARphinBuild.txt");
		if(newBuildDate != currentBuild)
		{
			//downloads KARphin
			KWQICommonInstalls.GetLatest_KARphin(KWStructure.GenerateKWStructure_Directory_NetplayClients(new DirectoryInfo(System.Environment.CurrentDirectory)));

			//updates the build data
			File.WriteAllText("KARphinBuild.txt", newBuildDate);
			File.Delete("new_KARphinBuild.txt");
		}
		headerText.text = "Loading menu...";
		SceneManager.LoadScene(1);
	}

	public void SetupKARphin()
	{
		StartCoroutine(KARphinSetup());
	}
	
	IEnumerator KARphinSetup()
	{
		headerText.text = "Setting up KARphin...";
		yield return new WaitForSeconds(0.01f);
		DirectoryInfo installDir = new DirectoryInfo(System.Environment.CurrentDirectory);

		try
		{
			Netplay.ResetClientFolder();
		}
		catch (Exception e)
		{
			UnityEngine.Debug.LogError(e);
			MainUI.instance.sfx.PlayOneShot(MainUI.instance.menu[4]);
			MessageUI.MessageBox(IntPtr.Zero, e.ToString(), "Download Failed!", 0);
		}

		RunCoroutines();
	}
}