using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchPos : MonoBehaviour
{
    public Text Touch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Touch.text = Screen.width.ToString();
        if (Input.touchCount > 0)
        {
            //Touch.text = Input.touches[0].position.x.ToString();
        }
    }
}
