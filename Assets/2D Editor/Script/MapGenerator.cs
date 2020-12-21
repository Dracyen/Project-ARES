using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public TrackTile[] tiles;
    public TrackAddOn[] AddOns;

    TrackTile Selected;
    Vector2 posToPlace;

    bool hasChoosenPos;
    bool hasChoosenTrack;

    public Transform posRef;

    public Vector3 scale;

    public int[,] posIndex;

    public bool isLoop = false;
    public int numOfLapsOfTheTrack;

    float GridSize;

    GameObject[] Tracks3D;
    
    // Start is called before the first frame update
    void Start()
    {
        numOfLapsOfTheTrack = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectPosition(Vector2 Pos, int x, int y)
    {
        posIndex[x, y] = Selected.index;
        posToPlace = Pos;
        //Debug.Log(Pos);
        hasChoosenPos = true;
        if (hasChoosenTrack)
        {
           // Place();
            
        }
        Place();
    }
    public void Place()
    {
        
        if (hasChoosenTrack)
        {
            //Debug.Log("Place");
            hasChoosenTrack = false;
            FindObjectOfType<MapDisplay>().Place(Selected, posToPlace, posRef);
        }
        if(FindObjectOfType<MapDisplay>().tileTracksIndex < 1)
        {
            FindObjectOfType<MapDisplay>().Place(tiles[0], posToPlace, posRef);
            
        }
    }
    public void SelectTrack(TrackTile selected)
    {
        Selected = selected;
        hasChoosenTrack = true;
        //Debug.Log(selected.name);
        if(FindObjectOfType<MapDisplay>().tileTracksIndex > 0)
        {
            Place();
        }
        
    }

    [System.Serializable]
    public struct TrackTile
    {
        public string name;
        public int index;
        public GameObject image;
        public GameObject version3D;

        public int SizeX;
        public int SizeY;

        public int StepX;
        public int StepY;

        public bool isCurve;
        public bool changeAxis;
        public Tile.RotationState rotationOutput;
        public UiManager.Tabs Category;
    }
    [System.Serializable]
    public struct TrackAddOn
    {
        public string name;
        public int index;
        public GameObject image;
        public GameObject version3D;
        public UiManager.Tabs Category;

        public bool IsSelected;
    }
    public void UpdateGrid(int size)
    {
        posIndex = new int[size, size];
        GridSize = size;
    }
    public void Generate3DTrack(List<Tile> tiles)
    {
        Vector3 StartPos = new Vector3(-tiles[0].entrancePos.x, 0,-tiles[0].entrancePos.y);
        Transform PlacementPos = FindObjectOfType<ARTapToPlaceObject>().placementIndicator.transform;
        Tracks3D = new GameObject[tiles.Count];
        Debug.Log("TESTANDO123");
        for (int i = 0; i < tiles.Count; i++)
        {
            GameObject Instanc;
            float rotationToPlace;
            rotationToPlace = tiles[i].rot.z;
            if (tiles[i].rot.z == 0 ) 
            {
                rotationToPlace = 180; 
            }
            else if (tiles[i].rot.z == -180) 
            {
                rotationToPlace = 0; 
            }
            Quaternion rotation = Quaternion.Euler(new Vector3(0, rotationToPlace, 0));

            Instanc = Instantiate(tiles[i].originalInfo.version3D, new Vector3(tiles[i].entrancePos.x + StartPos.x + PlacementPos.position.x, 0+ PlacementPos.position.y, tiles[i].entrancePos.y + StartPos.z+ PlacementPos.position.z), rotation);
            Tracks3D[i] = Instanc;
            Instanc.transform.localScale = new Vector3(tiles[i].Scale.x * (80f / GridSize), tiles[i].Scale.y * (80f / GridSize), tiles[i].Scale.z * (80f / GridSize));
        }
        FindObjectOfType<PlaceCars>().DistributeCars();

    }
    public void GenerateSaved3DTrack()
    {
        Save data = SaveSystem.LoadTracks(FindObjectOfType<MapDisplay>().trackName);
        isLoop = data.isLoop;
        numOfLapsOfTheTrack = data.numOfLaps;
        GameObject verson3D = null;
        Tracks3D = new GameObject[data.numOfTiles];
        for (int i = 0; i < data.numOfTiles; i++)
        {
            GameObject Instanc;
            float rotationToPlace;
            rotationToPlace = data.rotacaoEmZdeCadaTile[i];
            if (data.rotacaoEmZdeCadaTile[i] == 0)
            {
                rotationToPlace = 180;
            }
            else if (data.rotacaoEmZdeCadaTile[i] == -180)
            {
                rotationToPlace = 0;
            }
            Quaternion rotation = Quaternion.Euler(new Vector3(0, rotationToPlace, 0));
            for(int z = 0; z < tiles.Length; z++)
            {
                if(tiles[z].index == data.index[i])
                {
                    verson3D = tiles[z].version3D;
                }
                
            }
            Instanc = Instantiate(verson3D, new Vector3(data.PosicoesDeEntradaX[i], 0, data.PosicoesDeEntradaY[i]), rotation);
            Tracks3D[i] = Instanc;
            Instanc.transform.localScale = new Vector3(data.EscalaX[i] * (80f / GridSize), data.EscalaY[i] * (80f / GridSize), data.EscalaZ[i] * (80f / GridSize));
        }
        FindObjectOfType<PlaceCars>().DistributeCars();
    }
    public void Clear3DTrack()
    {
        for(int i = 0; i < Tracks3D.Length; i++)
        {
            Destroy(Tracks3D[i]);
        }
    }
    
}
