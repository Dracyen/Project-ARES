using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackBuilder : MonoBehaviour
{
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

    private void Awake()
    {
        _objects = new GameObject[400];

        for(int i = 0; i < _objects.Length; i++)
        {
            _objects[i] = (GameObject)Instantiate(prefab, transform);
            _objects[i].GetComponent<TrackSlot>().Grid = this;
        }
    }

    private void Start()
    {
        hasStart = false;
        CreateGrid(gridSize, gridSize);
    }

    private void Update()
    {
        if(gridSize != currentSize)
        {
            CreateGrid(gridSize, gridSize);
        }
    }

    void CreateGrid(int sizeX, int sizeY)
    {
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
                float SlotX = ((gridSize * slotSize + offset) / gridSize * x) - (gridSize * slotSize + offset) / 2;
                float SlotY = ((gridSize * slotSize + offset) / gridSize * y) - (gridSize * slotSize + offset) / 2;

                _objects[index].SetActive(true);

                SlotGrid[x, y] = _objects[index].GetComponent<TrackSlot>();

                SlotGrid[x, y].SetPosition(SlotX, SlotY);
                /*
                if(SlotGrid[x, y].CurrentState == TrackSlot.State.FULL)
                {
                    SlotGrid[x, y].Slot.mesh.gameObject.SetActive(true);
                }
                */
                SlotGrid[x, y].Slot.Resize(slotSize);

                SlotGrid[x, y].SetReady();

                index++;
            }
        }
    }

    public void SwitchStart()
    {
        hasStart = !hasStart;
        Debug.Log("Start is: " + hasStart);
    }
}
