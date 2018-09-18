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

   
   
}
