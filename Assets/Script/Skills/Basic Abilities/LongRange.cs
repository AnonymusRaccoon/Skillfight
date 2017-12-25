using System.Collections;
using UnityEngine;

public class LongRange : MonoBehaviour {

    private bool BasicCd = true;

    private GameManager Manager;

    void OnEnable()
    {
        EventManager.BasicAttack += Trigger;
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void OnDisable()
    {
        EventManager.BasicAttack -= Trigger;
    }

    public void Trigger()
    {

        if (BasicCd == true)
        {
            BasicCd = false;
            StartCoroutine(Cd());
            Manager.CmdLongShot(transform.name);
        }
    }

    private IEnumerator Cd()
    {
        yield return new WaitForSeconds(2.5f);
        BasicCd = true;
    }
}
