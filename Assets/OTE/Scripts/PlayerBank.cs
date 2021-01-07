using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBank : MonoBehaviour
{
    public int numOfPlayerCoins;
    void Awake()
    {
        PlayerPrefs.SetInt("PlayerCoins", 0);
        numOfPlayerCoins = PlayerPrefs.GetInt("PlayerCoins");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
