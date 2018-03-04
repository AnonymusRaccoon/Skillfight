using System.Collections;
using UnityEngine.Networking;
using UnityEngine;

public class NameUpdater : NetworkBehaviour {

    [SyncVar]
    public string playerName;
}
