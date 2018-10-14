using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyManger : MonoBehaviour {
    public static EnnemyManger instance;

    public int attackTypes = 2; //number of diffrent attacks, normally two
    int selected = 1; // current attack chosen
    public GameObject[] ennemies;


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

    // Use this for initialization
    void Start () {
        ennemies = GameObject.FindGameObjectsWithTag("Enemies");
	}
	
    //choosing attack
     public void selectAttack()
     {
      selected = Random.Range(1, attackTypes+1);

        foreach (GameObject en in ennemies)
        {
            if(en!= null)
            {
                en.GetComponent<EnBasicAttack>().prepare();
            }
       
        }
     }

    public bool reposition()
    {
        bool checkFinish = true;
        bool current = true;
        foreach (GameObject en in ennemies)
        {
            if (en != null)
            {
                current = en.GetComponent<EnBasicAttack>().reposition();
                if (!current)
                    checkFinish = false;
            }
         
        }
        return checkFinish;
    }


    public bool retreat()
    {
        bool checkFinish = true;
        bool current = true;
        foreach (GameObject en in ennemies)
        {
            if (en != null)
            {
                current = en.GetComponent<EnBasicAttack>().returnPos();
                if (!current)
                    checkFinish = false;
            }

        }
        return checkFinish;
    }

    //preforming attack
    public bool doAttack()
    {
        bool checkFinish = true;
        bool current = true;

        foreach(GameObject en in ennemies)
        {
            if(en != null)
            {
                if (selected == 1)
                  current =  en.GetComponent<EnBasicAttack>().attack1();
                if (selected == 2)
                    current = en.GetComponent<EnBasicAttack>().attack2();
            }

            if (!current)
                checkFinish = false;
        }

        return checkFinish;
       
    }
	
}
