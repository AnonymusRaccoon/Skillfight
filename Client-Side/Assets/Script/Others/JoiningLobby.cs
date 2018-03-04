using System.Collections;
using UnityEngine;

public class JoiningLobby : MonoBehaviour {

    private void OnEnable()
    {
        StartCoroutine(Enable());
    }

    private IEnumerator Enable()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
}
