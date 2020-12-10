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
    // Start is called before the first frame update
    void Start()
    {
       
        
    }
    public void DistributeCars()
    {
        UsedSlots = new List<int>();
        StartPoint = new List<Transform>();
        CarSlots = GameObject.FindGameObjectsWithTag("Start");
        
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
            CurCar.transform.localScale = new Vector3(3, 3, 3);
            UsedSlots.Remove(rand);
        }
        GameObject PlayC;
        PlayC = Instantiate(PlayerCar, StartPoint[UsedSlots[0]].position, Quaternion.identity);
        PlayC.transform.localScale = new Vector3(10, 10, 10);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
