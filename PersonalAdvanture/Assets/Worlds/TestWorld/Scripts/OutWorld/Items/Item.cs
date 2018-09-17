
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName = "Inventory/Item")]
public class Item :ScriptableObject {
  new  public string name = "New Item";
    public Sprite icon = null;
    public bool isHeal = false; //check if its a healing script, status removeal, or buffing script
    public int healAmount = 0;
    public int staminaAmount = 0;

    //status examples
    public bool hungery = false;
    public bool thirsty = false;


    public bool isAttack = false; // check if its an attack script

    //for specfic attack scripts that can't be used for mutlple items. Use so if attack name is equal to somthing it calls a specfic script
    public string attackName = "None";

    


}
