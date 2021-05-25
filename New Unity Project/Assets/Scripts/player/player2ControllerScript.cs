using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2ControllerScript : MonoBehaviour
{

    private CharacterController controller;     // handles controls and physics
    private Vector3 playerVelocity;
    private bool groundedPlayer = false;        // system to allow jump mechanic and fix bugs from custom physics
    private bool canMove = true;                // lock movement based on certain actions such as after a jump with the ball (double dribble) or during center or post scoring
    private float gravityValue = -10.0f;        // how stong gravity is // should be a universal value // maybe move to manager
    private float playerHeight;                 // {
    private float playerWidth;                  //      these will be changed dependant on character selection
    private float runSpeed;                     //      and change how the contoller will interact
    private float jumpHeight;                   // 
    public int shotAccuracy;                    // }
    public int characterSelect;
    public string spritePrefix;                 // can maybe use this to load different sprites for different characters in the future using the same script. NOT TESTED

    public bool possession = false;

    public GameObject manager;

    private void Start()                        // maybe use a public state to determin stats ie. state 1 == default stats, state 2 == tall stats etc.
    {
        characterSelect = settingsScript.player2CharacterSelect;
        controller = gameObject.GetComponent<CharacterController>();
        switch (characterSelect)
        {
            case 1:
                playerHeight = 6f;
                playerWidth = 1.2f;
                runSpeed = 6f;
                jumpHeight = 1.5f;
                shotAccuracy = 4;
                // add any extra stats here. ie strength

                controller.radius = playerWidth;
                controller.height = playerHeight;
                gameObject.transform.GetChild(0).transform.localScale = new Vector3(2.8f, playerHeight, 2.8f);      // sprite size
                gameObject.transform.GetChild(1).transform.localPosition = new Vector3(0f, 4.75f, 0f);              // hand position
                break;

            case 2:
                playerHeight = 8f;
                playerWidth = 1.2f;
                runSpeed = 4.5f;
                jumpHeight = 1.7f;
                shotAccuracy = 6;
                // *

                controller.radius = playerWidth;
                controller.height = playerHeight;
                gameObject.transform.GetChild(0).transform.localScale = new Vector3(2.8f, playerHeight, 2.8f);      // sprite size
                gameObject.transform.GetChild(1).transform.localPosition = new Vector3(0f, 5.25f, 0f);              // hand position
                break;

            case 3:
                playerHeight = 4.5f;
                playerWidth = 1f;
                runSpeed = 7.5f;
                jumpHeight = 1.3f;
                shotAccuracy = 5;
                // *

                controller.radius = playerWidth;
                controller.height = playerHeight;
                gameObject.transform.GetChild(0).transform.localScale = new Vector3(2.8f, playerHeight, 2.8f);      // sprite size
                gameObject.transform.GetChild(1).transform.localPosition = new Vector3(0f, 3.5f, 0f);               // hand position

                break;
        }
    }

    void Update()
    {
        // isGrounded system to stop the player from being slowed down by floor collisions from gravity
        if (controller.isGrounded)
        {
            groundedPlayer = true;
            playerVelocity.y = 0f;  // set velocity to 0 to prevent endless falling which prevents jumping
        }

        // basic movement x + y
        float horizontal = Input.GetAxisRaw("Horizontal2");
        float vertical = Input.GetAxisRaw("Vertical2");
        Vector3 move = new Vector3(horizontal, 0f, vertical).normalized;


        // only allow movement while grounded. this prevents movement while jumping (balancing)
        if (groundedPlayer && canMove)
        {
            if (move.magnitude >= 0.1f)
            {
                controller.Move(move * runSpeed * Time.deltaTime);
            }

        }

        //** change action inputs

        // JUMP
        // when spacebar is pressed and the player is grounded do a jump based on jump height var
        if (Input.GetKeyDown("2") && groundedPlayer && canMove)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            groundedPlayer = false;

            if (possession)         // if player had possession of the ball when they jumpedthey can no longer move
            {
                canMove = false;
            }
        }

        // SHOOT / TACKLE 
        // when j is pressed attempt a shot // shot function  is handled by the manager
        if (Input.GetKeyDown("1"))
        {
            if (possession == true)
            {
                manager.GetComponent<managerScript>().ballShotCheck = true;
                canMove = true;     // after attempting a shot the player is allowed to move
            }

            if (possession == false)
            {

            }
        }

        // gravity
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
