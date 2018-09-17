using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour {

    public GameObject player;
    battlePlayerMovement moveScript;
    BattleManager bScript;

    CharacterController controller;


    // Use this for initialization
    void Start () {
        player = GameObject.Find("BattlePlayer");
        moveScript = player.GetComponent<battlePlayerMovement>();
       
        bScript = BattleManager.instance;

    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public virtual int PreformItem()
    {
        return 0;
    }
}
