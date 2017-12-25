using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MidRange : NetworkBehaviour
{

    private GameReference gm;
    private FirstPersonController controller;
    private Animator anim;

    private Image darkMask;
    //private Text cdText;

    private float coolDownDuration = 0.5f;
    private float nextReadyTime;
    private float coolDownTimeLeft;

    public float CoconutSpeed = 750;

    private void Start()
    {
        controller = GetComponent<FirstPersonController>();
        StartCoroutine(LoadAnimator());
        gm = GameObject.Find("GameManager").GetComponent<GameReference>();
        darkMask = gm.PassifMask;
        darkMask.sprite = gm.PassifIcon.sprite;
        //cdText = gm.PassifText;
    }

    private IEnumerator LoadAnimator()
    {
        yield return new WaitForSeconds(1.5f);
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        bool coolDownComplete = (Time.time > nextReadyTime);
        if (coolDownComplete)
        {
            AbilityReady();
            if (Input.GetKeyDown(KeyManager.KM.fire) && controller.Select == false && controller.Pause == false && isLocalPlayer)
            {
                anim.Play("Mouse 1");
                SpellTriggered();
            }
        }
        else
        {
            CoolDown();
        }
    }

    private void AbilityReady()
    {
        //cdText.enabled = false;
        darkMask.enabled = false;
    }

    private void CoolDown()
    {
        coolDownTimeLeft -= Time.deltaTime;
        //int roundedCd = (int)Mathf.Round(coolDownTimeLeft);
        //cdText.text = roundedCd.ToString();
        darkMask.fillAmount = (coolDownTimeLeft / coolDownDuration);
    }

    private void SpellTriggered()
    {
        nextReadyTime = coolDownDuration + Time.time;
        coolDownTimeLeft = coolDownDuration;
        darkMask.enabled = true;
        //cdText.enabled = true;

        Fire();
    }

    public void Fire()
    {
        CmdFire(gameObject, GameObject.FindGameObjectWithTag("MainCamera").transform.forward);
    }

    [Command]
    void CmdFire(GameObject player, Vector3 forward)
    {
        Transform castPosition = player.transform.Find("Cast");
        GameObject Coconut = Instantiate(GameObject.Find("GameManager").GetComponent<GameReference>().CoconutsPrefab, castPosition.position, player.transform.rotation);
        Coconut.transform.name = "Coconut " + transform.name;
        Rigidbody CoconutRb = Coconut.GetComponent<Rigidbody>();
        CoconutRb.AddForce(forward * CoconutSpeed);
        NetworkServer.Spawn(Coconut);
    }
}