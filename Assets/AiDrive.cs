using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AiDrive : MonoBehaviour
{
   public GameObject[] Objectives;
    public List<GameObject> AllRefPoints;
    GameObject target;
    public int index = 0;
    bool Add = true;
    int numOfLapsCompleted = 0;
    public int numOfLaps;
    // Start is called before the first frame update
    void Start()
    {
        numOfLaps = FindObjectOfType<MapGenerator>().numOfLapsOfTheTrack;
        AllRefPoints = new List<GameObject>();
        AddTargetsToList();


        target = AllRefPoints[index];
    }

    // Update is called once per frame
    void Update()
    {
        target = AllRefPoints[index];
        //Debug.Log(transform.position);
        //Debug.Log(target.transform.position);
       
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, UnityEngine.Random.Range(0.45f, 0.55f));
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(target.transform.position), Time.time * 0.03f);
        
        transform.LookAt(target.transform);
        

        if(Vector3.Distance(transform.position,target.transform.position) < 0.5)
        {
            if (Add)
            {
                index++;
                target = AllRefPoints[index];
                Add = false;
            }   
        }
        if (Vector3.Distance(transform.position, target.transform.position) > 1)
        {
            Add = true;
        }
    }
    public void AddTargetsToList()
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
        for(int i = 0; i < Objectives.Length; i++)
        {
            AllRefPoints.Add(Objectives[i]);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Finish" && FindObjectOfType<MapGenerator>().isLoop && numOfLapsCompleted<numOfLaps-1)
        {
            numOfLapsCompleted++;
            AddTargetsToList();
        }
    }
}
