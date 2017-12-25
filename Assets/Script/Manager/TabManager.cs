using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class TabManager : MonoBehaviour {

    public GameObject tabPlayer;

    [Space]
    public Sprite MeleeSprite;
    public Sprite MidRangeSprite;
    public Sprite LongRangeSprite;

    [Space]
    public GameObject ScoreBoard;
    public GameObject Team1;
    public GameObject Team2;

    [Space]
    public Text Top1;
    public Text Top2;
    public Text Top3;

    [HideInInspector] public string Top1String = "Take Me !!";
    [HideInInspector] public string Top2String = "Take Me !!";
    [HideInInspector] public string Top3String = "Take Me !!";

    [HideInInspector] public bool team = false;
    private bool useTeam1 = true;

    FirstPersonController controller;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();   
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyManager.KM.scoreboard) && !controller.Select)
        {
            useTeam1 = true;

            ScoreBoard.SetActive(true);
            Top1.text = Top1String;
            Top2.text = Top2String;
            Top3.text = Top3String;

            foreach (Transform child in Team1.transform)
                Destroy(child.gameObject);
            foreach (Transform child in Team2.transform)
                Destroy(child.gameObject);

            foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (team)
                {
                    //Faire le tri par team
                }
                else
                {
                    GameObject tabObject = Instantiate(tabPlayer, transform.position, transform.rotation);
                    tabObject.transform.SetParent(useTeam1 ? Team1.transform : Team2.transform);
                    tabObject.transform.Find("Pseudo").GetComponent<Text>().text = player.GetComponent<SetupLocalPlayer>().pname;
                    tabObject.transform.Find("Score").GetComponent<Text>().text = player.GetComponent<SetupLocalPlayer>().score;

                    if (player.GetComponent<SetupLocalPlayer>().Passif == "Melee")
                        tabObject.transform.Find("Image").GetComponent<Image>().sprite = MeleeSprite;
                    if (player.GetComponent<SetupLocalPlayer>().Passif == "MidRange")
                        tabObject.transform.Find("Image").GetComponent<Image>().sprite = MidRangeSprite;
                    if (player.GetComponent<SetupLocalPlayer>().Passif == "LongRange")
                        tabObject.transform.Find("Image").GetComponent<Image>().sprite = LongRangeSprite;
                    useTeam1 = !useTeam1;
                }
            }
        }
        if(Input.GetKeyUp(KeyManager.KM.scoreboard))
        {
            ScoreBoard.SetActive(false);
        }
    }
}
