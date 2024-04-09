using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DameOfPlayer : MonoBehaviour
{
    public int DamageAmount = 1;
    public bool isDestroyOnDamage;
    public GameObject destroyEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            DealDamage();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            DealDamage();
        }
    }
    void DealDamage()
    {

        if (Enemy1HeathController.instance != null)
        {
            Enemy1HeathController.instance.DamageEnemy(DamageAmount);
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
