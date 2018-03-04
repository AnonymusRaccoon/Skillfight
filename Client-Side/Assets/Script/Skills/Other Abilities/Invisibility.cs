using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class Invisibility : NetworkBehaviour {

    private const string scriptName = "Invisibility";

	private bool BasicCd = true;
	public bool IsInvisible = false;

    private GameManager Manager;

    void OnEnable()
    {
        if (GetComponent<SetupLocalPlayer>().Abilities1 == scriptName)
            EventManager.Skill1 += Trigger;
        if (GetComponent<SetupLocalPlayer>().Abilities2 == scriptName)
            EventManager.Skill2 += Trigger;
        if (GetComponent<SetupLocalPlayer>().Abilities3 == scriptName)
            EventManager.Skill3 += Trigger;
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void OnDisable()
    {
        if (GetComponent<SetupLocalPlayer>().Abilities1 == scriptName)
            EventManager.Skill1 -= Trigger;
        if (GetComponent<SetupLocalPlayer>().Abilities2 == scriptName)
            EventManager.Skill2 -= Trigger;
        if (GetComponent<SetupLocalPlayer>().Abilities3 == scriptName)
            EventManager.Skill3 -= Trigger;
    }

    void Trigger ()
	{
		if (BasicCd)
		{
			BasicCd = false;
			IsInvisible = true;
			Manager.CmdInvisibility (transform.name, true);
		}
		if (IsInvisible)
		{
			IsInvisible = false;
			Manager.CmdInvisibility (transform.name, false);
			StartCoroutine (Cd ());
		}
	}

	private IEnumerator Cd ()
	{
		yield return new WaitForSeconds (10);
		BasicCd = true;
	}
}
