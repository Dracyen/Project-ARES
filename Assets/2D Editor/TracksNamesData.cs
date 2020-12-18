using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TracksNamesData
{
    public List<string> TracksNames = new List<string>();

    public TracksNamesData(string names)
    {  
        TracksNames.Add(names);
    }
}
