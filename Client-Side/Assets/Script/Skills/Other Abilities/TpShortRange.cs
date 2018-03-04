using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class TpShortRange : NetworkBehaviour {

    private const string scriptName = "TpShortRange";

	private bool BasicCd = true;

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
			StartCoroutine (Cd ());
			Manager.CmdTpShortRange (transform.name);
		}
	}

	private IEnumerator Cd ()
	{
		yield return new WaitForSeconds (25);
		BasicCd = true;
	}
}
