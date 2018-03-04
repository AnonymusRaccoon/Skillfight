using System.Collections;
using UnityEngine.Networking;
using UnityEngine;

public class cgConnect : NetworkBehaviour {

    private CustomGame cg;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.3f);
        cg = GameObject.Find("CustomGameManager").GetComponent<CustomGame>();
        CmdPlayerConnected(gameObject, GameObject.Find("AccountManager").GetComponent<SqlManager>().Username);
    }

    [Command]
    public void CmdPlayerConnected(GameObject player, string playerName)
    {
        player.GetComponent<NameUpdater>().playerName = playerName;
        RpcPlayerConnected();
    }

    [ClientRpc]
    private void RpcPlayerConnected()
    {
        StartCoroutine(SetPlayerName());
    }

    private IEnumerator SetPlayerName()
    {
        yield return new WaitForSeconds(0.2f);
        foreach (GameObject lPlayer in GameObject.FindGameObjectsWithTag("LobbyPlayer"))
        {
            lPlayer.name = lPlayer.GetComponent<NameUpdater>().playerName;
        }
        cg.ListPlayerInGame();
    }
}
