using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavedTracks_UIManager : MonoBehaviour
{
    public Button[] TracksToSelect = new Button[5];
    public Text[] TracksToSelectText = new Text[5];
    List<string> str = new List<string>();
    List<string> strOutput = new List<string>();
    string newTrackname;
    int indexOfTheEdit;
    public GameObject NameEditor;
    public InputField inputNameLoad;
    public string selectedTrack;

    public Slider OffSetInput;
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
        OffSetInput.maxValue = str.Count - TracksToSelect.Length;
        for (int i = 0; i < TracksToSelect.Length; i++)
        {
            if (i < str.Count)
            {
                TracksToSelect[i].enabled = true;
                TracksToSelect[i].image.color = new Color(255, 255, 255, 255);
                TracksToSelectText[i].color = new Color(0, 0, 0, 255);
                TracksToSelectText[i].text = str[i + (int)OffSetInput.value];
                Debug.Log(str[i + (int)OffSetInput.value]);
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
        string newfile = newTrackname;
        string fileName = str[indexOfTheEdit + (int)OffSetInput.value];
        Debug.Log(newfile);
        SaveSystem.RenameTrack(fileName, newfile);
        str[indexOfTheEdit + (int)OffSetInput.value] = newTrackname;
        inputNameLoad.text = " ";
        UpdateNames();
    }
    public void UpdateNames()
    {
        str = SaveSystem.LoadTrackNames();
        strOutput = str;
        OffSetInput.maxValue = str.Count - 1;
        for (int i = 0; i < TracksToSelect.Length; i++)
        {
            if (i < str.Count)
            {
                TracksToSelect[i].enabled = true;
                TracksToSelect[i].image.color = new Color(255, 255, 255, 255);
                TracksToSelectText[i].color = new Color(0, 0, 0, 255);
                TracksToSelectText[i].text = str[i + (int)OffSetInput.value];
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
        string fileName = str[ButtonIndex + (int)OffSetInput.value];
        Debug.Log(fileName);
        SaveSystem.DeleteTrack(fileName);
        UpdateNames();
    }
}
