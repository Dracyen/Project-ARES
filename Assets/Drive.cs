using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public float m_Speed;
    public float turn_Speed;

    void Start()
    {
        //Fetch the Rigidbody component you attach from your GameObject
        m_Rigidbody = GetComponent<Rigidbody>();
        //Set the speed of the GameObject
        //m_Speed = 1f;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            MoveL();
        }
        if (Input.GetKey(KeyCode.D))
        {
            MoveR();
        }
        if (Input.touchCount > 0)
        {
        if (Input.touches[0].position.x < Screen.width / 2 || Input.GetKey(KeyCode.A))
        {
            //Debug.Log("L");
            MoveL();
        }

        if (Input.touches[0].position.x > Screen.width / 2 || Input.GetKey(KeyCode.D))
        {
            //Debug.Log("R");
            MoveR();
        }
        }

        //Move the Rigidbody forwards constantly at speed you define (the blue arrow axis in Scene view)
        m_Rigidbody.velocity = transform.forward * m_Speed;



    }
    Vector3 RotationY = new Vector3(0, 0, 0);
    void MoveL()
    {
        //Debug.Log(transform.rotation.y);
        RotationY.y -= turn_Speed;
        transform.rotation = Quaternion.Euler(RotationY);

    }

    void MoveR()
    {
        //Debug.Log(transform.rotation.y);
        RotationY.y += turn_Speed;
        transform.rotation = Quaternion.Euler(RotationY);
    }
}