using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class slingAttack : MonoBehaviour {
    public GameObject player;
    battlePlayerMovement moveScript;
    BattleManager bScript;
    public Camera mainCamera;
    battleCamera camScript;
    CharacterController controller;
    float positionSpeed;
    public float launchSpeed;
    float accel;
    public float launchDecrease;
    public int attackStage; // 1- find position, 2- minigame, 3- unleashAttack, 4- return to start position, 5- attack phase over
    public int attackValue;
    float timer;

    //slingAmmo 
  public  GameObject ammo;
    GameObject currentAmmo;
   public int ammoIndex;

    //Mini Game
    GameObject MiniGame;


    //Liner render for paraboloa
    LineRenderer lineRend;
  public  float velocity;
  public  float angle;
  public  int resolution;
  float gravity;
    float radianAngle;
    float finalAngle;
    bool vBool;
    bool aBool;
    // Use this for initialization
    void Start()
    {
        // mainCamera = bScript.mainCamera;
        player = gameObject;
        moveScript = GetComponent<battlePlayerMovement>();
        bScript = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
        controller = GetComponent<CharacterController>();
        attackStage = 1;
        lineRend = GetComponent<LineRenderer>();
        camScript = mainCamera.GetComponent<battleCamera>();

        
 

        positionSpeed = 3;
        vBool = true;aBool = true;
        //using physics gravity
        gravity = Mathf.Abs(Physics2D.gravity.y);

        lineRend.SetPosition(0, transform.position);
        lineRend.SetPosition(1, transform.position);
    }

    //method to render the arc itself
    void renderArc()
    {
        lineRend.positionCount = (resolution + 1);
        lineRend.SetPositions(CalcArcArray());

    }
    //create an array of vector3 positions for the arc
    Vector3[] CalcArcArray()
    {
        Vector3[] arcArray = new Vector3[resolution + 1];
        radianAngle = Mathf.Deg2Rad * angle;
        float maxDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle) / gravity);

        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = CaluculateArcPoint(t, maxDistance);
        }

        return arcArray;
    }

    Vector3 CaluculateArcPoint(float t, float maxDistance)
    {
        float x = t * maxDistance;
        float y = x * Mathf.Tan(radianAngle) - (gravity * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle));
        return new Vector3(x + transform.position.x + 0.1f, y, transform.position.z);
    }


    // Update is called once per frame
    void Update () {
        timer += 1 * Time.deltaTime;
    }

    public int basicAttack()
    {
        //get into position
        if (attackStage == 1)
        {
            findPosition();
            followPlayer();
        }

        //minigame, set parabola
        if (attackStage == 2)
        {
            bScript.freezeEnnemies = true;
            miniGame1();
            followPlayer();
        }

        //create ball
        if (attackStage == 3)
        {
            createBullet();
            attackStage = 4;
            followPlayer();

        }

        //shoot ball
        if (attackStage == 4)
        {
            followBullet();
            fireBullet();

        }


        //zoom out and return to normal
        if (attackStage == 5)
        {
            followPlayer2();
            Destroy(currentAmmo);

        }

        if (attackStage == 6)
            return 1;
        return 0;
    }


    //start by finding a position to attack
    public void findPosition()
    {
        bScript.freezeEnnemies = false;

        Vector3 directionMove = new Vector3(0, moveScript.directionMove.y, Input.GetAxis("Vertical") * positionSpeed);
        controller.Move(directionMove * Time.deltaTime);
        lineRend.SetPosition(0, transform.position);
        lineRend.SetPosition(1, transform.position);
        if (Input.GetAxis("Interact") != 0)
        {
            attackStage = 2;
            timer = 0;
        }

    }

    public void miniGame1()
    {
        renderArc();

        velocity += 0.025f;
        angle -= .15f;
      //  velocity += (Input.GetAxis("Horizontal") * 0.03f);
      //  angle -= (Input.GetAxis("Horizontal") * 0.03f);

       

        /*
        angle += (Input.GetAxis("Vertical") * 0.4f);

        if(angle >= 55f)
        {
            angle = 55;
        }
        if (angle <= 35f)
        {
            angle = 35;
        }
        */
        if ((Input.GetAxis("Interact") != 0 && timer > 20 * Time.deltaTime) || velocity >= 8f)

        {
            finalAngle = angle;
            attackStage = 3;
        }
    }

   public void createBullet()
    {
        ammo.transform.position = lineRend.GetPosition(0);
        currentAmmo = Instantiate(ammo);
      
        ammoIndex = 0;
    }

    public void fireBullet()
    {
        bulletCollide cScript = currentAmmo.GetComponent<bulletCollide>();
        cScript.attackValue = attackValue;
        
        if (currentAmmo != null)
        {
            if (ammoIndex >= lineRend.positionCount)
            {
               
                attackStage = 5;
            }
            else
            {
                Vector3 newPos = Vector3.Lerp(currentAmmo.transform.position, lineRend.GetPosition(ammoIndex), 15 *Time.deltaTime);
                if (currentAmmo.transform.position.x >= lineRend.GetPosition(ammoIndex).x - 0.2f && currentAmmo.transform.position.y >= lineRend.GetPosition(ammoIndex).y - 0.2f)
                {
                    ammoIndex++;
                }
                Rigidbody ridgid = currentAmmo.GetComponent<Rigidbody>();
                ridgid.MovePosition(newPos);
              
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

    public void followBullet() // follow player during punch phase
    { //player z - -11.88
        //camera z - 17
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.z = currentAmmo.transform.position.z - 2.5f;
        cameraPos.y = 1.12f;
        cameraPos.x = currentAmmo.transform.position.x;
        camScript.cameraMoveTo(cameraPos);
    }

    public void followPlayer2() // follow player during return phase
    { //player z - -11.88
        //camera z - 17
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.z = player.transform.position.z - 4f;
        cameraPos.x = player.transform.position.x;
        cameraPos.y = 1.64f;
        camScript.cameraMoveTo(cameraPos);
    }

}
