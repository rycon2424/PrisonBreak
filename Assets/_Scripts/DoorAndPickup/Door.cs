using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, Interactable
{
    public int ID;
    public Animator doorAnim;

    public bool open = false;
    
    public void Action()
    {
        Open();
    }

    public void Open()
    {
        if (ID == -1 || Inventory.Instance.HasKey(ID))
        {
            doorAnim.Play("Open");
            open = !open;
        }
    }

}
