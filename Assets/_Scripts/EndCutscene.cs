using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCutscene : MonoBehaviour
{

    public Animator anim;
    public GameObject cameraObject;
    public GameObject playerCamera;

    void Start()
    {
        cameraObject.SetActive(false);
    }
    
    void Update()
    {
        if (Ship.hasPart1Static && Ship.hasPart2Static && Ship.hasPart3Static && Ship.hasPart4Static)
        {
            Debug.Log("Ending");
            
            cameraObject.SetActive(true);
            playerCamera.SetActive(true);
            anim.Play("Ending");
            Ship.hasPart1Static = false;
        }
    }
}
