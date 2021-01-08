using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MenuManager_OTE : MonoBehaviour
{
    //Var for coins Text
    PlayerBank Bank;
    public Text numOfCoinsText;

    //Var for tracks
    public Button NextTrackButton;
    public Button PreviewsTrackButton;
    public Image TracksImage;
    public Sprite[] TracksImages;
    public int TrackIndexOffSet;

    //Var for cars
    public Button NextCarButton;
    public Button PreviewsCarButton;
    public Image CarImage;
    public Sprite[] CarsImages;
    public int CarIndexOffSet;


    //Laps
    public Text numOfLapsText;
    private void Start()
    {
        //Seeks in the Bank Script the num of coins and Writes in the coins Text space in the top right corner
        Bank = FindObjectOfType<PlayerBank>();
        numOfCoinsText.text = "Coins: " + Bank.numOfPlayerCoins.ToString();

        UpdateTracksImage();
        UpdateCarsImage();
    }
    public void NextTrack()
    {
        //Anda uma imagem pra frente, mas se estiver na ultima volta pra primeira
        TrackIndexOffSet++;
        if (TrackIndexOffSet == TracksImages.Length)
        {
            TrackIndexOffSet = 0;
        }
    }
    public void PreviewsTrack()
    {
        //Anda uma imagem pra tras, mas se estiver na primeira volta pra ultima
        TrackIndexOffSet--;
        if (TrackIndexOffSet == -1)
        {
            TrackIndexOffSet = TracksImages.Length - 1;
        }
    }
    void UpdateTracksImage()
    {
        TracksImage.sprite = TracksImages[0 + TrackIndexOffSet];
    }
    public void NextCar()
    {
        //Anda uma imagem pra frente, mas se estiver na ultima volta pra primeira
        CarIndexOffSet++;
        if (CarIndexOffSet == CarsImages.Length)
        {
            CarIndexOffSet = 0;
        }
    }
    public void PreviewsCar()
    {
        //Anda uma imagem pra tras, mas se estiver na primeira volta pra ultima
        CarIndexOffSet--;
        if (CarIndexOffSet == -1)
        {
            CarIndexOffSet = CarsImages.Length - 1;
        }
    }
    void UpdateCarsImage()
    {
        CarImage.sprite = CarsImages[0 + CarIndexOffSet];
    }
    private void Update()
    {
        UpdateTracksImage();
        UpdateCarsImage();
        numOfLapsText.text = FindObjectOfType<PutSelectedTrack>().NumOfLaps.ToString();

    }
    public int Dif;
    public void Dificulty(int d)
    {
        Dif = d;
    }





}
