using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

//defines a replay
public struct Replay
{
    public string path; //the path this Replay
   
    public string gameID; //the Game ID we used to play
    public string date; //the date this Replay was generated
    public string port1PlayerName; //the name of the Player in port 1, we use it to play back codes
    public string port2PlayerName; //the name of the Player in port 2, if it existed, we use it to play back codes
    public string port3PlayerName; //the name of the Player in port 3, if it existed, we use it to play back codes
    public string port4PlayerName; //the name of the Player in port 4, if it existed, we use it to play back codes
   
    public UInt32 playCount; //the number of players that were in the game

    //loads a match data
    public bool LoadMatchData(string dir)
    {
        path = dir;

        StreamReader reader = new StreamReader(dir + "\\matchData.txt");

        if ((gameID = reader.ReadLine()) == null)
            return false;
        if ((date = reader.ReadLine()) == null)
            return false;

        if ((port1PlayerName = reader.ReadLine()) == null)
            return true;
        playCount++;
        
        if ((port2PlayerName = reader.ReadLine()) == null)
            return true;
        playCount++;

        if ((port3PlayerName = reader.ReadLine()) == null)
            return true;
        playCount++;

        if ((port4PlayerName = reader.ReadLine()) == null)
            return true;
        playCount++;

        reader.Close();
        return true;
    }
}

public class ReplayHandler : MonoBehaviour
{
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Transform listParent;
    public List<Replay> replays;
    void Awake()
    {
        replays = new List<Replay>();

        //the replay directory
        string[] replayDir = Directory.GetDirectories(System.Environment.CurrentDirectory + "/Replays");

        //goes through the directories
        foreach (string dir in replayDir)
        {
            //attempt to load the match data, if we fail, we know this Replay is invalid
            Replay r = new Replay();
            if (!r.LoadMatchData(dir))
                continue;

            //adds the Replay
            replays.Add(r);
        }
        
    }
    void OnEnable() => StartCoroutine(ListReplays());

    //private void OnEnable()
    //{
    //    foreach (Replay s in replays)
    //    {
    //        GameObject button = Instantiate(buttonPrefab, listParent);
    //        button.GetComponent<RectTransform>().sizeDelta = new Vector2(UnityEngine.Random.Range(-1000, 1000), 50);
    //        button.GetComponent<ButtonInfo>().info = "Watch Replay";
    //        button.GetComponentInChildren<TextMeshProUGUI>().text = "";
    //    }
    //}

    void OnDisable()
    {
        foreach (Transform c in listParent)
        {
            Destroy(c.gameObject);
        }
        //replays.Clear();
    }

   IEnumerator ListReplays()
   {
       foreach (Replay s in replays)
       {
           GameObject button = Instantiate(buttonPrefab, listParent);
           button.GetComponent<RectTransform>().sizeDelta = new Vector2(UnityEngine.Random.Range(-1000, 1000), 50);
           button.GetComponent<ButtonInfo>().info = "Watch Replay || " + s.port1PlayerName + " V " + s.port2PlayerName;
           button.GetComponentInChildren<TextMeshProUGUI>().text = s.playCount.ToString() + " | " + s.gameID + " | " + s.date;
           yield return new WaitForSeconds(0.01f);
       }
   }
}