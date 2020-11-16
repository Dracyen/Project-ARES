using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AiDrive : MonoBehaviour
{
   public GameObject[] Objectives;
    GameObject target;
    public int index = 0;
    bool Add = true;
    // Start is called before the first frame update
    void Start()
    {
        int rand = UnityEngine.Random.Range(0, 3);
        if (rand == 0)
        {
            Objectives = GameObject.FindGameObjectsWithTag("TargetToAi");
        }
        if (rand == 1)
        {
            Objectives = GameObject.FindGameObjectsWithTag("TargetToAi1");
        }
        if (rand == 2)
        {
            Objectives = GameObject.FindGameObjectsWithTag("TargetToAi2");
        }
        
        target = Objectives[index];
    }

    // Update is called once per frame
    void Update()
    {
        target = Objectives[index];
        //Debug.Log(transform.position);
        Debug.Log(target.transform.position);
       
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, UnityEngine.Random.Range(0.045f, 0.055f));
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(target.transform.position), Time.time * 0.03f);
        transform.LookAt(target.transform);
        

        if(Vector3.Distance(transform.position,target.transform.position) < 0.5)
        {
            if (Add)
            {
                index++;
                target = Objectives[index];
                Add = false;
            }   
        }
        if (Vector3.Distance(transform.position, target.transform.position) > 1)
        {
            Add = true;
        }
    }
}
