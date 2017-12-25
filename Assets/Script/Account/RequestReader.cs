using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class RequestReader : MonoBehaviour {

    private GameObject AccountManager;
    public GameObject CustomGameManager;
    private string[] requests;

    public GameObject FriendNotifPrefab;
    public GameObject GroupNotifPrefab;
    public GameObject CustomGamePrefab;

    public Transform MainMenuObject;
    public GameObject MatchMakerObject;


    private void OnEnable()
    {
        AccountManager = GameObject.Find("AccountManager");
        StartCoroutine(Read());
    }

    private IEnumerator Read ()
    {
        WWWForm form = new WWWForm();
        form.AddField("PlayerPost", AccountManager.GetComponent<SqlManager>().Username);

        WWW www = new WWW(SqlManager.url + "ReadRequest.php", form);

        yield return www;

        if (www.text.Contains("<!DOCTYPE HTML PUBLIC"))
        {
            StartCoroutine(Read());
            yield break;
        }

        if (!www.text.Contains("No Request"))
        {
            requests = www.text.Split(";".ToCharArray());
            foreach (string request in requests)
            {
                if (request == "")
                    yield break;
                if (request.Contains("HTML"))
                {
                    //Image erreur
                    yield break;
                }
                string type = request.Substring(0, request.IndexOf("//"));
                string value = request.Substring(request.IndexOf("//") + 2, request.IndexOf(":") - request.IndexOf("//") - 2);
                string info = request.Substring(request.IndexOf(":") + 1, request.Length - request.IndexOf(":") - 1);

                if (type == "Friend")
                {
                    GameObject FriendNotif = Instantiate(FriendNotifPrefab);
                    FriendNotif.transform.SetParent(GameObject.Find("Canvas").transform, false);
                    FriendNotif.GetComponentInChildren<Text>().text = value;
                    FriendNotif.transform.Find("Accept").GetComponent<Button>().onClick.AddListener(FriendRequestAcceptButton);
                    FriendNotif.transform.Find("Deny").GetComponent<Button>().onClick.AddListener(FriendRequestDenyButton);
                }
                if (type == "Group")
                {
                    GameObject GameNotif = Instantiate(GroupNotifPrefab);
                    GameNotif.transform.SetParent(GameObject.Find("Canvas").transform, false);
                    GameNotif.GetComponentInChildren<Text>().text = value;
                    GameNotif.transform.Find("Accept").GetComponent<Button>().onClick.AddListener(delegate { GroupRequestAcceptButton(int.Parse(info)); });
                    GameNotif.transform.Find("Deny").GetComponent<Button>().onClick.AddListener(GroupRequestDenyButton);
                }
                if (type == "CustomGame")
                {
                    GameObject CustomGameNotif = Instantiate(CustomGamePrefab);
                    CustomGameNotif.transform.SetParent(GameObject.Find("Canvas").transform, false);
                    CustomGameNotif.GetComponentInChildren<Text>().text = value;
                    CustomGameNotif.transform.Find("Accept").GetComponent<Button>().onClick.AddListener(delegate { CustomGameAcceptButton(value, info); });
                    CustomGameNotif.transform.Find("Deny").GetComponent<Button>().onClick.AddListener(CustomGameDenyButton);
                }
            }
        }
        yield return new WaitForSeconds(3);
        StartCoroutine(Read());
    }

    public IEnumerator ReadGroupChange()
    {
        WWWForm form = new WWWForm();
        form.AddField("PartyIDPost", AccountManager.GetComponent<SqlManager>().partyID);

        WWW www = new WWW(SqlManager.url + "ReadGroupChange.php", form);

        yield return www;

        if (www.text.Contains("<!DOCTYPE HTML"))
        {
            StartCoroutine(ReadGroupChange());
            yield break;
        }


        if (www.text == "Group removed")
        {
            AccountManager.GetComponent<SqlManager>().partyID = 0;
            AccountManager.GetComponent<SqlManager>().GroupID[0] = "";
            AccountManager.GetComponent<SqlManager>().GroupID[1] = "";
            AccountManager.GetComponent<SqlManager>().GroupID[2] = "";
            AccountManager.GetComponent<SqlManager>().GroupID[3] = "";
            //IMAGE GROUPE REMOVED
            yield break;
        }

        int friendNumber = 0;
        string[] players = www.text.Split("//".ToCharArray());

        foreach (string player in players)
        {
            if (player != "")
            {
                AccountManager.GetComponent<SqlManager>().GroupID[friendNumber] = player;
                friendNumber++;
            }
        }

        yield return new WaitForSeconds(3);
        StartCoroutine(ReadGroupChange());
    }

    
    public IEnumerator JoinQueueRead()
    {
        WWWForm form = new WWWForm();
        form.AddField("PlayerPost", AccountManager.GetComponent<SqlManager>().Username);

        WWW www = new WWW(SqlManager.url + "JoinQueueRead.php", form);

        yield return www;

        if (www.text == "Join")
        {
            AccountManager.GetComponent<SqlManager>().inQueue = true;
            MatchMaker matchMaker = GameObject.Find("NetworkManager").GetComponent<MatchMaker>();
            matchMaker.QueueObject.SetActive(true);
            StartCoroutine(matchMaker.ListPlayerInQueue());
            StartCoroutine(LeaveQueueRead());
            yield break;
        }

        yield return new WaitForSeconds(1.5f);
        StartCoroutine(JoinQueueRead());
    }

    public IEnumerator LeaveQueueRead()
    {
        WWWForm form = new WWWForm();
        form.AddField("PlayerPost", AccountManager.GetComponent<SqlManager>().Username);

        WWW www = new WWW(SqlManager.url + "LeaveQueueRead.php", form);

        yield return www;

        if (www.text == "Leave")
        {
            MatchMaker matchMaker = GameObject.Find("NetworkManager").GetComponent<MatchMaker>();
            matchMaker.QueueObject.SetActive(false);
            matchMaker.TimerMinutesFloat = 0;
            matchMaker.TimerSecondFloat = 0;
            StartCoroutine(JoinQueueRead());
            yield break;
        }

        yield return new WaitForSeconds(1.5f);
        StartCoroutine(LeaveQueueRead());
    }



    public void FriendRequestAcceptButton()
    {
        StartCoroutine(FriendRequestAccept());
    }

    private IEnumerator FriendRequestAccept ()
    {
        StartCoroutine(RemoveNotif("Friend"));

        WWWForm form = new WWWForm();
        form.AddField("PlayerPost", AccountManager.GetComponent<SqlManager>().Username);
        form.AddField("FriendPost", EventSystem.current.currentSelectedGameObject.transform.parent.GetComponentInChildren<Text>().text);

        WWW www = new WWW(SqlManager.url + "AcceptFriendRequest.php", form);
        yield return www;

        Destroy(EventSystem.current.currentSelectedGameObject.transform.parent.gameObject);
    }

    public void FriendRequestDenyButton()
    {
        StartCoroutine(RemoveNotif("Friend"));
        Destroy(EventSystem.current.currentSelectedGameObject.transform.parent.gameObject);
    }

    public void GroupRequestAcceptButton(int partyID)
    {
        StartCoroutine(GroupRequestAccept(partyID));
    }

    private IEnumerator GroupRequestAccept(int partyID)
    {
        StartCoroutine(RemoveNotif("Group"));

        if (AccountManager.GetComponent<SqlManager>().partyID != 0)
        {
            StartCoroutine(GameObject.Find("GroupManager").GetComponent<Group>().LeaveGroupIE());
            yield return new WaitForSeconds(2.5f);
        }

        WWWForm form = new WWWForm();
        form.AddField("PartyIDPost", partyID);
        form.AddField("PlayerPost", AccountManager.GetComponent<SqlManager>().Username);

        WWW www = new WWW(SqlManager.url + "JoinGroup.php", form);

        yield return www;

        AccountManager.GetComponent<SqlManager>().partyID = partyID;
        StartCoroutine(ReadGroupChange());
        StartCoroutine(JoinQueueRead());

        foreach(Transform child in MainMenuObject)
        {
            if(child.name != "MatchMaker")
                child.gameObject.SetActive(false);
        }

        MatchMakerObject.SetActive(true);

        Destroy(EventSystem.current.currentSelectedGameObject.transform.parent.gameObject);
    }

    public void GroupRequestDenyButton()
    {
        StartCoroutine(RemoveNotif("Group"));
        Destroy(EventSystem.current.currentSelectedGameObject.transform.parent.gameObject);
    }

    public void CustomGameAcceptButton(string player, string info)
    {
        CustomGameManager.GetComponent<CustomGame>().JoinGame(player, info);
        StartCoroutine(RemoveNotif("CustomGame"));
        Destroy(EventSystem.current.currentSelectedGameObject.transform.parent.gameObject);
    }

    public void CustomGameDenyButton()
    {
        StartCoroutine(RemoveNotif("CustomGame"));
        Destroy(EventSystem.current.currentSelectedGameObject.transform.parent.gameObject);
    }

    private IEnumerator RemoveNotif(string type)
    {
        WWWForm form = new WWWForm();
        form.AddField("ToPost", AccountManager.GetComponent<SqlManager>().Username);
        form.AddField("FromPost", EventSystem.current.currentSelectedGameObject.transform.parent.GetComponentInChildren<Text>().text);
        form.AddField("TypePost", type);

        WWW www = new WWW(SqlManager.url + "RemoveNotif.php", form);

        yield return www;
    }
}
