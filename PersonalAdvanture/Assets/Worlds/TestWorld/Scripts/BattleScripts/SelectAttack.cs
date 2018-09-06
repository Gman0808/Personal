using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectAttack : MonoBehaviour {
    BattleManager bScript;
    RotateBlocks rScript;
    float attackMode;//1 slingshot, 2 Item, 3 Runaway, 4 Punch
    float selectMode; //1 choosing block, 2- choosing from pannel, 3- proforming action
    float delayTimer;
    public float delay;
    public GameObject[] actionPanels; // 0 - runaway, 1- item, 2- slingshot, 3- punch
    public int chosenAction;

    // Use this for initialization
    void Start () {
        rScript = GetComponent<RotateBlocks>();
        bScript = GetComponent<BattleManager>();
        attackMode = rScript.blockStaging;
        selectMode = 1;
        delayTimer = 10;
        actionPanels = GameObject.FindGameObjectsWithTag("ActionPanel");
        foreach(GameObject pan in actionPanels)
        {
            pan.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        delayTimer += 1 * Time.deltaTime;
        if (delayTimer > delay + 1)
            delayTimer = delay + 1;

        if (Input.GetAxis("Interact") != 0 && delayTimer >= delay && selectMode == 1)
        {
            delayTimer = 0;
            selectMode = 2;
            bScript.stages = 2; //now selecting from panel
            checkPannel(rScript.blockStaging);
        }

        //this will be removed soon, for now we skip the selection of attacks from the panels
        if (Input.GetAxis("Interact") != 0 && delayTimer >= delay && selectMode == 2)
        {
            delayTimer = 0;
            selectMode = 3;
            bScript.stages = 3; //now proforming action
            disablePannels();
            bScript.delayTimer = 0;
            //subject to change once variations in options are added (diffrent punch choices or items)
            bScript.attackChoice = chosenAction;
        }
        if (Input.GetAxis("Quit") != 0 && delayTimer >= delay && selectMode == 2)
        {
            delayTimer = 0;
            selectMode = 1;
            bScript.stages = 1; //now selecting from panel
            disablePannels();
        }
    }

    public void checkPannel(int currentBlock)
    {
        chosenAction = currentBlock;

        //Slingshot
        if (currentBlock == 1)
        {
            actionPanels[2].SetActive(true);        
        }

        //Item
        if (currentBlock == 2)
        {
            actionPanels[1].SetActive(true);
        }
        //Runaway
        if (currentBlock == 3)
        {
            actionPanels[0].SetActive(true);
        }
        //punch
        if (currentBlock == 4)
        {
            actionPanels[3].SetActive(true);
        }
    }
    public void disablePannels()
    {
        foreach (GameObject pan in actionPanels)
        {
            pan.SetActive(false);
        }
    }

    ///Much more needed for this method, made for selecting through the panels
    public void selectOption()
    {

        //recives info on what attacks are present

        //displays specfic chocies

        //gives info based on what was chosen


    }
}
