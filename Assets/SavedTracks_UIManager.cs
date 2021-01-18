using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavedTracks_UIManager : MonoBehaviour
{
    public Button[] TracksToSelect;
    public Text[] TracksToSelectText;
    List<string> str = new List<string>();
    List<string> strOutput = new List<string>();
    string newTrackname;
    int indexOfTheEdit;
    public GameObject NameEditor;
    public InputField inputNameLoad;
    public string selectedTrack;

    public Slider OffSetInput;
    int OffSetInputValue = 0;
    int OffSetInputMaxValue;
    // Start is called before the first frame update
    void Start()
    {

        UpdateNames();
        
    }

    // Update is called once per frame
    public void ButtonPress(int index)
    {
        FindObjectOfType<MapDisplay>().TrackNameInputLoad(TracksToSelectText[index].text);
        selectedTrack = TracksToSelectText[index].text;
        indexOfTheEdit = index;
        Debug.Log(index);
        Debug.Log(TracksToSelectText[index].text);
        
    }

    public void ButtonPress2(int index)
    {
        FindObjectOfType<TrackBuilder>().TrackNameInputLoad(TracksToSelectText[index].text);
        selectedTrack = TracksToSelectText[index].text;
        indexOfTheEdit = index;
        Debug.Log(index);
        Debug.Log(TracksToSelectText[index].text);

    }

    public void GetIndex(int index)
    {
        indexOfTheEdit = index;
    }
    public void EditTrackName()
    {
        
        strOutput[indexOfTheEdit] = newTrackname;
        //FindObjectOfType<MapDisplay>().trackName = newTrackname;
    }
    public void GetInputText()
    {
        newTrackname = inputNameLoad.text;
        
        Debug.Log(newTrackname);

    }

    public void Scroll()
    {
        str = SaveSystem.LoadTrackNames();
        strOutput = str;
        OffSetInputMaxValue = str.Count - TracksToSelect.Length;
        for (int i = 0; i < TracksToSelect.Length; i++)
        {
            if (i < str.Count)
            {
                TracksToSelect[i].enabled = true;
                TracksToSelect[i].image.color = new Color(255, 255, 255, 255);
                TracksToSelectText[i].color = new Color(255, 255, 255, 255);
                TracksToSelectText[i].text = str[i + OffSetInputValue];
                Debug.Log(str[i + OffSetInputValue]);
            }
            else
            {
                TracksToSelect[i].enabled = false;
                TracksToSelectText[i].color = new Color(0, 0, 0, 0);
                TracksToSelect[i].image.color = new Color(0, 0, 0, 0);
            }
        }
    }


    public void Edit()
    {
        Debug.Log("Edit: " + indexOfTheEdit);
        Debug.Log("OffSet: " + OffSetInputValue);
        str = SaveSystem.LoadTrackNames();
        string newfile = newTrackname;
        string fileName = str[indexOfTheEdit + OffSetInputValue];
        Debug.Log(newfile);
        SaveSystem.RenameTrack(fileName, newfile);
        str[indexOfTheEdit + OffSetInputValue] = newTrackname;
        inputNameLoad.text = " ";
        UpdateNames();
    }
    public void UpdateNames()
    {
        str = SaveSystem.LoadTrackNames();
        strOutput = str;
        OffSetInputMaxValue = str.Count - 1;
        for (int i = 0; i < 4; i++)
        {
            if (i < str.Count)
            {
                TracksToSelect[i].enabled = true;
                TracksToSelect[i].image.color = new Color(255, 255, 255, 255);
                TracksToSelectText[i].color = new Color(255, 255, 255, 255);
                TracksToSelectText[i].text = str[i + OffSetInputValue];
            }
            else
            {
                TracksToSelect[i].enabled = false;
                TracksToSelectText[i].color = new Color(0, 0, 0, 0);
                TracksToSelect[i].image.color = new Color(0, 0, 0, 0);
            }
        }
    }

    public void DeleteTheTrack(int ButtonIndex)
    {
        string fileName = str[ButtonIndex + OffSetInputValue];
        Debug.Log(fileName);
        SaveSystem.DeleteTrack(fileName);
        UpdateNames();
    }

    public void AddOffSet()
    {
        if(OffSetInputValue < OffSetInputMaxValue)
        {
            OffSetInputValue++;
        }
        Scroll();
    }
    public void SubOffSet()
    {
        if(OffSetInputValue > 0)
        {
            OffSetInputValue--;
        }
        Scroll();
    }
}
