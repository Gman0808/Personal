using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnAnimate : MonoBehaviour {

    //attributes
    SpriteRenderer Rend; //render
    public Sprite[] walk; // holds each sprite
    public Sprite[] idle; // holds each sprite
    public Sprite[] hurt; // holds each sprite
   public  bool damaged;
    Sprite[] animations; // holds each sprite
    public float Timer; // timer to change sprite
   private int AniTime; //specfic timer that controls frames
    public float increase;

    // Use this for initialization
    void Start()
    {
        damaged = false;
       
        Timer = 0;
        AniTime = 0;
        Rend = (SpriteRenderer)GetComponent("SpriteRenderer");
    }

    // Update is called once per frame
    void Update()
    {
        animations = walk;
        if (damaged)
        {
            animations = hurt;
        }
        Timer += increase;
       
       
       

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
