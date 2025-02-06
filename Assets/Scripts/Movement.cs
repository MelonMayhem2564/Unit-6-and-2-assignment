using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class Movement : MonoBehaviour
{
    Animator anim;
    SpriteRenderer sr;
    bool isGrounded;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerJump();
    }
    void PlayerMovement()
    {

        if (Input.GetKey("up") == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
            anim.SetFloat("Speed", 5f);
        }
        else
        {
            anim.SetFloat("Speed", 0f);
        }

        if (Input.GetKey("down") == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
            anim.SetFloat("Speed", 5f);
        }

        if (Input.GetKey("left"))
        {
            transform.Rotate(0, -0.5f, 0, Space.Self);
        }

        if (Input.GetKey("right"))
        {
            transform.Rotate(0, 0.5f, 0, Space.Self);
        }
    }
    void PlayerJump()
    {

    } 
}
