using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamehaController : MonoBehaviour
{

    public float KamehaSpeed;

    public Rigidbody2D KamehaRB;

    public GameObject ExplosionEffect;

    public Vector2 moveDirection;

    public int damageAmount;

    public bool isDestructible;

    public float BlastRange; // bán kính vụ nổ
    public LayerMask DestructibleLayer;


    // Update is called once per frame
    void Update()
    {
        
        KamehaRB.velocity = moveDirection * KamehaSpeed;

        // Flip
        if (KamehaRB.velocity.x < 0)
        {
            // left
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (KamehaRB.velocity.x > 0)
        {
            // Right
            transform.localScale = Vector3.one;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy")) // dame of Palm of Player.
        {
            Enemy1HeathController enemy1HeathController = collision.GetComponent<Enemy1HeathController>();
            if (enemy1HeathController)
            {
                enemy1HeathController.DamageEnemy(damageAmount);
            }

            EnemyFlyHealthController enemyFlyHealthController = collision.GetComponent<EnemyFlyHealthController>();
            if (enemyFlyHealthController)
            {
                enemyFlyHealthController.GetComponent<EnemyFlyHealthController>().DamageEnemy(damageAmount);
            }

            CoolerHealthController CoolerHealthController = collision.GetComponent<CoolerHealthController>();
            if (CoolerHealthController)
            {
                CoolerHealthController.TakeDamage(damageAmount);
            }
        }

        if (ExplosionEffect != null)
        {
            Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        }

        if (isDestructible)
        {
            Collider2D[] objectsToDamage = Physics2D.OverlapCircleAll(transform.position, BlastRange, DestructibleLayer);
            Debug.Log("aaaaaaaaaaa" + objectsToDamage.Length);
            if (objectsToDamage.Length > 0)
            {
                foreach (Collider2D coll in objectsToDamage)
                {
                    Destroy(coll.gameObject);
                }
            }
        }

        Destroy(gameObject);
    }
    private void OnBecameInvisible() // ra khỏi vùng nhìn của camera thì sẽ detroy 
    {

        Destroy(gameObject);
    }
    public void SetMoveDirection(Vector2 Direction)
    {
        moveDirection = Direction;
    }


}

