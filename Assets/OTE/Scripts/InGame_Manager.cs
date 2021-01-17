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
    GameObject PlayerCar;
    public GameObject PauseMenu;
    private void Start()
    {
        
        carsTimers = FindObjectsOfType<Timer>();
        cars = FindObjectsOfType<PositionInLap>();
        num = new int[cars.Length];
        
    }
    IEnumerator UpdateText()
    {
        yield return new WaitForSeconds(0.5f);
        
        NumOfLaps.text = "Lap " + PlayerCar.GetComponent<PositionInLap>().numOfLapsCompleated.ToString() + "/" + FindObjectOfType<PutSelectedTrack>().NumOfLaps;
        if(playerPos == 1)
        {
            PlayerPosInRace.text = playerPos.ToString() + " st";
        }
        else if (playerPos == 2)
        {
            PlayerPosInRace.text = playerPos.ToString() + " nd";
        }
        else if (playerPos == 3)
        {
            PlayerPosInRace.text = playerPos.ToString() + " rd";
        }
        else if (playerPos > 3)
        {
            PlayerPosInRace.text = playerPos.ToString() + " th";
        }
        
        if (FindObjectOfType<NewPlayerDrive>().RaceiIsOnGoing)
        {
            StartCoroutine("UpdateText");
        }
        
    }
    private void Update()
    {
        //Debug.Log("In game manager is working");
       

        
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
    public void StartGame(GameObject Player)
    {
        PlayerCar = Player;
        StartCoroutine("UpdateText");
    }
    public bool pause;
    public void Pause()
    {
        if (pause)
        {
            pause = false;
            NewAIDrive[] Ais;
            Ais = FindObjectsOfType<NewAIDrive>();
            for (int i = 0; i < Ais.Length; i++)
            {
                Ais[i].canGo = true;
            }
            FindObjectOfType<NewPlayerDrive>().RaceiIsOnGoing = true;
        }
        else if (!pause)
        {
            pause = true;
            NewAIDrive[] Ais;
            Ais = FindObjectsOfType<NewAIDrive>();
            for(int i = 0; i < Ais.Length; i++)
            {
                Ais[i].canGo = false;
            }
            FindObjectOfType<NewPlayerDrive>().RaceiIsOnGoing = false;
        }
    }
}
