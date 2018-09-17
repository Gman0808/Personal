using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : BaseItem {

    public Camera mainCamera;
    public battleCamera camScript;

    //info vars
    public Sprite icon;
    public int hpHeal = 0;
    public int staminaHeal = 0;
    // may add var for specfic buffs

    //status effects
    public bool hungery = false;
    public bool thirsty = false;


    //controling the states
    float timer; // timer
    int stages;

    // Use this for initialization
    void Start () {
        stages = 1;
        timer = 0;
        camScript = mainCamera.GetComponent<battleCamera>();
        player = GameObject.Find("BattlePlayer");
    }
	
	// Update is called once per frame
	void Update () {
       
    }

    public override int PreformItem()
    {
        GameObject itemUsed = GameObject.Find("ItemUsed");
        Sprite usedSprite = itemUsed.GetComponent<SpriteRenderer>().sprite;

        timer += 1 * Time.deltaTime;


        //zoom in on player
        if (stages == 1)
      {
            zoomPlayer();
            if (timer > 1)
            {
                stages = 2;
                timer = 0;
            }
               
      }

      // reveals item
      if (stages == 2)
      {
            itemUsed.GetComponent<SpriteRenderer>().sprite = icon;
           

            if (itemUsed.GetComponent<SpriteRenderer>().sprite == icon)
            {
                Debug.Log("hellz yeah");
            
            }
        }
    
      //animates amber eating or placing item onto herself
      if (stages == 3)
      {

      }

     //amber is happy :D.  Show health, stamina, buffs, statuse disapear
      if(stages == 4)
      {

      }

      //zooms out to original position, next phase starts
      if(stages == 5)
        {

        }

        return 0;
  
    }

    public void zoomPlayer() // follow player during punch phase
    { //player z - -11.88
      //camera z - 17
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.z = player.transform.position.z - 2.5f;
        cameraPos.y = 1.12f;
        cameraPos.x = player.transform.position.x;
        camScript.cameraMoveTo(cameraPos);

       
       
    }
}
