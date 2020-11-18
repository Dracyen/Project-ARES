using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualDrift : MonoBehaviour
{
    public GameObject v_Body;

    public float curvature = 0;
    public float max_curvuture;
    public float multiplier;
    public float reset_multiplier;
    public bool curving = false;
    public float original_Angle = 180;
    public float reset_Margin = 1;

    void Update()
    {
        if (Input.touches.Length > 0)
        {
            curving = true;

            if (Input.touches[0].position.x < Screen.width / 2)
            {
                CurveLeft();
            }

            if (Input.touches[0].position.x > Screen.width / 2)
            {
                CurveRight();
            }
        }
        else
        {
            curving = false;
        }
        
        /*
        curving = false;

        if (Input.GetKey(KeyCode.D))
        {
            curving = true;
            CurveRight();
        }

        if (Input.GetKey(KeyCode.A))
        {
            curving = true;
            CurveLeft();
        }

        if (!curving)
        {
            ResetCurve();
        }
        */
    }

    void CurveLeft()
    {
        if (v_Body.transform.rotation.eulerAngles.y > original_Angle - max_curvuture)
            v_Body.transform.Rotate(0, multiplier * -(Time.deltaTime), 0, Space.Self);
    }

    void CurveRight()
    {
        if (v_Body.transform.rotation.eulerAngles.y < original_Angle + max_curvuture)
            v_Body.transform.Rotate(0, multiplier * Time.deltaTime, 0, Space.Self);
    }

    void ResetCurve()
    {
        if (v_Body.transform.rotation.eulerAngles.y > original_Angle + reset_Margin && v_Body.transform.rotation.eulerAngles.y < original_Angle + 80)
        {
            v_Body.transform.Rotate(0, -1 * reset_multiplier * Time.deltaTime, 0, Space.Self);
        }

        if (v_Body.transform.rotation.eulerAngles.y < original_Angle - reset_Margin && v_Body.transform.rotation.eulerAngles.y > original_Angle - 80)
        {
            v_Body.transform.Rotate(0, 1 * reset_multiplier * Time.deltaTime, 0, Space.Self);
        }

        if (v_Body.transform.rotation.eulerAngles.y < original_Angle + reset_Margin && v_Body.transform.rotation.eulerAngles.y > original_Angle - reset_Margin)
        {
            v_Body.transform.eulerAngles = new Vector3(v_Body.transform.eulerAngles.x, original_Angle, v_Body.transform.eulerAngles.z);
        }
    }
}