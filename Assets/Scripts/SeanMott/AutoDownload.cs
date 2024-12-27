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
		if (Directory.Exists("Clients")) 
			RunCoroutines();
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
		//StartCoroutine(KARphinDL());
	}
	
	//checks if the boot updater exists
	IEnumerator BootDL()
	{
		headerText.text = "Checking for Boot Updater updates...";
		yield return new WaitForSeconds(0.01f);
		BootLoader.GetBootLoader();
    }

    //checks that the launcher is at the latest
    IEnumerator LauncherDL()
	{
		headerText.text = "Checking for Launcher updates...";
		yield return new WaitForSeconds(0.01f);
        //LaunchBootLoader.GetLatest_Launcher();

        //System.Diagnostics.Process updater = new System.Diagnostics.Process();
        //updater.StartInfo.FileName = new DirectoryInfo(System.Environment.CurrentDirectory).FullName + "/KAR_BootUpdate.exe";
        //updater.StartInfo.WorkingDirectory = new DirectoryInfo(System.Environment.CurrentDirectory).FullName;
        //updater.StartInfo.Arguments = "-setVer " + Application.version + " -launcher";
        //updater.Start();
        //updater.WaitForExit();

        headerText.text = "Loading menu...";
        SceneManager.LoadScene(1);
    }

    //performs a download of KARphin
    //IEnumerator KARphinDL()
    //{
    //	headerText.text = "Checking for KARphin updates...";
    //	yield return new WaitForSeconds(0.01f);

    //	//checks the currently installed version
    //	string currentBuild = "00000";
    //	if (File.Exists("KARBuildData.txt"))
    //		currentBuild = File.ReadAllText("KARBuildData.txt");

    //	//performs a update
    //	System.Diagnostics.Process updater = new System.Diagnostics.Process();
    //	updater.StartInfo.FileName = new DirectoryInfo(System.Environment.CurrentDirectory).FullName + "/KAR_BootUpdate.exe";
    //	updater.StartInfo.WorkingDirectory = new DirectoryInfo(System.Environment.CurrentDirectory).FullName;
    //	updater.StartInfo.Arguments = "-setVer " + currentBuild + " -KARphin";
    //	updater.Start();
    //	updater.WaitForExit();

    //	headerText.text = "Loading menu...";
    //	SceneManager.LoadScene(1);
    //}

    public void SetupKARphin()
	{
		StartCoroutine(KARphinSetup());
	}
	
	IEnumerator KARphinSetup()
	{
		headerText.text = "Setting up KARphin...";
		yield return new WaitForSeconds(0.01f);
		//DirectoryInfo installDir = new DirectoryInfo(System.Environment.CurrentDirectory);

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