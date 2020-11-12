using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spin : MonoBehaviour
{
    public Slider Spiner;
    Vector3 RotationPos = new Vector3(0,0,0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotationPos.y = Spiner.value;
        transform.rotation = Quaternion.Euler(RotationPos);
        //transform.Rotate(RotationPos, Space.Self);
    }
}
