using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutSelectedTrack : MonoBehaviour
{
    public GameObject[] Tracks;
    public GameObject PlayerCar;
    public GameObject AiCar;
    Transform target;
    GameObject car;


   public void SelectedTrack()
    {
        
        Tracks[FindObjectOfType<UI_MenuManager_OTE>().TrackIndexOffSet].SetActive(true);
        target = GameObject.FindGameObjectWithTag("Start").transform;
        car = Instantiate(PlayerCar, target.position + new Vector3(0,1,0), Quaternion.Euler(0,-90,0));
        car.transform.localScale = new Vector3(3,3,3);
        car = Instantiate(AiCar, target.position + new Vector3(2, 1, 0), Quaternion.Euler(0, -90, 0));
        car.transform.localScale = new Vector3(3, 3, 3);
        car = Instantiate(AiCar, target.position + new Vector3(2, 1, 1), Quaternion.Euler(0, -90, 0));
        car.transform.localScale = new Vector3(3, 3, 3);
        car = Instantiate(AiCar, target.position + new Vector3(2, 1, 2), Quaternion.Euler(0, -90, 0));
        car.transform.localScale = new Vector3(3, 3, 3);
        car = Instantiate(AiCar, target.position + new Vector3(3, 1, 0), Quaternion.Euler(0, -90, 0));
        car.transform.localScale = new Vector3(3, 3, 3);
        car = Instantiate(AiCar, target.position + new Vector3(3, 1, 1), Quaternion.Euler(0, -90, 0));
        car.transform.localScale = new Vector3(3, 3, 3);
        car = Instantiate(AiCar, target.position + new Vector3(3, 1, 2), Quaternion.Euler(0, -90, 0));
        car.transform.localScale = new Vector3(3, 3, 3);
        //FindObjectOfType<PlaceCars>().DistributeCars();
    }

}
