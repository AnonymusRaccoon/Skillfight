using System.Collections;
using UnityEngine;

public class Pseudo : MonoBehaviour {

    private void OnEnable()
    {
        StartCoroutine(Enable());
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

    private IEnumerator Enable ()
    {
        yield return new WaitForSeconds(1);
        if (transform.parent.name == GameObject.Find("Main Camera").transform.parent.name)
        {
            foreach (Transform child in transform)
            {
                if (child.name != "Light")
                    child.GetComponent<MeshRenderer>().enabled = false;
                else
                    child.GetComponent<Light>().enabled = false;
            }
        }
    }

}
