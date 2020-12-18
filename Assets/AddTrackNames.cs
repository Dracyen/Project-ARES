using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTrackNames : MonoBehaviour
{
    List<string> namesOfTheTracks = new List<string>();
    public void Add(string name)
    {
        SaveSystem.SaveTracksNames(name);
    }

    public void OutPutAllNames()
    {
        TracksNamesData data = SaveSystem.LoadTrackNames();
        namesOfTheTracks = data.TracksNames;
        for(int i = 0; i < data.TracksNames.Count; i++)
        {
            Debug.Log(namesOfTheTracks[i]);
        }
    }
}
