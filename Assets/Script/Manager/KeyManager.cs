using System.Collections;
using UnityEngine;

public class KeyManager : MonoBehaviour {

    public static KeyManager KM;

    public KeyCode forward { get; set; }
    public KeyCode backward { get; set; }
    public KeyCode left { get; set; }
    public KeyCode right { get; set; }
    public KeyCode jump { get; set; }
    public KeyCode fire { get; set; }
    public KeyCode scoreboard { get; set; }
    public KeyCode firstskill { get; set; }
    public KeyCode secondskill { get; set; }
    public KeyCode thirdskill { get; set; }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

        if (KM == null)
        {
            KM = this;
        }
        else if (KM != this)
        {
            Destroy(gameObject);
        }

        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forward", "Z"));
        backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backward", "S"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("left", "Q"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("right", "D"));
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jump", "Space"));
        fire = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("fire", "Mouse0"));
        scoreboard = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("scoreboard", "Tab"));
        firstskill = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("firstskill", "LeftShift"));
        secondskill = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("secondskill", "E"));
        thirdskill = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("thirdskill", "A"));
    }
}
