using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleMap : MonoBehaviour
{
    public GameObject Origin;
    public Slider value;
    void Update()
    {
        Debug.Log(value.value);
        Origin.transform.localScale = new Vector3(value.value, value.value, value.value);
    }

    // Start is called before the first frame update
    public void ChangeScale()
    {
        
        Origin.transform.localScale = new Vector3(value.value, value.value, value.value);
        
    }
}
