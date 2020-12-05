using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackBuilder : MonoBehaviour
{
    public Object _prefab;

    public float slotSize = 5;

    [Space(10)]

    [Range(10, 20)]
    public int gridSize = 10;

    int currentSize = 0;

    GameObject[] _objects;

    TrackSlot[,] SlotGrid;

    private void Awake()
    {
        _objects = new GameObject[400];

        for(int i = 0; i < _objects.Length; i++)
        {
            _objects[i] = (GameObject)Instantiate(_prefab, transform);
        }
    }

    private void Start()
    {
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
                float SlotX = ((gridSize * slotSize + 20) / gridSize * x) - (gridSize * slotSize + 20) / 2;
                float SlotY = ((gridSize * slotSize + 20) / gridSize * y) - (gridSize * slotSize + 20) / 2;

                _objects[index].SetActive(true);

                SlotGrid[x, y] = _objects[index].GetComponent<TrackSlot>();

                SlotGrid[x, y].SetPosition(SlotX, SlotY);

                SlotGrid[x, y]._mesh.Resize(slotSize);

                index++;
            }
        }

        Debug.Log(index);
    }
}
