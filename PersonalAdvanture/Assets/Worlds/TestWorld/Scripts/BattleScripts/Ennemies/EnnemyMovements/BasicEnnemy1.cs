using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnnemy1 : MonoBehaviour {

    public GameObject[] paths;
    public int pathMarker;
    public CharacterController controller;
    public BoxCollider collide;
    public Vector3 directionMove;
    public float gravScale;
    public float speed;
    SpriteRenderer rend;
    public Vector3 velocity;
    BattleManager bScript;

    public Vector3 startPos;
    public bool attackOn = false;


    // Use this for initialization
    void Start () {
        collide = GetComponent<BoxCollider>();
        controller = GetComponent<CharacterController>();
        rend = GetComponent<SpriteRenderer>();
        bScript = BattleManager.instance;
        startPos = transform.position;

     
    }
	
	// Update is called once per frame
	void Update () {

        if (!attackOn)
        {
            if (!bScript.freezeEnnemies)
            {
                controller.enabled = true;
                collide.enabled = false;
                Vector3 distance = (paths[pathMarker].transform.position - transform.position).normalized;
                directionMove = new Vector3(distance.x * speed, directionMove.y, distance.z * speed);
                controller.Move(directionMove * Time.deltaTime);

            }
            else
            {
                controller.enabled = false;
                collide.enabled = true;
            }

        }


    }

    public void returnPos()
    {
        controller.enabled = true;
        collide.enabled = false;
        Vector3 distance = (startPos - transform.position).normalized;
        directionMove = new Vector3(distance.x * speed, directionMove.y, distance.z * speed);
        controller.Move(directionMove * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject == paths[pathMarker])
        {
            pathMarker++;
            if (pathMarker >= paths.Length)
                pathMarker = 0;
        }

    }
}
