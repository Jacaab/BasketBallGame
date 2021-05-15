using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControllerScript : MonoBehaviour {

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float gravityValue = -10f;
    public float playerHeight = 6.0f;
    public float playerWidth = 1.2f;
    public float runSpeed = 4.0f;
    public float jumpHeight = 15.0f;
    public string spritePrefix; //can maybe use this to load different sprites for different characters in the future using the same script. NOT TESTED

    public GameObject manager;
    public GameObject ball;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        controller.radius = playerWidth;
        controller.height = playerHeight;
    }

    void Update()
    {
        
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer)
        {
            playerVelocity.y = 0f;
        }
        
        // basic movement x + y
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 move = new Vector3(horizontal, 0f, vertical).normalized;

        if (move.magnitude >= 0.1f)
        {
            controller.Move(move * runSpeed * Time.deltaTime);
        }
        // only allow movement while grounded. this prevents movement while jumping (balancing)
        if (groundedPlayer)
        {

        }

        // when spacebar is pressed 
        if (Input.GetButtonDown("Jump"))
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        if (Input.GetKeyDown("j"))
        {
            manager.GetComponent<managerScript>().ballShotCheck = true;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
} 