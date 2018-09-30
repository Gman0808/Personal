using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battlePlayerMovement : MonoBehaviour {
    public float speed;
    public float jumpForce;
    public CharacterController controller;
    public Vector3 directionMove;
    private Vector3 prevMove;
    public float gravScale;
    public int floatJump;
    float timer;
    SpriteRenderer rend;

    public static battlePlayerMovement instance;

    #region SingleTon
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Warning multiple ennemyeMangers found");
            return;
        }
        instance = this;


    }
    #endregion

    //  controling bools
    public bool floatJumpOn;

    public bool faceRight;
    // Use this for initialization
    void Start()
    {

        controller = GetComponent<CharacterController>();
        rend = GetComponent<SpriteRenderer>();
        floatJump = 0;
        timer = 0;

        //controling bools

        floatJumpOn = false;

    }

    public void flipSprite()
    {
        //Instead of keeping player, make a gameobject decide players position
        if (Input.GetAxis("Horizontal") < 0 && faceRight)
        {
            rend.flipX = true;
            faceRight = false;
        }
        if (Input.GetAxis("Horizontal") > 0 && !faceRight)
        {
            rend.flipX = false;
            faceRight = true;
        }
    }
    // Update is called once per frame
    void Update()
    {


    }

    public void freeMovement()
    {
        directionMove = new Vector3(Input.GetAxis("Horizontal") * speed, directionMove.y, Input.GetAxis("Vertical") * speed);

        flipSprite();

        if (controller.isGrounded)
        {
            floatJumpOn = true;
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
            if (Input.GetButtonDown("Jump") && floatJump == 1 && floatJumpOn)
            {
                floatJump = 2;
                floatJumpOn = false;
            }

        }

        if (floatJump == 2)
        {
            directionMove.y = 1f;
            timer += 1 * Time.deltaTime;
            directionMove = new Vector3(Input.GetAxis("Horizontal") * speed / 2, directionMove.y, Input.GetAxis("Vertical") * speed / 2);
            if (timer > 40 * Time.deltaTime)
            {
                floatJump = 3;
            }

        }
       
        controller.Move(directionMove * Time.deltaTime);
        prevMove = directionMove; 

    }
}


