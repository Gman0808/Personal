
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UI.CoroutineTween;

public class InventorySlot : MonoBehaviour {
    Item item;
    public Image icon;
    public Text text;

    public void AddItem(Item newItem)
    {
        Debug.Log("adding item to slot");
        item = newItem;

       icon.sprite = item.icon;
        icon.enabled = true;

        text.text = item.name;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        text.text = " ";
    }
}
