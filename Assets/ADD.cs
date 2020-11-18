using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADD : MonoBehaviour
{
    public GameObject track;
    // Start is called before the first frame update
    void Awake()
    {
        //Instantiate(track, new Vector3(10, 10, 10), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Place()
    {
        Instantiate(track, new Vector3(10, 10, 10), Quaternion.identity);
    }
}
