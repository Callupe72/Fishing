using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Animator levelTransition;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] int level;

    bool isPause;
    public static GameManager Instance;

    void Start()
    {
        ActualizeLevelText();
    }
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        level = PlayerPrefs.GetInt("Level");
        if (level == 0)
            level = 1;

    }
    public void Pause()
    {
        isPause = !isPause;
        Time.timeScale = Convert.ToInt32(isPause);
        pauseMenu.SetActive(isPause);
    }

    public void LevelTransition()
    {
        levelTransition.gameObject.SetActive(true);
        Time.timeScale = 0;
        StartCoroutine(SetActiveFalseTransition());
        StartCoroutine(ChangeLevelText());
    }

    IEnumerator SetActiveFalseTransition()
    {
        yield return new WaitForSecondsRealtime(3);
        levelTransition.gameObject.SetActive(false);
        Time.timeScale = 1;

    }
    IEnumerator ChangeLevelText()
    {
        yield return new WaitForSecondsRealtime(1.1f);
        level++;
        PlayerPrefs.SetInt("Level", level);
        ActualizeLevelText();
    }

    void ActualizeLevelText()
    {
        levelText.text = "Level " + level.ToString();
    }
}
