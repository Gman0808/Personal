using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManger : MonoBehaviour {
    public GameObject[] players;
    // Use this for initialization
    void Start () {
        players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 1)
        {
            for (int i = 1; i < players.Length; i++)
            {
                Destroy(players[i]);
            }

        }
        if (!players[0].activeSelf)
            players[0].SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
