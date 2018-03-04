using UnityEngine.UI;
using System.Collections;
using UnityEngine;

public class mouseOver : MonoBehaviour {

    private Sprite PointerOn;
    private Sprite PointerOff;

    public void GetSprite (Sprite Pointer)
    {
        PointerOn = Pointer;
    }

    public void MouseOver (GameObject button)
    {
        PointerOff = button.GetComponent<Image>().sprite;
        StartCoroutine(WaitMouseOver(button));
    }

    public void OnMouseExit(GameObject button)
    {
        Abilities AbiltitiesScript = GetComponent<Abilities>();
        if (AbiltitiesScript.SelectedAbilities == 1)
        {
            AbiltitiesScript.Fond.GetComponent<Image>().sprite = AbiltitiesScript.FondPassif;
        } else
        {
            button.GetComponent<Image>().sprite = PointerOff;
        }
    }

    private IEnumerator WaitMouseOver(GameObject button)
    {
        yield return new WaitForEndOfFrame ();
        Abilities AbiltitiesScript = GetComponent<Abilities>();
        if (AbiltitiesScript.SelectedAbilities == 1)
        {
            AbiltitiesScript.Fond.GetComponent<Image>().sprite = PointerOn;
        }
        else
        {
            button.GetComponent<Image>().sprite = PointerOn;
        }
    } 
}