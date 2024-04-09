using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Patroler : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int currentPoint;

    public float moveSpeed;
    public float waitAtPoint;
    private float waitCounter;

    public float jumpForce;
    public Rigidbody2D enemyRB;

    [SerializeField]
    private Animator EnemyAnim;



    // Start is called before the first frame update
    void Start()
    {
        waitCounter = waitAtPoint;

        foreach (Transform pPoint in patrolPoints)
        {
            pPoint.SetParent(null); // set patroipoit ra ngoài để cùng cấp với enemy walker để di chuyển theo code
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x - patrolPoints[currentPoint].position.x) > 0.2f)
        {
            if (transform.position.x < patrolPoints[currentPoint].position.x)
            {
                enemyRB.velocity = new Vector2(moveSpeed, enemyRB.velocity.y);
                transform.localScale = new Vector3(-1, 1, 1); // set enemy quay đầu 
            }
            else
            {
                enemyRB.velocity = new Vector2(-moveSpeed, enemyRB.velocity.y);
                transform.localScale = Vector3.one; // vecter3.one = vecter3(1,1,1)

            }
            if (transform.position.y < patrolPoints[currentPoint].position.y && enemyRB.velocity.y < 0.1f) // kiểm tra nếu enemy thấp hơn patroipoint thì sẽ nhảy && nếu nó đang o
            {
                EnemyAnim.SetTrigger("Jump");
                enemyRB.velocity = new Vector2(enemyRB.velocity.x, jumpForce);
            }


        }
        else
        {
            enemyRB.velocity = new Vector2(0, enemyRB.velocity.y);
            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0)
            {
                waitCounter = waitAtPoint;
                currentPoint++; // lấy vị trí patroipoint khác +1

                if (currentPoint >= patrolPoints.Length) // set nếu lớn hơn độ dài của mảng thì quay về 0
                {
                    currentPoint = 0;
                }
            }


        }
        EnemyAnim.SetFloat("Speed", Mathf.Abs(enemyRB.velocity.x));
    }
}

