using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine;

public class CustomGameNetworker : NetworkBehaviour {

    public CustomGame cg;

    private GameObject ChangeWhat;

    [SyncVar]
    public string GameMode = "Random";
    [SyncVar]
    public string Map = "Random";

    public void StartGame()
    {
        if (!isServer)
            return;
        RpcSetReady();
    }

    [ClientRpc]
    public void RpcSetReady()
    {
        if (GameObject.FindGameObjectsWithTag("LobbyPlayer").Length > 0)
        {
            foreach (GameObject netPlayerObj in GameObject.FindGameObjectsWithTag("LobbyPlayer"))
            {
                NetworkLobbyPlayer netPlayer = netPlayerObj.GetComponent<NetworkLobbyPlayer>();
                if (netPlayer.isLocalPlayer)
                    netPlayer.SendReadyToBeginMessage();
            }
        }
    }

    public void AddFriendButton()
    {
        if (!isServer)
            return;
        cg.AddFriendObj.SetActive(true);
        StartCoroutine(cg.ListFriend());
    }

    public void KickPlayer()
    {
        if (!isServer)
            return;
        foreach (Transform child in cg.PlayersList.transform)
        {
            if (child.GetComponentInChildren<Text>().text != "Waiting...")
            {
                child.GetComponentInChildren<Text>().color = Color.red;
                child.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
                child.GetComponentInChildren<Button>().onClick.AddListener(delegate { CmdKickPlayerButton(child.GetComponentInChildren<Text>().text); });
            }
        }
        EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.AddListener(KickPlayerEnd);
    }

    [Command]
    public void CmdKickPlayerButton(string player)
    {
        foreach(GameObject lPlayer in GameObject.FindGameObjectsWithTag("LobbyPlayer"))
        {
            if (lPlayer.name == player)
                lPlayer.GetComponent<NetworkIdentity>().connectionToClient.Disconnect();
        }
        foreach (Transform child in cg.PlayersList.transform)
        {
            child.GetComponentInChildren<Text>().color = Color.black;
            child.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        }
        RpcPlayerKicked(player);
    }

    [ClientRpc]
    private void RpcPlayerKicked(string player)
    {
        foreach(Transform child in cg.PlayersList.transform)
        {
            if (child.GetComponentInChildren<Text>().text == player)
                child.GetComponentInChildren<Text>().text = "Waiting...";
        }
    }

    void KickPlayerEnd()
    {
        EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.AddListener(KickPlayer);
        foreach (Transform child in cg.PlayersList.transform)
        {
            child.GetComponentInChildren<Text>().color = Color.black;
            child.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        }
    }

    public void SelectMap()
    {
        if (!isServer)
            return;
        cg.StartGameButton.SetActive(false);
        cg.BackGround.GetComponent<Image>().sprite = cg.SelectBackground;
        cg.MapChangeObject.SetActive(true);
        ChangeWhat = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.AddListener(CancelChooser);
    }

    public void SelectGameMode()
    {
        if (!isServer)
            return;
        cg.StartGameButton.SetActive(false);
        cg.BackGround.GetComponent<Image>().sprite = cg.SelectBackground;
        cg.GameModeChangeObject.SetActive(true);
        ChangeWhat = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.AddListener(CancelChooser);
    }

    public void CancelChooser()
    {
        cg.StartGameButton.SetActive(true);
        cg.BackGround.GetComponent<Image>().sprite = cg.DefaultBackground;
        cg.GameModeChangeObject.SetActive(false);
        cg.MapChangeObject.SetActive(false);
        cg.GameModeChangeObject.GetComponent<Button>().onClick.RemoveAllListeners();
        cg.GameModeChangeObject.GetComponent<Button>().onClick.AddListener(SelectGameMode);
        cg.MapChangeObject.GetComponent<Button>().onClick.RemoveAllListeners();
        cg.MapChangeObject.GetComponent<Button>().onClick.AddListener(SelectMap);
    }

    public void ChangeGameMode(string GM)
    {
        CancelChooser();
        cg.StartGameButton.SetActive(true);
        GameMode = GM;
        cg.BackGround.GetComponent<Image>().sprite = cg.DefaultBackground;
        cg.GameModeChangeObject.SetActive(false);
        ChangeWhat.transform.GetChild(0).GetComponent<Text>().text = GM;
    }

    public void ChangeMap(string newMap)
    {
        CancelChooser();
        cg.StartGameButton.SetActive(true);
        Map = newMap;
        cg.BackGround.GetComponent<Image>().sprite = cg.DefaultBackground;
        cg.MapChangeObject.SetActive(false);
        ChangeWhat.transform.GetChild(0).GetComponent<Text>().text = newMap;
    }

    public void LeaveGame()
    {
        NetworkLobbyManager manager = GameObject.Find("NetworkManager").GetComponent<NetworkLobbyManager>();
        manager.StopClient();
        Network.Disconnect();
        if (isServer)
        {
            manager.StopHost();
            NetworkLobbyManager.Shutdown();
            NetworkServer.Reset();
            GameObject.Find("NetworkManager").GetComponent<NetworkManager>().StopServer();
        }

        foreach (Transform Child in cg.CanvasObject)
        {
            Child.gameObject.SetActive(false);
        }
        cg.MainMenu.SetActive(true);
        cg.PlayButton.GetComponent<Button>().onClick.RemoveAllListeners();
        cg.PlayButton.GetComponent<Button>().onClick.AddListener(delegate { EventSystem.current.currentSelectedGameObject.transform.parent.GetComponent<MainMenu>().Play(); });
    }
}
