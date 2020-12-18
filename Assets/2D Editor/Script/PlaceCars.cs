using System.Collections;
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
    // Start is called before the first frame update
    void Start()
    {
       
        
    }
    public void DistributeCars()
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
            CurCar = Instantiate(AiCar, StartPoint[UsedSlots[rand]].position, Quaternion.identity);
            Cars[i] = CurCar;
            CurCar.transform.localScale = new Vector3(3, 3, 3);
            UsedSlots.Remove(rand);
        }
        GameObject PlayC;
        PlayC = Instantiate(PlayerCar, StartPoint[UsedSlots[0]].position, Quaternion.Euler(0,90,0));
        Cars[numOfAiCars] = PlayC;
        PlayC.transform.localScale = new Vector3(10, 10, 10);
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
