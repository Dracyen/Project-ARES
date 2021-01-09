using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PutSelectedTrack : MonoBehaviour
{
    public GameObject[] Tracks;
    public GameObject PlayerCar;
    public GameObject AiCar;
    Transform target;
    GameObject car;
    int numOfAi = 4;
    int countDownTimer = 4;
    public Text CountDownText;
    public Image AIToggle;
    public int NumOfLaps = 1;
    bool Ai = false;
    IEnumerator CountDown()
    {
       
        yield return new WaitForSeconds(1);
        if(countDownTimer > 0)
        {
           
            countDownTimer--;
            CountDownText.text = countDownTimer.ToString();
            StartCoroutine("CountDown");
        }
        else if(countDownTimer <= 0)
        {
            CountDownText.gameObject.SetActive(false);
            Debug.Log(numOfAi);
            for (int i = 0; i < numOfAi; i++)
            {
                car = Instantiate(AiCar, target.position + new Vector3(2, 1, i), Quaternion.Euler(0, -90, 0));
                car.transform.localScale = new Vector3(3, 3, 3);
            }
            car = Instantiate(PlayerCar, target.position + new Vector3(5, 1, 1), Quaternion.Euler(0, -90, 0));
            car.transform.localScale = new Vector3(3, 3, 3);
            FindObjectOfType<InGame_Manager>().StartGame(car);
        }
    }
   public void SelectedTrack()
    {
        Tracks[FindObjectOfType<UI_MenuManager_OTE>().TrackIndexOffSet].SetActive(true);
        target = GameObject.FindGameObjectWithTag("Start").transform;
        StartCoroutine("CountDown");
    }
    public void AddLap()
    {
        NumOfLaps++;
    }

    public void SubLap()
    {
        
        NumOfLaps--;
        if(NumOfLaps <= 0)
        {
            NumOfLaps = 0;
        }
    }
    public void AddAi()
    {
        //numOfAi++;
    }

    public void SubAi()
    {
        //numOfAi--;
    }

    public void AiToggle()
    {
        Debug.Log("Toggle");
        if (!Ai)
        {
            //AIToggle.gameObject.SetActive(false);
            Ai = true;
            numOfAi = 4;
        }
        else if (Ai)
        {
            //AIToggle.gameObject.SetActive(true);
            Ai = false;
            numOfAi = 0;
        }
    }
    
}
