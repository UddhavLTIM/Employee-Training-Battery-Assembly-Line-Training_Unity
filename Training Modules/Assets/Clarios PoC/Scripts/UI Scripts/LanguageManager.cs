using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public static string SelectedTraining;

    public static string SelectedLanguage;
    private static LanguageManager instance;

    public void SetTraining(string training)
    {
        SelectedTraining = training;
        PlayerPrefs.SetString("SelectedTraining", training);
    }

    public void SetLanguage(string language)
    {
        SelectedLanguage = language;
        PlayerPrefs.SetString("SelectedLanguage", language);
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
}
