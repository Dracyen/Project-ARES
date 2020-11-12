using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaceOnPlane : MonoBehaviour
{
    ARSessionOrigin sessionOrigin;
    private List<ARRaycastHit> hits;

    public GameObject Model;
    public GameObject Canvas;
    // Start is called before the first frame update
    void Start()
    {
        sessionOrigin = GetComponent<ARSessionOrigin>();
        hits = new List<ARRaycastHit>();

       // Model.SetActive(false);
        //Canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        Pose pose = hits[0].pose;

        if (!Model.activeInHierarchy)
        {
            //Model.SetActive(true);
            Model.transform.position = pose.position;
            Model.transform.rotation = pose.rotation;
            //Canvas.SetActive(true);
        }

        /*if (sessionOrigin.Raycast(touch.position, hits, UnityEngine.Experimental.XR.TrackableType.PlaneWithinPolygon))
        {
            Pose pose = hits[0].pose;

            if (!Model.activeInHierarchy)
            {
                Model.SetActive(true);
                Model.transform.position = pose.position;
                Model.transform.rotation = pose.rotation;
                Canvas.SetActive(true);
            }
        }*/
    }

    public void Place()
    {
        
    }
}
