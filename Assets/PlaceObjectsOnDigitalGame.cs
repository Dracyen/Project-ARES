using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjectsOnDigitalGame : MonoBehaviour
{
    int rand = 0;
    int last = 2;
    int lastStreat;
    Vector3 lastPos = new Vector3(0, 0, 0);
    Vector3 newPos = new Vector3(0, 0, 0);
    public GameObject[] ObjectsToPlace;
    public GameObject YourCar;
    public GameObject AICAR;
    bool rightAndUp = true;
    int distance = 9;

    public int direction = 1;
    // Start is called before the first frame update
    void Awake()
    {
        
        Instantiate(ObjectsToPlace[3], new Vector3(0, 0, 0), Quaternion.identity);
        for ( int i = 0; i < 10; i++)
        {
            
            rand = (int) Random.Range(0, 10);
            
            if (last == 0)
            {
                if(rand < 5)
                {
                    newPos = lastPos + new Vector3(0, 0, distance * direction);
                    Instantiate(ObjectsToPlace[0], newPos, Quaternion.identity);
                    lastPos = newPos;
                    lastStreat = 0;
                    last = 0;
                }
                else
                {
                    newPos = lastPos + new Vector3(0, 0, distance * direction);
                    Instantiate(ObjectsToPlace[2], lastPos + new Vector3(0, 0, distance * direction), Quaternion.Euler(0,90,0));
                    lastPos = newPos;
                    last = 2;
                }
            }
            if (last == 2)
            {
                if (lastStreat == 0)
                {
                    newPos = lastPos + new Vector3(distance * direction, 0, 0);
                    Instantiate(ObjectsToPlace[1], newPos, Quaternion.identity);
                    lastPos = newPos;
                    lastStreat = 1;
                    last = 1;
                }
                else
                {
                    newPos = lastPos + new Vector3(0, 0, distance * direction);
                    Instantiate(ObjectsToPlace[0], newPos, Quaternion.identity);
                    lastPos = newPos;
                    lastStreat = 0;
                    last = 0;
                }
            }
            if (last == 1)
            {
                if (rand < 5)
                {
                    newPos = lastPos + new Vector3(distance * direction, 0, 0);
                    Instantiate(ObjectsToPlace[1], newPos, Quaternion.identity);
                    lastPos = newPos;
                    lastStreat = 1;
                    last = 1;
                }
                else
                {
                    newPos = lastPos + new Vector3(distance * direction, 0, 0);
                    Instantiate(ObjectsToPlace[2], newPos, Quaternion.Euler(0, -90, 0));
                    lastPos = newPos;
                    last = 2;
                }
            }

        }
        Instantiate(YourCar, new Vector3(0, 0.5f, -1), Quaternion.Euler(0,90,0));
        for (int i = 0; i < 5; i++)
        {
            Instantiate(AICAR, new Vector3(0, 0.5f, i), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
