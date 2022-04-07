using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Animator levelTransition;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI levelTextSlider;
    [SerializeField] int level;
    [SerializeField] RectTransform renderTexture;
    [SerializeField] GameObject buttonPlay;

    bool isPause;
    [SerializeField] Animation imageHand;
    public static GameManager Instance;
    [SerializeField] Button pauseBtn;


    void Start()
    {
        imageHand.Play();
        ActualizeLevelText();
        isPause = true;
        Time.timeScale = 0;
    }

    public void Hide()
    {
        imageHand.gameObject.SetActive(false);
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
        HideGame(isPause);
        Time.timeScale = Convert.ToInt32(!isPause);
        buttonPlay.SetActive(isPause);
        pauseBtn.gameObject.SetActive(!isPause);
        //StartCoroutine(WaitBeforeTime());
    }

    IEnumerator WaitBeforeTime()
    {
        yield return new WaitForSecondsRealtime(Convert.ToInt32(!isPause));

    }

    void HideGame(bool hide)
    {
        if (hide)
        {
            renderTexture.DOAnchorPosY(-1000, 1).SetUpdate(true);
        }
        else
        {
            renderTexture.DOAnchorPosY(0, 1).SetUpdate(true);
        }
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
        SpawnerManager.Instance.ResetDucks();

    }
    IEnumerator ChangeLevelText()
    {
        yield return new WaitForSecondsRealtime(1.1f);
        level++;
        PlayerPrefs.SetInt("Level", level);
        ActualizeLevelText();

        if(level % 3 == 1)
        {
            MonetizationManager.Instance.StartAd();
        }
    }

    void ActualizeLevelText()
    {
        levelText.text = "Level " + level.ToString();
        levelTextSlider.text = "Level " + level.ToString();
    }

    public void Replay()
    {
        Pause();
        SpawnerManager.Instance.ResetDucks();
    }

}
