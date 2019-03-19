using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour, Interactable
{

    public string objectname;
    public float weight;
    
    void Start()
    {
        PlayerController.Instance.objectsInInventory.Add(objectname, this);
    }
    
    public void Action()
    {
        if (Inventory.Instance.AddItem(CreateItem()))
        {
            gameObject.SetActive(false);
        }
    }

    protected abstract Item CreateItem();

}
