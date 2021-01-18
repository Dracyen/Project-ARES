using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamePlace : MonoBehaviour
{
    public GameObject object1;

    public GameObject object2;

    void Update()
    {
        object1.transform.position = object2.transform.position;
    }
}
