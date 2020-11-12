using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoAiMoviment : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            if (Input.touches[0].position.x < Screen.width/2)
            {
                MoveL();
            }

            if (Input.touches[0].position.x > Screen.width/2)
            {
                MoveR();
            }
        }
        

        transform.position = transform.position + new Vector3(0, 0, 0.06f);

        if (Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position + new Vector3(-0.09f, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position + new Vector3(0.09f, 0, 0);
        }
    }

    public void MoveL()
    {
        transform.position = transform.position + new Vector3(-0.09f, 0, 0);
    }

    public void MoveR()
    {
        Debug.Log("TAN TAN TAN");
        transform.position = transform.position + new Vector3(0.09f, 0, 0);
    }
}
