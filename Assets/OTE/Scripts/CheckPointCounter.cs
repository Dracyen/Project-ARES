using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointCounter : MonoBehaviour
{
    int numOfAiCars = 0;
    private void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "AiCar")
        {
            numOfAiCars++;
            //Debug.Log(numOfAiCars);
        }
        if(other.tag == "Player")
        {
            Debug.Log(numOfAiCars);
            FindObjectOfType<InGame_Manager>().playerPos = numOfAiCars + 1;
        }
        if (numOfAiCars >= FindObjectOfType<InGame_Manager>().num.Length)
        {
            numOfAiCars = 1;
        }
    }
}
