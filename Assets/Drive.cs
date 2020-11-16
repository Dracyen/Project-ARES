using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public float m_Speed;
    public float turn_Speed;

    public bool isReversed = false;

    void Start()
    {
        //Fetch the Rigidbody component you attach from your GameObject
        m_Rigidbody = GetComponent<Rigidbody>();
        //Set the speed of the GameObject
        //m_Speed = 1f;
    }

    void Update()
    {
        isReversed = false;

        if (Input.GetKey(KeyCode.A))
        {
            MoveL();
        }

        if (Input.GetKey(KeyCode.D))
        {
            MoveR();
        }

        switch (Input.touchCount)
        {
            case 1:
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
                break;

            case 2:
                if (Input.touches[0].position.x < Screen.width / 2 && Input.touches[1].position.x > Screen.width / 2)
                {
                    isReversed = true;
                }
                break;
        }

        Movement();

        /*
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
        */
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

    void Movement()
    {
        if(isReversed)
        {
            m_Rigidbody.velocity = transform.forward * -m_Speed;
        }

        if(!isReversed)
        {
            //Move the Rigidbody forwards constantly at speed you define (the blue arrow axis in Scene view)
            m_Rigidbody.velocity = transform.forward * m_Speed;
        }
    }
}