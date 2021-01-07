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
        SaveSystem.LoadTrackNames();
    }
}
