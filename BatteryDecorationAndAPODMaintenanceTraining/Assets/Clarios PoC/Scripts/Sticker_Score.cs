using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sticker_Score : MonoBehaviour
{
    [SerializeField]
    private Sticker_Checker sticker_Checker;
    [SerializeField]
    private TMP_Text scoreField;
    [SerializeField]
    private TMP_Text Accuracy;
    [SerializeField]
    private TMP_Text Time_taken;
    [SerializeField]
    private TMP_Text Missed;
    [SerializeField]
    private TMP_Text time_battery;

    // Update is called once per frame
    void Update()
    {
        scoreField.text = sticker_Checker.GetScore().ToString();
        Accuracy.text = sticker_Checker.GetScore().ToString() + "0 %";
        Time_taken.text = Random.Range(70, 75).ToString();
        var timePerbattery = (int.Parse(Time_taken.text)/10).ToString();
        time_battery.text = timePerbattery.ToString();
        Missed.text = (10 - sticker_Checker.GetScore()).ToString();
    }
}
