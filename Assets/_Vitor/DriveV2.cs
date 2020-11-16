using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveV2 : MonoBehaviour
{
    public GameObject m_Body;
    public GameObject v_Body;

    public float curvature = 0;
    public float max_curvuture;
    public float multiplier;
    public float reset_multiplier;
    public bool curving = false;

    public float original_Angle = 180;

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            CurveLeft();
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            CurveRight();
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            curving = true;
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            curving = false;
        }
        
        if(!curving)
        {
            ResetCurve();
        }
    }

    void CurveLeft()
    {
        if (curving != true)
        {
            curving = true;
        }

        if (v_Body.transform.rotation.eulerAngles.y > original_Angle - max_curvuture)
            v_Body.transform.Rotate(0, multiplier * -(Time.deltaTime), 0, Space.Self);
    }

    void CurveRight()
    {
        if (curving != true)
        {
            curving = true;
        }

        if (v_Body.transform.rotation.eulerAngles.y < original_Angle + max_curvuture)
            v_Body.transform.Rotate(0, multiplier * Time.deltaTime, 0, Space.Self);
    }

    void ResetCurve()
    {
        if (v_Body.transform.rotation.eulerAngles.y > original_Angle && v_Body.transform.rotation.eulerAngles.y < original_Angle + 80)
        {
            v_Body.transform.Rotate(0, -1 * reset_multiplier * Time.deltaTime, 0, Space.Self);
        }

        if (v_Body.transform.rotation.eulerAngles.y < original_Angle && v_Body.transform.rotation.eulerAngles.y > original_Angle - 80)
        {
            v_Body.transform.Rotate(0, 1 * reset_multiplier * Time.deltaTime, 0, Space.Self);
        }
        
        if (v_Body.transform.rotation.eulerAngles.y < original_Angle + 1 && v_Body.transform.rotation.eulerAngles.y > original_Angle - 1)
        {
            v_Body.transform.eulerAngles = new Vector3(v_Body.transform.eulerAngles.x, 180, v_Body.transform.eulerAngles.z);
        }
    }
}