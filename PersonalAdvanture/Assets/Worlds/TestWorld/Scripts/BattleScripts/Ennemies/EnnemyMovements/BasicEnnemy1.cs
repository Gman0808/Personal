using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnnemy1 : MonoBehaviour {

    public GameObject[] paths;
    public int pathMarker;
    private int startMarker;
    public BoxCollider collide;
    public Vector3 directionMove;
    public float gravScale;
    public float speed;
    BattleManager bScript;
    public Vector3 startPos;
    public bool attackOn = false;

    // Use this for initialization
    void Start () {
        collide = GetComponent<BoxCollider>();
        bScript = BattleManager.instance;
        startPos = transform.position;
        startMarker = pathMarker;
     
    }
	
	// Update is called once per frame
	void Update () {

        if (!bScript.freezeEnnemies)
        {
            Vector3 distance = (paths[pathMarker].transform.position - transform.position).normalized;
            directionMove = new Vector3(distance.x * speed, directionMove.y, distance.z * speed);
            gameObject.transform.position += directionMove * Time.deltaTime;

        }

    }

  
    public bool returnPos(float rSpeed)
    {
        pathMarker = startMarker;
        Vector3 distance = (startPos - transform.position).normalized;
        directionMove = new Vector3(distance.x * rSpeed, directionMove.y, distance.z * rSpeed);
        gameObject.transform.position += directionMove * Time.deltaTime;

        if (transform.position.x <= startPos.x + 1f && transform.position.x >= startPos.x - 1f && transform.position.z <= startPos.z + 1f && 
            transform.position.z >= startPos.z - 1f && transform.position.y <= startPos.y + 1f && transform.position.y >= startPos.y - 1f)
            return true;
       return false;
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
