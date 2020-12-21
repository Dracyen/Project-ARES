﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceCars : MonoBehaviour
{
    public GameObject PlayerCar;
    public GameObject AiCar;
    
    [Range(0, 7)]
    public int numOfAiCars;
    public GameObject[] CarSlots;
    List<Transform> StartPoint;
    List<int> UsedSlots;

    GameObject[] Cars;

    public bool hasBeenPlaced = false;
    // Start is called before the first frame update
    void Start()
    {
       
        
    }
    public void DistributeCars()
    {
        if (!hasBeenPlaced)
        {
            UsedSlots = new List<int>();
            StartPoint = new List<Transform>();
            CarSlots = GameObject.FindGameObjectsWithTag("Start");
            Cars = new GameObject[numOfAiCars + 1];
            for (int i = 0; i < numOfAiCars; i++)
            {

                StartPoint.Add(CarSlots[i].transform);
                UsedSlots.Add(i);
            }

            for (int i = 0; i < numOfAiCars; i++)
            {
                GameObject CurCar;
                int rand = Random.Range(0, UsedSlots.Count);
                CurCar = Instantiate(AiCar, StartPoint[UsedSlots[rand]].position + new Vector3(0,0.3f,0), Quaternion.identity);
                Cars[i] = CurCar;
                CurCar.transform.localScale = new Vector3(3, 3, 3);
                UsedSlots.Remove(rand);
            }
            GameObject PlayC;
            PlayC = Instantiate(PlayerCar, StartPoint[UsedSlots[0]].position + new Vector3(0, 0.3f, 0), Quaternion.Euler(0, 90, 0));
            Cars[numOfAiCars] = PlayC;
            PlayC.transform.localScale = new Vector3(10, 10, 10);
            hasBeenPlaced = true;
        }
       
    }
    // Update is called once per frame
    public void DestroyCars()
    {
        for(int i = 0; i< Cars.Length; i++)
        {
            Destroy(Cars[i]);
        }
    }
}
