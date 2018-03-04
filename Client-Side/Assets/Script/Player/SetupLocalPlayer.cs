using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class SetupLocalPlayer : NetworkBehaviour {

    private string ID;

    [SerializeField] private RuntimeAnimatorController MeleeAnimation;
    [SerializeField] private RuntimeAnimatorController MidRangeAnimation;
    [SerializeField] private RuntimeAnimatorController LongRangeAnimation;


    [SyncVar]
	public string pname = "player";

	[SyncVar]
	public string score = "00/00";

	[SyncVar]
	public int TimerMinute = 1;

	[SyncVar]
	public float TimerSecond = 30;

    [SyncVar]
    public string OldPassif;

	[SyncVar]
	public string Passif;


	[SyncVar]
	public string Abilities1;

	[SyncVar]
	public string Abilities2;

	[SyncVar]
	public string Abilities3;

    [SyncVar]
    public GameObject NickName;

	public bool ready = false;

    public bool canStartLoading = false;

    private bool allReady = false;

    FirstPersonController controller;

	public delegate void Spawn(string playerID);
    public static event Spawn playerSpawn;
	
	void OnEnable ()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            ID = "Player " + player.GetComponent<NetworkIdentity>().netId.ToString();
            player.transform.name = ID;
        }
	}

    private IEnumerator Start ()
    {
        yield return new WaitForSeconds(1);
        canStartLoading = true;
        foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponent<NetworkIdentity>().isLocalPlayer)
                player.GetComponent<SetupLocalPlayer>().pname = GameObject.Find("AccountManager").GetComponent<SqlManager>().Username;
        }
    }

	public override void OnStartLocalPlayer ()
    {
        base.OnStartLocalPlayer();
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject player in players)
        {
            if (player.GetComponent<NetworkIdentity> ().isLocalPlayer)
            {
				GameObject.FindGameObjectWithTag ("MainCamera").transform.SetParent (transform);
				GameObject.FindGameObjectWithTag ("MainCamera").transform.localPosition = new Vector3 (0, 1.5f, 0);
				//transform.gameObject.AddComponent (typeof(EventManager));
                StartCoroutine(SetupFromGameManager());
            }
		}
	}

    private IEnumerator SetupFromGameManager()
    {
        yield return new WaitForSeconds(1.5f);
        string playerID = "";
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponent<NetworkIdentity>().isLocalPlayer)
                playerID = player.transform.name;
        }
        if(playerSpawn != null)
            playerSpawn(playerID);
    }

	void Update ()
    {
		if (controller == null)
        {
			GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
			foreach (GameObject player in players)
            {
				if (player.GetComponent<NetworkIdentity> ().isLocalPlayer)
                {
					controller = player.GetComponent<FirstPersonController> ();
				}
			}
		} 
		if (controller != null && controller.Select == true && canStartLoading)
        {
            if (TimerSecond > 9)
            {
                GameObject.FindGameObjectWithTag("TimerText").GetComponent<Text>().text = TimerMinute + ":" + Mathf.RoundToInt(TimerSecond).ToString();
            }
            else
            {
                GameObject.FindGameObjectWithTag("TimerText").GetComponent<Text>().text = TimerMinute + ":0" + Mathf.RoundToInt(TimerSecond).ToString();
            }
            if (isServer)
            {
                if (TimerMinute <= -1)
                {
                    RpcEndTimer();
                    RpcChangeName();
                    return;
                }
                TimerSecond -= Time.deltaTime;
                if (TimerSecond <= 0)
                {
                    TimerMinute -= 1;
                    TimerSecond = 59;
                }
                if (allReady == false)
                {
                    foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                    {
                        if (player.GetComponent<SetupLocalPlayer>().ready == false)
                        {
                            return;
                        }
                    }
                    TimerSecond = 10;
                    TimerMinute = 0;
                    allReady = true;
                }                
			}
		}
        if ("Player " + GetComponent<NetworkIdentity> ().netId.ToString () != transform.name)
        {
            CmdNetIdChanged("Player " + GetComponent<NetworkIdentity>().netId.ToString(), transform.name);
        }
	}

    [Command]
    void CmdNetIdChanged (string newName, string oldName)
    {
        RpcNetIdChanged(newName, oldName);
    }

    [ClientRpc]
    void RpcNetIdChanged (string newName, string oldName)
    {
        if(GameObject.Find(oldName) != null)
            GameObject.Find(oldName).transform.name = newName;
    }		

	[ClientRpc]
	void RpcEndTimer ()
    {
        GameObject.Find("SkillSelect").SetActive(false);
        controller.Select = false;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            Destroy(player.GetComponent<AbilitiesNetworker>());
        }
    }

    [ClientRpc]
    void RpcChangeName ()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            player.GetComponent<SetupLocalPlayer>().NickName.transform.Find("Pseudo").GetComponent<TextMesh>().text = player.GetComponent<SetupLocalPlayer>().pname;
        }
    }

    [Command]
    public void CmdReady (string playerID)
    {
        GameObject player = GameObject.Find(playerID);
        player.GetComponent<SetupLocalPlayer>().ready = true;
        RpcReady(player);
    }

    [ClientRpc]
    void RpcReady(GameObject player)
    {
        foreach (NetworkBehaviour comp in player.GetComponents<NetworkBehaviour>())
        {
            if (comp.enabled == false)
                Destroy(comp);
        }
    }
}