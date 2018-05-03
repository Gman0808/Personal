using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour {
    //Animation class

    //attributes
    SpriteRenderer Rend; //render
    public Sprite[] walk; // holds each sprite
    public Sprite[] idle; // holds each sprite
    public Sprite[] jumpUp;
    public Sprite[] fallNormal;
    public Sprite[] floatUp;
    public Sprite[] fallFloat;
    Sprite[] animations; // holds each sprite
   float Timer; // timer to change sprite
    public int AniTime; //specfic timer that controls frames
    public float increase;
    public float idIncrease;
    public float incr;

    // Use this for initialization
    void Start()
    {
        Timer = 0;
        AniTime = 0;
        Rend = (SpriteRenderer)GetComponent("SpriteRenderer");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement pScript = (PlayerMovement)GetComponent("PlayerMovement");
        Timer += incr;
        incr = idIncrease;
        animations = idle;
        if (pScript.moveing && pScript.controller.isGrounded)
        {
            incr = increase;
            animations = walk;
        }
        if (pScript.floatJump == 1 &&  pScript.directionMove.y > 0)
        {
            incr = increase;
            animations = jumpUp;
        }
        if (pScript.floatJump == 1 && pScript.directionMove.y <= 0)
        {
            incr = increase;
            animations = fallNormal;
        }
        if (pScript.floatJump == 2)
        {
            incr = increase;
            animations = floatUp;
        }
        if (pScript.floatJump == 3)
        {
            incr = increase;
            animations = fallFloat;
        }

        AniTime = (int)Mathf.Round(Timer);
    
        if (AniTime >= animations.Length)
        {
            Timer = 0;
        }

        if (AniTime < animations.Length)
        {
            Rend.sprite = animations[AniTime];
        }
    }


}
