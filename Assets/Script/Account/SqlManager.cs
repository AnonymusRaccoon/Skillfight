using UnityEngine;

public class SqlManager : MonoBehaviour {

    [HideInInspector]
    public const string url = "http://skillfight.gear.host/";

    public int ID;
    public string Username;

    public string[] GroupID;
    public int partyID;

    [Space]
    public bool inQueue = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            print(GetType());
            Destroy(gameObject);
        }
    }

}
