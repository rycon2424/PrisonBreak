using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Dictionary<string, Pickup> objectsInInventory = new Dictionary<string, Pickup>();

    Ray myRay;
    RaycastHit hit;
    [Header("RaycastRange")]
    public int range = 3;

    [Header("Inventory")]
    public int inventorySlot;
    public bool cursorLocked;
    public GameObject dropButton;
    public Text currentWeight;
    public Text maxWeight;
    public GameObject inventoryObject;
    public Inventory inventoryRef;
    public GameObject[] items = new GameObject[10];
    public Text[] itemNames = new Text[10];

    #region instance

    public static PlayerController Instance;

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
    #endregion

    void Update()
    {
        if (Input.GetButtonDown("Action"))
        {
            Interact();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            cursorLocked = !cursorLocked;
            OpenInventory();
            UpdateInventory();
        }
        CheckSelectedItem();
        CursorLock();
    }
    
    void Interact()
    {
        myRay = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * range);
        if (Physics.Raycast(myRay, out hit, range))
        {
            Interactable i = hit.collider.gameObject.GetComponent<Interactable>();
            if (i != null)
            {
                i.Action();
                UpdateInventory();
            }
        }
    }

    public void UpdateInventory()
    {
        inventorySlot = -1;
        for (int i = 0; i < inventoryRef._inventoryList.Count; i++)
        {
            items[i].SetActive(true);
            itemNames[i].text = inventoryRef._inventoryList[i].itemName.ToString();
        }
        items[inventoryRef._inventoryList.Count].SetActive(false);
        currentWeight.text = inventoryRef.totalWeight.ToString();
        maxWeight.text = inventoryRef.weightCap.ToString();
    }

    void OpenInventory()
    {
        if (inventoryObject.activeSelf == true)
        {
            inventoryObject.SetActive(false);
            return;
        }
        inventoryRef.PrintToConsole();
        inventoryObject.SetActive(true);
    }

    void CursorLock()
    {
        if (cursorLocked && Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!cursorLocked && Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void CheckSelectedItem()
    {
        if (inventorySlot >= 0 )
        {
            dropButton.SetActive(true);
        }
        else
        {
            dropButton.SetActive(false);
        }
    }

    public void RemoveSelectedItem()
    {
        Pickup pickup = objectsInInventory[inventoryRef._inventoryList[inventorySlot].itemName];
        pickup.transform.position = Camera.main.transform.position/* + new Vector3(0, 0, 0f)*/;
        pickup.gameObject.SetActive(true);
        inventoryRef.RemoveItem(inventoryRef._inventoryList[inventorySlot]);
        UpdateInventory();
        inventorySlot = -1;
    }
}