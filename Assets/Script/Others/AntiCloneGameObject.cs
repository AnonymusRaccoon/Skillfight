using UnityEngine;

public class AntiCloneGameObject : MonoBehaviour {

    public static bool isSpawned = false;

    private void Awake()
    {
        if (!isSpawned)
        {
            isSpawned = true;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 1)
            return;
        gameObject.SetActive(false);
    }
}
