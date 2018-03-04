using UnityEngine;

public class LoginError : MonoBehaviour {


	public void Done ()
    {
        foreach(Transform error in transform)
        {
            error.gameObject.SetActive(false);
        }
    }
}
