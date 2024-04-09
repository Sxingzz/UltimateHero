using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1HeathController : MonoBehaviour
{
    public static Enemy1HeathController instance;
    public int CurrentHealth;
    public int MaxHealth;

    public GameObject DeathEffect;

        // Start is called before the first frame update
        void Start()
        {
            CurrentHealth = MaxHealth;
        }

    public void DamageEnemy(int damageAmount)
    {
        CurrentHealth -= damageAmount;

        if (CurrentHealth <= 0)
        {
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlaySE(AUDIO.SE_ENEMY_EXPLODE);
            }
            CurrentHealth = 0;
            if (DeathEffect != null)
            {
                Instantiate(DeathEffect, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }
}