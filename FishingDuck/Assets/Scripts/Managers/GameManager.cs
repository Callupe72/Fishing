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

        LevelTransition();
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
        StartCoroutine(SetActiveFalseTransition());
        StartCoroutine(ChangeLevelText());
    }

    IEnumerator SetActiveFalseTransition()
    {
        yield return new WaitForSeconds(3);
        levelTransition.gameObject.SetActive(false);
    }
    IEnumerator ChangeLevelText()
    {
        yield return new WaitForSeconds(1.1f);
        level++;
        ActualizeLevelText();
    }

    void ActualizeLevelText()
    {
        levelText.text = "Level " + level.ToString();
    }
}
