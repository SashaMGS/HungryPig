using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameProcess : MonoBehaviour
{
    [Space(10)]
    [Header("Игровые переменные")]
    public float Difficulty = 2; // Сложность игры (чем выше сложность, тем больше число и тем быстрее убывает голод)
    public float hungry = 100; // Голод (при достижении нуля игра заканчивается)
    public float xpCurrent; // Текущее количество опыта
    public float scoreCurrent; // Текущее количество очков
    public int xpMaxOnLvl; // Нужное количество опыта для повышения уровня
    public int xpLvl; // Уровень опыта игрока

    [Space(10)]
    [Header("Текущая прокачка персонажа")]
    public int speedLvlUpg;
    public int hungerLvlUpg;
    public int maxHungerLvlUpg;
    public int eatLvlUpg;
    public int coinsLvlUpg;

    [Space(10)]
    [Header("Основные элементы интерфейса")]
    public Text hungryText;
    public Slider hungrySlider;
    public Text XPText;
    public Slider XPSlider;
    public TMP_Text scoreCurrentText;

    [Space(10)]
    [Header("Элементы интерфейса прокачки")]
    public Text upgSpeedText;
    public Slider upgSpeedSlider;
    public Text upgHungerText;
    public Slider upgHungerSlider;
    public Text upgMaxHungerText;
    public Slider upgMaxHungerSlider;
    public Text upgEatText;
    public Slider upgEatSlider;
    public Text upgCoinsText;
    public Slider upgCoinsSlider;

    [Space(10)]
    [Header("Элементы интерфейса настроек")]
    public Text volumeText;
    public Slider volumeSound;
    public Dropdown graphicsDD;

    [Space(10)]
    [Header("Обьекты меню")]
    public GameObject panUI;
    public GameObject upgUI;
    public GameObject panPause;
    public GameObject panGameOver;
    public GameObject panSettings;
    public bool gamePause; // Остановка игры
    public bool upgUIisActive;
    public GameObject JoyObj;
    public bool Joy;

    void Start()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer)
            Joy = false;
        else
            Joy = true;

        xpMaxOnLvl = 20;
        XPSlider.maxValue = 20;
        AudioListener.volume = 50;

        Cursor.visible = false;
        gamePause = false;
        panUI.SetActive(true);
        upgUI.SetActive(false);
        panPause.SetActive(false);
        panSettings.SetActive(false);
        panGameOver.SetActive(false);


        graphicsDD.value = 0;
        /*
        //Загрузка сохранений
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
            AudioListener.volume = 100;
            volumeSound.value = 100;
        }
    }

    void Update()
    {
        statsVoid();
        UIVoid();

        if (Input.GetKeyDown(KeyCode.I))
            upgUIVoid();

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            pauseVoid();
    }
    //Вычисления с переменной голода
    void statsVoid()
    {
        if ((int)scoreCurrent > PlayerPrefs.GetInt("Score"))
            PlayerPrefs.SetInt("Score", (int)scoreCurrent);
        if (!gamePause)
        {
            if (hungry <= 0)
            {
                GameOverVoid();
            }
            if (hungry > 100 + (20 * maxHungerLvlUpg))
                hungry = 100 + (20 * maxHungerLvlUpg);
            if (hungry > 0 && !gamePause)
                hungry -= 1 * (Difficulty + hungerLvlUpg / 10) * Time.deltaTime; // Difficulty - текущая сложность игры (чем выше сложность, тем больше число и тем быстрее убывает голод)

            scoreCurrent += 1f * Time.deltaTime;
            Difficulty += 0.0005f * Time.deltaTime;
        }

    }
    //Джойстик
    public void JoyVoid()
    {
        if (Joy)
        {
            PlayerPrefs.SetInt("Joy", 0);
            Joy = false;
        }
        else
        {
            PlayerPrefs.SetInt("Joy", 1);
            Joy = true;
        }

    }


    //Демонстрация переменных на элемент интерфейса
    void UIVoid()
    {
        hungryText.text = ((int)hungry).ToString();
        hungrySlider.value = hungry;
        hungrySlider.maxValue = 100 + (1 * maxHungerLvlUpg * 20);
        if (xpCurrent >= xpMaxOnLvl)
        {
            xpCurrent = 0;
            xpLvl++;
            xpMaxOnLvl += 20 * ((int)Difficulty / 2);
            XPSlider.maxValue += 20 * Difficulty / 2;
        }
        XPText.text = xpLvl.ToString();
        XPSlider.value = xpCurrent;
        scoreCurrentText.text = ((int)scoreCurrent).ToString();


        upgSpeedText.text = speedLvlUpg + "/10";
        upgSpeedSlider.value = speedLvlUpg;

        upgHungerText.text = hungerLvlUpg + "/10";
        upgHungerSlider.value = hungerLvlUpg;

        upgMaxHungerText.text = maxHungerLvlUpg + "/10";
        upgMaxHungerSlider.value = maxHungerLvlUpg;

        upgEatText.text = eatLvlUpg + "/10";
        upgEatSlider.value = eatLvlUpg;

        upgCoinsText.text = coinsLvlUpg + "/10";
        upgCoinsSlider.value = coinsLvlUpg;

    }

    public void upgSpeedVoid()
    {
        if (xpLvl > 0 && speedLvlUpg < 5)
        {
            xpLvl--;
            speedLvlUpg++;
            GetComponent<AudioSource>().Play();
        }
    }
    public void upgHungerVoid()
    {
        if (xpLvl > 0 && hungerLvlUpg < 10)
        {
            xpLvl--;
            hungerLvlUpg++;
            GetComponent<AudioSource>().Play();
        }
    }
    public void upgMaxHungerVoid()
    {
        if (xpLvl > 0 && maxHungerLvlUpg < 10)
        {
            xpLvl--;
            maxHungerLvlUpg++;
            GetComponent<AudioSource>().Play();
        }
    }
    public void upgEatLvlVoid()
    {
        if (xpLvl > 0 && eatLvlUpg < 10)
        {
            xpLvl--;
            eatLvlUpg++;
            GetComponent<AudioSource>().Play();
        }
    }
    public void upgCoinsLvlVoid()
    {
        if (xpLvl > 0 && coinsLvlUpg < 10)
        {
            xpLvl--;
            coinsLvlUpg++;
            GetComponent<AudioSource>().Play();
        }
    }

    public void upgUIVoid()
    {
        if (!upgUIisActive)
        {
            Cursor.visible = true;
            upgUIisActive = true;
            upgUI.SetActive(true);
        }
        else
        {
            Cursor.visible = false;
            upgUIisActive = false;
            upgUI.SetActive(false);
        }
    }

    void GameOverVoid()
    {
        gamePause = true;
        panUI.SetActive(false);
        panPause.SetActive(false);
        panGameOver.SetActive(true);
        panSettings.SetActive(false);
        Cursor.visible = true;
    }

    public void pauseVoid()
    {
        if (!gamePause)
        {
            gamePause = true;
            panUI.SetActive(false);
            panPause.SetActive(true);
            panGameOver.SetActive(false);
            panSettings.SetActive(false);
            Cursor.visible = true;
        }
        else
        {
            gamePause = false;
            panUI.SetActive(true);
            panPause.SetActive(false);
            panGameOver.SetActive(false);
            panSettings.SetActive(false);
            Cursor.visible = false;
        }
    }

    public void loadVoid()
    {
        gamePause = false;
        panUI.SetActive(true);
        panPause.SetActive(false);
        panGameOver.SetActive(false);
        panSettings.SetActive(false);
        Cursor.visible = false;
    }

    public void inMain()
    {
        Cursor.visible = true;
        SceneManager.LoadScene("Menu");
    }

    public void settings()
    {
        gamePause = true;
        panUI.SetActive(false);
        panPause.SetActive(false);
        panGameOver.SetActive(false);
        panSettings.SetActive(true);
        Cursor.visible = true;
    }

    public void mainPan()
    {
        gamePause = true;
        panUI.SetActive(false);
        panPause.SetActive(true);
        panGameOver.SetActive(false);
        panSettings.SetActive(false);
        Cursor.visible = true;
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
}
