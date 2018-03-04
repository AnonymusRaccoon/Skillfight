using System.Collections;
using UnityEngine;

public class RightClickRemove : MonoBehaviour {

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            StartCoroutine(EXPLOSION());
        }
    }

    private IEnumerator EXPLOSION()
    {
        yield return new WaitForSeconds(0.15f);
        Destroy(gameObject);
    }
}
