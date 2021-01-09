using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class InGame_Manager : MonoBehaviour
{
    PositionInLap[] cars;
    public int[] num;
    Timer[] carsTimers;
     public int numOfLaps = 3;
    public string[] positions;
   public int playerPos;
    public Text PlayerPosInRace;
    public Text NumOfLaps;
    GameObject Player;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        carsTimers = FindObjectsOfType<Timer>();
        cars = FindObjectsOfType<PositionInLap>();
        num = new int[cars.Length];
    }
    private void Update()
    {
        Debug.Log("In game manager is working");
        Player = GameObject.FindGameObjectWithTag("Player");
        NumOfLaps.text = "Lap " + Player.GetComponent<PositionInLap>().numOfLapsCompleated.ToString() + "/" + FindObjectOfType<PutSelectedTrack>().NumOfLaps;

        PlayerPosInRace.text = playerPos.ToString() + " st";
            for (int i = 0; i< num.Length; i++)
        {
            num[i] = cars[i].TileNum;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < num.Length; i++)
            {
                Debug.Log(num[i]);
            }
        }
    }

}
