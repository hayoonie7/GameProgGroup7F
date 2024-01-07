using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public bool bossRoom = false;
    public GameObject boss;
    public Text timer;
    public Text gameText;
    public static bool isGameOver;
    public AudioClip gameOverSFX;
    public AudioClip gameWonSFX;
    public string nextLevel;
    public static float currentTime;
    public static int score = 0;
    public Text scoreText;

    public Text buttonPrompt;
    
    void Awake()
    {
        PlayerInfo.LoadStartingValues();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        currentTime = 0.0f;
        SetTimerText();
        scoreText.text = score.ToString();
        //currentScore = PickupBehavior.pickupCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            scoreText.text = score.ToString();
            currentTime += Time.deltaTime; 
            SetTimerText();
            if (bossRoom)
            {
                if (boss.GetComponent<BossHealthBarController>().healthSlider.value <= 0)
                {
                    LevelBeat();
                }
            }
            /*
             This is where we set condition for next level
            if ()
            {
                LevelBeat();
            }*/
        } else {
            return;
        }
    }

    private void SetTimerText()
    {
        timer.text = currentTime.ToString("f2");
    }

    public void LevelLost()
    {
        isGameOver = true;
        gameText.text = "Game Over!";
        gameText.gameObject.SetActive(true);
        AudioSource.PlayClipAtPoint(gameOverSFX, Camera.main.transform.position);
        Invoke("LoadCurrentLevel", 2);
        // score = 0;
    }

    public void LevelBeat()
    {
        isGameOver = true;
        gameText.text = "You win!";
        gameText.gameObject.SetActive(true);
        AudioSource.PlayClipAtPoint(gameWonSFX, Camera.main.transform.position);
        if (!string.IsNullOrEmpty(nextLevel))
        { 
            Invoke("LoadNextLevel", 2);
            score = 0;
        }
        if (bossRoom)
        {
            PlayerInfo.weaponType = "sword";
            PlayerInfo.maxHealth = 100;
            PlayerInfo.currentHealth = 100;
            PlayerInfo.maxStamina = 100;
        }
        PlayerInfo.OverwriteStartingValues();
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetButtonPromptActive(bool state)
    {
        this.buttonPrompt.enabled = state;
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }
}
