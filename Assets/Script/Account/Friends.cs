using UnityEngine.UI;
using System.Collections;
using UnityEngine;

public class Friends : MonoBehaviour {

    private GameObject AccountManager;
    public GameObject FriendPrefab;
    public GameObject FriendParent;
    public GameObject Overlay;
    public GameObject FriendList;
    public GameObject PnotFound;


    public GameObject FriendInput;

    public GameObject rightClickPrefab;

    public GameObject NotifSend;

    private string[] friends;
    private int friendCount = 0;


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
        StartCoroutine(ListFriend());
    }

    public void AddFriendButton ()
    {
        StartCoroutine(AddFriend());
    }

    private IEnumerator AddFriend()
    {
        if (AccountManager.GetComponent<SqlManager>().Username == FriendInput.GetComponent<InputField>().text)
            yield break;

        WWWForm form = new WWWForm();
        form.AddField("PlayerPost", AccountManager.GetComponent<SqlManager>().Username);
        form.AddField("FriendPost", FriendInput.GetComponent<InputField>().text);

        WWW www = new WWW(SqlManager.url + "AddFriend.php", form);

        yield return www;
        print(www.text);

        if (www.text.Contains("<!DOCTYPE HTML PUBLIC"))
        {
            StartCoroutine(AddFriend());
            yield break;
        }

        if(www.text.Contains("unknow player"))
        {
            PnotFound.SetActive(true);
        }
        else
        {
            FriendInput.GetComponent<InputField>().text = "";
            NotifSend.SetActive(true);
            yield return new WaitForSeconds(3);
            NotifSend.SetActive(false);
        }
    }

    public void OpenFriendList()
    {
        FriendList.SetActive(true);
        Overlay.SetActive(false);
    }

    public void CloseFriendList()
    {
        FriendList.SetActive(false);
        Overlay.SetActive(true);
    }

    private IEnumerator ListFriend ()
    {
        WWWForm form = new WWWForm();
        form.AddField("PlayerPost", AccountManager.GetComponent<SqlManager>().Username);

        WWW www = new WWW(SqlManager.url + "ListFriend.php", form);

        yield return www;

        if (www.text.Contains("<!DOCTYPE HTML PUBLIC"))
        {
            StartCoroutine(ListFriend());
            yield break;
        }

        foreach (Transform child in FriendParent.transform)
        {
            Destroy(child.gameObject);
        }

        if (www.text == "No friend")
        {
            print("no friend");
            //Image JE SUIS SEUL AU MONDE
        }
        else
        {
            friendCount = 0;
            friends = www.text.Split("||".ToCharArray());
            foreach(string friend in friends)
            {
                if (friend != "")
                {
                    GameObject FriendObject = Instantiate(FriendPrefab);
                    FriendObject.transform.SetParent(FriendParent.transform, false);
                    FriendObject.GetComponentInChildren<Text>().text = friend;
                    friendCount++;
                }
            }
        }

        yield return new WaitForSeconds(10);
        StartCoroutine(ListFriend());
    }

    public void RemoveFriendButton (string friend)
    {
        StartCoroutine(RemoveFriend(friend));
    }

    private IEnumerator RemoveFriend (string friend)
    {
        WWWForm form = new WWWForm();
        form.AddField("PlayerPost", AccountManager.GetComponent<SqlManager>().Username);
        form.AddField("FriendPost", friend);

        WWW www = new WWW(SqlManager.url + "RemoveFriend.php", form);

        yield return www;
        print(www.text);

        if (www.text.Contains("<!DOCTYPE HTML PUBLIC"))
        {
            StartCoroutine(RemoveFriend(friend));
            yield break;
        }

        yield return new WaitForSeconds(1);
        StopCoroutine(ListFriend());
        StartCoroutine(ListFriend());
    }
}
