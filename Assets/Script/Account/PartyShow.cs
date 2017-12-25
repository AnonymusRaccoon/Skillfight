using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class PartyShow : MonoBehaviour {

    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;

    [Space]
    public GameObject MainMenuScene;

    private GameObject AccountManager;

    private void OnEnable()
    {
        AccountManager = GameObject.Find("AccountManager");
        StartCoroutine(ListGroup());
    }

    public IEnumerator ListGroup()
    {
        Player1.SetActive(false);
        Player2.SetActive(false);
        Player3.SetActive(false);
        Player4.SetActive(false);
        if (AccountManager.GetComponent<SqlManager>().GroupID[0] == "")
        {
            //IMAGE PAS DE TYPE DANS LA PARTY
        }
        else
        {
            Player1.SetActive(true);
            Player1.GetComponentInChildren<Text>().text = AccountManager.GetComponent<SqlManager>().GroupID[0];
            if (AccountManager.GetComponent<SqlManager>().GroupID[1] != "")
            {
                Player2.SetActive(true);
                Player2.GetComponentInChildren<Text>().text = AccountManager.GetComponent<SqlManager>().GroupID[1];
            }
            if (AccountManager.GetComponent<SqlManager>().GroupID[2] != "")
            {
                Player3.SetActive(true);
                Player3.GetComponentInChildren<Text>().text = AccountManager.GetComponent<SqlManager>().GroupID[2];
            }
            if (AccountManager.GetComponent<SqlManager>().GroupID[3] != "")
            {
                Player4.SetActive(true);
                Player4.GetComponentInChildren<Text>().text = AccountManager.GetComponent<SqlManager>().GroupID[3];
            }
        }

        yield return new WaitForSeconds(5);
        StartCoroutine(ListGroup());
    }

    public void Back()
    {
        MainMenuScene.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }
}
