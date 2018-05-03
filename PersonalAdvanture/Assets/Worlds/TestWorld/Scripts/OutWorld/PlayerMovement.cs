using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool moveing;
  

    public bool faceRight;
    // Use this for initialization
    void Start () {
      
        controller = GetComponent<CharacterController>();
        rend = GetComponent<SpriteRenderer>();
        floatJump = 0;
        timer = 0;
        DontDestroyOnLoad(this);
        control = true;
        moveing = false;
        

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

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            moveing = true;
        }
        else
            moveing = false;


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

        controller.Move(directionMove * Time.deltaTime);
        prevMove = directionMove;
    }
}
