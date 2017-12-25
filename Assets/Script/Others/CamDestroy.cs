using UnityEngine;

public class CamDestroy : MonoBehaviour {

	void Start ()
    {
        DontDestroyOnLoad(gameObject);
	}
}