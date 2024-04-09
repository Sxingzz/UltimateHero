using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D playerRB;

    //Jump
    [SerializeField]
    private Transform groundCheckPoint;

    public Animator playerStandingAnim;

    [SerializeField]
    private LayerMask groundLayer;

    
    [SerializeField]
    private float jumpForce;

    private bool isOnGround;

    private bool canDoubleJump;

    // super1
    public Animator playerSuper1Anim;

    public GameObject standingState;
    public GameObject Super1State;

    // super 2
    public Animator playerSuper2Anim;
    public GameObject Super2State;
    
    // Dash
    public float DashSpeed;
    public float DashTime;
    private float DashCounter;

    public SpriteRenderer playerStandSprite;
    public SpriteRenderer playerSuper1Sprite;
    public SpriteRenderer playerSuper2Sprite;
    public SpriteRenderer afterImage;

    public float afterImageLifeTime;
    public float timeBetweenAfterImage;

    public Color afterImageColor;

    private float afterImageCounter;
    public float waitAfterDashing;
    private float dashRechargerCounter;



    // Run, Move
    [SerializeField]
    private float moveSpeed;

    private float directionX;
    private float directionY;

    // Anility Tracker
    [HideInInspector]
    public  PlayerAbilityTracker abilities;

    // Fly

    //private bool flying;


    // nếu đang trong trạng thái bay thì sẽ set di duyển bay
    // nếu chạm đất thì set flying = false , và di chuyển bình thường


    public bool canMove;


    // Start is called before the first frame update
    void Start()
    {
        abilities = GetComponent<PlayerAbilityTracker>();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (dashRechargerCounter > 0)
            {
                dashRechargerCounter -= Time.deltaTime;
            }
            else
            {
                if (Input.GetButtonDown("Fire2"))
                {
                    DashCounter = DashTime;
                    ShowAfterImage();
                }
            }
            if (DashCounter > 0)
            {
                DashCounter -= Time.deltaTime;
                playerRB.velocity = new Vector2(DashSpeed * transform.localScale.x, playerRB.velocity.y);
                afterImageCounter -= Time.deltaTime;
                if (afterImageCounter <= 0)
                {
                    ShowAfterImage();
                }
                dashRechargerCounter = waitAfterDashing;
            }
            else
            {
                // Get Input
                directionX = Input.GetAxisRaw("Horizontal"); // A,D
                directionY = Input.GetAxisRaw("Vertical"); // Space

                // Move
                playerRB.velocity = new Vector2(directionX * moveSpeed, playerRB.velocity.y);


                // Flip
                if (playerRB.velocity.x < 0)
                {
                    // left
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (playerRB.velocity.x > 0)
                {
                    // Right
                    transform.localScale = Vector3.one;
                }
            }

            // Jump
            if (Input.GetButtonDown("Jump") && (isOnGround || (canDoubleJump )))
            {
                if (isOnGround)
                {
                    canDoubleJump = true;
                }
                else
                {
                    canDoubleJump = false;
                    playerStandingAnim.SetTrigger("DoubleJump"); // call animation jump double
                    playerSuper1Anim.SetTrigger("Double1Jump"); // call animation jump double
                    playerSuper2Anim.SetTrigger("Double2Jump");
                }
                playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
            }

            // Fly

            //if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) && !canDoubleJump)
            //{
            //    playerRB.gravityScale = 0;
            //}

            //if (Input.GetKeyUp(KeyCode.D) && !canDoubleJump)
            //{
            //    playerRB.gravityScale = 5;
            //}

        }
        else
        {
            playerRB.velocity = Vector2.zero;
        }

        // check is on ground
        isOnGround = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, groundLayer);

        // Animation
        // standing state
        if (standingState.activeSelf)
        {
            playerStandingAnim.SetFloat("Speed", Mathf.Abs(playerRB.velocity.x));
            // gọi hiệu ứng chạy, Math.Abs là trị tuyệt đối để trục X luôn dương
            playerStandingAnim.SetBool("IsOnGround", isOnGround); // gọi hiệu ứng nhảy
        }
        // super 1
        if (Super1State.activeSelf)
        {
            playerSuper1Anim.SetFloat("Speed", Mathf.Abs(playerRB.velocity.x));
            // gọi hiệu ứng chạy, Math.Abs là trị tuyệt đối để trục X luôn dương
            playerSuper1Anim.SetBool("IsOnGround", isOnGround); // gọi hiệu ứng nhảy
        }
        // super 2
        if (Super2State.activeSelf)
        {
            playerSuper2Anim.SetFloat("Speed", Mathf.Abs(playerRB.velocity.x));
            playerSuper2Anim.SetBool("IsOnGround", isOnGround);
        }

    }
    private void ShowAfterImage()
    {
        SpriteRenderer image = Instantiate(afterImage, transform.position, transform.rotation);
        // tạo ra một hình ảnh mới của prefab afterImage (có kiểu dữ liệu là SpriteRenderer) tại vị trí và góc quay của đối tượng
        if (standingState.activeSelf)
        {
            image.sprite = playerStandSprite.sprite;
        } 
        else if (Super1State.activeSelf)
        {
            image.sprite = playerSuper1Sprite.sprite;
        }
        else if (Super2State.activeSelf)
        {
            image.sprite= playerSuper2Sprite.sprite;
        }
        
        // sao chép sprite từ người chơi (Player) vào AfterImage để nó có cùng hình dạng.
        image.transform.localScale = transform.localScale;
        // sao chép tỷ lệ kích thước của đối tượng hiện tại (Player) vào AfterImage.
        image.color = afterImageColor; // gán giá trị màu sắc cho hình ảnh mới
        Destroy(image.gameObject, afterImageLifeTime); // hủy hình ảnh mới sau 1 khoảng thời gian
        afterImageCounter = timeBetweenAfterImage; // đếm ngược thời gian giửa các lần hiển thị
    }
    // tutorial double click
    //1. Unity 2D detech double click on gameobject
    //2. transform player -> enemy
    //3. check distance < 0.1f
    //4. play animation attack
    //5. deal damge


}
