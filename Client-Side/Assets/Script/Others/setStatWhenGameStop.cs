using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine;

public class setStatWhenGameStop : MonoBehaviour {

    private string statsFile = Directory.GetCurrentDirectory() + "/Stats.txt"; //A modifier aussi dans le FPSManager

    [SerializeField]
    private GameObject Kills;
    [SerializeField]
    private GameObject Deaths;
    [SerializeField]
    private GameObject DamageTaken;
    [SerializeField]
    private GameObject DamageDealt;
    [SerializeField]
    private GameObject BestKillSteak;

    void setStats (string playerID)
    {
        playerStats stats = GameObject.Find(playerID).GetComponent <playerStats> ();
        Kills.GetComponent<Text>().text = stats.Kills.ToString();
        UpdateConfig("Kills", Kills.ToString());
        Deaths.GetComponent<Text>().text = stats.Deaths.ToString();
        UpdateConfig("Deaths", Deaths.ToString());
        DamageTaken.GetComponent<Text>().text = stats.DamageTaken.ToString();
        UpdateConfig("DamageTaken", DamageTaken.ToString());
        DamageDealt.GetComponent<Text>().text = stats.DamageDealt.ToString();
        UpdateConfig("DamageDealt", DamageDealt.ToString());
        BestKillSteak.GetComponent<Text>().text = stats.BestKillStreak.ToString();
        UpdateConfig("BestKillStreak", BestKillSteak.ToString());
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
