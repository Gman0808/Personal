using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleCamera : MonoBehaviour {
    public GameObject player;
    public GameObject[] enemies;
    public Camera mainCamera;
    public float angleAdjust;
    public float blockAdjust;
    public GameObject[] blocks;
    public static battleCamera instance;

    private void Awake()
    {

        #region SingleTon
        if (instance != null)
        {
            Debug.Log("Warning multiple battleMangers found");
            return;
        }
        instance = this;
        #endregion

    }
    // Use this for initialization
    void Start () {
        player = GameObject.Find("BattlePlayer");
        enemies = GameObject.FindGameObjectsWithTag("Enemies");
        player.GetComponent<SpriteRenderer>().flipX = true;
        blocks = GameObject.FindGameObjectsWithTag("BattleBlock");
    }
	
	// Update is called once per frame
	void Update () {

        
        /*
        foreach(GameObject bloc in blocks)
        {
            Vector3 blockDir = mainCamera.transform.position;
            blockDir.x -= blockAdjust;
            bloc.transform.forward = blockDir;
        }
        */
    }

    //Position player during punch, item eating, and slingshot
    public void playerActions()
    {
        Vector3 cameraAngle = mainCamera.transform.position;
        cameraAngle.x -= angleAdjust;
        player.transform.forward = cameraAngle;
        foreach (GameObject eni in enemies)
        {
            if (eni != null)
            {
                eni.GetComponent<SpriteRenderer>().flipX = true;
                eni.transform.forward = cameraAngle;
            }

        }
    }

    public void playerMove()
    {
        player.transform.forward = Vector3.zero;
    }

    // Position camera
    public void cameraMoveTo(Vector3 target)
    {
        Vector3 newPos = Vector3.Lerp(mainCamera.transform.position, target, 0.1f);
        mainCamera.transform.position = newPos;
    }

    public void followPlayer() // follow player during positiong phase
    { //player z - -11.88
        //camera z - 17
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.z = player.transform.position.z - 3.5f;
        cameraPos.x = -6.44f;
        cameraPos.y = 1.64f;
        cameraMoveTo(cameraPos);
    }

    public void followPlayer2() // follow player during punch phase
    { //player z - -11.88
        //camera z - 17
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.z = player.transform.position.z - 2.5f;
        cameraPos.y = 1.12f;
        cameraPos.x = player.transform.position.x;
       cameraMoveTo(cameraPos);
    }
    public void followPlayer3() // follow player during return phase
    { //player z - -11.88
        //camera z - 17
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.z = player.transform.position.z - 4f;
        cameraPos.x = player.transform.position.x;
        cameraPos.y = 1.64f;
        cameraMoveTo(cameraPos);
    }
    public void followPlayer4() // follow player during return phase
    { //player z - -11.88
        //camera z - 17
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.z = player.transform.position.z - 5f;
        cameraPos.x = -12;
        cameraPos.y = 1.97f;
        cameraMoveTo(cameraPos);
    }

    public void followPlayer5() // follow player during return phase
    { //player z - -11.88
        //camera z - 17
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.z = player.transform.position.z - 3.4f;
        cameraPos.x = player.transform.position.x;
        cameraPos.y = 1.60f;
        cameraMoveTo(cameraPos);
    }
}
