using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleCamera : MonoBehaviour {
    public GameObject bPlayer;
    public GameObject[] enemies;
    public Camera mainCamera;
    public float angleAdjust;
    public float blockAdjust;
    public GameObject[] blocks;

    // Use this for initialization
    void Start () {
        bPlayer = GameObject.Find("BattlePlayer");
        enemies = GameObject.FindGameObjectsWithTag("Enemies");
        bPlayer.GetComponent<SpriteRenderer>().flipX = true;
        blocks = GameObject.FindGameObjectsWithTag("BattleBlock");
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 cameraAngle = mainCamera.transform.position;
        cameraAngle.x -= angleAdjust;
        bPlayer.transform.forward = cameraAngle;
        foreach(GameObject eni in enemies)
        {
            if(eni != null)
            {
                eni.GetComponent<SpriteRenderer>().flipX = true;
                eni.transform.forward = cameraAngle;
            }
     
        }
        /*
        foreach(GameObject bloc in blocks)
        {
            Vector3 blockDir = mainCamera.transform.position;
            blockDir.x -= blockAdjust;
            bloc.transform.forward = blockDir;
        }
        */
    }

    public void cameraMoveTo(Vector3 target)
    {
        Vector3 newPos = Vector3.Lerp(mainCamera.transform.position, target, 0.1f);
        mainCamera.transform.position = newPos;
    }
}
