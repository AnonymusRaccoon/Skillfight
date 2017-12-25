using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    [SerializeField]
    private GameObject MainMenuScene;
    [SerializeField]
    private GameObject MatchmakerScene;
    [SerializeField]
    private GameObject SkillsScene;
    [SerializeField]
    private GameObject StatsScene;
    [SerializeField]
    private GameObject SettingsScene;
    [SerializeField]
    private GameObject LeavePopUp;

    [Space]
    [SerializeField]
    private Sprite VideoSettings;
    [SerializeField]
    private Sprite AudioSettings;
    [SerializeField]
    private Sprite Keybind;

    [Space]
    [SerializeField]
    private GameObject VideoScene;
    [SerializeField]
    private GameObject AudioScene;
    [SerializeField]
    private GameObject KeybindScene;

	public void Play ()
    {
        MatchmakerScene.SetActive(true);
        MainMenuScene.SetActive(false);
    } 

    public void TrainingArea ()
    {
        //SceneManager.LoadScene("Training");
    }

    public void Skills ()
    {
        MainMenuScene.SetActive(false);
        SkillsScene.SetActive(true);
    }

    public void Stats ()
    {
        MainMenuScene.SetActive(false);
        StatsScene.SetActive(true);
        StatsScene.GetComponent<Stats>().ExternLoadConfig();
    }

    public void Settings ()
    {
        MainMenuScene.SetActive(false);
        SettingsScene.SetActive(true);
    }

    public void SettingVideo ()
    {
        SettingsScene.GetComponent<Image>().sprite = VideoSettings;
        VideoScene.SetActive(true);
        AudioScene.SetActive(false);
        KeybindScene.SetActive(false);
    }

    public void SettingsAudio ()
    {
        SettingsScene.GetComponent<Image>().sprite = AudioSettings;
        VideoScene.SetActive(false);
        AudioScene.SetActive(true);
        KeybindScene.SetActive(false);
    }

    public void SettingsKeybind ()
    {
        SettingsScene.GetComponent<Image>().sprite = Keybind;
        VideoScene.SetActive(false);
        AudioScene.SetActive(false);
        KeybindScene.SetActive(true);
    }

    public void LeaveGame ()
    {
        LeavePopUp.SetActive(true);
    }

    public void ConfirmLeaveGame ()
    {
        Application.Quit();
    }

    public void UndoLeaveGame ()
    {
        LeavePopUp.SetActive(false);
    }

    public void BackButton ()
    {
        SettingsScene.SetActive(false);
        StatsScene.SetActive(false);
        SkillsScene.SetActive(false);
        MainMenuScene.SetActive(true);
    }
}
