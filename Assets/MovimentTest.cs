using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovimentTest : MonoBehaviour
{
    NavMeshAgent PlayerCar;
    public GameObject Destination;
    // Start is called before the first frame update
    void Start()
    {
        Destination = GameObject.FindGameObjectWithTag("Destination");
        PlayerCar = GameObject.FindGameObjectWithTag("PlayerCar").GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
       
        PlayerCar.destination = Destination.transform.position;
    }

    public void MoveLeft()
    {
        PlayerCar.destination = transform.position + new Vector3(2, 0, 0);
    }

    public void MoveRight()
    {
        PlayerCar.destination = transform.position + new Vector3(-2, 0, 0);
    }
}
