using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using UnityEngine;

public class Graphic : MonoBehaviour {

    private string configFile = Directory.GetCurrentDirectory() + "/Settings.txt";
    private List<string> configLine = new List<string> ();

    private string currentResolution;

    [SerializeField]
    private GameObject Resolution;
    [SerializeField]
    private GameObject LessResolution;
    [SerializeField]
    private GameObject MoreResolution;

    [Space]
    [SerializeField]
    private GameObject FullScreen;
    [SerializeField]
    private GameObject LessFullScreen;
    [SerializeField]
    private GameObject MoreFullScreen;

    [Space]
    [SerializeField]
    private GameObject AntiAliasing;
    [SerializeField]
    private GameObject LessAntiAliasing;
    [SerializeField]
    private GameObject MoreAntiAliasing;

    [Space]
    [SerializeField]
    private GameObject Vsync;
    [SerializeField]
    private GameObject LessVsync;
    [SerializeField]
    private GameObject MoreVsync;

    [Space]
    [SerializeField]
    private GameObject BrightnessSlider;
    [SerializeField]
    private GameObject BrightnessText;

    [Space]
    [SerializeField]
    private GameObject Fov;
    [SerializeField]
    private GameObject FovText;

    [Space]
    [SerializeField]
    private GameObject AnisotropicFilteringText;
    [SerializeField]
    private GameObject LessAnisotropicFiltering;
    [SerializeField]
    private GameObject MoreAnisotropicFiltering;

    [Space]
    [SerializeField]
    private GameObject FpsLimitation;
    [SerializeField]
    private GameObject LessFps;
    [SerializeField]
    private GameObject MoreFps;
    [SerializeField]
    private GameObject CustomFps;

    private void Start()
    {
        if (File.Exists(configFile))
        {
            foreach (string line in File.ReadAllLines(configFile))
            {
                configLine.Add(line);
            }
            LoadConfig(configLine.ToArray ());
        }
        else
        {
            string[] defaultConfig = new string[8];
            defaultConfig[0] = "Resolution = 1920 x 1080";
            defaultConfig[1] = "Fullscreen = true";
            defaultConfig[2] = "vSync = false";
            defaultConfig[3] = "AntiAliasing = 4x";
            defaultConfig[4] = "AnisotropicFiltering = false";
            defaultConfig[5] = "Fov = 60";
            defaultConfig[6] = "Brightness = 100";
            defaultConfig[7] = "FpsLimitation = 60";
            File.WriteAllLines(configFile, defaultConfig);
            foreach (string line in File.ReadAllLines(configFile))
            {
                configLine.Add(line);
            }
            LoadConfig(configLine.ToArray());
        }
    }

    void LoadConfig (string[] lines)
    {
        foreach (string line in lines)
        {
            string command = line.Substring(0, line.IndexOf("=") - 1);
            string value = line.Substring(line.IndexOf("=") + 2, line.Length - line.IndexOf("=") - 2);
            switch (command)
            {
                case "Resolution":
                    ChangeResolution(value);
                    break;
                case "Fullscreen":
                    if (value == "true")
                        Borderless();
                    if (value == "false")
                        Windowed();
                    break;
                case "vSync":
                    if (value == "true")
                    {
                        QualitySettings.vSyncCount = 1;
                        Vsync.GetComponent<Text>().text = "True";
                        MoreVsync.SetActive(false);
                    }
                    if (value == "false")
                    {
                        QualitySettings.vSyncCount = 0;
                        Vsync.GetComponent<Text>().text = "False";
                        LessVsync.SetActive(false);
                    }
                    break;
                case "AntiAliasing":
                    SetAntiAliasing(value);
                    break;
                case "Fov":
                    SetFOV(int.Parse(value));
                    break;
                case "Brightness":
                    SetBrightess(int.Parse(value));
                    break;
                case "AnisotropicFiltering":
                    if (value == "true")
                        EnableAnisotropicFiltering();
                    if (value == "false")
                        DisableAnisotropicFiltering();
                    break;
                case "FpsLimitation":
                    SetFpsLimitation(int.Parse(value));
                    break;
            }
        }
    }

    void UpdateConfig (string needChange, string value)
    {
        string[] readFile = File.ReadAllLines(configFile);
        int i = -1;
        foreach (string line in readFile)
        {
            i++;
            string command = line.Substring(0, line.IndexOf("=") - 1);
            if (command == needChange)
            {
                readFile[i] = needChange + " = " + value;
                File.WriteAllLines(configFile, readFile);
                return;
            }
        }
    }

    public void Windowed()
    {
        Screen.fullScreen = false;
        FullScreen.GetComponent<Text>().text = "Windowed";
        LessFullScreen.SetActive(false);
        MoreFullScreen.SetActive(true);
        UpdateConfig("Fullscreen", "false");
    }

    public void Borderless ()
    {
        Screen.fullScreen = true;
        FullScreen.GetComponent<Text>().text = "Borderless";
        LessFullScreen.SetActive(true);
        MoreFullScreen.SetActive(false);
        UpdateConfig("Fullscreen", "true");
    }

    public void vSync ()
    {
        if (QualitySettings.vSyncCount == 0)
        {
            QualitySettings.vSyncCount = 1;
            Vsync.GetComponent<Text>().text = "True";
            LessVsync.SetActive(true);
            MoreVsync.SetActive(false);
            UpdateConfig("vSync", "true");
        }
        else if (QualitySettings.vSyncCount == 1)
        {
            QualitySettings.vSyncCount = 0;
            Vsync.GetComponent<Text>().text = "False";
            LessVsync.SetActive(false);
            MoreVsync.SetActive(true);
            UpdateConfig("vSync", "false");
        }
    }

    public void DecreaseResolution()
    {
        if (currentResolution == "1920 x 1080")
        {
            ChangeResolution("1600 x 900");
            return;
        }
        if (currentResolution == "1600 x 900")
        {
            ChangeResolution("1280 x 720");
            return;
        }
    }

    public void IncreaseResolution()
    {
        if (currentResolution == "1280 x 720")
        {
            ChangeResolution("1600 x 900");
            return;
        }
        if (currentResolution == "1600 x 900")
        {
            ChangeResolution("1920 x 1080");
            return;
        }
    }

    void ChangeResolution(string resolutionWanted)
    {
        if (resolutionWanted == "1920 x 1080")
        {
            Screen.SetResolution(1920, 1080, Screen.fullScreen);
            currentResolution = "1920 x 1080";
            Resolution.GetComponent<Text>().text = currentResolution;
            MoreResolution.SetActive(false);
            UpdateConfig("Resolution", "1920 x 1080");
        }
        if (resolutionWanted == "1600 x 900")
        {
            Screen.SetResolution(1600, 900, Screen.fullScreen);
            currentResolution = "1600 x 900";
            Resolution.GetComponent<Text>().text = currentResolution;
            MoreResolution.SetActive(true);
            LessResolution.SetActive(true);
            UpdateConfig("Resolution", "1600 x 900");
        }
        if (resolutionWanted == "1280 x 720")
        {
            Screen.SetResolution(1280, 720, Screen.fullScreen);
            currentResolution = "1280 x 720";
            Resolution.GetComponent<Text>().text = currentResolution;
            LessResolution.SetActive(false);
            UpdateConfig("Resolution", "1280 x 720");
        }
    }

    public void DecreaseAntiAliasing ()
    {
        if (QualitySettings.antiAliasing == 8)
        {
            SetAntiAliasing("4x");
            return;
        }
        if (QualitySettings.antiAliasing == 4)
        {
            SetAntiAliasing("2x");
            return;
        }
        if (QualitySettings.antiAliasing == 2)
        {
            SetAntiAliasing("0x");
            return;
        }
    }

    public void IncreaseAntiAliasing()
    {
        if (QualitySettings.antiAliasing == 0)
        {
            SetAntiAliasing("2x");
            return;
        }
        if (QualitySettings.antiAliasing == 2)
        {
            SetAntiAliasing("4x");
            return;
        }
        if (QualitySettings.antiAliasing == 4)
        {
            SetAntiAliasing("8x");
            return;
        }
    }

    void SetAntiAliasing (string value)
    {
        if (value == "8x")
        {
            QualitySettings.antiAliasing = 8;
            MoreAntiAliasing.SetActive(false);
            AntiAliasing.GetComponent<Text>().text = "8x";
            UpdateConfig("AntiAliasing", "8x");
        }
        if (value == "4x")
        {
            QualitySettings.antiAliasing = 4;
            LessAntiAliasing.SetActive(true);
            MoreAntiAliasing.SetActive(true);
            AntiAliasing.GetComponent<Text>().text = "4x";
            UpdateConfig("AntiAliasing", "4x");
        }
        if (value == "2x")
        {
            QualitySettings.antiAliasing = 2;
            LessAntiAliasing.SetActive(true);
            MoreAntiAliasing.SetActive(true);
            AntiAliasing.GetComponent<Text>().text = "2x";
            UpdateConfig("AntiAliasing", "2x");
        }
        if (value == "0x")
        {
            QualitySettings.antiAliasing = 0;
            LessAntiAliasing.SetActive(false);
            AntiAliasing.GetComponent<Text>().text = "0x";
            UpdateConfig("AntiAliasing", "0x");
        }
    }

    public void SetFOV (int value)
    {
        if (value == 0)
        {
            float fValue = Fov.GetComponent<Slider>().value;
            value = (int) fValue;
        }
        FovText.GetComponent<Text>().text = value.ToString();
        Fov.GetComponent<Slider>().value = value;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().fieldOfView = value;
        UpdateConfig("Fov", value.ToString());
    }

    public void SetBrightess (int value)
    {
        float endValue = 0;
        if (value == 0)
        {
            float fValue = BrightnessSlider.GetComponent<Slider>().value;
            endValue = fValue / 100;
            BrightnessText.GetComponent<Text>().text = fValue.ToString() + "%";
            BrightnessSlider.GetComponent<Slider>().value = fValue;
            UpdateConfig("Brightness", fValue.ToString());
        }
        else
        {
            endValue = value / 100;
            BrightnessText.GetComponent<Text>().text = value.ToString() + "%";
            BrightnessSlider.GetComponent<Slider>().value = value;
            UpdateConfig("Brightness", value.ToString());
        }
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Brightness>().brightness = endValue;
    }

    public void DisableAnisotropicFiltering ()
    {
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
        AnisotropicFilteringText.GetComponent<Text>().text = "False";
        LessAnisotropicFiltering.SetActive(false);
        MoreAnisotropicFiltering.SetActive(true);
        UpdateConfig("AnisotropicFiltering", "false");
    }

    public void EnableAnisotropicFiltering ()
    {
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
        AnisotropicFilteringText.GetComponent<Text>().text = "True";
        LessAnisotropicFiltering.SetActive(true);
        MoreAnisotropicFiltering.SetActive(false);
        UpdateConfig("AnisotropicFiltering", "true");
    }

    public void SetFpsLimitation (int fpsLimitation)
    {
        if (fpsLimitation == 30)
        {
            Application.targetFrameRate = 30;
            FpsLimitation.GetComponent<Text>().text = "30";
            LessFps.SetActive(false);
            MoreFps.SetActive(true);
            return;
        }
        if (fpsLimitation == 60)
        {
            Application.targetFrameRate = 60;
            FpsLimitation.GetComponent<Text>().text = "60";
            LessFps.SetActive(true);
            MoreFps.SetActive(true);
            return;
        }
        if (fpsLimitation == 120)
        {
            FpsLimitation.SetActive(true);
            Application.targetFrameRate = 120;
            FpsLimitation.GetComponent<Text>().text = "120";
            CustomFps.SetActive(false);
            LessFps.SetActive(true);
            MoreFps.SetActive(true);
            return;
        }
        if (fpsLimitation == 0)
        {
            FpsLimitation.SetActive(false);
            CustomFps.SetActive(true);
            LessFps.SetActive(true);
            MoreFps.SetActive(false);
            return;
        }
        else
        {
            FpsLimitation.SetActive(true);
            LessFps.SetActive(true);
            MoreFps.SetActive(true);
            CustomFps.SetActive(false);
            Application.targetFrameRate = fpsLimitation;
            FpsLimitation.GetComponent<Text>().text = fpsLimitation.ToString();
            return;
        }
    }

    public void SetCustomFps ()
    {
        int value = int.Parse(CustomFps.GetComponent<InputField>().text);
        SetFpsLimitation(value);
    }

    public void DecreaseFps ()
    {
        if (!FpsLimitation.activeSelf)
        {
            SetFpsLimitation(120);
            return;
        }
        int currentFpsLimitation = int.Parse(FpsLimitation.GetComponent<Text>().text);
        if (currentFpsLimitation == 120)
        {
            SetFpsLimitation(60);
            return;
        }
        if (currentFpsLimitation == 60)
        {
            SetFpsLimitation(30);
            return;
        }
        else
        {
            SetFpsLimitation(120);
            return;
        }
    }

    public void IncreaseFps ()
    {
        int currentFpsLimitation = int.Parse(FpsLimitation.GetComponent<Text>().text);
        if (currentFpsLimitation == 30)
        {
            SetFpsLimitation(60);
            return;
        }
        if (currentFpsLimitation == 60)
        {
            SetFpsLimitation(120);
            return;
        }
        else
        {
            SetFpsLimitation(0);
            return;
        }
    }
}
