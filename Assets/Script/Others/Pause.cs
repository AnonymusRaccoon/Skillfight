using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine;

public class Pause : NetworkBehaviour {

    [SerializeField]
    private GameObject pauseUI;

    [SerializeField]
    private GameObject MainPauseMenu;

    [SerializeField]
    private GameObject GraphicMenu;
    [SerializeField]
    private GameObject AudioMenu;
    [SerializeField]
    private GameObject KeybindMenu;


    public void Resume ()
    {
        AudioMenu.SetActive(false);
        KeybindMenu.SetActive(false);
        MainPauseMenu.SetActive(true);
        pauseUI.SetActive(false);
        GameObject.Find("Main Camera").transform.parent.GetComponent<FirstPersonController>().Pause = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void GraphicView ()
    {
        MainPauseMenu.SetActive(false);
        AudioMenu.SetActive(false);
        KeybindMenu.SetActive(false);
        GraphicMenu.SetActive(true);
    }

    public void AudioView ()
    {
        GraphicMenu.SetActive(false);
        AudioMenu.SetActive(true);
        KeybindMenu.SetActive(false);
    }

    public void KeybindView ()
    {
        GraphicMenu.SetActive(false);
        AudioMenu.SetActive(false);
        KeybindMenu.SetActive(true);
    }

    public void Back ()
    {
        GraphicMenu.SetActive(false);
        AudioMenu.SetActive(false);
        KeybindMenu.SetActive(false);
        MainPauseMenu.SetActive(true);
    }

    public void LeaveGame ()
    {
        Network.Disconnect();
    }

}
