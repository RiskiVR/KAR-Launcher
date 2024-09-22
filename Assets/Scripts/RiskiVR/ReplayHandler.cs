using System;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;

public class ReplayHandler : MonoBehaviour
{
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Transform listParent;
    [SerializeField] string[] dir;
    void Awake()
    { 
        dir = Directory.GetDirectories("Replays\\");
        
        DirectoryInfo replayDir = new DirectoryInfo(dir); //the directory containing all the data we want for this replay

        FileInfo replayData = new FileInfo(replayDir.FullName + "\\replayData.dmt"); //the emulation data we replay

        //loads the match data
        StreamReader reader = new StreamReader(replayDir.FullName + "\\matchData.txt");
        string line;

        /*
        struct MatchData
        {
            string gameID = "",
                date = "00-00-0000",
                port1PlayerName = "",
                port2PlayerName = String.Empty,
                port3PlayerName = String.Empty,
                port4PlayerName = String.Empty;
        }
        */

        int matchDataIndex = 0;
        while ((line = reader.ReadLine()) != null)
        {
            switch(matchIndex)
            {
                case 0:
                    matchData.gameID = line;
                    break;

                case 1:
                    matchData.date = line;
                    break;
            }
        }
        reader.Close();
    }
    void OnEnable() => StartCoroutine(ListReplays());
    void OnDisable()
    {
        foreach (Transform c in listParent)
        {
            Destroy(c.gameObject);
        }
        Array.Clear(replays, 0, replays.Length);
    }
    IEnumerator ListReplays()
    {
        foreach (string s in replays)
        {
            GameObject button = Instantiate(buttonPrefab, listParent);
            button.GetComponent<RectTransform>().sizeDelta = new Vector2(UnityEngine.Random.Range(-1000, 1000), 50);
            button.GetComponent<ButtonInfo>().info = "Watch Replay";
            button.GetComponentInChildren<TextMeshProUGUI>().text = "";
            yield return new WaitForSeconds(0.01f);
        }
    }
}