using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrackSave
{
    public int gridSize;

    public bool hasStart;

    public bool hasFinish;

    public bool isLoop;

    public List<TrackSlot> UsedGrid;

    public List<int> UsedGridIndex;

    public TrackSave(TrackBuilder builder)
    {
        gridSize = builder.gridSize;

        hasStart = builder.hasStart;

        hasFinish = builder.hasFinish;

        isLoop = builder.isLoop;

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                if(builder.SlotGrid[x, y].CurrentState == TrackSlot.State.FULL)
                {
                    UsedGrid.Add(builder.SlotGrid[x, y]);
                }
            }
        }
    }
}
