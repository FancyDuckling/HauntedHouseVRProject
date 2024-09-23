using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpFlashlight : MonoBehaviour
{
    GameObject startTrans;  //start transformation/where the flashlight spawns
    Transform controller; //when connected with controller
    static public bool pickedUp = false;

    public InventoryVR inventory; // Reference to the InventoryVR script


    // Start is called before the first frame update
    void Start()
    {
        startTrans = new GameObject();

        startTrans.transform.position = transform.position;
        startTrans.transform.rotation = transform.rotation;


    }

    // Update is called once per frame
    void Update()
    {
        if (pickedUp == true)
        {
            transform.position = controller.transform.position;
            transform.rotation = controller.transform.rotation;
        }
        else
        {
            transform.position = startTrans.transform.position;
            transform.rotation = startTrans.transform.rotation;
        }

    }

   /* private void OnTriggerEnter(Collider other)
    {
        pickedUp = true;
        controller = other.gameObject.transform;
    }*/


    // Method to be called when picked up by flashlight
    public void PickUp()
    {
        if (inventory != null)
        {
            // Try to add the item to the inventory
            bool addedToInventory = inventory.AddItemToInventory(this.gameObject);

            if (addedToInventory)
            {
                // Successfully added to inventory, deactivate item
                gameObject.SetActive(false);
                pickedUp = true;
            }
        }
    }
}
