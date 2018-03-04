using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectVisibility : MonoBehaviour {

    [SerializeField]
    private GameObject CustomGameManager;

    private void Start()
    {
        CustomGameManager.SetActive(true);
    }
}
