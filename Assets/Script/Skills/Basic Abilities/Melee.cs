using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;

public class Melee : NetworkBehaviour
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
        anim = GetComponentInChildren<Animator>();
        gm = GameObject.Find("GameManager").GetComponent<GameReference>();
        darkMask = gm.PassifMask;
        darkMask.sprite = gm.PassifIcon.sprite;
        //cdText = gm.PassifText;
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
        Transform castPosition = transform.Find("Cast");
        RaycastHit hit;
        if (Physics.Raycast(castPosition.position, castPosition.forward, out hit, 100))
        {
            print(hit.transform.name);
            gameObject.GetComponent<Hp>().CmdDamage(250, hit.collider.transform.parent.name, transform.name);
        }
    }
}
