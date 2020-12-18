using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public GameObject TabsOfTracks;
    public GameObject TabsOfTracksHiden;
    public enum Tabs { Straights, Turns, Verticals, Barriers, All};

    public Button[] trackButtons;
    public Button goRight;
    public Button goLeft;

    public Button Straights;
    public Button Turns;
    public Button Verticals;
    public Button Barriers;
    public Button All;

    Tabs TabSelected;

    int Tab;
    int OffSet = 0;
    MapGenerator MapInfo;
    int maxOffset;
    int TabsButtonIndex = 0;
    int buttonPressdIndex;

    public Slider numLaps;
    public GameObject Lock;
   List <GameObject> Locks;

    MapGenerator.TrackTile[] tracks;

    bool canGoR = true;
    bool canGoL = false;
    int dex = 0;
    private void Awake()
    {
        tracks = new MapGenerator.TrackTile[5];
        TabSelected = Tabs.All;
        Locks = new List<GameObject>();
        MapInfo = FindObjectOfType<MapGenerator>();
        maxOffset = MapInfo.tiles.Length - 6;
        putLocks();
       
        AjustButton();
        SeeIfIsLocked();
    }
    private void Update()
    {
        OffSetCheck();


    }
    void OffSetCheck()
    {
       
        if (OffSet <= 0)
        {
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
        if(tracks[4].name == null)
        {
            goRight.GetComponent<Button>().image.color = new Color(100, 100, 100, 10);
            goRight.GetComponent<Button>().interactable = false;
        }
    }

    public void GoLeft()
    {
        OffSet--;
        AjustButton();
        SeeIfIsLocked();
    }
    public void GoRight()
    {     
            OffSet++;
            AjustButton();
            SeeIfIsLocked();
    }

    public void SelectTrack(int ind)
    {
        buttonPressdIndex = ind;
        

        MapInfo.SelectTrack(tracks[buttonPressdIndex]);
    }

    public void SelectPosToPlace(Vector2 Pos, int x, int y)
    {
        MapInfo.SelectPosition(Pos, x, y);
    }

    public void SelectPlacedTrack(Vector2 tapedTrackPos)
    {
        FindObjectOfType<MapDisplay>().trackTaped(tapedTrackPos);
    }
    public void NumOfLaps()
    {
        MapInfo.numOfLapsOfTheTrack =(int) numLaps.value;
    }
    int locksIndex = 0;
    void SeeIfIsLocked()
    {

        for (int i = 0; i> trackButtons.Length; i++)
        {
           
            Locks[i].SetActive(false);
            
        }
        //Locks.Clear();

        for (int i = 0; i < trackButtons.Length; i++)
        {
            Locks[i].SetActive(false);


            if (tracks[i].name == "Curva180" || tracks[i].name == "S")
            {
                
                Locks[i].SetActive(true);
                
            }
        }
    }
    void putLocks()
    {
        for (int i = 0; i < trackButtons.Length; i++)
        {
                Locks.Add(Instantiate(Lock, trackButtons[i].transform.position, Quaternion.identity, trackButtons[i].transform)); 
        }
    }

    public void SelectStraights()
    {
        OffSet = 0;
        TabSelected = Tabs.Straights;
        TabsButtonIndex = 0;
        AjustButton();
        SeeIfIsLocked();
       
    }
    public void SelectTurns()
    {
        OffSet = 0;
        TabSelected = Tabs.Turns;
        TabsButtonIndex = 0;
        AjustButton();
        SeeIfIsLocked();
        
    }
    public void SelectVertical()
    {
        OffSet = 0;
        TabSelected = Tabs.Verticals;
        TabsButtonIndex = 0;
        AjustButton();
        SeeIfIsLocked();
        
    }
    public void SelectBarrier()
    {
        OffSet = 0;
        TabSelected = Tabs.Barriers;
        TabsButtonIndex = 0;
        AjustButton();
        SeeIfIsLocked();
       
    }
    public void SelectAll()
    {
        OffSet = 0;
        TabSelected = Tabs.All;
        TabsButtonIndex = 0;
        AjustButton();
        SeeIfIsLocked();
       
    }

    void AjustButton()
    {

        dex = 0;
        bool goOn = true;
        for (int i = 0; i < MapInfo.tiles.Length; i++)
        {
            if (TabSelected == Tabs.All)
            {
                if (i < MapInfo.tiles.Length && i < trackButtons.Length)
                {
                    trackButtons[i].enabled = true;
                    trackButtons[i].image.color = new Color(255, 255, 255, 255);
                    trackButtons[i].image.sprite = MapInfo.tiles[i + OffSet + 1].image.GetComponent<Image>().sprite;
                    tracks[i] = MapInfo.tiles[i + OffSet + 1];
                    //canGoR = true;
                }
                else if(i < trackButtons.Length)
                {
                    trackButtons[i].image.color = new Color(0, 0, 0, 0);
                    trackButtons[i].enabled = false;
                    canGoR = false;
                }
            }
            else
            {
                if (i < MapInfo.tiles.Length && MapInfo.tiles[dex + OffSet + 1].Category == TabSelected && goOn)
                {
                    
                    trackButtons[TabsButtonIndex].enabled = true;
                    trackButtons[TabsButtonIndex].image.color = new Color(255, 255, 255, 255);
                    trackButtons[TabsButtonIndex].image.sprite = MapInfo.tiles[dex + OffSet + 1].image.GetComponent<Image>().sprite;
                    tracks[TabsButtonIndex] = MapInfo.tiles[dex + OffSet + 1];
                    TabsButtonIndex++;
                }


            }
            if(dex<= trackButtons.Length)
            {
                dex++;

            }
            else
            {
                goOn = false;
            }
            
        }
        

        for (int i = TabsButtonIndex; i < trackButtons.Length; i++)
        {
            if (TabSelected != Tabs.All)
            {
                trackButtons[i].image.color = new Color(0,0,0,0);
                trackButtons[i].enabled = false;
                tracks[i].name = null;
            }
        }
        
    }

    public void HideTabs()
    {
        TabsOfTracks.SetActive(false);
        TabsOfTracksHiden.SetActive(true);
    }

    public void ShowTabs()
    {
       
        if (FindObjectOfType<MapDisplay>().tileTracksIndex > 0)
        {
            TabsOfTracks.SetActive(true);
            TabsOfTracksHiden.SetActive(false);
        }
    }
}
