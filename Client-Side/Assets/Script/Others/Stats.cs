using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine;

public class Stats : MonoBehaviour {

    private string statsFile = Directory.GetCurrentDirectory() + "/Stats.txt"; //A modifier aussi dans le FPSManager
    private List<string> statsLine = new List<string>();

    [SerializeField]
    private GameObject Kills;
    [SerializeField]
    private GameObject Deaths;
    [SerializeField]
    private GameObject GamePlayed;
    [SerializeField]
    private GameObject Wins;
    [SerializeField]
    private GameObject Loses;
    [SerializeField]
    private GameObject MinutesPlayed;
    [SerializeField]
    private GameObject Skills;
    [SerializeField]
    private GameObject LastKill;
    [SerializeField]
    private GameObject LastDeaths;
    [SerializeField]
    private GameObject LastDamageDealt;
    [SerializeField]
    private GameObject LastDamageTaken;
    [SerializeField]
    private GameObject LastKillStreak;

    private void Start()
    {
        if (File.Exists(statsFile))
        {
            foreach (string line in File.ReadAllLines(statsFile))
            {
                statsLine.Add(line);
            }
            LoadConfig(statsLine.ToArray());
        }
        else
        {
            string[] defaultConfig = new string[12];
            defaultConfig[0] = "Kills = 0";
            defaultConfig[1] = "Deaths = 0";
            defaultConfig[2] = "GamePlayed = 0";
            defaultConfig[3] = "Wins = 0";
            defaultConfig[4] = "Loses = 0";
            defaultConfig[5] = "MinutesPlayed = 0";
            defaultConfig[6] = "Skills = 0";
            defaultConfig[7] = "LastKill = 0";
            defaultConfig[8] = "LastDeaths = 0";
            defaultConfig[9] = "LastDamageDealt = 0";
            defaultConfig[10] = "LastDamageTaken = 0";
            defaultConfig[11] = "LastKillStreak = 0";
            File.WriteAllLines(statsFile, defaultConfig);
            foreach (string line in File.ReadAllLines(statsFile))
            {
                statsLine.Add(line);
            }
            LoadConfig(statsLine.ToArray());
        }
    }

    public void ExternLoadConfig ()
    {
        foreach (string line in File.ReadAllLines(statsFile))
        {
            statsLine.Add(line);
        }
        LoadConfig(statsLine.ToArray());
    }

    void LoadConfig(string[] lines)
    {
        foreach (string line in lines)
        {
            string command = line.Substring(0, line.IndexOf("=") - 1);
            string value = line.Substring(line.IndexOf("=") + 2, line.Length - line.IndexOf("=") - 2);
            switch (command)
            {
                case "Kills":
                    Kills.GetComponent<Text> ().text = value;
                    break;
                case "Deaths":
                    Deaths.GetComponent<Text>().text = value;
                    break;
                case "GamePlayed":
                    GamePlayed.GetComponent<Text>().text = value;
                    break;
                case "Wins":
                    Wins.GetComponent<Text>().text = value;
                    break;
                case "Loses":
                    Loses.GetComponent<Text>().text = value;
                    break;
                case "MinutesPlayed":
                    MinutesPlayed.GetComponent<Text>().text = value;
                    break;
                case "Skills":
                    Skills.GetComponent<Text>().text = value;
                    break;
                case "LastKill":
                    LastKill.GetComponent<Text>().text = value;
                    break;
                case "LastDeaths":
                    LastDeaths.GetComponent<Text>().text = value;
                    break;
                case "LastDamageDealt":
                    LastDamageDealt.GetComponent<Text>().text = value;
                    break;
                case "LastDamageTaken":
                    LastDamageTaken.GetComponent<Text>().text = value;
                    break;
                case "LastKillStreak":
                    LastKillStreak.GetComponent<Text>().text = value;
                    break;
            }
        }
    }

    void UpdateConfig(string needChange, string value)
    {
        string[] readFile = File.ReadAllLines(statsFile);
        int i = -1;
        foreach (string line in readFile)
        {
            i++;
            string command = line.Substring(0, line.IndexOf("=") - 1);
            if (command == needChange)
            {
                readFile[i] = needChange + " = " + value;
                File.WriteAllLines(statsFile, readFile);
                return;
            }
        }
    }
}
