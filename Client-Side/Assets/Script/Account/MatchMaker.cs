using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine;

public class MatchMaker : MonoBehaviour {

    public string gameSelected = "Random";

    private GameObject AccountManager;
    private NetworkManager manager;

    [Space]
    public GameObject QueueObject;
    public GameObject PlayerNumber;
    public GameObject GameSelectedObject;
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;

    [Space]
    public GameObject GameFoundObject;
    public GameObject GameFoundNEPObject;
    public GameObject GameAccept;
    public GameObject GameDeny;

    public GameObject JoiningGame;


    [Space]
    public GameObject Timer;

    [HideInInspector]
    public int TimerMinutesFloat = 0;
    [HideInInspector]
    public float TimerSecondFloat = 0;


    private bool DoneChoise = false;


    private void OnEnable()
    {
        AccountManager = GameObject.Find("AccountManager");
        manager = GetComponent<NetworkManager>();
    }

    private void Update()
    {
        if (AccountManager.GetComponent<SqlManager>().inQueue)
        {
            TimerSecondFloat += Time.deltaTime;
            if (TimerSecondFloat >= 60)
            {
                TimerSecondFloat = 0;
                TimerMinutesFloat += 1;
            }
            if (TimerSecondFloat > 9)
                Timer.GetComponent<Text>().text = TimerMinutesFloat.ToString() + ":" + Mathf.RoundToInt(TimerSecondFloat).ToString();
            else
                Timer.GetComponent<Text>().text = TimerMinutesFloat.ToString() + " : 0" + Mathf.RoundToInt(TimerSecondFloat).ToString();
        }
    }

    public void MatchMakerButton()
    {
        StartCoroutine(JoinQueue());
    }

    private IEnumerator JoinQueue()
    {
        AccountManager.GetComponent<SqlManager>().inQueue = true;
        QueueObject.SetActive(true);
        WWWForm form = new WWWForm();

        int i = 0;
        foreach (string row in AccountManager.GetComponent<SqlManager>().GroupID)
        {
            if (row != "")
                i++;
        }

        form.AddField("GroupLenghtPost", i);
        form.AddField("GroupGamePost", gameSelected);

        form.AddField("Player1Post", AccountManager.GetComponent<SqlManager>().GroupID[0]);
        form.AddField("Player2Post", AccountManager.GetComponent<SqlManager>().GroupID[1]);
        form.AddField("Player3Post", AccountManager.GetComponent<SqlManager>().GroupID[2]);
        form.AddField("Player4Post", AccountManager.GetComponent<SqlManager>().GroupID[3]);


        WWW www = new WWW(SqlManager.url + "MatchMaking.php", form);
        yield return www;

        if (www.text.Contains("/1/"))
        {
            StartCoroutine(GameFound(www.text));
        }
        else
        {
            PlayerNumber.GetComponent<Text>().text = www.text;
            GameSelectedObject.GetComponent<Text>().text = gameSelected;
            //SET PLAYER'S ICON
            StartCoroutine(ListPlayerInQueue());
        }
    }

    public IEnumerator ListPlayerInQueue()
    {
        while (AccountManager.GetComponent<SqlManager>().inQueue)
        {
            if (TimerMinutesFloat >= 3 && int.Parse(PlayerNumber.GetComponent<Text>().text) >= 4 && AccountManager.GetComponent<SqlManager>().Username == AccountManager.GetComponent<SqlManager>().GroupID[0])
            {
                WWWForm form2 = new WWWForm();
                form2.AddField("GroupGamePost", gameSelected);

                WWW www2 = new WWW(SqlManager.url + "GameJoinNEP.php", form2);

                yield return www2;

                if (www2.text.Contains("HTML"))
                    StartCoroutine(ListPlayerInQueue());

                yield return new WaitForSeconds(2);

                StartCoroutine(GameFoundNEP(www2.text));
                yield break;
            }

            WWWForm form = new WWWForm();
            form.AddField("PlayerPost", AccountManager.GetComponent<SqlManager>().ID);
            form.AddField("GroupGamePost", gameSelected);

            WWW www = new WWW(SqlManager.url + "ListPlayerInQueue.php", form);
            yield return www;

            if (www.text.Contains("/1/"))
            {
                StartCoroutine(GameFound(www.text));
                break;
            }
            else
            {
                PlayerNumber.GetComponent<Text>().text = www.text;
                yield return new WaitForSeconds(3);
            }
        } 
    }

    private IEnumerator GameFoundNEP (string wwwText)
    {
        GameFoundNEPObject.SetActive(true);
        GameFoundNEPObject.transform.Find("Ready").GetComponent<Button>().onClick.AddListener(delegate { Ready(wwwText); });
        GameFoundNEPObject.transform.Find("Ready").GetComponent<Button>().onClick.AddListener(delegate { Afraid(wwwText); });
        GameFoundNEPObject.transform.Find("PlayerNumber").GetComponent<Text>().text = PlayerNumber.GetComponent<Text>().text;
        yield return new WaitForSeconds(5);

        if (!DoneChoise)
        {
            Afraid(wwwText);
        }
        yield return new WaitForSeconds(1.5f);

        GameAccept.SetActive(false);
        GameDeny.SetActive(false);

        WWWForm form = new WWWForm();
        form.AddField("PlayerPost", wwwText.Substring(0, wwwText.Length - wwwText.IndexOf("/1/") - 1));

        WWW www = new WWW(SqlManager.url + "CheckAccept.php", form);
        yield return www;

        if (www.text.Contains("<!DOCTYPE HTML"))
        {
            StartCoroutine(GameFoundNEP(wwwText));
            yield break;
        }

        int Accept = int.Parse(www.text.Substring(0, www.text.IndexOf("//")));
        int Deny = int.Parse(www.text.Substring(www.text.IndexOf("//") + 2, www.text.Length - www.text.IndexOf("//") - 2));

        if (Deny == 0)
        {
            StartCoroutine(JoinGame(wwwText));
        }
        else
        {
            //Dodge.SetActive(true);
            //yield return new WaitForSeconds(3);
            //Dodge.SetActive(false);
        }
    }

    private IEnumerator GameFound (string wwwText)
    {
        GameFoundObject.SetActive(true);
        GameFoundObject.transform.Find("Ready").GetComponent<Button>().onClick.AddListener(delegate { Ready(wwwText); });
        GameFoundObject.transform.Find("Ready").GetComponent<Button>().onClick.AddListener(delegate { Afraid(wwwText); });
        yield return new WaitForSeconds(5);

        if (!DoneChoise)
        {
            Afraid(wwwText);
        }
        yield return new WaitForSeconds(1.5f);

        GameAccept.SetActive(false);
        GameDeny.SetActive(false);

        WWWForm form = new WWWForm();
        form.AddField("PlayerPost", wwwText.Substring(0, wwwText.Length - wwwText.IndexOf("/1/") - 1));

        WWW www = new WWW(SqlManager.url + "CheckAccept.php", form);
        yield return www;

        if (www.text.Contains("<!DOCTYPE HTML"))
        {
            StartCoroutine(GameFound(wwwText));
            yield break;
        }

        int Accept = int.Parse(www.text.Substring(0, www.text.IndexOf("//")));
        int Deny = int.Parse(www.text.Substring(www.text.IndexOf("//") + 2, www.text.Length - www.text.IndexOf("//") - 2));

        if (Deny == 0)
        {
            StartCoroutine(JoinGame(wwwText));
        }
        else
        {
            //Dodge.SetActive(true);
            //yield return new WaitForSeconds(3);
            //Dodge.SetActive(false);
        }
    }

    public void Ready(string wwwText)
    {
        GameFoundObject.SetActive(false);
        GameFoundNEPObject.SetActive(false);
        GameAccept.SetActive(true);
        DoneChoise = true;

        WWWForm form = new WWWForm();
        form.AddField("PlayerPost", wwwText.Substring(0, wwwText.Length - wwwText.IndexOf("/1/") - 1));

        new WWW(SqlManager.url + "AcceptGame.php", form);
    }

    public void Afraid(string wwwText)
    {
        GameFoundObject.SetActive(false);
        GameFoundNEPObject.SetActive(false);
        GameDeny.SetActive(true);
        DoneChoise = true;

        WWWForm form = new WWWForm();
        form.AddField("PlayerPost", wwwText.Substring(0, wwwText.Length - wwwText.IndexOf("/1/") - 1));

        new WWW(SqlManager.url + "DenyGame.php", form);
    }

    private IEnumerator JoinGame(string wwwText)
    {
        JoiningGame.SetActive(true);
        TimerSecondFloat = 0;
        TimerMinutesFloat = 0;
        QueueObject.SetActive(false);
        AccountManager.GetComponent<SqlManager>().inQueue = false;
        string gamelist = wwwText;
        string player1 = gamelist.Substring(0, gamelist.Length - gamelist.IndexOf("/1/") - 1);
        string player2 = gamelist.Substring(gamelist.IndexOf("/1/") + 3, gamelist.IndexOf("/2/") - 3);
        string player3 = gamelist.Substring(gamelist.IndexOf("/2/") + 3, gamelist.IndexOf("/3/") - 3);
        string player4 = gamelist.Substring(gamelist.IndexOf("/3/") + 3, gamelist.IndexOf("/4/") - 3);
        string player5 = gamelist.Substring(gamelist.IndexOf("/4/") + 3, gamelist.IndexOf("/5/") - 3);
        string player6 = gamelist.Substring(gamelist.IndexOf("/5/") + 3, gamelist.IndexOf("/6/") - 3);
        string player7 = gamelist.Substring(gamelist.IndexOf("/6/") + 3, gamelist.IndexOf("/7/") - 3);
        string player8 = gamelist.Substring(gamelist.IndexOf("/7/") + 3, gamelist.IndexOf("/8/") - 3);
        print(player1);
        print(player2);
        print(player3);
        print(player4);
        print(player5);
        print(player6);
        print(player7);
        print(player8);
        if (AccountManager.GetComponent<SqlManager>().Username == player1)
        {
            manager.StartMatchMaker();
            manager.matchMaker.CreateMatch(player1, 8, true, "", "", "", 0, 0, OnMatchCreate);
        }
        else
        {
            yield return new WaitForSeconds(2);
            manager.StartMatchMaker();
            manager.matchMaker.ListMatches(0, 100, player1, true, 0, 0, JoinMatch);
        }
    }

    private void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        //if (!success)
            //Erreur
    }

    private void JoinMatch(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
        if (success)
        {
            if (matches.Count != 0)
            {
                manager.matchMaker.JoinMatch(matches[matches.Count - 1].networkId, "", "", "", 0, 0, OnMatchJoin);
            }
        }
        else
        {
            //ERREUR
        }
    }

    private void OnMatchJoin(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
            manager.StartClient(matchInfo);
        //else
            //Erreur
    }

    public void UpdateGameMode(string GameMode)
    {
        gameSelected = GameMode;
    }

    public void LeaveQueue ()
    {
        QueueObject.SetActive(false);
        TimerMinutesFloat = 0;
        TimerSecondFloat = 0;
        StartCoroutine(LeaveQueueIE());
    }

    private IEnumerator LeaveQueueIE()
    {
        print("leave");
        WWWForm form = new WWWForm();
        form.AddField("PlayerPost", AccountManager.GetComponent<SqlManager>().GroupID[0]);

        WWW www = new WWW(SqlManager.url + "LeaveQueue.php", form);

        yield return www;

        if (www.text.Contains("<!DOCTYPE HTML"))
            StartCoroutine(LeaveQueueIE());
    }

    private void OnApplicationQuit()
    {
        if (AccountManager.GetComponent<SqlManager>().inQueue)
            LeaveQueue();
    }
}
