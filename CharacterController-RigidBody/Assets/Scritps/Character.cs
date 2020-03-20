using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float Gravity = -9f;
    public float GroundDistance = 0.2f;
    public float DashDistance = 5f;
    public float Speed = 5f;
    public float JumpHeight = 2f;
    public LayerMask Ground;
    public Vector3 Drag;

    private CharacterController ccontrol;
    private Vector3 ve;
    private bool isGround = true;
    private Transform _groundChecker;


    void Start()
    {
        ccontrol = GetComponent<CharacterController>();
        _groundChecker = transform.GetChild(0);
    }

    void Update()
    {
        if (ccontrol.isGrounded && ve.y < 0)
            ve.y = 0f;
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        ccontrol.Move(move * Time.deltaTime * Speed);
        if (move != Vector3.zero)
            transform.forward = move;
        if (Input.GetButtonDown("Jump") && isGround)
            ve.y += Mathf.Sqrt(JumpHeight * -2f * Gravity);
        if (Input.GetButtonDown("Dash"))
        {
            Debug.Log("Dash");
            ve += Vector3.Scale(transform.forward, DashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * Drag.x + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * Drag.z + 1)) / -Time.deltaTime)));
        }
        ve.y += Gravity * Time.deltaTime;
        ve.x /= 1 + Drag.x * Time.deltaTime;
        ve.y /= 1 + Drag.y * Time.deltaTime;
        ve.z /= 1 + Drag.z * Time.deltaTime;

        ccontrol.Move(ve * Time.deltaTime);
    }

}
