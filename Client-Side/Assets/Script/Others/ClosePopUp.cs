using UnityEngine;
using UnityEngine.EventSystems;

public class ClosePopUp : MonoBehaviour {

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }

    public void RemovePopUp()
    {
        gameObject.SetActive(false);
    }
}
