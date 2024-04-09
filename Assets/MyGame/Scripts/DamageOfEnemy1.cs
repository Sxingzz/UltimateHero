using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOfEnemy1 : MonoBehaviour
{
    public int DamageAmount = 1;
    public bool isDestroyOnDamage;
    public GameObject destroyEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DealDamage();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DealDamage();
        }
    }
    void DealDamage()
    {
        if (PlayerHealthController.Instance != null)
        {
            PlayerHealthController.Instance.DamagePlayer(DamageAmount);
        }
        if (isDestroyOnDamage)
        {
            if (destroyEffect != null)
            {
                Instantiate(destroyEffect, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }


  
}
