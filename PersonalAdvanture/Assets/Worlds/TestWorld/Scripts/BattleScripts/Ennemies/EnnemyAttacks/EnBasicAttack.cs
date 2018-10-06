using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnBasicAttack : MonoBehaviour {

    CharacterController controller;
    Collider collide;
    BasicEnnemy1 moveScript; // need to change to make movement use inheritence
  public  GameObject player;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        moveScript = GetComponent<BasicEnnemy1>();
        collide = GetComponent<BoxCollider>();
        player = GameObject.Find("BattlePlayer");
    }

    public virtual bool attack1()
    {
        return true;
    }

    public virtual bool attack2()
    {
        return true;
    }

    //normally reseved for boss bois
    public virtual bool attack3()
    {
        return true;
    }

    public virtual bool attack4()
    {
        return true;
    }

    //method for spawning baddies next to center, and then to walk into the center. Also where they gen info for attacks.
    //z -7.3 - -13.3
    public virtual void prepare()
    {
        moveScript.attackOn = true;
        Vector3 startPos = new Vector3(-8, 0.25f, Random.Range(-13.3f, -7.3f));

        transform.position = startPos;
      
    }

    public virtual bool reposition()
    {
      
        if (transform.position.x <= -8.93)
        {
            return true;
        }
        else
        {
            transform.position += new Vector3(-1.5f, 0, 0) * Time.deltaTime;
            return false;
        }
         
    }
    public void returnPos()
    {
        GetComponent<BasicEnnemy1>().returnPos();
    }

  

}
