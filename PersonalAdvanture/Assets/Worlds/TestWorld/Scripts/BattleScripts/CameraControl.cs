using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public static CameraControl instance;

    public Camera mainCamera;
    battleCamera camScript;
    GameObject player;

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
        camScript = mainCamera.GetComponent<battleCamera>();
        player = GameObject.Find("BattlePlayer");
    }

    public void followPlayer() // follow player during positiong phase
    { //player z - -11.88
        //camera z - 17
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.z = player.transform.position.z - 4f;
        cameraPos.x = -6.44f;
        cameraPos.y = 1.64f;
        camScript.cameraMoveTo(cameraPos);
    }

    public void followPlayer2() // follow player during punch phase
    { //player z - -11.88
        //camera z - 17
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.z = player.transform.position.z - 2.5f;
        cameraPos.y = 1.12f;
        cameraPos.x = player.transform.position.x;
        camScript.cameraMoveTo(cameraPos);
    }
    public void followPlayer3() // follow player during return phase
    { //player z - -11.88
        //camera z - 17
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.z = player.transform.position.z - 4f;
        cameraPos.x = player.transform.position.x;
        cameraPos.y = 1.64f;
        camScript.cameraMoveTo(cameraPos);
    }
}
