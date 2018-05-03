using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnnemy1 : MonoBehaviour {

    public GameObject[] paths;
  public   int pathMarker;
    public CharacterController controller;
    public BoxCollider collide;
    public Vector3 directionMove;
    public float gravScale;
    public float speed;
    SpriteRenderer rend;
    public Vector3 velocity;
    public GameObject battleManager;
    BattleManager bScript;


    // Use this for initialization
    void Start () {
        collide = GetComponent<BoxCollider>();
        controller = GetComponent<CharacterController>();
        rend = GetComponent<SpriteRenderer>();
        battleManager = GameObject.FindGameObjectWithTag("BattleManager");
        bScript = battleManager.GetComponent<BattleManager>();
    }
	
	// Update is called once per frame
	void Update () {
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
