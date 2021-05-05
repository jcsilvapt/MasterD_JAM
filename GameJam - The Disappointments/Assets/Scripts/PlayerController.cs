using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Rigidbody Reference
    private Rigidbody rb;

    //Animator Reference
    private Animator animator;

    //Movement Vector
    private Vector3 movement;

    //Flag Controlling if Player's can Jump (NOT WORKING BECAUSE UNITY SUCKS)
    [SerializeField] private bool isGrounded;

    //Flag Controlling Player's Jump
    private bool jump;

    //Ray Distance
    [SerializeField] private float rayDistance;

    //Animation States
    private Dictionary<string, string> AnimationStates = new Dictionary<string, string>()
    {
        ["Idle"] = "Idle",
        ["RunLeft"] = "Run_Left",
        ["RunRight"] = "Run_Right",
        ["JumpInPlace"] = "Jump_InPlace",
        ["JumpLeft"] = "Jump_Left",
        ["JumpRight"] = "Jump_Right"
    };

    //Holds the Current Animation State
    private string currentState;

    //Speed Array
    [SerializeField] private float[] speedSet;

    // Start is called before the first frame update
    void Start()
    {
        //Get References
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Detect Player Movement Input
        movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);

        if(isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = new Vector3(0, .5f, 0);
        }
    }

    private void FixedUpdate()
    {
        //Check if Player in on the Ground
        if (Physics.Raycast(transform.position, Vector3.down, rayDistance))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        //Check Movement Based on Input
        Vector3 velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
        if (movement.x < 0)
        {
            velocity.x = -speedSet[1];
        }
        else if (movement.x > 0)
        {
            velocity.x = speedSet[1];
        }
        else
        {
            velocity.x = speedSet[0];
        }

        //Check if Trying to Jump
        if (jump && isGrounded)
        {
            rb.AddForce(Vector3.up * 1000, ForceMode.Force);
            jump = false;
        }

        //Animation Control
        if (isGrounded)
        {
            if (movement.x < 0)
            {
                ChangeAnimationState(AnimationStates["RunLeft"]);
            }
            else if (movement.x > 0)
            {
                ChangeAnimationState(AnimationStates["RunRight"]);
            }
            else
            {
                ChangeAnimationState(AnimationStates["Idle"]);
            }
        }
        else
        {
            if(movement.x < 0)
            {
                ChangeAnimationState(AnimationStates["JumpLeft"]);
            }
            else if (movement.x > 0)
            {
                ChangeAnimationState(AnimationStates["JumpRight"]);
            }
            else
            {
                ChangeAnimationState(AnimationStates["JumpInPlace"]);
            }
        }

        //Assign new Velocity to Rigidbody
        rb.velocity = velocity;
    }

    #region Animator Controller Methods
    private void ChangeAnimationState(string newState)
    {
        //Stop the same animation from interrupting itself - Guard
        if(currentState == newState)
        {
            return;
        }

        //Play the Animation
        animator.Play(newState);

        //Reassign the current state
        currentState = newState;
    }
    #endregion
}
