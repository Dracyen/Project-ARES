using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float time = 0;
    float[] lapTimes;
    int lapIndex = 0;
    public Text TimeCounter;
    int latestCheckPointIndex;
    float CheckPointTime;
    GameObject[] AllCheckPoints;
    public string CarName;
    public bool finished = false;


    private void Start()
    {
        TimeCounter = GameObject.FindGameObjectWithTag("TimeCounter").GetComponent<Text>();
        AllCheckPoints = GameObject.FindGameObjectsWithTag("TargetToAi");
        lapTimes = new float[3];
        StartCoroutine("Time");
    }
    IEnumerator Time()
    {
        //Debug.Log(time);
        yield return new WaitForSeconds(0.1f);
        if (!finished)
        {
            time += 0.1f;
            if (this.tag == "Player")
            {
                string t = ((int)time).ToString();

                TimeCounter.text = "Time: " + t;

            }
            StartCoroutine("Time");
        }
        
        
        
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Finish")
        {
            lapTimes[lapIndex] = time;
           // CarName = GetComponent<NewAIDrive>().CarName;
           // Debug.Log(lapTimes[lapIndex]+ "Car: " + GetComponent<NewAIDrive>().CarName) ;
            //time = 0;
            lapIndex++;
            
        }
        if (other.tag == "TargetToAi")
        {
            for(int i = 0; i< AllCheckPoints.Length; i++)
            {
                if(other.transform.position == AllCheckPoints[i].transform.position)
                {
                    latestCheckPointIndex = i;
                    CheckPointTime = time;
                }
            }
        }
    }
}
