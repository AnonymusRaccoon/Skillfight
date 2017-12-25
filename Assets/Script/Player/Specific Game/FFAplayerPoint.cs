using System.Collections;
using UnityEngine.Networking;
using UnityEngine;

public class FFAplayerPoint : NetworkBehaviour {

    [SyncVar]
    public int score = 0;
}
