using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeWhoFinished : MonoBehaviour
{
    public List<string> names;
    int index = 0;
    public string latestToFinish;
    // Start is called before the first frame update
    void Start()
    {
        names = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        names.Add(other.name);
        //Debug.Log(other.name);
        latestToFinish = other.name;
        index++;
    }
}
