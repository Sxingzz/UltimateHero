using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyHealthController : MonoBehaviour
{
    public static Enemy1HeathController instance;

    public int TotalHealth = 3;

    public GameObject DeathEffect;

    public void DamageEnemy(int damegeAmount)
    {
        TotalHealth -= damegeAmount;

        if (TotalHealth <= 0)
        {
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlaySE(AUDIO.SE_ENEMY_EXPLODE);
            }

            if (DeathEffect != null)
            {
                Instantiate(DeathEffect, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }
}
