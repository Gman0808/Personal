using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCollide : MonoBehaviour {
    public int attackValue;
   public bool alive;
	// Use this for initialization
	void Start () {
        alive = true;
    }
	
	// Update is called once per frame
	void Update () {
  
    }

  private void OnTriggerEnter(Collider other)
    {
        alive = true;

        if (other.gameObject.tag == "Obsticals")
        {
            Debug.Log("OOps");
            alive = false;

        }
        if (other.gameObject.tag == "Enemies")
        {
            EnInfo info = other.gameObject.GetComponent<EnInfo>();
            other.gameObject.GetComponent<EnInfo>().health -= (attackValue - info.defense);
            other.gameObject.GetComponent<EnInfo>().damaged = true;
      
            

        }
    
    }
}
