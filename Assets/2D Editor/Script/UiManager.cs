using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Button[] trackButtons;
    public Button goRight;
    public Button goLeft;

    int Tab;
    int OffSet = 0;
    MapGenerator MapInfo;
    int maxOffset;

    int buttonPressdIndex;

    

    private void Awake()
    {
        
        MapInfo = FindObjectOfType<MapGenerator>();
        maxOffset = MapInfo.tiles.Length - 5;
    }
    private void Update()
    {
        OffSetCheck();

        for (int i =0; i < trackButtons.Length; i++)
        {
            if(i< MapInfo.tiles.Length)
            {
                trackButtons[i].image.sprite = MapInfo.tiles[i + OffSet].image.GetComponent<Image>().sprite;
                Debug.Log(MapInfo.tiles[i + OffSet].name);
            }
            else
            {
                trackButtons[i].image.sprite = null;
            }
            
        }


    }
    void OffSetCheck()
    {
        if (OffSet <= 0)
        {
            OffSet = 0;
            goLeft.GetComponent<Button>().image.color = new Color(100,100,100,10);
            goLeft.GetComponent<Button>().interactable = false;
           
        }
        else
        {
            goLeft.GetComponent<Button>().image.color = new Color(255, 255, 255, 255);
            goLeft.GetComponent<Button>().interactable = true;
        }
        if (OffSet >= maxOffset)
        {
            goRight.GetComponent<Button>().image.color = new Color(100, 100, 100, 10);
            goRight.GetComponent<Button>().interactable = false;
            OffSet = maxOffset;
        }
        else
        {
            goRight.GetComponent<Button>().image.color = new Color(255, 255, 255, 255);
            goRight.GetComponent<Button>().interactable = true;
        }
    }

    public void GoLeft()
    {
        OffSet--;

    }
    public void GoRight()
    {
        OffSet++;
        
    }

    public void SelectTrack(int ind)
    {
        buttonPressdIndex = ind;
        

        MapInfo.SelectTrack(MapInfo.tiles[buttonPressdIndex + OffSet]);
    }

    public void SelectPosToPlace(Vector2 Pos, int x, int y)
    {
        MapInfo.SelectPosition(Pos, x, y);
    }

    public void SelectPlacedTrack(Vector2 tapedTrackPos)
    {
        FindObjectOfType<MapDisplay>().trackTaped(tapedTrackPos);
    }
}
