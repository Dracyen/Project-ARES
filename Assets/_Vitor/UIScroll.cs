using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScroll : MonoBehaviour
{
    public GameObject[] Sectors;

    private int index;

    private void Awake()
    {
        index = 0;
        Show();
    }

    public void Next()
    {
        if(index < Sectors.Length - 1)
        {
            index++;
        }
        else
        {
            index = 0;
        }

        Show();
    }

    public void Previous()
    {
        if (index > 0)
        {
            index--;
        }
        else
        {
            index = Sectors.Length - 1;
        }

        Show();
    }

    public void Show()
    {
        foreach (GameObject Sector in Sectors)
        {
            Sector.SetActive(false);
        }

        Sectors[index].SetActive(true);
    }
}
