using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnInfo : MonoBehaviour {
   public int health;
   public int defense;
 float deathTimer;
    public float timerIncrease;
    public bool damaged;
    EnAnimate Animate;

	void Start () {
        deathTimer = 0;
        Animate = GetComponent<EnAnimate>();
	}
	
	// Update is called once per frame
	void Update () {
        if (damaged)
        {
            Animate.damaged = damaged;
        }
	}

    public void targetDead()
    {
        Destroy(gameObject);
    }
}
