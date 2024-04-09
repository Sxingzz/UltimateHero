using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Palm2Controller : MonoBehaviour
{
    public float palm2Speed;

    public Rigidbody2D palm2RB;

    public GameObject impactEffect;

    public Vector2 moveDirection;

    public int damageAmount;


    // Update is called once per frame
    void Update()
    {
        palm2RB.velocity = moveDirection * palm2Speed;

        // Flip
        if (palm2RB.velocity.x < 0)
        {
            // left
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (palm2RB.velocity.x > 0)
        {
            // Right
            transform.localScale = Vector3.one;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy")) // dame of Palm of Player.
        {
            collision.GetComponent<Enemy1HeathController>().DamageEnemy(damageAmount);
        }

        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
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
