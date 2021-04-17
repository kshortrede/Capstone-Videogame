using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Some credit to "Single Sapling Games" for inspiration


//In charge of moving and animating player
public class PlayerMovement : MonoBehaviour
{
    //Physical character
    private CharacterController controller;
    private Animator represent;

    //Movement Speed
    private float idleSpeed = 0f;
    private float walkSpeed = 5f;
    private float runSpeed = 10f;    
    private float speed;

    private Vector3 move;
    private Vector3 velocity;

    //Physics
    [SerializeField] private bool isGrounded; //[SerializeField]
    [SerializeField] private LayerMask groundMask;
    //[SerializeField] private float groundCheckDistance;    
    //[SerializeField] private float gravity;

    private float groundCheckDistance = 0.2f;
    private float gravity = -9.8f;

    private float jumpHeight = 2f;
    
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        represent = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement() 
    {
        //Check status of character in relation to ground
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if(isGrounded && velocity.y < 0) 
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //Vector3 move = transform.right * horizontal + transform.forward * vertical;

        //Movement is in accordance to where the character is facing
        move = new Vector3(horizontal, 0, vertical);
        move = transform.TransformDirection(move);

        //Can only control 
        if (isGrounded) 
        {
            //Movement command is checked
            if (move != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                //if(Input.GetKey(KeyCode.))
                Walk();
            }
            else if (move != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }
            else if (move == Vector3.zero)
            {
                Idle();
            }

            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                Jump();
            }

            move *= speed;
        }             
        
        //Movement is applied to character
        controller.Move(move * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    //Determines movement type
    private void Walk()
    {
        speed = walkSpeed;
        if (Input.GetKeyDown(KeyCode.UpArrow) || (Input.GetKey("w")))
        {
            //Second value represents animation type to utilize from the movement blend tree
            represent.SetFloat("Pace", 0.1f, 0.1f, Time.deltaTime);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || (Input.GetKey("s")))
        {
            represent.SetFloat("Pace", 0.2f, 0.1f, Time.deltaTime);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || (Input.GetKey("a")))
        {
            represent.SetFloat("Pace", 0.3f, 0.1f, Time.deltaTime);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || (Input.GetKey("d")))
        {
            represent.SetFloat("Pace", 0.4f, 0.1f, Time.deltaTime);
        }        
    }
    private void Run()
    {
        speed = runSpeed;
        if (Input.GetKeyDown(KeyCode.UpArrow) || (Input.GetKey("w")))
        {
            //Second value represents animation type to utilize from the movement blend tree
            represent.SetFloat("Pace", 0.5f, 0.1f, Time.deltaTime);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || (Input.GetKey("s")))
        {
            represent.SetFloat("Pace", 0.6f, 0.1f, Time.deltaTime);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || (Input.GetKey("a")))
        {
            represent.SetFloat("Pace", 0.7f, 0.1f, Time.deltaTime);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || (Input.GetKey("d")))
        {
            represent.SetFloat("Pace", 0.8f, 0.1f, Time.deltaTime);
        }
    }
    private void Idle()
    {
        speed = idleSpeed;
        represent.SetFloat("Pace", 0);
    }
    private void Jump() 
    {
        represent.SetFloat("Pace", 0.9f);
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);        
    }

}
