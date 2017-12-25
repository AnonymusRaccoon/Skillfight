using UnityEngine;
using UnityEngine.Networking;

 public class EventManager : NetworkBehaviour {

	FirstPersonController controller;

	public delegate void GetBasicAttack();
	public static event GetBasicAttack BasicAttack;

	public delegate void GetSkill1 ();
	public static event GetSkill1 Skill1;

	public delegate void GetSkill2 ();
	public static event GetSkill2 Skill2;

	public delegate void GetSkill3 ();
	public static event GetSkill3 Skill3;

    private void Start()
    {
        controller = GetComponent<FirstPersonController>();
    }

    public virtual bool InputBasicAttack {

		get {
            return Input.GetKey(KeyManager.KM.fire);
        }

    }

	public virtual bool InputSkill1 {

		get {
			return Input.GetKey (KeyManager.KM.firstskill);
		}

	}

	public virtual bool InputSkill2 {

		get {
            return Input.GetKey(KeyManager.KM.secondskill);
        }

    }

	public virtual bool InputSkill3 {

		get {
            return Input.GetKey(KeyManager.KM.thirdskill);
        }

    }


	void Update () {

		if (InputBasicAttack == true && gameObject.GetComponent<NetworkIdentity>().isLocalPlayer && controller.Select == false && controller.Pause == false) {
			if (BasicAttack != null) BasicAttack ();		 
		} if (InputSkill1 == true && gameObject.GetComponent<NetworkIdentity>().isLocalPlayer && controller.Select == false && controller.Pause == false) {
			if (Skill1 != null) Skill1 ();		 
		} if (InputSkill2 == true && gameObject.GetComponent<NetworkIdentity>().isLocalPlayer && controller.Select == false && controller.Pause == false) {
			if (Skill2 != null) Skill2 ();		 
		} if (InputSkill3 == true && gameObject.GetComponent<NetworkIdentity>().isLocalPlayer && controller.Select == false && controller.Pause == false) {
			if (Skill3 != null) Skill3 ();		 
		}
	 }
}