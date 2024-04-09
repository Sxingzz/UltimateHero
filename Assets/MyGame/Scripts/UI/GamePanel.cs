using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    public Slider HealthSlide;
    public TextMeshProUGUI HealthText;
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private TextMeshProUGUI livesText;
    private float timeRemaining;
    private bool timerIsRunning = false;

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                if (UIController.HasInstance && GameManager.HasInstance && AudioManager.HasInstance)
                {
                    AudioManager.Instance.PlaySE(AUDIO.SE_BOSS_DEATH);
                    GameManager.Instance.PauseGame();
                    UIController.Instance.ActiveLosePanel(true);
                }
            }
        }
    }


    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        HealthSlide.maxValue = maxHealth;
        HealthSlide.value = currentHealth;
        HealthText.text = "Health: " + currentHealth;
    }

    public void UpdateLives(int currentLives)
    {
        livesText.text = "Lives: " + currentLives;
    }

    public void SetTimeRemain(float v)
    {
        timeRemaining = v;
        timerIsRunning = true;
    }
}
