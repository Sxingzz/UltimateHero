using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController Instance;

    public int currentHealth;
    public int maxHealth;

    public int currentLives;
    public int maxLives;

    public float invicibilityLength;
    private float invincCounter;

    public float flashLength;
    private float flashCounter;

    public SpriteRenderer[] playerSprites;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentLives = maxLives;

        if (UIController.HasInstance)
        {
            UIController.Instance.GamePanel.UpdateHealth(currentHealth, maxHealth);
            UIController.Instance.GamePanel.UpdateLives(currentLives);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (invincCounter > 0)
        {
            invincCounter -= Time.deltaTime;
            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                foreach (SpriteRenderer sprite in playerSprites)
                {
                    sprite.enabled = !sprite.enabled;
                }
                flashCounter = flashLength;
            }

            if (invincCounter <= 0)
            {
                foreach (SpriteRenderer sprite in playerSprites)
                {
                    sprite.enabled = true;
                }
                flashCounter = 0;
            }
        }
    }

    public void DamagePlayer(int damageAmount)
    {
        if (invincCounter <= 0)
        {
            currentHealth -= damageAmount;

            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlaySE(AUDIO.SE_PLAYER_HURT);
            }

            if (currentHealth <= 0)
            {

                if (currentLives > 0)
                {
                    LivesLost();
                    currentHealth = maxHealth;
                }
                else
                {
                    if (UIController.HasInstance && GameManager.HasInstance && AudioManager.HasInstance)
                    {
                        currentHealth = 0;
                        maxHealth = 0;
                        UIController.Instance.GamePanel.UpdateHealth(currentHealth, maxHealth);
                        GameManager.Instance.PauseGame();
                        AudioManager.Instance.PlaySE(AUDIO.SE_PLAYER_DEATH);
                        UIController.Instance.ActiveLosePanel(true);
                    }
                }
            }
            else
            {
                invincCounter = invicibilityLength;
            }

            if (currentLives > 0)
            {
                if (UIController.HasInstance)
                {
                    UIController.Instance.GamePanel.UpdateHealth(currentHealth, maxHealth);
                }
            }
            else
            {
                if (UIController.HasInstance)
                {
                    UIController.Instance.GamePanel.UpdateHealth(0, maxHealth);
                }
            }
        }
    }

    public void LivesLost(int amount = 1)
    {
        currentLives -= amount;

        if (currentLives <= 0)
        {
            currentLives = 0;

            if (UIController.HasInstance && GameManager.HasInstance && AudioManager.HasInstance)
            {
                GameManager.Instance.PauseGame();
                AudioManager.Instance.PlaySE(AUDIO.SE_PLAYER_DEATH);
                UIController.Instance.ActiveLosePanel(true);
            }
        }

        if (UIController.HasInstance)
        {
            UIController.Instance.GamePanel.UpdateLives(currentLives);
        }
    }



    public void FillHealth()
    {
        currentHealth = maxHealth;

        if (UIController.HasInstance)
        {
            UIController.Instance.GamePanel.UpdateHealth(currentHealth, maxHealth);
        }
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (UIController.HasInstance)
        {
            UIController.Instance.GamePanel.UpdateHealth(currentHealth, maxHealth);
        }
    }


}


