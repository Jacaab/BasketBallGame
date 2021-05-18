using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControllerScript : MonoBehaviour {

    private CharacterController controller;     // handles controls and physics
    private Vector3 playerVelocity;             
    private bool groundedPlayer;                // system to allow jump mechanic and fix bugs from custom physics
    private float gravityValue = -10f;          // how stong gravity is // should be a universal value // maybe move to manager
    public float playerHeight = 6.0f;           // {
    public float playerWidth = 1.2f;            //      these will be changed dependant on character selection
    public float runSpeed = 4.0f;               //      and change how the contoller will interact
    public float jumpHeight = 15.0f;            // }
    public int shotAccuracy = 5;
    public string spritePrefix;                 // can maybe use this to load different sprites for different characters in the future using the same script. NOT TESTED

    public bool possession = false;

    public GameObject manager;
    public GameObject ball;

    private void Start()                        // maybe use a public state to determin stats ie. state 1 == default stats, state 2 == tall stats etc.
    {
        controller = gameObject.GetComponent<CharacterController>();
        controller.radius = playerWidth;
        controller.height = playerHeight;
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
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 move = new Vector3(horizontal, 0f, vertical).normalized;


        // only allow movement while grounded. this prevents movement while jumping (balancing)
        if (groundedPlayer)
        {
            if (move.magnitude >= 0.1f)
            {
                controller.Move(move * runSpeed * Time.deltaTime);
            }
            
        }


        // when spacebar is pressed and the player is grounded do a jump based on jump height var
        if (Input.GetKeyDown("k") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            groundedPlayer = false;
        }

        // when j is pressed attempt a shot // shot function  is handled by the manager
        if (Input.GetKeyDown("j"))
        {
            if (possession == true)
            { 
                manager.GetComponent<managerScript>().ballShotCheck = true;
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