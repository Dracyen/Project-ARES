using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;
using System;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject placementIndicator;
    public GameObject[] ObjectToPlace;
    public int ObjectToBePlaced = 0;

    public bool StartARInteraction = false;
    private Pose PlacementPose;
    private ARRaycastManager aRRaycastManager;
    private bool placementPoseIsValid = false;
    private bool place = true;

    void Start()
    {
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
    }
   
    void Update()
    {
        //UpdatePlacementPose();
        UpdatePlacementIndicator();
        if(StartARInteraction && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && place && FindObjectOfType<MapDisplay>().canGoToAR)
        {
            place = false;
            placementIndicator.SetActive(false);
            PlaceObject();
        }
        
        if(!place)
        {
            placementIndicator.SetActive(false);
        }
    }
    public void PlaceButton()
    {
        place = false;
        placementIndicator.SetActive(false);
        PlaceObject();
    }
    private void PlaceObject()
    {
        FindObjectOfType<MapDisplay>().GiveListToGenerate();
       // Instantiate(ObjectToPlace[ObjectToBePlaced],PlacementPose.position,PlacementPose.rotation);
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }


    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            PlacementPose = hits[0].pose;
        }
    }
}
