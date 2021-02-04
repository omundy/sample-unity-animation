using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    // references to components we need
    public Animator animator;
    public CharacterController2D controller;

    // the horizontal movement speed / direction
    float horizontalMove = 0f;
    public float runSpeed = 40f;

    // boolean for jump
    bool jump = false;



    // use Update to get input from player 
    void Update ()
    {
        // get horizontal input (between -1 and 1) from player
        horizontalMove = Input.GetAxisRaw ("Horizontal") * runSpeed;
        //Debug.Log("horizontalMove = " + horizontalMove);

        // set the speed variable
        animator.SetFloat ("Speed", Mathf.Abs (horizontalMove));

        // if jump button pressed
        if (Input.GetButtonDown ("Jump")) {
            jump = true;
            animator.SetBool ("IsJumping", true);
        }
    }

    // use FixedUpdate to move character so position is in sync with physics calculations 
    private void FixedUpdate ()
    {
        // move the character = horizontal movement * amount of time elapsed since last time
        // ensures the same amount regardless of the frame rate and platform
        // param 2 = crouch, param 3 = jump boolean
        controller.Move (horizontalMove * Time.fixedDeltaTime, false, jump);

        // reset jump
        jump = false;
    }

    // called from character controller
    public void OnLanding ()
    {
        // stop jump animation when characte lands
        animator.SetBool ("IsJumping", false);
    }


}