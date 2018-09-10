using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public float radius = 1f;
    public GameObject player;
    public bool interacted = false;

    public void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {       
    Interact();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public virtual void Interact()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if(distance <= radius && Input.GetAxis("Interact") > 0)
        {
            interacted = true;
        }
      
    }
}
