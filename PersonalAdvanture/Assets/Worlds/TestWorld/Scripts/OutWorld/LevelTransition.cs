using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour {

    //Variables
    public GameObject player;

    public string scene;
    public Scene floop;
    public Vector3 newPos;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement pScript = player.GetComponent<PlayerMovement>();


        //Loading in the next scene
        SceneManager.LoadScene(scene);
            //Resetting the position of the eplayer
            player.transform.position = newPos;

       


       
    }
}
