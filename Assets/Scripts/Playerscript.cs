using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Playerscript: MonoBehaviour
{
    public CharacterController controller;
    public Vector3 playerVelocity;
    public bool groundedPlayer;
    public float speed = 20f;
    public float jumpHeight = 1.5f;
    public float gravityValue = -9.81f;
    public Transform cam;
    Animator anim;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    bool isGrounded;
    bool isJumping;

    void Start()
    {
       anim = GetComponent<Animator>();
       controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        CameraAndMovement();
        PlayerJump();
        PlayerPunch();

        SetDebugSpeed();
    }

    void CameraAndMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 SimpleMoveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(SimpleMoveDir.normalized * speed * Time.deltaTime);
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }
    }
    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if( groundedPlayer )
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -4.0f * gravityValue);
                anim.SetBool("Jump", true);
                print("do jump");
            }

            if (playerVelocity.y > 6)
            {
                ;
            }
            else
            {
                print("can't jump");

            }
        }
        else
        {
            anim.SetBool("Jump", false);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        groundedPlayer = controller.isGrounded;

        if (groundedPlayer)
        {
            playerVelocity.y = -6f;
        }

    }
    void PlayerPunch()
    {
        if (Input.GetKeyDown("x") == true)
        {
            anim.SetBool("Punch", true);
        }
        else
        {
            anim.SetBool("Punch", false);
        }
        
    }

    void SetDebugSpeed()
    {
        if( Input.GetKeyDown("1"))
        {
            Time.timeScale = 1;
        }
        if (Input.GetKeyDown("0"))
        {
            Time.timeScale = 0.35f;
        }



    }
}
