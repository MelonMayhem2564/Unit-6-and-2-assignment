using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Playerscript: MonoBehaviour
{
    public CharacterController controller;
    public Vector3 playerVelocity;
    public bool groundedPlayer;
    private float speed = 5f;
    private float jumpHeight = 1f;
    public float gravityValue = -9.81f;
    public Transform cam;
    Animator anim;
    private float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    //bool isGrounded;
    //bool isJumping;

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

        groundedPlayer = controller.isGrounded;
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

            Vector3 MoveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(MoveDir.normalized * speed * Time.deltaTime);
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }
    }
    void PlayerJump()
    {
        float speed = new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude;

        print("speed=" + speed);
         

        if (Input.GetButtonDown("Jump"))
        {
            if (groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -4.0f * gravityValue);
                if (speed < 0.1)
                {
                    JumpDelay();
                    anim.SetBool("StandingJump", true);
                    print("do standing jump");
                }

                if (speed > 0.1)
                {
                    anim.SetBool("Jump", true);
                    print("do jump");
                } 
            }
            else
            {
                print("can't jump");
            }
        }
        else
        {
        }

        if( playerVelocity.y < 0 )
        {
            if(groundedPlayer == true)
            {
                anim.SetBool("Jump", false);
                anim.SetBool("StandingJump", false);
                playerVelocity.y = -2;
                print("landed");
            }
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

    }
    IEnumerator JumpDelay()
    {
        yield return new WaitForSeconds(1f);
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
