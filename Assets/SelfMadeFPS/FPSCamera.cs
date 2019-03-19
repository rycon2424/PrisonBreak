using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    FPSPlayer FPSPlayer;
    RaycastHit hit;

    public OpeningTwitterDoor twitterDoorref;

    // Explanation Text on inspector
    [Help("THERE MUST BE A GAMEOBJECT WITH THE FPSPLAYER FOR THIS SCRIPT TO WORK"
    + " /// PUT THIS SCRIPT ON THE CAMERA THE PLAYER IS GOING TO USE")]

    [SerializeField] float viewRangeY = 80; // THE RANGE THE PLAYER CAN LOOK UP AND DOWN IN THE Y AXIS
    [HideInInspector]
    [SerializeField] Transform cameraTransform;
    float rotX;
    float rotY;

    void Start()
    {
        FPSPlayer = FindObjectOfType<FPSPlayer>();
        cameraTransform = Camera.main.transform;
    }
    
    void Update()
    {
        //ShowHideCursor();
        MoveView();
    }

    private void ShowHideCursor()   // SHOWS AND HIDES THE CURSOR
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (Input.GetMouseButton(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void MoveView() // CAMERA ROTATION AND CLAMPING
    {
        transform.Rotate(Vector3.left * Input.GetAxis("Mouse Y") * FPSPlayer.mouseSensitivity * Time.deltaTime);
        rotX += Input.GetAxis("Mouse X") * FPSPlayer.mouseSensitivity;
        rotY += Input.GetAxis("Mouse Y") * FPSPlayer.mouseSensitivity / 35;
        rotY = Mathf.Clamp(rotY, -viewRangeY, viewRangeY);
        cameraTransform.localRotation = Quaternion.Euler(-rotY, 0f, 0f);
    }
    
    public void Dash() // DASH FORWARD (IF ENABLED ON THE FPSPLAYER INSPECTOR)
    {
        FPSPlayer.rigidBody.AddForce(transform.forward * FPSPlayer.dashForce, ForceMode.Impulse);
    }

    public void Raycast() // RAYCASTING
    {
        Debug.DrawRay(this.gameObject.transform.position, this.gameObject.transform.TransformDirection(Vector3.forward) * FPSPlayer.rangeCameraRay);  //RayCast Debug

        Ray myRay = new Ray(this.gameObject.transform.position, this.gameObject.transform.TransformDirection(Vector3.forward));

        if (Input.GetKeyDown(KeyCode.E) && Physics.Raycast(myRay, out hit, FPSPlayer.rangeCameraRay))
        {
            if (hit.collider.CompareTag("Twitter"))
            {
                twitterDoorref.ShowTwitterMenu();
            }
        }
    }

}
