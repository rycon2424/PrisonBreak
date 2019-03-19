using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    
    public List<Item> _inventoryList = new List<Item>();
    public float totalWeight = 0;
    public float weightCap = 20;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

    }

    public bool AddItem(Item item)
    {
        if (totalWeight + item.weight > weightCap)
        {
            Debug.Log("You carry too much weight");
            return false;
        }
        else
        {
            totalWeight += item.weight;
            _inventoryList.Add(item);
            Debug.Log("Item added your total weight is " + totalWeight + "/" + weightCap);
            return true;
        }
    }
    
    public void RemoveItem(Item item)
    {
        if (_inventoryList.Remove(item))
        {
            totalWeight -= item.weight;
        }
    }

    public bool HasKey(int id)
    {
        for (int i = 0; i < _inventoryList.Count ; i++)
        {
            if (_inventoryList[i] is AccessItem)
            {
                AccessItem it = (AccessItem)_inventoryList[i];
                if (it.doorNumber == id)
                {
                    RemoveItem(it);
                    return true;
                }
            }
        }
        return false;
    }

    public void PrintToConsole()
    {
        foreach (var i in _inventoryList)
        {
            Debug.Log(i.itemName + " weight:" + i.weight);
        }
    }

}