using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapDisplay : MonoBehaviour
{
    public int[] TestIndex;
    public Tile[] TestTileTracks;

    public int GridSize;
    public GameObject[,] Grid;

    public int WidthOffset;
    public int HeightOffset;

    public GameObject Dots;

    List<GameObject> tile;


    public Transform GridDisplay;

    public Slider Size;

    bool GridCreated = false;

    public int width;
    public int height;
    [Range(0.8f,2f)]
    public float Scale;

    public List<Tile> tileTracks;
    public int tileTracksIndex = 0;

    public Vector2 directionFlow = new Vector2(1,0);

    Vector2 lasPosUsedInGrid;
    MapGenerator MapInfo;

    bool canBePlaced = true;

    public GameObject erroScreen;
    public GameObject LoopScreen;

    public string trackName;
    public InputField inputName;
    public InputField inputNameLoad;

    public bool canGoToAR = false;
    // Start is called before the first frame update
    void Start()
    {
        MapInfo = FindObjectOfType<MapGenerator>();
        directionFlow = new Vector2(1, 1);
        tile = new List<GameObject>();
        tileTracks = new List<Tile>();
        GridSize = (int)Size.value;
        width = Screen.height - HeightOffset * 2;
        height = Screen.height - HeightOffset * 2;
        Step = height / GridSize;
        WidthOffset = (Screen.width / 2) - ((Step * GridSize) / 2);
        DeleteGrid();
    }

    // Update is called once per frame
    void Update()
    {
        GridDisplay.localScale = new Vector3(Scale, Scale, Scale);
    }
    public void DeleteGrid()
    {
        //When GridSize is Updated by the player the grid is redrawn
        if (GridCreated)
        {
            for(int y = 0; y < GridSize; y++)
            {
                for (int x = 0; x < GridSize; x++)
                {
                    Destroy(Grid[x, y]);
                }
            }
            GridCreated = false;
            if (tileTracksIndex > 0)
            {
                for (int i = 0; i < tileTracksIndex; i++)
                {
                    Destroy(tile[i]);

                }
                tileTracksIndex = 0;
                tile.Clear();
            }
            
        }
        GridSize = (int)Size.value;
        MapInfo.UpdateGrid(GridSize);
        DrawGrid();
    }
    public int Step;
    public void DrawGrid()
    {
        
        //Drawing Grid Based on size given by player
        Grid = new GameObject[GridSize, GridSize];
        Step = height / GridSize;
           
        for (int y = 0; y < GridSize; y+= 1)
        {
            for (int x = 0; x < GridSize; x += 1)
            {
                
                

                Grid[x, y] = Instantiate(Dots, new Vector2(WidthOffset + x * Step, HeightOffset + y * Step ), Quaternion.identity, GridDisplay);
                Grid[x, y].transform.localScale = new Vector3((Step-1),( Step-1), 1);
                Grid[x, y].GetComponent<FindYourSelfInGrid>().GridPosition(x, y);


            }
            
        }
        GridCreated = true;
    }

    public void Place(MapGenerator.TrackTile Selected, Vector3 posToPlace, Transform posRef)
    {
        if (tileTracksIndex > 0)
        {
            posToPlace.x = tileTracks[tileTracksIndex - 1].exitPos.x;
            posToPlace.y = tileTracks[tileTracksIndex - 1].exitPos.y;
            
        }
        for (int i = 0; i< tileTracksIndex; i++)
        {
            //Debug.Log("posToPlace " + posToPlace);
            //Debug.Log("tile[i].transform.position " + tile[i].transform.position);
            if (posToPlace == tile[i].transform.position && Selected.name != "Final")
            { 
                    canBePlaced = false;
                erroScreen.SetActive(true);
                break;
                
            }
            else
            {
                canBePlaced = true;
            }           
        }

        
        if (canBePlaced)
        {
            // A track is beeing placed in the seleceted pos
            tile.Add(Instantiate(Selected.image, posToPlace, Quaternion.identity, posRef));
            tileTracks.Add(new Tile());
            Vector3 TileScale = tileTracks[tileTracksIndex].Scale;

            tile[tileTracksIndex].transform.localScale = new Vector3((width / GridSize - 1) * Scale * TileScale.x, (height / GridSize - 1) * Scale * TileScale.y, 1 * TileScale.z);




            if (tileTracksIndex > 0)
            {
                // Debug.Log("UsingExit");
                //tileTracks[tileTracksIndex].AddTile(tileTracks[tileTracksIndex - 1].exitPos, Selected, directionFlow, tileTracks[tileTracksIndex - 1].rotation, tileTracks[tileTracksIndex - 1].originalInfo.isCurve);
                tileTracks[tileTracksIndex].AddTile(Selected, tileTracks[tileTracksIndex - 1]);
            }
            else
            {
               
                tileTracks[tileTracksIndex].AddFirstTile(new Vector2(posToPlace.x, posToPlace.y), Selected, directionFlow, Tile.RotationState.Left, false);
                
            }
            tile[tileTracksIndex].transform.rotation = Quaternion.Euler(tileTracks[tileTracksIndex].rot);
            //SpinLastTrack(tileTracksIndex);
            //Debug.Log(TileScale + "Instance");
            if (Selected.name == "Final")
            {
                
                for (int i = 0; i < tileTracksIndex; i++)
                {
                    if (posToPlace == tile[i].transform.position && tileTracks[tileTracksIndex].outPutRot != tileTracks[i].rotation)
                    {
                        erroScreen.SetActive(true);
                        Debug.Log("Clear");
                        ClearTrackFromTouchPos(tileTracksIndex-1);
                    }
                }
                if (tile[tileTracksIndex].transform.position == tile[0].transform.position)
                {
                    MapInfo.isLoop = true;
                    LoopScreen.SetActive(true);
                }
            }

            tileTracksIndex++;
            //canBePlaced = true;
        }
        FindObjectOfType<UiManager>().ShowTabs();
    }

    public void trackTaped(Vector2 touchPos)
    {
        for(int i = 0; i < tileTracks.Count; i++)
        {
            if(touchPos == tileTracks[i].entrancePos)
            {
                if(i < tileTracks.Count - 1)
                {
                    ClearTrackFromTouchPos(i);
                }
                else if(i == tileTracks.Count - 1)
                {
                    //tileTracks[i] has been selected in the grid for change
                    SpinLastTrack(i);
                }
                
            }
        }
    }
    void ClearTrackFromTouchPos(int i)
    {
        Debug.Log("Not the Last");
        for (int z = tileTracks.Count - 1; z > i; z--)
        {
            Destroy(tile[z]);
            tile.Remove(tile[z]);
            tileTracks.Remove(tileTracks[z]);
            tileTracksIndex--;
        }
    }
    void SpinLastTrack(int i)
    {
        tileTracks[i].BeSelected();
        
        tile[i].transform.localScale = new Vector3(Mathf.Abs(tile[i].transform.localScale.x) * tileTracks[i].Scale.x, Mathf.Abs(tile[i].transform.localScale.y) * tileTracks[i].Scale.y, Mathf.Abs(tile[i].transform.localScale.z) * tileTracks[i].Scale.z);
        tile[i].transform.rotation = Quaternion.Euler(tileTracks[i].rot);
    }
    public void SaveTrack()
    {
        SaveSystem.SaveTrack(this, trackName, FindObjectOfType<MapGenerator>());
    }
    public bool hasBeenLoaded = false;
    public void LoadTrack()
    {// esta notificando o metodo GiveListToGenerate que deve gerar a pista que foi escolhida e nao a que esta no editor
        
        hasBeenLoaded = true;
        
    }
    public void GiveListToGenerate()
    {
        canGoToAR = true;
        if (hasBeenLoaded)
        {
            FindObjectOfType<MapGenerator>().GenerateSaved3DTrack();
            hasBeenLoaded = false;
        }
        else
        {
            FindObjectOfType<MapGenerator>().Generate3DTrack(tileTracks);
        }
        
    }
    public void TrackNameInput()
    {
        trackName = inputName.text;
        Debug.Log(trackName);
    }
    public void TrackNameInputLoad()
    {
        trackName = inputNameLoad.text;
        Debug.Log(trackName);
    }
}
