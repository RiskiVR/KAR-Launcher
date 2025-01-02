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
            SetUpExecute();
        else
        {
            headerText.text = "There is no KARphin client installed. Set up KARphin?";
            buttons.SetActive(true);
        }
    }

    //performs the set up/boot sequence process
    public void SetUpExecute()
    {
        StartCoroutine(Setup());
    }


	//checks if the boot updater exists
	IEnumerator Setup()
	{
		headerText.text = "Checking for Boot Updater updates...";
		BootLoader.GetBootLoader();

        headerText.text = "Checking for Launcher updates...";
        BootLoader.UpdateLauncher();

        headerText.text = "Setting up KARphin...";

        //checks if we have a valid KARphin build
        try
        {
            if (!Directory.Exists(Path.Combine(System.Environment.CurrentDirectory, "Clients")))
            {
                Directory.CreateDirectory("Clients");
                Netplay.ResetClientFolder();
            }
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError(e);
            MainUI.instance.sfx.PlayOneShot(MainUI.instance.menu[4]);
            MessageUI.MessageBox(IntPtr.Zero, e.ToString(), "Download Failed!", 0);
        }

        headerText.text = "Loading menu...";
        SceneManager.LoadScene(1);

        yield return new WaitForSeconds(0.01f);
    }
}