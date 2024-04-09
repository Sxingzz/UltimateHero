using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController playerTransform;

    // CameraBound
    public BoxCollider2D BoundsBox;

    private float halfWidth;
    private float halfHeight;



    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindObjectOfType<PlayerController>();
        halfHeight = Camera.main.orthographicSize; // OrthographicSixe đại diện cho kích thước độ cao của camera
        halfWidth = halfHeight * Camera.main.aspect; // Camera.main.aspect đại diện cho tỉ lệ khung hình aspect ratio của camera
    }

    // Update is called once per frame  
    void Update()
    {
        if (playerTransform != null)
        {
            this.transform.position = new Vector3(
               Mathf.Clamp(playerTransform.transform.position.x, BoundsBox.bounds.min.x + halfWidth, BoundsBox.bounds.max.x - halfWidth),
               Mathf.Clamp(playerTransform.transform.position.y, BoundsBox.bounds.min.y + halfHeight, BoundsBox.bounds.max.y - halfHeight),
               this.transform.position.z);
        }
        else
        {
            playerTransform = FindObjectOfType<PlayerController>();
        }
    }
}
