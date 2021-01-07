using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    TrackSlot[,] SlotGrid;

    public bool hasStart { get; private set; }

    public bool hasFinish { get; private set; }

    public Object Car;

    GameObject CarRef;

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

    public void ChangeSize(float value)
    {
        gridSize = (int)value;
    }

    private void Update()
    {
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
                    Debug.Log("Found the slot.");

                    CarRef = (GameObject)Instantiate(Car, slot.Piece.transform);

                    Debug.Log("Car is here!");

                    CarRef.transform.position = new Vector3(slot.Piece.transform.position.x, slot.Piece.transform.position.y, slot.Piece.transform.position.z);

                    Debug.Log("It's position is x: " + CarRef.transform.position.x + " / y: " + CarRef.transform.position.y + " / z: " + CarRef.transform.position.z);
                }
                else
                {
                    Debug.Log("Didn't find the slot.");
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
    }

    public void SwitchFinish()
    {
        hasFinish = !hasFinish;
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
}
