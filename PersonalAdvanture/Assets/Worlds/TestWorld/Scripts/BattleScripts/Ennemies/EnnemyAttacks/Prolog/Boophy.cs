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
    bool moveLeft = true;

    float transitionTime = 0; //time for transitioning to new phase

    float transitionEnd = 0;

    float countAllies;

    public float attackTime = 0;//how long the attack will last

    public override void prepare()
    {
        moveUp = true;
        attackStage = 0;
        timeChange = 0;
        attackTime = 0;
        countAllies = 0;
        timeEnd = Random.Range(50f, 700f) * Time.deltaTime;
        transitionEnd = Random.Range(50, 700f) * Time.deltaTime;

        int rgen = Random.Range(1, 3);
        if(rgen == 2)
        {
            moveLeft = false;
            moveUp = false;
        }
        else
        {
            moveLeft = true;
            moveUp = true;
        }
          


        foreach(GameObject eni in EnnemyManger.instance.ennemies)
        {
            if (eni != null)
                countAllies++;
        }
    }

    public void boundCheck()
    {
        if (transform.position.z > -6.85)
        {
            moveUp = !moveUp;
            transform.position = new Vector3(transform.position.x, transform.position.y, -6.85f);
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
        attackTime += 0.01f * Time.deltaTime;
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

        if (attackTime < (5 * countAllies)  * Time.deltaTime)
            return false;

        return true;
    }

    public override bool attack2()
    {
        attackTime += 0.01f * Time.deltaTime;

        if (attackStage == 0)
        {        
            transform.position += new Vector3(0, 0, 2.5f) * Time.deltaTime;
            if (transform.position.z >= -5.94)
                attackStage = 1;
        }
        if (attackStage == 1)
        {
            transform.position += new Vector3(-2.5f, 0, 0) * Time.deltaTime;
            if (transform.position.x <= -12.19)
                attackStage = 2;
        }
        if (attackStage == 2)
        {
            float zSpeed = Random.Range(0.2f, 2.5f);
            float xSpeed;


            if (transform.position.x <= -14.83)
                moveLeft = false;
            if (transform.position.x >= -9.447)
                moveLeft = true;

            if (moveLeft)
                xSpeed = -1 * Random.Range(0.5f, 3.5f);
            else
                xSpeed = Random.Range(0.5f, 3.5f);

            if (player.transform.position.x < transform.position.x && moveLeft)
                xSpeed += -1;
            if (player.transform.position.x > transform.position.x && !moveLeft)
                xSpeed += 1;

            transform.position += new Vector3(xSpeed, 0, -zSpeed) * Time.deltaTime;

            if (transform.position.z <= -14.19)
                attackStage = 3;
        }
        if (attackStage == 3)
        {
            transform.position += new Vector3(2.5f, 0, 0) * Time.deltaTime;
            if (transform.position.x >= -8.6)
                attackStage = 0;
        }

        if (attackTime < (5 * countAllies) * Time.deltaTime)
            return false;

        return true;
    }

}
