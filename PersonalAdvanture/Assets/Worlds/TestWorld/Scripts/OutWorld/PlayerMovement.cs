using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
    public float speed;
    public float jumpForce;
    public CharacterController controller;
    public Vector3 directionMove;
private Vector3 prevMove;
    public float gravScale;
    public int floatJump;
    float timer;
    SpriteRenderer rend;
    public bool control;
    public Animator animator;
    


    //Ui stuff
    public GameObject MenuCanvas;
    public bool pause;
    int pauseInt;

    public bool faceRight;
    // Use this for initialization
    void Start () {
      
        controller = GetComponent<CharacterController>();
        rend = GetComponent<SpriteRenderer>();
        floatJump = 0;
        timer = 0;
        DontDestroyOnLoad(this);
        control = true;
        pause = false;
       MenuCanvas = GameObject.FindGameObjectWithTag("MenuUi");
        MenuCanvas.SetActive(false);
    

    }
	
	// Update is called once per frame
	void Update () {

        directionMove = new Vector3(Input.GetAxis("Horizontal") * speed, directionMove.y, Input.GetAxis("Vertical") * speed);

        //Instead of keeping player, make a gameobject decide players position
        if (Input.GetAxis("Horizontal") < 0 && faceRight)
        {
            rend.flipX = true;
            faceRight = false;
        }
        if (Input.GetAxis("Horizontal") > 0 && !faceRight)
        {
            rend.flipX = false;
            faceRight =true;
        }

      

        if (controller.isGrounded)
        {
            timer = 0;
            floatJump = 0;
            if (Input.GetButtonDown("Jump"))
            {
                directionMove.y = jumpForce;
                floatJump = 1;
            }
        }
        if (!controller.isGrounded)
        {
            directionMove.y = directionMove.y + (Physics.gravity.y * gravScale * Time.deltaTime);
            if(Input.GetButtonDown("Jump") && floatJump == 1)
            {
                floatJump = 2;               

            }

        }

        if(floatJump == 2)
        {
            directionMove.y = 1f;
           timer += 1 * Time.deltaTime;
            directionMove = new Vector3(Input.GetAxis("Horizontal") * speed/2, directionMove.y, Input.GetAxis("Vertical") * speed/2);
            if (timer > 40 * Time.deltaTime)
            {
                floatJump = 3;
            }

        }


     

        //animation checks
        animator.SetFloat("Speed", Mathf.Abs(directionMove.x) + Mathf.Abs(directionMove.z));
        animator.SetInteger("Jump", floatJump);
        animator.SetBool("Grounded", controller.isGrounded);
        animator.SetFloat("Fall", directionMove.y);

       

        //menu Ui 
        if (Input.GetButtonDown("Pause") && !pause )
        {
            pause = true;
   
        }
        else if(Input.GetButtonDown("Pause")  && pause)
        {
           pause = false;

        }

  
     
        if (!pause)
        {
            controller.Move(directionMove * Time.deltaTime);
            prevMove = directionMove;
            MenuCanvas.SetActive(false);
        }
        else
        {
            MenuCanvas.SetActive(true);
        }


    

    }
 
}
