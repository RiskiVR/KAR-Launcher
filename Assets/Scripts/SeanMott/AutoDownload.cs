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
	void Awake()
	{
		//checks that the launcher is at the latest
		System.Diagnostics.Process updater = new System.Diagnostics.Process();
		updater.StartInfo.FileName = new DirectoryInfo(System.Environment.CurrentDirectory).FullName + "/KAR_BootUpdate.exe";
		updater.StartInfo.WorkingDirectory = new DirectoryInfo(System.Environment.CurrentDirectory).FullName;
        updater.StartInfo.Arguments = "-launcher " + Application.version;
        updater.Start();
		updater.WaitForExit();

        //performs a KARphin update
        StartCoroutine(Download());
    }

	//performs a download of KARphin
	IEnumerator Download()
	{
		headerText.text = "Checking for updates...";
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
				MainUI.MessageUI.MessageBox(IntPtr.Zero, ex.ToString(), "Error!", 0);
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
}