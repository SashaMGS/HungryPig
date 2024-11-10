using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class Main : MonoBehaviour
{
    public GameObject panMain;
    public GameObject panSettings;

    public TMP_Text scoreTxt;
    public Text volumeText;
    public Slider volumeSound;
    public Dropdown graphicsDD;
    public Dropdown resolutionDD;
    Resolution[] resolutions;

    private void Start()
    {
        Cursor.visible = true;
        panMain.SetActive(true);
        panSettings.SetActive(false);
        AudioListener.volume = 50;
        resolutions = Screen.resolutions;

        graphicsDD.value = 0;
        /*
        if (PlayerPrefs.HasKey("graphics"))
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("graphics"));
            graphicsDD.value = PlayerPrefs.GetInt("graphics");
        }
        else
        {
            QualitySettings.SetQualityLevel(2);
            graphicsDD.value = 2;
        }
        */
        if (PlayerPrefs.HasKey("soundVolume"))
        {
            AudioListener.volume = PlayerPrefs.GetInt("soundVolume");
            volumeSound.value = PlayerPrefs.GetInt("soundVolume");
        }
        else
        {
            AudioListener.volume = 50;
            volumeSound.value = 50;
        }
        if (PlayerPrefs.HasKey("Score"))
        {
            scoreTxt.text = PlayerPrefs.GetInt("Score").ToString();
        }
        else
        {
            scoreTxt.text = "0";
        }

        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            resolutionDD.ClearOptions();
            List<string> options = new List<string>();
            resolutions = Screen.resolutions;
            int currentResolutionIndex = 0;

            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRate + "Hz";
                options.Add(option);
            }
            currentResolutionIndex = options.Count;
            resolutionDD.AddOptions(options);
            resolutionDD.RefreshShownValue();
            LoadSettings(currentResolutionIndex);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.Q))
        {
            PlayerPrefs.DeleteAll();
            Application.Quit();
        }
    }

    public void Game()
    {
        Cursor.visible = false;
        SceneManager.LoadScene("Game");
    }
    public void Settings()
    {
        panMain.SetActive(false);
        panSettings.SetActive(true);
    }
    public void main()
    {
        panMain.SetActive(true);
        panSettings.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void GraphicsDrTriggered(Dropdown dropdown)
    {
        if (dropdown.value == 0)
        {
            QualitySettings.SetQualityLevel(0);
            PlayerPrefs.SetInt("graphics", 0);
        }
        if (dropdown.value == 1)
        {
            QualitySettings.SetQualityLevel(2);
            PlayerPrefs.SetInt("graphics", 2);
        }
        if (dropdown.value == 2)
        {
            QualitySettings.SetQualityLevel(5);
            PlayerPrefs.SetInt("graphics", 5);
        }
    }

    public void SlVolumeTriggered(Slider SlVolume)
    {
        volumeText.text = SlVolume.value.ToString() + "%";
        AudioListener.volume = SlVolume.value;
        PlayerPrefs.SetInt("soundVolume", (int)SlVolume.value);
    }

    public void LoadSettings(int currentResolutionIndex)
    {
        if (PlayerPrefs.HasKey("ResolutionPreference"))
            resolutionDD.value = PlayerPrefs.GetInt("ResolutionPreference");
        else
            resolutionDD.value = currentResolutionIndex;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        PlayerPrefs.SetInt("ResolutionPreference", resolutionDD.value);
    }

}
