using UnityEngine.UI;
using UnityEngine;

public class HealthText : MonoBehaviour {

    public Slider slider;
    public Transform noHandle;
    public Transform Handle;

    public void ValueChange()
    {
        GetComponent<Text>().text = slider.value.ToString() + "/" + slider.maxValue.ToString() + " Hp";
        if(slider.value/slider.maxValue <= 0.25)
        {
            transform.SetParent(noHandle);
        }
        else
        {
            transform.SetParent(Handle);
            transform.localPosition = new Vector3(-56.1f, 5.7f, 0);
        }
    }
}
