using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class playerStats : NetworkBehaviour {

    [SyncVar]
    public int Kills = 0;
    [SyncVar]
    public int Deaths = 0;
    [SyncVar]
    public int DamageTaken = 0;
    [SyncVar]
    public int DamageDealt = 0;
    [SyncVar]
    public int KillStreak;
    [SyncVar]
    public int BestKillStreak = 0;
}
