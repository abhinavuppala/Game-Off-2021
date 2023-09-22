using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // references
    public CharacterController controller;
    public TMPro.TextMeshProUGUI ammoDisplay;
    public Transform groundCheck;
    public LayerMask groundMask;

    public KeyCode lagbackKeybind = KeyCode.G;
    public KeyCode sprintKeybind = KeyCode.LeftShift;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public int currentAmmo;

    // speed is default speed, sprint is how much sprinting speeds you up
    public float speed = 14f;
    public float sprint = 5f;
    public float jumpHeight = 5f;

    Vector3 velocity;
    float moveSpeed = 0f;
    bool isGrounded;
    Vector3 lastGroundedPos;

    // Update is called once per frame
    void Update()
    {
        // GetKey - true if key is pressed currently
        // GetKeyDown - true only when the key is pressed
        moveSpeed = speed;
        if (Input.GetKey(sprintKeybind))
        {
            moveSpeed += 5;
        }
        
        // true if collides with anything in the groundMask (makes sphere radius of groundDistance)
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded)
        {
            // save last position on ground for lagback ability
            lastGroundedPos = transform.position;
            if (velocity.y < 0) { velocity.y = 0f; }
        }
        
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // takes into account player's rotation
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * moveSpeed * Time.deltaTime);

        // v = sqrt(-2 * h * g)
        // v: velocity needed to jump h high
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // decrease velocity
        velocity.y += gravity * Time.deltaTime;

        // y = (1/2)g(t^2) according to physics
        // that is why it is again multiplied by Time.deltaTime
        controller.Move(velocity * Time.deltaTime);

        // lagback, overriding all other movement if clicked
        if (Input.GetKeyDown(lagbackKeybind))
        {
            // move back to the last grounded position if in air when pressed
            controller.Move(lastGroundedPos - transform.position);
            // TODO: increase instability
        }
    }

}
