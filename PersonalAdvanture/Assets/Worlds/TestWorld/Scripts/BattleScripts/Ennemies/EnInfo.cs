using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnInfo : MonoBehaviour {
   public int health;
   public int defense;
   float deathTimer;
    public bool damaged;
    Animator animator;

	void Start () {
        deathTimer = 0;
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (damaged)       
            animator.SetBool("Damaged", true);       
        else
            animator.SetBool("Damaged", false);
    }


}
