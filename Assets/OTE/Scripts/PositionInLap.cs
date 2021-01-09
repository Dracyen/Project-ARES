using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionInLap : MonoBehaviour
{
    public int numOfLapsCompleated = 0;
    int numOfTiles = 0;
    public int TileNum = 0;
    bool coolDown = true;
    private void Awake()
    {
        numOfTiles = GameObject.FindGameObjectsWithTag("TargetToAi").Length;
        
    }
    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(1);
        coolDown = true;

    }
    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == "Finish")
        {
            //Debug.Log(FindObjectOfType<PutSelectedTrack>().NumOfLaps);
            if (numOfLapsCompleated >= FindObjectOfType<PutSelectedTrack>().NumOfLaps)
            {
                FindObjectOfType<NewPlayerDrive>().RaceiIsOnGoing = false;
                GetComponent<Timer>().finished = true;
                FindObjectOfType<InGame_Manager>().PauseMenu.SetActive(true);
                //Debug.Log(FindObjectOfType<PutSelectedTrack>().NumOfLaps);
            }
            else if (coolDown)
            {
                numOfLapsCompleated++;
                coolDown = false;
                StartCoroutine("CoolDown");
            }
            
 
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
