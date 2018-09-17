using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {


    public static BattleManager instance;

    public GameObject player;
    public GameObject bPlayer;
    public int stages;   //1- choose block, 2- select action from panel, 3- Proform action
    public int attackChoice; // 1- slingshot, 2- item, 3- runaway, 4- punch
    public bool freezeEnnemies; // freezes ennemies when player attacks
    public Camera mainCamera;
    battleCamera camScript;
    public float delayTimer;



    //enemies and obsticals
    public GameObject[] enemies;
    public GameObject[] obsticals;

    //attack scripts
    punchAttack punchScript;
    slingAttack slingScript;
   public BaseItem itemScript;

    // Use this for initialization

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (player.activeSelf)
                player.SetActive(false);
        }

        #region SingleTon
        if (instance != null)
        {
            Debug.Log("Warning multiple battleMangers found");
            return;
        }
        instance = this;
        #endregion

    }
    void Start () {
        bPlayer = GameObject.Find("BattlePlayer");
        camScript = mainCamera.GetComponent<battleCamera>();
        stages = 1;
        delayTimer = 10;
        punchScript = bPlayer.GetComponent<punchAttack>();
        slingScript = bPlayer.GetComponent<slingAttack>();
        enemies = GameObject.FindGameObjectsWithTag("Enemies");
        obsticals = GameObject.FindGameObjectsWithTag("Obsticals");
        freezeEnnemies = false;
        Inventory.instance.UpdateUI();
    }
	
	// Update is called once per frame
	void Update () {
        if(stages == 1)
        {
       
            camScript.cameraMoveTo(new Vector3(-6.2f, 1.64f, -16.55f));
        }
        if(stages == 2)
        {
            delayTimer = 0;
           
        }
        delayTimer += 1 * Time.deltaTime;
          
        if (stages == 3)
        {     
            if (attackChoice == 4 && delayTimer >= 20 * Time.deltaTime)
            {
             stages += punchScript.basicAttack();
            }
            if (attackChoice == 1 && delayTimer >= 20 * Time.deltaTime)
            {
                stages += slingScript.basicAttack();
            }
            if (attackChoice == 2 && delayTimer >= 20 * Time.deltaTime)
            {
                stages += itemScript.PreformItem();
            }
            if (stages == 4)
                delayTimer = 0;

        }
        if(stages == 4 && delayTimer >= 20 * Time.deltaTime) // killing all destroyed ennemies
        {
            foreach(GameObject en in enemies)
            {
                if(en != null)
                {
                    if(en.GetComponent<EnInfo>().health < 1)
                    {
                        en.GetComponent<EnInfo>().targetDead();
                    }
                }
            }

        }
	}

   
}
