using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayer : MonoBehaviour
{
    // Explanation Text on inspector
    [Help("THERE MUST BE A CAMERA WITH THE FPSCAMERA SCRIPT FOR THIS SCRIPT TO WORK" 
    + " /// PUT THIS SCRIPT ON THE MAIN CHARACTER WITH A RIGIDBODY AND CONTRAIN THE X AND Z ROTATION")]

    [Header("Player Settings")]
    [SerializeField] float moveSpeed = 5f;  // THE PLAYERS MOVEMENTSPEED
    private float mainSpeed;
    [SerializeField] public float mouseSensitivity = 100f;    // THE MOUSE SENSITIVITY IN THE X AND Y AXIS
    
    [Header("RayCast Settings")]
    [SerializeField] bool useRayCasting;    // ENABLED THE USE OF RAYCAST IN THE FPSCAMERA SCRIPT
    public float rangeCameraRay = 4;

    [Header("Jump Settings")]
    [SerializeField] bool canJump = true;
    [SerializeField] int amountOfJumps = 2;  // HOW MANY TIMES THE PLAYER CAN JUMP
    [SerializeField] float jumpForce = 5;    // THE FORCE THE PLAYER GETS LAUNCHED AT
    private int jumpCount;
    private bool jumpReady = true;
    [Help("SET THIS RANGE TO BARELY REACH UNDER THE CHARACTER")]
    [SerializeField] float jumpRaycastRange = 1.1f;

    [Header("Dash")]
    [SerializeField] bool canDash = true;
    public float dashForce = 30f;     // THE FORCE THAT THE PLAYER GETS LAUNCHED AT
    [SerializeField] float dashCooldown = 2f;    // THE COOLDOWN OF THE DASH
    private bool dashReady = true;
    
    [HideInInspector] public Rigidbody rigidBody;
    FPSCamera FPSCamera;
    
    void Start()
    {
        FPSCamera = FindObjectOfType<FPSCamera>();  // FINDING THE FPSCAMERA SCRIPT TO ACCESS THE RAYCAST FUNCTION
        rigidBody = this.gameObject.GetComponent<Rigidbody>();
        jumpCount = amountOfJumps;
        mainSpeed = moveSpeed;
    }

    void Update()
    {
        if (useRayCasting)
        {
            FPSCamera.Raycast();
        }
        if (canJump)
        {
            Jump();
        }
        if (canDash && dashReady)
        {
            Dash();
        }
        //Move();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move() // THE MOVEMENT OF THE CHARACTER WITH WASD AND ARROW KEYS
    {
        float sprintSpeed = 10f;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = sprintSpeed;
        }
        else
        {
            moveSpeed = mainSpeed;
        }
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime);

        Vector3 pos = transform.position + transform.right * Input.GetAxis("Horizontal") * (moveSpeed * Time.deltaTime);
        pos = pos + transform.forward * Input.GetAxis("Vertical") * (moveSpeed * Time.deltaTime);
        rigidBody.MovePosition(pos);
    }

    RaycastHit hit;  // THE HIT TO RAYCAST THE JUMP
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)   // CHECKS YOUR REMAINING JUMPS TO SEE IF YOURE ALLOWED TO JUMP ONCE MORE
        {
            jumpCount = jumpCount - 1;
            rigidBody.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }
    }

    private void ResetJump() // RESETING THE JUMP
    {
        Debug.DrawRay(this.gameObject.transform.position, this.gameObject.transform.TransformDirection(Vector3.down) * jumpRaycastRange);  //RayCast Debug

        Ray myRay = new Ray(this.gameObject.transform.position, this.gameObject.transform.TransformDirection(Vector3.down));

        if (Physics.Raycast(myRay, out hit, jumpRaycastRange))
        {
            if (hit.collider.tag == "Nothing" == false)         // Checks if you are hitting the Ground
            {
                jumpCount = amountOfJumps;
            }
        }
    }
    void OnCollisionStay(Collision collision)
    {
        ResetJump();
    }
    private void OnCollisionEnter(Collision collision)
    {
        ResetJump();
    }
    
    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            FPSCamera.Dash();
            dashReady = false;
            StartCoroutine(DashCooldown());
        }
    }
    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        yield return new WaitForSeconds(dashCooldown);
        dashReady = true;
    }

}