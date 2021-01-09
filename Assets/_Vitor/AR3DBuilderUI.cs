using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AR3DBuilderUI : MonoBehaviour
{
    public GameObject[] Tiles;

    public GameObject MainPanel;

    public GameObject[] SHButtons;

    public GameObject BuilderCanvas;
    public GameObject TesterCanvas;

    public GameObject TrackCreator;

    public Text GridSizeDisplay;

    private void Start()
    {
        foreach (GameObject Tab in Tiles)
        {
            Tab.SetActive(false);
        }

        Tiles[0].SetActive(true);
    }

    public void UpOne()
    {
        int i = System.Int32.Parse(GridSizeDisplay.text);

        if (i < 20)
        {
            i++;

            GridSizeDisplay.text = i.ToString();
        }
    }

    public void DownOne()
    {
        int i = System.Int32.Parse(GridSizeDisplay.text);

        if (i > 10)
        {
            i--;

            GridSizeDisplay.text = i.ToString();
        }
    }

    public void Show(int i)
    {
        if (TrackCreator.activeSelf)
        {
            foreach (GameObject Tab in Tiles)
            {
                Tab.SetActive(false);
            }

            Tiles[i].SetActive(true);
        }
    }

    public void ShowPanel()
    {
        if (TrackCreator.activeSelf)
        {
            MainPanel.SetActive(true);

            SHButtons[0].SetActive(true);
            SHButtons[1].SetActive(false);
        }
    }

    public void HidePanel()
    {
        if (TrackCreator.activeSelf)
        {
            MainPanel.SetActive(false);

            SHButtons[0].SetActive(false);
            SHButtons[1].SetActive(true);
        }
    }

    public void GoBack(int i)
    {
        if (TrackCreator.activeSelf)
        {
            SceneManager.LoadScene(i);
        }
    }

    public void TestTrack()
    {
        if (TrackCreator.activeSelf)
        {
            BuilderCanvas.SetActive(!BuilderCanvas.activeSelf);
            TesterCanvas.SetActive(!TesterCanvas.activeSelf);
        }
    }
}