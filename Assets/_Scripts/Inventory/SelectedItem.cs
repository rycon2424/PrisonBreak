using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedItem : MonoBehaviour
{
    public int itemNumber;
    public PlayerController playerControllerRef;

    public void Select()
    {
        playerControllerRef.inventorySlot = itemNumber;
    }

}
