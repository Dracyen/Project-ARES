﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackBuilder : MonoBehaviour
{
    public Text _display;

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
    }

    void CreateGrid(int sizeX, int sizeY)
    {
        hasStart = false;

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

                SlotGrid[x, y].pos = new Vector2(x, y);

                SlotGrid[x, y].SetPosition(SlotX, SlotY);

                SlotGrid[x, y].Slot.Resize(slotSize);

                SlotGrid[x, y].SetReady();

                index++;
            }
        }
    }

    public void TestTrack()
    {
        int index = 0;

        foreach (TrackSlot slot in SlotGrid)
        {
            if (slot.CurrentState != TrackSlot.State.FULL)
            {
                slot.Slot.gameObject.SetActive(false);
            }

            /*
            if (slot.Piece.name == "Start(Clone)")
            {
                CarRef = (GameObject)Instantiate(Car);

                CarRef.transform.position = slot.Piece.transform.position;
            }
            */

            index++;
            Debug.Log("Count: " + index);
        }
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
            /*
            if(CarRef != null)
            Destroy(CarRef);
            */
            index++;
            Debug.Log("Count: " + index);
        }
    }

    public void SwitchStart()
    {
        hasStart = !hasStart;
    }

    public bool CheckSlots(Vector2 pos, Vector2[] slots)
    {
        foreach (Vector2 slot in slots)
        {
            float x = pos.x + slot.x;
            float y = pos.y + slot.y;

            if (x != pos.x && y != pos.y)
            {
                if (SlotGrid[(int)x, (int)y].CurrentState == TrackSlot.State.FULL)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void SetEmpty(Vector2 pos, Vector2[] slots)
    {
        foreach (Vector2 slot in slots)
        {
            float x = pos.x + slot.x;
            float y = pos.y + slot.y;

            SlotGrid[(int)x, (int)y].DeletePiece();
        }
    }

    public void SetFull(Vector2 pos, Vector2[] slots)
    {
        _display.text = "Hello";

        int i = 0;

        foreach (Vector2 slot in slots)
        {
            float x = pos.x + slot.x;
            float y = pos.y + slot.y;

            SlotGrid[(int)x, (int)y].SetMultiFull();

            i++;

            _display.text = "Count: " + i.ToString();
        }
    }
}
