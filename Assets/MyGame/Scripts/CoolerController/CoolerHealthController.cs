using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolerHealthController : MonoBehaviour
{
    public static CoolerHealthController Instance;

    public Slider bossHealthSlider;

    public int health = 50;

    public GameObject deathEffect;

    public bool isInvulnerable = false;

    public GameObject winObject;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        bossHealthSlider.maxValue = health;
        bossHealthSlider.value = health;
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
            return;

        health -= damage;

        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_BOSS_IMPACT);
        }

        if (health <= 20)
        {
            GetComponent<Animator>().SetBool("IsEnraged", true);
        }

        if (health <= 0)
        {
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlaySE(AUDIO.SE_BOSS_DEATH);
            }

            if (winObject != null)
            {
                winObject.SetActive(true);
            }
            Die();
        }
        bossHealthSlider.value = health;
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
