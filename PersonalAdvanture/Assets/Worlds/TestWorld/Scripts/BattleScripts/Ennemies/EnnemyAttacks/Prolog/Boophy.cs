using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boophy : EnBasicAttack {
    public float speed1;
    public float speed2;
    int attackStage = 0;
    float timeChange = 0;
    float timeEnd = 0;
    bool moveUp = true;

    float transitionTime = 0; //time for transitioning to new phase

    float transitionEnd = 0;

    public override void prepare()
    {
        moveUp = true;
        attackStage = 0;
        timeChange = 0;
        timeEnd = Random.Range(50f, 700f) * Time.deltaTime;
        transitionEnd = Random.Range(50, 700f) * Time.deltaTime;

        int rgen = Random.Range(1, 3);
        if(rgen == 2)
            moveUp = false;
    }

    public void boundCheck()
    {
        if (transform.position.z > -6.75)
        {
            moveUp = !moveUp;
            transform.position = new Vector3(transform.position.x, transform.position.y, -6.77f);
        }
           
        if (transform.position.z < -13.79)
        {
            moveUp = !moveUp;
            transform.position = new Vector3(transform.position.x, transform.position.y, -13.78f);
        }
           

     
    }

    public override bool attack1()
    {
        boundCheck();
        timeChange += 1 * Time.deltaTime;
        transitionTime += 1 * Time.deltaTime;
        if (timeChange >= timeEnd)
        {
            timeEnd = Random.Range(100f, 200f) * Time.deltaTime;
            moveUp = !moveUp;
            timeChange = 0;
        }
       if(attackStage == 0)
        {
            if (moveUp)
                transform.position += new Vector3(0, 0, speed1) * Time.deltaTime;
            else
                transform.position += new Vector3(0, 0, -speed1) * Time.deltaTime;

            if (transitionTime > transitionEnd)
                attackStage++;
        }
       if(attackStage == 1)
        {
            transitionTime = 0;
            float zSpeed = Random.Range(0.5f, 3f);
            float xSpeed = Random.Range(1.5f, speed2);

            Vector3 distance = (player.transform.position - transform.position).normalized;
            transform.position += new Vector3(-xSpeed, 0, distance.z * zSpeed) * Time.deltaTime;

           

            if (transform.position.x < -15)
                attackStage++;
        }
        if (attackStage == 2)
        {
            if (moveUp)
                transform.position += new Vector3(0, 0, speed1) * Time.deltaTime;
            else
                transform.position += new Vector3(0, 0, -speed1) * Time.deltaTime;

            if (transitionTime > transitionEnd)
                attackStage++;

        }
        if (attackStage == 3)
        {
            transitionTime = 0;
            float zSpeed = Random.Range(0.5f, 3f);
            float xSpeed = Random.Range(1.5f, speed2);

            Vector3 distance = (player.transform.position - transform.position).normalized;
            transform.position += new Vector3(xSpeed, 0, distance.z * zSpeed) * Time.deltaTime;



            if (transform.position.x > -9)
                attackStage = 0;
        }

        return false;
    }
    
}
