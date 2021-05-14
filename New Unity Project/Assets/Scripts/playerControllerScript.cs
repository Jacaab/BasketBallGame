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
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (groundedPlayer)
        {
            controller.Move(move * Time.deltaTime * runSpeed);
        }

        // Changes the height position of the player..
        if (Input.GetKeyDown("n") && groundedPlayer)
        { 
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            Debug.Log("Error");
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
} 