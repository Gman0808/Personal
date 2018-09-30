using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{

    public static PlayerInfo instance;


    public int Health;
    public int Stamina;
    private void Awake()
    {
        #region SingleTon
        if (instance != null)
        {
            Debug.Log("Warning multiple playerinfos found");
            return;
        }
        instance = this;
        #endregion
    }
}