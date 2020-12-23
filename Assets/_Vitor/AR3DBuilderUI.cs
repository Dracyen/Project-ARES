using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class AR3DBuilderUI : MonoBehaviour
{
    public GameObject[] Tabs;
    public GameObject[] Tiles;

    public GameObject MainPanel;

    private void Start()
    {
        Show(0);
    }

    public void Show(int i)
    {
        foreach (GameObject Tab in Tabs)
        {
            Tab.SetActive(false);
        }

        Tabs[i].SetActive(true);
    }

    public void ShowHidePanel()
    {
        MainPanel.SetActive(!MainPanel.activeSelf);
    }

    public void GoBack(int i)
    {
        SceneManager.LoadScene(i);
    }
}