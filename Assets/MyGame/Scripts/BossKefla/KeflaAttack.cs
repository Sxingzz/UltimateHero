using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeflaAttack : MonoBehaviour
{
    public int attackDamage = 20;
    public int enragedAttackDamage = 40;

    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;

    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            PlayerHealthController playerHealth = colInfo.GetComponent<PlayerHealthController>();
            if (playerHealth != null)
            {
                playerHealth.DamagePlayer(attackDamage);
            }
            if (PlayerHealthController.Instance != null)
            {
                PlayerHealthController.Instance.DamagePlayer(attackDamage);
            }
        }
    }

    public void EnragedAttack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            PlayerHealthController playerHealth = colInfo.GetComponent<PlayerHealthController>();
            if (playerHealth != null)
            {
                playerHealth.DamagePlayer(enragedAttackDamage);
            }
            if (PlayerHealthController.Instance != null)
            {
                PlayerHealthController.Instance.DamagePlayer(enragedAttackDamage);
            }
        }
    }


    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
