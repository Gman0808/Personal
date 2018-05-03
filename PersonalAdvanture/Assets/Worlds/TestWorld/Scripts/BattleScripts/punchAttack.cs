using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class punchAttack : MonoBehaviour {
    public GameObject player;
    battlePlayerMovement moveScript;
   BattleManager bScript;
  public Camera mainCamera;
    battleCamera camScript;
   CharacterController controller;
 float positionSpeed;
    public float launchSpeed;
  public  float accel;
    public float launchDecrease;
    public int attackStage; // 1- find position, 2- minigame, 3- unleashAttack, 4- return to start position, 5- attack phase over
    public int attackValue;
    float timer;

    //Mini Game
    GameObject MiniGame;
  public GameObject PunchDial;
  public GameObject[] PunchNodes;
    Vector3 dialStartPos;
    public Sprite failedNode;
    public Sprite passedNode;
    public Sprite activeNode;

    battleAnimate animateScript;


    // Use this for initialization
    void Start () {
       // mainCamera = bScript.mainCamera;
        player = gameObject;
        moveScript = GetComponent<battlePlayerMovement>();  
        bScript = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
        controller = moveScript.controller;
        attackStage = 1;
        MiniGame = GameObject.FindGameObjectWithTag("PunchMiniGame");
        PunchDial = GameObject.FindGameObjectWithTag("PunchDial");
        PunchNodes = GameObject.FindGameObjectsWithTag("PunchNodes");
        MiniGame.SetActive(false);
        dialStartPos = PunchDial.transform.position;

        positionSpeed = 3;

        camScript = mainCamera.GetComponent<battleCamera>();
        setUpNodes();

        animateScript = player.GetComponent<battleAnimate>();
    }
	
	// Update is called once per frame
	void Update () {
        timer += 1 * Time.deltaTime;
    
	}


    public int basicAttack()
    {
        if (attackStage == 1)
        {
            animateScript.select = 0;
            findPosition();
            followPlayer();
            timer = 0;

        }
           
        if (attackStage == 2)
        {

            animateScript.select = 1;

            followPlayer2();
           miniGame();
        }
        if (attackStage == 3)
        {

            animateScript.select = 2;
            if (animateScript.finishedPunch)
            {
                attackStage = 4;
            }
            followPlayer2();
      
        }

        if (attackStage == 4)
        {
            animateScript.select = 3;
            followPlayer2();
            setUpNodes();
            basicPunch();
        }
       
        if (attackStage == 5)
        {
            animateScript.select = 0;
            followPlayer3();
            returnStart();
      
        }
            
        if (attackStage == 6)
            return 1;
        return 0;
    }

    //start by finding a position to attack
    public void findPosition()
    {
        bScript.freezeEnnemies = false;
        
        Vector3  directionMove = new Vector3(0, moveScript.directionMove.y, Input.GetAxis("Vertical") * positionSpeed);
        controller.Move(directionMove * Time.deltaTime);
        if (Input.GetAxis("Interact") != 0)
        {
            attackStage = 2;
        }
    
    }

    //Mini game methoid
    public bool checkDialTouching()
    {
        bool touching = false;
        BoxCollider2D dialCollide = PunchDial.GetComponent<BoxCollider2D>();
        foreach (GameObject node in PunchNodes)
        {
            BoxCollider2D nodeCollider = node.GetComponent<BoxCollider2D>();

            if (dialCollide.IsTouching(nodeCollider))
            {
                touching = true;
            }
        }
        return touching;
    }


    //Mini game method
    public void setUpNodes()
    {
        float[] setUp1 = { 710.2f, 783.5f, 633.2f};
       float[] setUp2 = { 677.5f, 820f, 582.1f };
        float[] setUp3 = { 688.8f, 858f, 617.7f};

        int rgen = Random.Range(0, 3);
        if (rgen == 0)
        {
            for (int i = 0; i < PunchNodes.Length; i++)
            {
                PunchNodes[i].transform.position = new Vector3(setUp1[i], PunchNodes[i].transform.position.y, PunchNodes[i].transform.position.z);
            }
        }
        else if(rgen == 1)
        {
            for (int i = 0; i < PunchNodes.Length; i++)
            {
                PunchNodes[i].transform.position = new Vector3(setUp2[i], PunchNodes[i].transform.position.y, PunchNodes[i].transform.position.z);
            }
        }
        else
        {
            for (int i = 0; i < PunchNodes.Length; i++)
            {
                PunchNodes[i].transform.position = new Vector3(setUp3[i], PunchNodes[i].transform.position.y, PunchNodes[i].transform.position.z);
            }
        }
    }

    public void miniGame()
    {
 
        bool end = false;
        MiniGame.SetActive(true);
        bScript.freezeEnnemies = true;
        if (timer > 25 * Time.deltaTime)
        {

            Vector3 DialMove = PunchDial.transform.position;

            BoxCollider2D dialCollide = PunchDial.GetComponent<BoxCollider2D>();
            foreach (GameObject node in PunchNodes)
            {

                BoxCollider2D nodeCollider = node.GetComponent<BoxCollider2D>();

                if (dialCollide.IsTouching(nodeCollider))
                {
                    if (node.GetComponent<Image>().sprite != passedNode)
                    {
                        node.GetComponent<Image>().sprite = activeNode;
                    }

                    if (Input.GetAxis("Interact") != 0)
                    {

                        node.GetComponent<Image>().sprite = passedNode;
                    }
                }
                else
                {

                    if (node.GetComponent<Image>().sprite == activeNode)
                    {
                        node.GetComponent<Image>().sprite = failedNode;
                    }
                }



            }

            if (PunchDial.transform.position.x < 850.7)
            {
                DialMove.x += (170 * Time.deltaTime);
                PunchDial.transform.position = DialMove;
            }
            else
            {
                end = true;

            }

            if (!checkDialTouching() && Input.GetAxis("Interact") != 0)
            {
                end = true;
              
            }

            if (end)
            {
                accel = launchSpeed;
                foreach (GameObject node in PunchNodes)
                {
                    if (node.GetComponent<Image>().sprite != passedNode)
                    {
                        accel -= (launchSpeed / 6);
                    }
                }
               
                attackStage = 3;
            }
        }
     
     
     
      
       


    }
    public void basicPunch()
    {
        MiniGame.SetActive(false);
        PunchDial.transform.position = dialStartPos;
        Vector3 directionMove = new Vector3(accel, 0, 0);
        float constantY = player.transform.position.y;
        if (accel > 0)
        {
            timer = 0;
            controller.Move(directionMove * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, constantY, transform.position.z);
            accel-= launchDecrease;
        }
        else
        {
            if (timer > 10 * Time.deltaTime)
                attackStage = 5;           
        }
    }

    public void returnStart()
    {
        Vector3 directionMove = new Vector3(-4, 0, 0);
        controller.Move(directionMove * Time.deltaTime);
        if (transform.position.x <= -8.88f)
        {
            attackStage = 6;
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Obsticals")
        {
            accel = 0;
        }
        if(other.gameObject.tag == "Enemies")
        {
            EnInfo info = other.gameObject.GetComponent<EnInfo>();
            other.gameObject.GetComponent<EnInfo>().health -= (attackValue - info.defense);
            other.gameObject.GetComponent<EnInfo>().damaged = true;
            Debug.Log("Hit");
            if(info.defense > 0)
            {
                accel = 0;
            }
        }

    }

    ///CAMERA METHODS
    public void followPlayer() // follow player during positiong phase
    { //player z - -11.88
        //camera z - 17
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.z = player.transform.position.z - 4f;
        cameraPos.x = -6.44f;
        cameraPos.y = 1.64f;
     camScript.cameraMoveTo(cameraPos);
    }

    public void followPlayer2() // follow player during punch phase
    { //player z - -11.88
        //camera z - 17
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.z = player.transform.position.z - 2.5f;
        cameraPos.y = 1.12f;
        cameraPos.x = player.transform.position.x;
        camScript.cameraMoveTo(cameraPos);
    }
    public void followPlayer3() // follow player during return phase
    { //player z - -11.88
        //camera z - 17
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.z = player.transform.position.z - 4f;
        cameraPos.x = player.transform.position.x;
        cameraPos.y = 1.64f;
        camScript.cameraMoveTo(cameraPos);
    }
}
