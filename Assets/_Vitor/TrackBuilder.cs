using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class TrackBuilder : MonoBehaviour
{
    //public Text _display;

    public Object prefab;

    public float slotSize = 5;

    public int offset = 20;

    [Space(10)]

    [Range(10, 20)]
    public int gridSize = 10;

    int currentSize = 0;

    GameObject[] _objects;

    public TrackSlot[,] SlotGrid;

    public bool hasStart;

    public bool hasFinish;

    public Object Car;

    GameObject CarRef;

    public bool isLoop;

    public int laps = 0;

    public int count = 0;

    public Text gridSizeDisplay;

    //Playable Track

    GameObject[] Tracks3D;

    float StepSize;

    private void Awake()
    {
        _objects = new GameObject[400];

        for (int i = 0; i < _objects.Length; i++)
        {
            _objects[i] = (GameObject)Instantiate(prefab, transform);
            _objects[i].GetComponent<TrackSlot>().Grid = this;
        }
    }

    private void Start()
    {
        hasStart = false;
        hasFinish = false;
        gridSize = 10;
        CreateGrid(gridSize, gridSize);
    }

    /*
    public void ChangeSize(float value)
    {
        gridSize = (int)value;
    }
    */

    private void Update()
    {
        gridSize = System.Int32.Parse(gridSizeDisplay.text);

        if (gridSize != currentSize)
        {
            CreateGrid(gridSize, gridSize);
        }

        if(CarRef != null)
            Debug.Log("It's position is x: " + CarRef.transform.position.x + " / y: " + CarRef.transform.position.y + " / z: " + CarRef.transform.position.z);
    }

    void CreateGrid(int sizeX, int sizeY)
    {
        hasStart = false;

        hasFinish = false;

        currentSize = sizeX;

        SlotGrid = new TrackSlot[sizeX, sizeY];

        for (int i = 0; i < _objects.Length; i++)
        {
            _objects[i].SetActive(false);
        }

        int index = 0;

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                float SlotX = ((gridSize * slotSize + offset * gridSize) / gridSize * x) - (gridSize * slotSize + offset * gridSize) / 2;
                float SlotY = ((gridSize * slotSize + offset * gridSize) / gridSize * y) - (gridSize * slotSize + offset * gridSize) / 2;

                _objects[index].SetActive(true);

                SlotGrid[x, y] = _objects[index].GetComponent<TrackSlot>();

                //SlotGrid[x, y]._display = _display;

                SlotGrid[x, y].pos = new Vector2(x, y);

                SlotGrid[x, y].SetPosition(SlotX, SlotY);

                SlotGrid[x, y].Slot.Resize(slotSize);

                SlotGrid[x, y].SetReady();

                index++;
            }
        }
    }

    public void StartTestTrack()
    {
        int index = 0;

        foreach (TrackSlot slot in SlotGrid)
        {
            if (slot.CurrentState != TrackSlot.State.FULL)
            {
                slot.Slot.gameObject.SetActive(false);
            }

            if(slot.Piece != null)
            {
                if (slot.Piece.name == "Start(Clone)")
                {
                    CarRef = (GameObject)Instantiate(Car, slot.Piece.transform);
                }
            }

            index++;
        }

        Debug.Log("Count: " + index);
    }

    public void StopTestTrack()
    {
        int index = 0;

        foreach (TrackSlot slot in SlotGrid)
        {
            if (slot.CurrentState != TrackSlot.State.FULL)
            {
                slot.Slot.gameObject.SetActive(true);
            }

            if(CarRef != null)
            Destroy(CarRef);

            index++;
            Debug.Log("Count: " + index);
        }
    }

    public void SwitchStart()
    {
        hasStart = !hasStart;

        CheckLoop();
    }

    public void SwitchFinish()
    {
        hasFinish = !hasFinish;

        CheckLoop();
    }

    public void CheckLoop()
    {
        if (hasStart && hasFinish)
        {
            //is finish leading to start?
            //Yes then show loop


        }
        else
        {
            isLoop = false;
        }
    }

    public bool CheckSlots(Vector2 pos, Vector2[] slots)
    {
        int i = 0;

        foreach (Vector2 slot in slots)
        {
            //Debug.Log("Slot " + i + " is x: " + slot.x + " / y: " + slot.y);

            //if (slot.x != 0 && slot.y != 0){}

            float x = pos.x + slot.x;
            float y = pos.y + slot.y;

            Debug.Log("Target Pos is x: " + x + " / y: " + y + " is " + SlotGrid[(int)x, (int)y].CurrentState);

            if (SlotGrid[(int)x, (int)y].CurrentState == TrackSlot.State.FULL)
            {
                //Debug.Log("x: " + x + " / y: " + y + " Is Full");

                return false;
            }

            Debug.Log("x: " + x + " / y: " + y + " is Ready");

            i++;
        }

        return true;
    }

    public void SetEmpty(Vector2 pos, Vector2[] slots)
    {
        Debug.Log("Start SetEmpty");

        foreach (Vector2 slot in slots)
        {
            float x = pos.x + slot.x;
            float y = pos.y + slot.y;

            Debug.Log("SE - Target Pos is x: " + x + " / y: " + y);

            SlotGrid[(int)x, (int)y].SetMultiReady();
        }
    }

    public void SetFull(Vector2 pos, Vector2[] slots)
    {
        Debug.Log("Start SetFull");

        int i = 0;

        foreach (Vector2 slot in slots)
        {
            float x = pos.x + slot.x;
            float y = pos.y + slot.y;

            Debug.Log("SF - Target Pos is x: " + x + " / y: " + y);

            SlotGrid[(int)x, (int)y].SetMultiFull();

            i++;
        }
    }

    public void SaveTrack(string name)
    {
        SaveSystem.SaveTrack(this, name);
    }

    public void LoadTrack(string name)
    {
        TrackPicker picker = FindObjectOfType<TrackPicker>();

        Save data = SaveSystem.LoadTracks(name);
        isLoop = data.isLoop;
        laps = data.numOfLaps;
        GameObject verson3D = null;
        Tracks3D = new GameObject[data.numOfTiles];
        if (Screen.height == 1080)
        {
            StepSize = 1;
        }
        else if (Screen.height == 1440)
        {
            StepSize = 1;
        }
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
            for (int z = 0; z < picker._prefabs.Length; z++)
            {
                if (picker._prefabs[z].index == data.index[i])
                {
                    verson3D = (GameObject)picker._prefabs[z].Mesh;
                }
            }
            Instanc = Instantiate(verson3D, new Vector3(data.PosicoesDeEntradaX[i]/1000, 0, data.PosicoesDeEntradaY[i]/1000), rotation, transform);
            Tracks3D[i] = Instanc;
            Instanc.transform.localScale = new Vector3(data.EscalaX[i] * (StepSize / data.SizeOfTheGrid) * 10, data.EscalaY[i] * (StepSize / data.SizeOfTheGrid) * 10, data.EscalaZ[i] * (StepSize / data.SizeOfTheGrid) * 10);
        }
        FindObjectOfType<PlaceCars>().DistributeCars();
    }
}