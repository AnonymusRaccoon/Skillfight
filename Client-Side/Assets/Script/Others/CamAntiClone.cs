using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAntiClone : MonoBehaviour
{
    private void Awake()
    {
        if (Camera.main != null)
        {
            Destroy(gameObject);
            return;
        }
        GetComponent<Camera>().enabled = true;
        GetComponent<AudioListener>().enabled = true;
        GetComponent<CamDestroy>().enabled = true;
        GetComponent<Brightness>().enabled = true;
    }
}
