using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EricaController : MonoBehaviour
{

    //Variable
    float speed = 2f;
    float rotSpeed = 80;
    float gravity = 8;
    float rot = 0f;

    Vector3 moveDir = Vector3.zero;

    //Character Controller
    CharacterController controller;

    //Animator
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //Get the character controller that we set up in the Editor
        controller = GetComponent<CharacterController>();

        //Get the animator that we set up in the Editor
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        GetInput();
    }

    void GetInput()
    {
        if (controller.isGrounded)
        {
            if (Input.GetMouseButton(0))
            {
                if (anim.GetBool("walking"))
                {
                    anim.SetBool("walking", false);
                    anim.SetInteger("condition", 0);
                }
                else if (anim.GetBool("walking") == false)
                {
                    Punching();
                }

            }

            if(Input.GetMouseButton(1))
            {
                if (anim.GetBool("walking"))
                {
                    anim.SetBool("walking", false);
                    anim.SetInteger("condition", 3);
                }

                else if(anim.GetBool("walking") == false)
                {
                    Jumping();
                }
            }
        }
    }

    void Movement()
    {
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (anim.GetBool("punching"))
                {
                    return;
                }

                else if (anim.GetBool("jumping"))
                    return;

                if (anim.GetBool("punching") == false)
                {
                    anim.SetBool("walking", true);
                    //Set condition to 1
                    anim.SetInteger("condition", 1);

                    moveDir = new Vector3(0, 0, 1);
                    moveDir = moveDir * speed;
                    moveDir = transform.TransformDirection(moveDir);
                }

                if(anim.GetBool("jumping") == false)
                {
                    anim.SetBool("walking", true);
                    anim.SetInteger("condition", 1);

                    moveDir = new Vector3(0, 0, 1);
                    moveDir = moveDir * speed;
                    moveDir = transform.TransformDirection(moveDir);
                }

            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                anim.SetBool("walking", false);
                anim.SetInteger("condition", 0);
                moveDir = Vector3.zero;
            }
        }

        rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);

        moveDir.y = moveDir.y - gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }

    void Punching()
    {
        StartCoroutine(PunchRoutine());
    }

    void Jumping()
    {
        StartCoroutine(JumpRoutine());
    }

    IEnumerator PunchRoutine()
    {
        anim.SetBool("punching", true);
        anim.SetInteger("condition", 2);
        yield return new WaitForSeconds(1);
        anim.SetInteger("condition", 0);
        anim.SetBool("punching", false);
    }

    IEnumerator JumpRoutine()
    {
        anim.SetBool("jumping", true);
        anim.SetInteger("condition", 3);
        yield return new WaitForSeconds(1);
        anim.SetInteger("condition", 0);
        anim.SetBool("jumping", false);
    }
}
