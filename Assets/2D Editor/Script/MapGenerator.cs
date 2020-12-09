using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public TrackTile[] tiles;

    TrackTile Selected;
    Vector2 posToPlace;

    bool hasChoosenPos;
    bool hasChoosenTrack;

    public Transform posRef;

    public Vector3 scale;

    public int[,] posIndex;
    // Start is called before the first frame update
    void Start()
    {
        
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
            Place();
            
        }
    }
    public void Place()
    {
        
        if (hasChoosenTrack)
        {
            //Debug.Log("Place");
            hasChoosenTrack = false;
            FindObjectOfType<MapDisplay>().Place(Selected, posToPlace, posRef);
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
    }

    public void UpdateGrid(int size)
    {
        posIndex = new int[size, size];
    }
    public void Generate3DTrack(List<Tile> tiles)
    {
        for(int i = 0; i < tiles.Count; i++)
        {
            GameObject Instanc;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, tiles[i].rot.z, 0));

            Instanc = Instantiate(tiles[i].originalInfo.version3D, new Vector3(tiles[i].entrancePos.x, 0, tiles[i].entrancePos.y), rotation);
            Instanc.transform.localScale = new Vector3(8, 8, 8);
        }
    }
}
