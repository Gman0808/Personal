using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable {
    public Item item;
	public override void Interact()
    {
        base.Interact();
        if(interacted)
        PickUp();
    }
	
   public void PickUp()
    {
   
        if (Inventory.instance.Add(item))
        {
            Debug.Log("Adding " + item.name);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Not enough space to add " + item.name);
        }
      
    }
}
