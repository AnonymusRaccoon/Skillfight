using System.Collections;
using System;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;

public class FFAManager : NetworkBehaviour {

    public string Top1;
    public string Top2;
    public string Top3;

    public TabManager Scoreboard;

    public GameObject WinScreen;
    public GameObject LoseScreen;
    public GameObject SeeMoreStats;
    public GameObject GameDoneHUD;
    public GameObject InGameHUD;

    [Space]
    public GameObject Timer;

    [SyncVar]
    private int TimerMinutes = 4;
    [SyncVar]
    private float TimerSeconds = 60;



    private void OnEnable()
    {
        Hp.playerDeath += Dead;
    }

    private void OnDisable()
    {
        Hp.playerDeath -= Dead;
    }

    private void Start()
    {
        Scoreboard = GetComponent<TabManager>();
    }

    private void Update()
    {
        if (isServer && GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().Select == false)
        {
            TimerSeconds -= Time.deltaTime;
            if (TimerSeconds <= 0)
            {
                TimerSeconds = 59;
                TimerMinutes -=  1;
                if (TimerMinutes < 0)
                {
                    GameEnd();
                    return;
                }
            }
        }        
        if (TimerSeconds > 9)
            Timer.GetComponent<Text>().text = TimerMinutes.ToString() + ":" + Mathf.RoundToInt(TimerSeconds).ToString();
        else
            Timer.GetComponent<Text>().text = TimerMinutes.ToString() + ":0" + Mathf.RoundToInt(TimerSeconds).ToString();
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        print("test");
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponent<NetworkIdentity>().isLocalPlayer)
                player.AddComponent(typeof(FFAplayerPoint));
        }
    }

    void Dead(string playerID, string attackerID)
    {
        GameObject player = GameObject.Find(playerID);
        player.GetComponent<FFAplayerPoint>().score -= 2;
        string score = player.GetComponent<SetupLocalPlayer>().score;
        player.GetComponent<SetupLocalPlayer>().score = score.Substring(0, score.IndexOf("/") - 1) + (int.Parse(score.Substring(score.IndexOf("/"), score.Length - score.IndexOf("/"))) - 1).ToString();
        GameObject attacker = GameObject.Find(attackerID);
        attacker.GetComponent<FFAplayerPoint>().score += 3;
        score = player.GetComponent<SetupLocalPlayer>().score;
        attacker.GetComponent<SetupLocalPlayer>().score = (int.Parse(score.Substring(0, score.IndexOf("/") - 1)) + 1).ToString() + score.Substring(score.IndexOf("/"), score.Length - score.IndexOf("/"));
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        String[] playersScore = new String[players.Length];
        int i = 0;
        foreach (GameObject aPlayer in players)
        {
            playersScore[i] = aPlayer.transform.name + " - " + aPlayer.GetComponent<FFAplayerPoint>().score;
            i++;
        }
        Array.Sort(playersScore, delegate (string player1, string player2) {
            return GameObject.Find(player1.Substring(0, player1.IndexOf(" - ") - 3)).GetComponent<FFAplayerPoint> ().score.CompareTo(GameObject.Find(player1.Substring(0, player1.IndexOf(" - ") - 3)).GetComponent<FFAplayerPoint>().score);
        });
        Top1 = playersScore[0].Substring(0, playersScore[0].IndexOf(" - ") - 3);
        Scoreboard.Top1String = Top1;
        if (playersScore.Length < 2)
            return;
        Top2 = playersScore[1].Substring(0, playersScore[1].IndexOf(" - ") - 3);
        Scoreboard.Top2String = Top2;
        if (playersScore.Length < 3)
            return;
        Top3 = playersScore[2].Substring(0, playersScore[2].IndexOf(" - ") - 3);
        Scoreboard.Top3String = Top3;
    }

    void GameEnd ()
    {
        RpcGameDone();
    }

    [ClientRpc]
    public void RpcGameDone ()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject localPlayer = gameObject;
        foreach (GameObject player in players)
        {
            if (player.GetComponent<NetworkIdentity> ().isLocalPlayer)
            {
                localPlayer = player;
            }
        }
        localPlayer.GetComponent<FirstPersonController>().Select = true;
        localPlayer.GetComponent<SetupLocalPlayer>().canStartLoading = false;
        if (Top1 == localPlayer.GetComponent<SetupLocalPlayer>().pname)
            WinScreen.SetActive(true);
        else
            LoseScreen.SetActive(true);
        InGameHUD.SetActive(false);
        GameDoneHUD.SetActive(true);
        GameDoneHUD.transform.Find("Top1").GetComponent<Text>().text = Top1;
        GameDoneHUD.transform.Find("Top2").GetComponent<Text>().text = Top2;
        GameDoneHUD.transform.Find("Top3").GetComponent<Text>().text = Top3;
        GameDoneHUD.transform.Find("Kills").GetComponent<Text>().text = localPlayer.GetComponent<playerStats>().Kills.ToString();
        GameDoneHUD.transform.Find("Deaths").GetComponent<Text>().text = localPlayer.GetComponent<playerStats>().Deaths.ToString();
        GameDoneHUD.transform.Find("Damage Dealt").GetComponent<Text>().text = localPlayer.GetComponent<playerStats>().DamageDealt.ToString();
        GameDoneHUD.transform.Find("Damage Taken").GetComponent<Text>().text = localPlayer.GetComponent<playerStats>().DamageTaken.ToString();
        GameDoneHUD.transform.Find("Best KillStreak").GetComponent<Text>().text = localPlayer.GetComponent<playerStats>().BestKillStreak.ToString();
        for(int i = 1; i < players.Length; i++)
        {
            GameObject Pseudo = SeeMoreStats.transform.Find("Pseudo (" + i + ")").gameObject;
            Pseudo.SetActive(true);
            Pseudo.transform.Find("Pseudo (" + i + ")").GetComponent<Text>().text = players[i -1].ToString();
            Pseudo.transform.Find("Kills").GetComponent<Text>().text = players[i -1].GetComponent<playerStats>().Kills.ToString();
            Pseudo.transform.Find("Deaths").GetComponent<Text>().text = players[i -1].GetComponent<playerStats>().Deaths.ToString();
            Pseudo.transform.Find("Damage Dealt").GetComponent<Text>().text = players[i -1].GetComponent<playerStats>().DamageDealt.ToString();
            Pseudo.transform.Find("Damage Taken").GetComponent<Text>().text = players[i -1].GetComponent<playerStats>().DamageTaken.ToString();
            Pseudo.transform.Find("Best KillStreak").GetComponent<Text>().text = players[i -1].GetComponent<playerStats>().BestKillStreak.ToString();
        }

        DontDestroyOnLoad(GameDoneHUD.transform.parent);
        if (!isServer)
        {
            Network.Disconnect();
        }
        else
        {
            StartCoroutine(DisconectServer());
        }
    }

    private IEnumerator DisconectServer ()
    {
        yield return new WaitForSeconds(4);
        foreach (NetworkPlayer netPlayer in Network.connections)
        {
            Network.CloseConnection(netPlayer, true);
        }
    }
}
