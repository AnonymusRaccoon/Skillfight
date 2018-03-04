using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class Group : MonoBehaviour {

    private GameObject AccountManager;
    public GameObject NotifSend;
    public GameObject PnotFound;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        AccountManager = GameObject.Find("AccountManager");
    }

    public void InviteFriendToGroupButton(string friend)
    {
        StartCoroutine(InviteFriendToGroup(friend));
    }

    private IEnumerator InviteFriendToGroup(string friend)
    {
        if (AccountManager.GetComponent<SqlManager>().partyID == 0)
        {
            WWWForm form = new WWWForm();
            form.AddField("PlayerPost", AccountManager.GetComponent<SqlManager>().Username);

            WWW www = new WWW(SqlManager.url + "CreateGroup.php", form);

            yield return www;

            if (www.text.Contains("<!DOCTYPE HTML"))
            {
                StartCoroutine(InviteFriendToGroup(friend));
                yield break;
            }

            AccountManager.GetComponent<SqlManager>().partyID = int.Parse(www.text);
        }
        WWWForm form2 = new WWWForm();
        form2.AddField("PlayerPost", AccountManager.GetComponent<SqlManager>().Username);
        form2.AddField("FriendPost", friend);
        form2.AddField("PartyIDPost", AccountManager.GetComponent<SqlManager>().partyID);

        WWW www2 = new WWW(SqlManager.url + "InviteInGroup.php", form2);

        yield return www2;

        if(www2.text.Contains("unknow player"))
        {
            PnotFound.SetActive(true);
            yield break;
        }

        if (www2.text.Contains("<!DOCTYPE HTML"))
        {
            StartCoroutine(InviteFriendToGroup(friend));
            yield break;
        }
        NotifSend.SetActive(true);
        StartCoroutine(GameObject.Find("RequestReader").GetComponent<RequestReader>().ReadGroupChange());
        yield return new WaitForSeconds(3);
        NotifSend.SetActive(false);
    }

    public void LeaveGroup()
    {
        StartCoroutine(LeaveGroupIE());
    }

    public IEnumerator LeaveGroupIE()
    {
        WWWForm form = new WWWForm();
        form.AddField("PartyIDPost", AccountManager.GetComponent<SqlManager>().partyID);

        if (AccountManager.GetComponent<SqlManager>().GroupID[0] == AccountManager.GetComponent<SqlManager>().Username)
            form.AddField("PlayerNumberPost", 1);

        if (AccountManager.GetComponent<SqlManager>().GroupID[1] == AccountManager.GetComponent<SqlManager>().Username)
            form.AddField("PlayerNumberPost", 2);

        if (AccountManager.GetComponent<SqlManager>().GroupID[2] == AccountManager.GetComponent<SqlManager>().Username)
            form.AddField("PlayerNumberPost", 3);

        if (AccountManager.GetComponent<SqlManager>().GroupID[3] == AccountManager.GetComponent<SqlManager>().Username)
            form.AddField("PlayerNumberPost", 4);

        WWW www = new WWW(SqlManager.url + "LeaveGroup.php", form);

        yield return www;

        StopCoroutine(GameObject.Find("RequestManager").GetComponent<RequestReader>().ReadGroupChange());
        StopCoroutine(GameObject.Find("RequestManager").GetComponent<RequestReader>().JoinQueueRead());
        StopCoroutine(GameObject.Find("RequestManager").GetComponent<RequestReader>().LeaveQueueRead());

        AccountManager.GetComponent<SqlManager>().partyID = 0;
        AccountManager.GetComponent<SqlManager>().GroupID[0] = AccountManager.GetComponent<SqlManager>().Username;
        AccountManager.GetComponent<SqlManager>().GroupID[1] = "";
        AccountManager.GetComponent<SqlManager>().GroupID[2] = "";
        AccountManager.GetComponent<SqlManager>().GroupID[3] = "";

        MatchMaker matchMaker = GameObject.Find("NetworkManager").GetComponent<MatchMaker>();
        matchMaker.QueueObject.SetActive(false);
        matchMaker.TimerMinutesFloat = 0;
        matchMaker.TimerSecondFloat = 0;

        if (www.text.Contains("HTML"))
            StartCoroutine(LeaveGroupIE());
    }

    private void OnApplicationQuit()
    {
        if (AccountManager.GetComponent<SqlManager>().partyID != 0)
            LeaveGroup();
    }
}