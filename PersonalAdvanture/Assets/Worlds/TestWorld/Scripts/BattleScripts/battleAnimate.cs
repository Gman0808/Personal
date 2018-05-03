using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleAnimate : MonoBehaviour {

    //attributes
    SpriteRenderer Rend; //render
    public Sprite[] walk; // holds each sprite
    public Sprite[] idle; // holds each sprite
    public Sprite[] jumpUp;
    public Sprite[] fallNormal;
    public Sprite[] floatUp;
    public Sprite[] fallFloat;

    public Sprite[] chargePunch;
    public Sprite[] punchRelease;
    public Sprite[] launchPunch;
   public  bool finishedPunch;
 
   public int select;

    Sprite[] animations; // holds each sprite
    float Timer; // timer to change sprite
    public int AniTime; //specfic timer that controls frames
   float increase;


    // Use this for initialization
    void Start()
    {
        select = 0;
        Timer = 0;
        AniTime = 0;
        Rend = (SpriteRenderer)GetComponent("SpriteRenderer");
    }

    // Update is called once per frame
    void Update()
    {
        finishedPunch = false;
        animations = idle;
        increase = 0.2f;
        if (select == 1)
        {
            increase = 0.1f;
               animations = chargePunch;
        }
        if (select == 2)
        {
            increase = 0.25f;
            animations = punchRelease;
        }
        if (select == 3)
        {
            animations = launchPunch;
        }


        Timer += increase;
        AniTime = (int)Mathf.Round(Timer);

        if(select != 2)
        {
            if (AniTime >= animations.Length)
            {
                Timer = 0;
            }
        }
        else
        {
            if (AniTime >= animations.Length)
            {
                Timer = punchRelease.Length;
                finishedPunch = true;
            }
           
        }
      

        if (AniTime < animations.Length)
        {
            Rend.sprite = animations[AniTime];
        }
    }


}
