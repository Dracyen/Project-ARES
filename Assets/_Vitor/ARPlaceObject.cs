using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;
using System;

public class ARPlaceObject : MonoBehaviour
{
    public bool debug = false;
    public bool debug2 = false;

    public GameObject objectToPlace;
    public GameObject placementIndicator;
    public GameObject centerIndicator;

    private bool objectPlaced = false;

    private ARRaycastManager arRaycastManager;
    private Pose placementPose;
    private bool placementPoseIsValid = false;

    public GameObject TrackCreator;

    void Start()
    {
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        if(debug2)
        {
            UpdatePlacementPose();
        }

        UpdatePlacementIndicator();

        if(debug)
        {
            debug = false;
            PlaceObject();
        }

        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !objectPlaced)
        {
            objectPlaced = true;
            PlaceObject();
        }
    }

    private void PlaceObject()
    {
        TrackCreator.SetActive(true);
        TrackCreator.transform.position = placementPose.position;
        TrackCreator.transform.rotation = placementPose.rotation;
        //Instantiate(objectToPlace, placementPose.position, placementPose.rotation);

        Debug.Log("Debug Working");
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid && !objectPlaced)
        {
            placementIndicator.SetActive(true);
            centerIndicator.SetActive(false);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
            centerIndicator.SetActive(true);
        }
    }

    
    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }
}
