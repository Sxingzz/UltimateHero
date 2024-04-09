using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PalmController : MonoBehaviour
{
    public float palmSpeed;

    public Rigidbody2D palmRB;

    public GameObject impactEffect;

    public Vector2 moveDirection;

    public int damageAmount;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        palmRB.velocity = moveDirection * palmSpeed;

        // Flip
        if (palmRB.velocity.x < 0)
        {
            // left
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (palmRB.velocity.x > 0)
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

        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
        }
        Instantiate(impactEffect, transform.position, Quaternion.identity); // Quaternion.identity: reset rotaion cái object sinh ra
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
