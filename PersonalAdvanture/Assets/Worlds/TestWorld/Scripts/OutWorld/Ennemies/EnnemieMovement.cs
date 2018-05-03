using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnnemieMovement : MonoBehaviour {

    public CharacterController controller;
    public Vector3 directionMove;
    public float gravScale;
    public float speed;
    SpriteRenderer rend;
    GameObject player;
   public Vector3 velocity;
    public Object scene;
     string sceneName;
    public float chase;


    //attributes for wander
    public float Cdistance;
    public float Cradius;
    public float WanderAngle;
    public float AngleChange;
    public Vector3 WanderForce;
    public Vector3 displacement;
    public Vector3 circleCenter;
   public float  wanderTimer;
    public float wSpeed;


    public GameObject SurpriseMark;
    GameObject inSurpriseMark;
  public  float surpriseTimer;
    int starttled;
    public float jumpForce;
    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
        rend = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        wanderTimer = 30000;
        surpriseTimer = 110;
        starttled = 0;
        chase = 0;
    }
	
	// Update is called once per frame
	void Update () {
       


        surpriseTimer += 1 * Time.deltaTime;
        if (surpriseTimer > 10 * Time.deltaTime)
        {
            if (inSurpriseMark != null)
                Destroy(inSurpriseMark);
        }
        if (starttled != 0)
        {
            surprisedFreeze();
        }
        if(starttled == 0)
        {
            checkPlayerY();
            if (chase > 10 )
            {
                Wander();
            }
           if(chase < 10 )
            {
                chasePlayer();
            }
        }
        chase += 1;
        if (chase > 10)
            chase = 11;

    }
    public void Wander()
    {
        wanderTimer+= 1 * Time.deltaTime;

        if (wanderTimer < 500 * Time.deltaTime)
        {
            circleCenter = velocity;
            circleCenter.Normalize();
            circleCenter *= Cdistance;

            //calculate displacement force
            displacement = new Vector3(0, -1);
            displacement *= Cradius;


           

            if (WanderAngle < -90 || WanderAngle > 90)
            {
                WanderAngle = 0;
            }

            //randomly change vector direction
            float length = displacement.magnitude;
            displacement.x = Mathf.Cos(WanderAngle) * length;
            displacement.z = Mathf.Sin(WanderAngle) * length;



            WanderForce = circleCenter + displacement - velocity;

            WanderForce.Normalize();

        }
        else
        {
            WanderForce = Vector3.zero;
            velocity = Vector3.zero;
        }

        if(wanderTimer > 200 * Time.deltaTime)
        {
            WanderAngle += Random.Range(-1f, 2f) * AngleChange - AngleChange * .5f;
            wanderTimer = 0;
        }



        directionMove = WanderForce;
        


        if (!controller.isGrounded)
        {
            directionMove.y = directionMove.y + (Physics.gravity.y * gravScale * Time.deltaTime);

        }

            // Add accel to velocity
            velocity += directionMove;

            velocity = Vector3.ClampMagnitude(velocity, wSpeed * 2);

            controller.Move(velocity * Time.deltaTime);
     
    }
    private void OnTriggerEnter(Collider other)
    {


        if (player.GetComponent<Collider>() == other && chase > 10  && checkPlayerY())
        {
            surpriseTimer = 0;
            starttled = 1;
            SurpriseMark.transform.position = new Vector3(transform.position.x + 0.1f,transform.position.y + 0.7f, transform.position.z - 0.1f);
            inSurpriseMark = Instantiate(SurpriseMark);
    
        }
    }


        private void OnTriggerStay(Collider other)
         {
      
        if (player.GetComponent<Collider>() == other && checkPlayerY())
        {
            chase = 0;
        }
       
        
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider == player.GetComponent<Collider>())
        {
       
            sceneName = scene.name;
            //Loading in the next scene
            SceneManager.LoadScene(sceneName);
            //Resetting the position of the eplayer
        }
    }
    public void chasePlayer()
    {

        float xPos = 0; float zPos = 0;
        if (player.transform.position.x < transform.position.x - .1)
            xPos = -1;
        if (player.transform.position.x > transform.position.x + .1)
            xPos = 1;
        if (player.transform.position.z < transform.position.z - .1)
            zPos = -1;
        if (player.transform.position.z > transform.position.z + .1)
            zPos = 1;

     
         directionMove = new Vector3(xPos * speed, directionMove.y, zPos * speed);

       


        if (!controller.isGrounded)
        {
            directionMove.y = directionMove.y + (Physics.gravity.y * gravScale * Time.deltaTime);

        }
            controller.Move(directionMove * Time.deltaTime);
        
       
    }

    public void surprisedFreeze()
    {
        if (!controller.isGrounded)
        {
            directionMove.y = directionMove.y + (Physics.gravity.y * gravScale * Time.deltaTime);

        }
        if (controller.isGrounded && starttled == 2)
        {
            starttled = 0;
        }
        if (controller.isGrounded && starttled == 1)
        {
            directionMove = new Vector3(0, jumpForce, 0);
            starttled = 2;
        }
        

        
        controller.Move(directionMove * Time.deltaTime);
    }

    public bool checkPlayerY()
    {
        PlayerMovement pScript = player.GetComponent<PlayerMovement>();

        if (!pScript.controller.isGrounded)
        {
            Debug.Log("bloop");
            return true;
        }

        if (player.transform.position.y < transform.position.y + 0.4f && player.transform.position.y > transform.position.y - 0.4f) 
        {
            return true;
        }
        else
        {
            if (pScript.controller.isGrounded)
            {
                chase = 11;
            }
            return false;
        }
    }

}

