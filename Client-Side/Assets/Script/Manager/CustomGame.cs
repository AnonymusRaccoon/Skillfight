using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;

public class CustomGame : MonoBehaviour {

    private GameObject AccountManager;
    public CustomGameNetworker cgNetworker;
    public GameObject CustomGameLobby;
    public GameObject BackGround;
    public GameObject MainMenu;
    public GameObject PlayButton;
    public Transform CanvasObject;

    [Space]
    public GameObject GameModeChangeObject;
    public GameObject MapChangeObject;
    public GameObject StartGameButton;

    [Space]
    public GameObject AddFriendObj;
    public GameObject PlayersList;
    public GameObject FriendField;
    public GameObject FriendParent;
    public GameObject FriendPrefab;
    public GameObject NotifSend;
    public GameObject PnotFound;

    [Space]
    public Sprite DefaultBackground;
    public Sprite SelectBackground;

    [Space]
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;
    public GameObject Player5;
    public GameObject Player6;
    public GameObject Player7;
    public GameObject Player8;

    private void OnEnable()
    {
        AccountManager = GameObject.Find("AccountManager");
    }

    public void CustomGameCreate()
    {
        NetworkLobbyManager manager = GameObject.Find("NetworkManager").GetComponent<NetworkLobbyManager>();
        manager.networkAddress = "localhost";
        manager.networkPort = 4444;
        manager.StartHost();
        foreach (Transform Child in CanvasObject)
        {
            if (Child.name != "Custom Game")
                Child.gameObject.SetActive(false);
        }
        CustomGameLobby.SetActive(true);
    }

    public void JoinGame(string player, string info)
    {
        NetworkLobbyManager manager = GameObject.Find("NetworkManager").GetComponent<NetworkLobbyManager>();
        manager.networkAddress = info;
        manager.networkPort = 4444;
        manager.StartClient();
        foreach (Transform Child in CanvasObject)
        {
            if (Child.name != "Custom Game")
                Child.gameObject.SetActive(false);
        }
        CustomGameLobby.SetActive(true);
    }

    public void ListPlayerInGame()
    {
        List<string> PlayersID = new List<string> ();
        foreach(GameObject lPlayer in GameObject.FindGameObjectsWithTag("LobbyPlayer"))
        {
            PlayersID.Add(lPlayer.name);
        }
        Player1.GetComponentInChildren<Text>().text = PlayersID[0];
        if (PlayersID.Count > 1)
            Player2.GetComponentInChildren<Text>().text = PlayersID[1];
        if (PlayersID.Count > 2)
            Player2.GetComponentInChildren<Text>().text = PlayersID[2];
        if (PlayersID.Count > 3)
            Player2.GetComponentInChildren<Text>().text = PlayersID[3];
        if (PlayersID.Count > 4)
            Player2.GetComponentInChildren<Text>().text = PlayersID[4];
        if (PlayersID.Count > 5)
            Player2.GetComponentInChildren<Text>().text = PlayersID[5];
        if (PlayersID.Count > 6)
            Player2.GetComponentInChildren<Text>().text = PlayersID[6];
        if (PlayersID.Count > 7)
            Player2.GetComponentInChildren<Text>().text = PlayersID[7];
    }

    public IEnumerator ListFriend()
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
            int friendCount = 0;
            string[] friends = www.text.Split("||".ToCharArray());
            foreach (string friend in friends)
            {
                if (friend != "")
                {
                    GameObject FriendObject = Instantiate(FriendPrefab);
                    FriendObject.transform.SetParent(FriendParent.transform, false);
                    FriendObject.GetComponentInChildren<Text>().text = friend;
                    FriendObject.GetComponentInChildren<Button>().onClick.AddListener(delegate { StartCoroutine(InviteFriend(friend)); });
                    friendCount++;
                }
            }
        }
    }

    public void InviteFriendField()
    {
        StartCoroutine(InviteFriend(FriendField.GetComponent<InputField>().text));
        FriendField.GetComponent<InputField>().text = "";
    }

    private IEnumerator InviteFriend(string friend)
    {
        WWWForm form = new WWWForm();
        form.AddField("PlayerPost", AccountManager.GetComponent<SqlManager>().Username);
        form.AddField("FriendPost", friend);
        form.AddField("ipPost", Network.player.ipAddress);

        WWW www = new WWW(SqlManager.url + "CGInvite.php", form);

        yield return www;

        if (www.text.Contains("<!DOCTYPE HTML PUBLIC"))
        {
            StartCoroutine(InviteFriend(friend));
            yield break;
        }
        if(www.text.Contains("unknow player"))
        {
            PnotFound.SetActive(true);
            yield break;
        }
        NotifSend.SetActive(true);
        yield return new WaitForSeconds(3);
        NotifSend.SetActive(false);
    }

    public void Back()
    {
        foreach(Transform child in CanvasObject)
        {
            child.gameObject.SetActive(false);
        }
        MainMenu.SetActive(true);
        PlayButton.GetComponent<Button>().onClick.RemoveAllListeners();
        PlayButton.GetComponent<Button>().onClick.AddListener(ShowCustomGameLobby);
    }

    public void ShowCustomGameLobby()
    {
        MainMenu.SetActive(true);
        CustomGameLobby.SetActive(true);
    }
}
