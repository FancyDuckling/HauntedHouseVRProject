using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryVR : MonoBehaviour
{
    public GameObject Inventory;
    public GameObject Anchor;
    bool UIActive;

    public Slot[] slots; // Array of slots in the inventory



    private void Start()
    {
        Inventory.SetActive(false);
        UIActive = false;

        // Initialize slots
        slots = Inventory.GetComponentsInChildren<Slot>();

        

        // Log the number of slots detected to confirm population
        Debug.Log("Slots found in inventory: " + slots.Length);
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            UIActive = !UIActive;
            Inventory.SetActive(UIActive);
        }
        if (UIActive)
        {
            Inventory.transform.position = Anchor.transform.position;
            Inventory.transform.eulerAngles = new Vector3(Anchor.transform.eulerAngles.x + 15, Anchor.transform.eulerAngles.y, 0);
        }
    }

    // Method to add an item to the inventory
    public bool AddItemToInventory(GameObject item)
    {
        foreach (Slot slot in slots)
        {
            if (slot.ItemInSlot == null) // Check if slot is empty
            {
                slot.InsertItem(item);
                return true; // Successfully added
            }
        }
        Debug.Log("Inventory is full!");
        return false; // Failed to add (inventory full)
    }
}


