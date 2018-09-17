
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UI.CoroutineTween;

public class InventorySlot : MonoBehaviour {
    Item item;
    public Image icon;
    public Text text;
    public bool isHeal;
    public bool isAttack;

    public void AddItem(Item newItem)
    {

        item = newItem;

       icon.sprite = item.icon;
        icon.enabled = true;

        text.text = item.name;

        isHeal = item.isHeal;
        isAttack = item.isAttack;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        text.text = " ";
    }



    //For Battles, need to make one for overworld
    public void SelectItem()
    {
        if (isHeal)
        {
            SelectAttack.instance.selectOption();
            Debug.Log("Clicked and Ready");
            HealItem healScript = GameObject.Find("Items").GetComponent<HealItem>();
            healScript.icon = icon.sprite;
            healScript.hpHeal = item.healAmount;
            healScript.staminaHeal = item.staminaAmount;
            healScript.hungery = item.hungery;
            healScript.thirsty = item.thirsty;

            BattleManager.instance.itemScript = healScript;
        }
    }

}
