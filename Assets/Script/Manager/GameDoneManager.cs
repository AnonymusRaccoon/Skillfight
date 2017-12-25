using UnityEngine;

public class GameDoneManager : MonoBehaviour {

    public GameObject SeeMoreStats;
    public GameObject GameDone;

	public void SeeMoreStatsBack ()
    {
        GameDone.SetActive(true);
        SeeMoreStats.SetActive(false);
    }

    public void SeeMoreStatsButton ()
    {
        SeeMoreStats.SetActive(true);
        GameDone.SetActive(false);
    }

    public void PlayAgain ()
    {
        Destroy(GameDone.transform.parent);
    }
}
