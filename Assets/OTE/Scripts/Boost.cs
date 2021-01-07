using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boost : MonoBehaviour
{
    List<GameObject> cars;
    int index = 0;
    private void Start()
    {
        cars = new List<GameObject>();
        Debug.Log("Ha");
    }
    IEnumerator coolDown()
    {
        yield return new WaitForSeconds(0.5f);
        cars[index].GetComponent<NavMeshAgent>().speed = 13;
        cars[index].GetComponent<NavMeshAgent>().acceleration = 11;
        index++;
    }
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.tag == "Player" || other.tag == "AiCar")
        {
           
            other.GetComponent<NavMeshAgent>().speed = 260;
            other.GetComponent<NavMeshAgent>().acceleration = 500;
            cars.Add(other.gameObject);
            StartCoroutine("coolDown");    
        }
    }
   
}
