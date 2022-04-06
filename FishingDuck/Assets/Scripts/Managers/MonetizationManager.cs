using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class MonetizationManager : MonoBehaviour
{
    string googlePlay_Id = "4696247";
    string adName = "AdBetween2Levels";
    bool testMode = true;

    public static MonetizationManager Instance;

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
    }

    private void Start()
    {
        Advertisement.Initialize(googlePlay_Id, testMode);
    }

    public void StartAd()
    {
        Advertisement.Show(adName);
    }
}
