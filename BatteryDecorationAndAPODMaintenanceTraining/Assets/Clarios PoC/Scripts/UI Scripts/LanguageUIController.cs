using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageUIController : MonoBehaviour
{
    public GameObject m_DecoEnglishUI;
    public GameObject m_DecoSpanishUI;

    public GameObject m_ApodEnglishUI;
    public GameObject m_ApodSpanishUI;

    private void Start()
    {
        string selectedTraining = PlayerPrefs.GetString("SelectedTraining", "Battery Decoration Training");
        string selectedLanguage = PlayerPrefs.GetString("SelectedLanguage", "English");

        if (selectedTraining == "Battery Decoration Training")
        {
            if (selectedLanguage == "English")
            {
                m_DecoEnglishUI.SetActive(true);
                m_DecoSpanishUI.SetActive(false);
            }
            else if (selectedLanguage == "Spanish")
            {
                m_DecoEnglishUI.SetActive(false);
                m_DecoSpanishUI.SetActive(true);
            }
            else
            {
                Debug.Log("Setting Default Language");
            }
        }
        else if (selectedTraining == "Apod Maintainence Training")
        {
            if (selectedLanguage == "English")
            {
                m_ApodEnglishUI.SetActive(true);
                m_ApodSpanishUI.SetActive(false);
            }
            else if (selectedLanguage == "Spanish")
            {
                m_ApodEnglishUI.SetActive(false);
                m_ApodSpanishUI.SetActive(true);
            }
            else
            {
                Debug.Log("Setting Default Language");
            }
        }
        else
        {
            Debug.Log("Setting Default Training");
        }
    }
}
