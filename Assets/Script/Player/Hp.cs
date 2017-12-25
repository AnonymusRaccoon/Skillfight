using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class Hp : NetworkBehaviour {

	[SyncVar]
	public float Health = 1000;
    [SyncVar]
    public float maxHealth = 1000;

	public Slider HealthSlider;
    private GameObject DeathObj;
    private GameObject DeathText;
    private GameObject InGameHUD;

    public delegate void Death(string playerID, string attackerID);
    public static event Death playerDeath;

    private void OnEnable()
    {
        playerDeath += deathCD;
        
    }

    private void OnDisable()
    {
        playerDeath -= deathCD;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        DeathObj = GameObject.Find("GameManager").GetComponent<GameReference>().deathUI;
        DeathText = GameObject.Find("GameManager").GetComponent<GameReference>().deathText;
        HealthSlider = GameObject.Find("GameManager").GetComponent<GameReference>().HealthSlider;
        InGameHUD = GameObject.Find("GameManager").GetComponent<GameReference>().InGameHUD;
    }

    [Command]
	public void CmdDamage (float degat, string playerID, string attackerID) 
	{
		RpcDamage (degat, playerID, attackerID);	
	}
	
	[ClientRpc]
	void RpcDamage (float degat, string playerID, string attackerID) 
	{
        GameObject player = GameObject.Find(playerID);
        GameObject attacker = GameObject.Find(attackerID);
        player.GetComponent<Hp> ().Health -= degat;
        player.GetComponent<playerStats>().DamageTaken += (int) degat;
        attacker.GetComponent<playerStats>().DamageDealt += (int)degat;
        print(player);
        player.GetComponent<SetupLocalPlayer>().NickName.transform.Find("Hp").GetComponent<TextMesh>().text = player.GetComponent<Hp>().Health.ToString() + "/" + player.GetComponent<Hp>().maxHealth.ToString() + "Hp";
		if (player.GetComponent<Hp> ().Health <= 0) 
		{
            player.GetComponent<playerStats>().Deaths += 1;
            attacker.GetComponent<playerStats>().Kills += 1;
            if (player.GetComponent<playerStats>().BestKillStreak < player.GetComponent<playerStats>().KillStreak)
                player.GetComponent<playerStats>().BestKillStreak = player.GetComponent<playerStats>().KillStreak;
            player.GetComponent<playerStats>().KillStreak = 0;
            attacker.GetComponent<playerStats>().KillStreak += 1;
            if (playerDeath != null)
                playerDeath(playerID, attackerID);
        }
        if (transform.name == playerID)
        {
            HealthSlider.value = Health;
        }
    }

    void deathCD (string playerID, string attackerID)
    {
        GameObject player = GameObject.Find(playerID);
        if (player.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            DeathObj.SetActive(true);
            DeathText.GetComponent<Text>().text = "5";
            InGameHUD.SetActive(false);
            StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn ()
    {
        yield return new WaitForSeconds(1);
        DeathText.GetComponent<Text>().text = "4";
        yield return new WaitForSeconds(1);
        DeathText.GetComponent<Text>().text = "3";
        yield return new WaitForSeconds(1);
        DeathText.GetComponent<Text>().text = "3";
        yield return new WaitForSeconds(1);
        DeathText.GetComponent<Text>().text = "2";
        yield return new WaitForSeconds(1);
        DeathText.GetComponent<Text>().text = "1";
        yield return new WaitForSeconds(1);
        transform.position = GameObject.Find("Spawn (" + Random.Range(1, 8).ToString() + ")").transform.position;
        Health = maxHealth;
        InGameHUD.SetActive(true);
        DeathObj.SetActive(false);
    }
}
