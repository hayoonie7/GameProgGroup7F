using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    public float speed = 10f;
    public float jumpHeight = 10f;
    public float gravity = 9.81f;
    public float airControl = 10f;
    private Vector3 input;

    private Vector3 moveDirection;

    private Animator animator;

    private float distanceToGround;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        this.distanceToGround = GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.isGameOver) return;
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
        Vector3 right = Camera.main.transform.right;

        input = (right * moveHorizontal+ forward * moveVertical).normalized;
        input *= speed;

        if (this.IsGrounded())
        {
            moveDirection = input;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = Mathf.Sqrt(2 * jumpHeight * gravity);
            }
            else
            {
                moveDirection.y = 0.0f;
            }
        }
        else
        {
            input.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        if (Mathf.Abs(moveHorizontal) > 0 || Mathf.Abs(moveVertical) > 0)
        {
            transform.forward = new Vector3(input.x, 0, input.z);
            animator.SetFloat("Speed_f", speed);
        }
        else
        {
            animator.SetFloat("Speed_f", 0);
        }
        
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(this.transform.position + GetComponent<CharacterController>().center, -Vector3.up, this.distanceToGround + 0.1f);
    }
}
