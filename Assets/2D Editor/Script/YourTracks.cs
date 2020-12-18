using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class YourTracks
{
    static public List<Tile>[] SavedTracks = new List<Tile>[25];
    static int IndexToAdd = 0;

    public static void SaveTrack(List<Tile> track)
    {
        SavedTracks[IndexToAdd] = track;
        Debug.Log(SavedTracks[0].Count);
        IndexToAdd++;
        
    }
}
