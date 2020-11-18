using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    Vector3 targetPos;
    public GameObject[] Objectives;
    GameObject target;
    int index = 0;
    bool Add = true;
    AiDrive car;
    // Start is called before the first frame update
    void Start()
    {
        //car = GameObject.FindGameObjectWithTag("Player").GetComponent<AiDrive>();
        Objectives = GameObject.FindGameObjectsWithTag("TargetToAi");
        //target = Objectives[index];
    }

    // Update is called once per frame
    void Update()
    {
        
        //index = car.index;
        target = Objectives[index];
        //Debug.Log(transform.position);
        Debug.Log(target.transform.position);
        targetPos = new Vector3(target.transform.position.x - 10, target.transform.position.y + 10, target.transform.position.z - 10);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, UnityEngine.Random.Range(0.015f, 0.025f));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(target.transform.position), Time.time * 0.03f);
        transform.LookAt(car.transform);


        if (Vector3.Distance(transform.position, target.transform.position) < 0.5)
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
