using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewAIDrive : MonoBehaviour
{
    public GameObject[] Objectives;
    public List<GameObject> AllRefPoints;
    GameObject target;
    public int index = 0;
    bool Add = true;
    NavMeshAgent car;
    bool go = false;
    public NavMeshSurface Track;
    public GameObject model3D;
    RaycastHit hit;
    public string CarName;
    int numOfLapsFinished = 0;

    // Start is called before the first frame update
    void Start()
    {
        CarName = Random.Range(0, 100).ToString();
        GenerateNavMesh();
        car = GetComponent<NavMeshAgent>();
        AllRefPoints = new List<GameObject>();
        AddTargetsToList();


        target = AllRefPoints[index];
    }

    void Update()
    {     
            car.destination = target.transform.position;
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
        for (int i = 0; i < Objectives.Length; i++)
        {
            AllRefPoints.Add(Objectives[i]);
        }
    }

    public void GenerateNavMesh()
    {
        Track = GameObject.FindGameObjectWithTag("Track").GetComponent<NavMeshSurface>();
        Track.BuildNavMesh();


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            GetComponent<NavMeshAgent>().speed = Random.Range(80, 100 + FindObjectOfType<UI_MenuManager_OTE>().Dif);
            numOfLapsFinished++;
            if (numOfLapsFinished < FindObjectOfType<PutSelectedTrack>().NumOfLaps)
            {
                AddTargetsToList();
            }
            else
            {
                GetComponent<Timer>().finished = true;
            }
            
            
        }
        if (other.tag == AllRefPoints[index].tag)
        {
            index++;
            target = AllRefPoints[index];
            GetComponent<NavMeshAgent>().speed = Random.Range(80, 100+FindObjectOfType<UI_MenuManager_OTE>().Dif);
        }
    }
}
