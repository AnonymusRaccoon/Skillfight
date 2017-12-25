using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class KeybindMenu : MonoBehaviour {

    Transform menuPanel;
    Event keyEvent;
    Text buttonText;
    KeyCode newKey;

    bool waitingForKey;

    bool NeedWait = true;
    bool checkKey = false;


    void Start()
    {

        menuPanel = transform;
        waitingForKey = false;

        for (int i = 0; i < menuPanel.childCount; i++)
        {
            if (menuPanel.GetChild(i).name == "Forward")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = KeyManager.KM.forward.ToString();
            else if (menuPanel.GetChild(i).name == "Backward")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = KeyManager.KM.backward.ToString();
            else if (menuPanel.GetChild(i).name == "Left")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = KeyManager.KM.left.ToString();
            else if (menuPanel.GetChild(i).name == "Right")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = KeyManager.KM.right.ToString();
            else if (menuPanel.GetChild(i).name == "Jump")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = KeyManager.KM.jump.ToString();
            else if (menuPanel.GetChild(i).name == "Fire")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = KeyManager.KM.fire.ToString();
            else if (menuPanel.GetChild(i).name == "Scoreboard")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = KeyManager.KM.scoreboard.ToString();
            else if (menuPanel.GetChild(i).name == "FirstSkill")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = KeyManager.KM.firstskill.ToString();
            else if (menuPanel.GetChild(i).name == "SecondSkill")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = KeyManager.KM.secondskill.ToString();
            else if (menuPanel.GetChild(i).name == "ThirdSkill")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = KeyManager.KM.thirdskill.ToString();
        }
    }

    private void Update()
    {
        if (checkKey)
        if (keyEvent.isKey || keyEvent.isMouse)
            NeedWait = false;
    }

    void OnGUI()
    {
        keyEvent = Event.current;

        if (keyEvent.isKey && waitingForKey)
        {
            newKey = keyEvent.keyCode;
            waitingForKey = false;
        }
        else if (keyEvent.isMouse && waitingForKey)
        {
            newKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Mouse" + keyEvent.button, true);
            waitingForKey = false;
        }
    }

    public void StartAssignment(string keyName)
    {
        if (!waitingForKey)
            StartCoroutine(AssignKey(keyName));
    }

    public void SendText(Text text)
    {
        buttonText = text;
    }

    IEnumerator WaitForKey()
    {
        yield return new WaitForSeconds(1);
        checkKey = true;
        while (NeedWait)
            yield return null;
    }

    public IEnumerator AssignKey(string keyName)
    {
        waitingForKey = true;

        Image ButtonColor = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
        Color savedColor = ButtonColor.color;
        ButtonColor.color = new Color32 (18, 169, 226, 255);
        yield return WaitForKey();
        ButtonColor.color = savedColor;


        switch (keyName)
        {
            case "forward":
                KeyManager.KM.forward = newKey;
                buttonText.text = KeyManager.KM.forward.ToString();
                PlayerPrefs.SetString("forward", KeyManager.KM.forward.ToString());
                break;
            case "backward":
                KeyManager.KM.backward = newKey;
                buttonText.text = KeyManager.KM.backward.ToString();
                PlayerPrefs.SetString("backward", KeyManager.KM.backward.ToString());
                break;
            case "left":
                KeyManager.KM.left = newKey;
                buttonText.text = KeyManager.KM.left.ToString();
                PlayerPrefs.SetString("left", KeyManager.KM.left.ToString());
                break;
            case "right":
                KeyManager.KM.right = newKey;
                buttonText.text = KeyManager.KM.right.ToString();
                PlayerPrefs.SetString("right", KeyManager.KM.right.ToString());
                break;
            case "jump":
                KeyManager.KM.jump = newKey;
                buttonText.text = KeyManager.KM.jump.ToString();
                PlayerPrefs.SetString("jump", KeyManager.KM.jump.ToString());
                break;
            case "fire":
                KeyManager.KM.fire = newKey;
                buttonText.text = KeyManager.KM.fire.ToString();
                PlayerPrefs.SetString("fire", KeyManager.KM.fire.ToString());
                break;
            case "scoreboard":
                KeyManager.KM.scoreboard = newKey;
                buttonText.text = KeyManager.KM.scoreboard.ToString();
                PlayerPrefs.SetString("scoreboard", KeyManager.KM.scoreboard.ToString());
                break;
            case "firstskill":
                KeyManager.KM.firstskill = newKey;
                buttonText.text = KeyManager.KM.firstskill.ToString();
                PlayerPrefs.SetString("firstskill", KeyManager.KM.firstskill.ToString());
                break;
            case "secondskill":
                KeyManager.KM.secondskill = newKey;
                buttonText.text = KeyManager.KM.secondskill.ToString();
                PlayerPrefs.SetString("secondskill", KeyManager.KM.secondskill.ToString());
                break;
            case "thirdskill":
                KeyManager.KM.thirdskill = newKey;
                buttonText.text = KeyManager.KM.thirdskill.ToString();
                PlayerPrefs.SetString("thirdskill", KeyManager.KM.thirdskill.ToString());
                break;
        }

        yield return null;
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        for (int i = 0; i < menuPanel.childCount; i++)
        {
            if (menuPanel.GetChild(i).name == "Forward")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = KeyManager.KM.forward.ToString();
            else if (menuPanel.GetChild(i).name == "Backward")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = KeyManager.KM.backward.ToString();
            else if (menuPanel.GetChild(i).name == "Left")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = KeyManager.KM.left.ToString();
            else if (menuPanel.GetChild(i).name == "Right")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = KeyManager.KM.right.ToString();
            else if (menuPanel.GetChild(i).name == "Jump")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = KeyManager.KM.jump.ToString();
            else if (menuPanel.GetChild(i).name == "Fire")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = KeyManager.KM.fire.ToString();
            else if (menuPanel.GetChild(i).name == "Scoreboard")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = KeyManager.KM.scoreboard.ToString();
            else if (menuPanel.GetChild(i).name == "FirstSkill")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = KeyManager.KM.firstskill.ToString();
            else if (menuPanel.GetChild(i).name == "SecondSkill")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = KeyManager.KM.secondskill.ToString();
            else if (menuPanel.GetChild(i).name == "ThirdSkill")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = KeyManager.KM.thirdskill.ToString();
        }
    }
}
