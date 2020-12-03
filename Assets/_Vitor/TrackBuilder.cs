using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackBuilder : MonoBehaviour
{
    public Object _prefab;

    GameObject[] _objects;

    TrackSlot[,] TrackGrid;

    private void Awake()
    {
        _objects = new GameObject[400];

        for(int i = 0; i < _objects.Length; i++)
        {
            _objects[i] = (GameObject)Instantiate(_prefab, transform);
        }
    }

    private void Update()
    {
        
    }

    void CreateGrid(int sizeX, int sizeY)
    {
        TrackGrid = new TrackSlot[sizeX, sizeY];

        for (int x = 0; x < sizeX; x++)
        {
            int index = 0;

            for (int y = 0; y < sizeY; y++)
            {
                index++;

                TrackGrid[x, y] = _objects[index].GetComponent<TrackSlot>();
            }
        }
    }
}
