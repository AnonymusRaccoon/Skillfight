using UnityEngine;

public class AntiCloneNetworkManager : MonoBehaviour
{

    public static bool hasSpawn = false;

    private void Awake()
    {
        if (!hasSpawn)
        {
            hasSpawn = true;
        }
        else
            Destroy(gameObject);
    }
}
