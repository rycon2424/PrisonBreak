using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningTwitterDoor : MonoBehaviour
{

    public GameObject twitterGateMenu;
    public static bool opening;

    public Animator doorAnim;

    void Start()
    {
        twitterGateMenu.SetActive(false);
        opening = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hide();
        }

        if (opening == true)
        {
            doorAnim.Play("Open2");
            Hide();
        }
    }

    void Hide()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        twitterGateMenu.SetActive(false);
    }

    public void ShowTwitterMenu()
    {
        twitterGateMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
