using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    TimeSpan timeSpan;
    
    void Update()
    {
        timeSpan = TimeSpan.FromSeconds(Time.time);
        timerText.text = string.Format("Total Time: {0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
}
