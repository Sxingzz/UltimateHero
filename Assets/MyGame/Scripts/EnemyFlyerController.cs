using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyerController : MonoBehaviour
{
    public float RangeToStartChase; // bán kính đánh player
    private bool isChasing; // kiểm tra có đáng dí k

    public float MoveSpeed;
    public float TurnSpeed;

    private Transform playerTF;

    public Animator EnemyAnim;

    // Start is called before the first frame update
    void Start()
    {
        playerTF = PlayerHealthController.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isChasing) // = false nếu mà không có dí
        {
            if (Vector3.Distance(transform.position, playerTF.position) < RangeToStartChase) // tính khoảng cách
            {
                isChasing = true;
                EnemyAnim.SetBool("IsChasing", isChasing);
            }
        }
        else
        {
            if (playerTF.gameObject.activeSelf)
            {
                // xoay
                Vector3 direction = transform.position - playerTF.position; // tính vecter hướng (đường xéo)
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // công thức tìm 1 góc xoay của enemy dựa váo hướng tính ở trên
                Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward); // tính góc xoay theo trục Z (vecter 3 forward là trục Z)
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, TurnSpeed * Time.deltaTime); // Quaternion.Slerp tạo thời gian xoay
                transform.position = Vector3.MoveTowards(transform.position, playerTF.position, MoveSpeed * Time.deltaTime); // enemy di chuyển tới player
            }
        }

    }
}
