using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDrive : MonoBehaviour
{
   public GameObject[] Objectives;
    GameObject target;
    int index = 0;
    bool Add = true;
    // Start is called before the first frame update
    void Start()
    {
        target = Objectives[index];
    }

    // Update is called once per frame
    void Update()
    {
        target = Objectives[index];
        Debug.Log(target.transform.position);
       
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Random.Range(0.015f, 0.025f));
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(target.transform.position), Time.time * 0.03f);
        transform.LookAt(target.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Add)
        {
            Debug.Log("OLA");
            index++;
            target = Objectives[index];
            Add = false;
        }
        
        
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (!Add)
        {
            Debug.Log("Tchau");
            Add = true;
        }



    }
}
