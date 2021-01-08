using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionInLap : MonoBehaviour
{
    public int numOfLapsCompleated = 0;
    int numOfTiles = 0;
    public int TileNum = 0;
    private void Awake()
    {
        numOfTiles = GameObject.FindGameObjectsWithTag("TargetToAi").Length;
        //Debug.Log("Num of Tiles: " + numOfTiles);
    }
    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == "Finish")
        {
           
            if(numOfLapsCompleated >= FindObjectOfType<PutSelectedTrack>().NumOfLaps)
            {
                FindObjectOfType<NewPlayerDrive>().RaceiIsOnGoing = false;
                GetComponent<Timer>().finished = true;
                Debug.Log(GetComponent<Timer>().time);
            }
            numOfLapsCompleated++;
            //Debug.Log("End of Lap");
            //Debug.Log(numOfLapsCompleated);
        }
        if (other.tag == "TargetToAi")
        {
            if(TileNum == 9)
            {
                TileNum = 0;
            }
            TileNum++;
            //Debug.Log("OneMoreTile: " + TileNum);           
        }
    }
}
