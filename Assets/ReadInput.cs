using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadInput : MonoBehaviour
{
    public GameObject canvas;
    public Vector2[] Positions = new Vector2[30];
    int index = 0;
    public int width;
    public int height;

    public int GridSize;

    public int[] Results = new int[30];

    public ADD put;
    // Start is called before the first frame update
    void Start()
    {
        Positions = new Vector2[40];
        Results = new int[40];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("Click");
            Positions[index] = Input.mousePosition;
            index++;
        }
        width = Screen.width;
        height = Screen.height;

        if (Input.GetKeyDown(KeyCode.A))
        {

            Read();
            canvas.SetActive(false);
        }
    }

    private void Read()
    {
        for (int i = 0; i < Positions.Length; i++)
        {
            if (Positions[i].x < width / 10)
            {
                Positions[i].x = width / 10;
            }
            if (Positions[i].x < (width / 10) * 2 && Positions[i].x > width / 10)
            {
                Positions[i].x = (width / 10) * 2;
            }
            if (Positions[i].x < (width / 10) * 3 && Positions[i].x > (width / 10) * 2)
            {
                Positions[i].x = (width / 10) * 3;
            }
            if (Positions[i].x < (width / 10) * 4 && Positions[i].x > (width / 10) * 3)
            {
                Positions[i].x = (width / 10) * 4;
            }
            if (Positions[i].x < (width / 10) * 5 && Positions[i].x > (width / 10) * 4)
            {
                Positions[i].x = (width / 10) * 5;
            }
            if (Positions[i].x < (width / 10) * 6 && Positions[i].x > (width / 10) * 5)
            {
                Positions[i].x = (width / 10) * 6;
            }
            if (Positions[i].x < (width / 10) * 7 && Positions[i].x > (width / 10) * 6)
            {
                Positions[i].x = (width / 10) * 7;
            }
            if (Positions[i].x < (width / 10) * 8 && Positions[i].x > (width / 10) * 7)
            {
                Positions[i].x = (width / 10) * 8;
            }
            if (Positions[i].x < (width / 10) * 9 && Positions[i].x > (width / 10) * 8)
            {
                Positions[i].x = (width / 10) * 9;
            }
            if (Positions[i].x < (width / 10) * 10 && Positions[i].x > (width / 10) * 9)
            {
                Positions[i].x = (width / 10) * 10;
            }
        }
        for (int i = 0; i < Positions.Length; i++)
        {
            if (Positions[i].y < height / 10)
            {
                Positions[i].y = height / 10;
            }
            if (Positions[i].y < (height / 10) * 2 && Positions[i].y > height / 10)
            {
                Positions[i].y = (height / 10) * 2;
            }
            if (Positions[i].y < (height / 10) * 3 && Positions[i].y > (height / 10) * 2)
            {
                Positions[i].y = (height / 10) * 3;
            }
            if (Positions[i].y < (height / 10) * 4 && Positions[i].y > (height / 10) * 3)
            {
                Positions[i].y = (height / 10) * 4;
            }
            if (Positions[i].y < (height / 10) * 5 && Positions[i].y > (height / 10) * 4)
            {
                Positions[i].y = (height / 10) * 5;
            }
            if (Positions[i].y < (height / 10) * 6 && Positions[i].y > (height / 10) * 5)
            {
                Positions[i].y = (height / 10) * 6;
            }
            if (Positions[i].y < (height / 10) * 7 && Positions[i].y > (height / 10) * 6)
            {
                Positions[i].y = (height / 10) * 7;
            }
            if (Positions[i].y < (height / 10) * 8 && Positions[i].y > (height / 10) * 7)
            {
                Positions[i].y = (height / 10) * 8;
            }
            if (Positions[i].y < (height / 10) * 9 && Positions[i].y > (height / 10) * 8)
            {
                Positions[i].y = (height / 10) * 9;
            }
            if (Positions[i].y < (height / 10) * 10 && Positions[i].y > (height / 10) * 9)
            {
                Positions[i].y = (height / 10) * 10;
            }
        }
        translate();
    }

    void translate()
    {
        for (int i = 0; i < Positions.Length; i++)
        {
            if (i > 1)
            {
               /* Debug.Log("Positions[i].x" + Positions[i].x);
                Debug.Log("Positions[i-2].x" + Positions[i-2].x);

                Debug.Log("Positions[i].y = " + Positions[i].y);
                Debug.Log("Positions[i-2].y = " + Positions[i - 2].y);
               */


                Results[i] = 0;
                if(Positions[i].x == Positions[i-2].x || Positions[i].y == Positions[i - 2].y)
                {
                    Results[i] = 0;
                    //Debug.Log("0");
                }
                if(Positions[i].x != Positions[i - 2].x && Positions[i].y != Positions[i - 2].y)
                {
                    if (Positions[i].x > Positions[i-1].x && Positions[i].x > Positions[i-2].x && Positions[i].y == Positions[i-1].y && Positions[i].y > Positions[i-2].y)
                    {
                        Debug.Log("Vem d baixo e vai pra direita");
                        Results[i] = 1;
                    }
                    if (Positions[i].x > Positions[i-1].x && Positions[i].x > Positions[i-2].x && Positions[i].y == Positions[i-1].y && Positions[i].y < Positions[i-2].y)
                    {                      
                        Debug.Log("Vem d cima e vai pra direita");
                        Results[i] = 1;
                    }
                    if (Positions[i].x == Positions[i-1].x && Positions[i].x < Positions[i-2].x && Positions[i].y < Positions[i-1].y && Positions[i].y < Positions[i-2].y)
                    {

                        Debug.Log("Vem da direita e vai pra baixo");
                        Results[i] = 2;
                    }
                    if (Positions[i].x == Positions[i-1].x && Positions[i].x > Positions[i-2].x && Positions[i].y > Positions[i-1].y && Positions[i].y > Positions[i-2].y)
                    {
                        Debug.Log("Vem da esquerda e vai pra cima");
                        Results[i] = 1;
                    }
                    if (Positions[i].x == Positions[i-1].x && Positions[i].x > Positions[i-2].x && Positions[i].y < Positions[i-1].y && Positions[i].y < Positions[i-2].y)
                    {

                        Debug.Log("Vem da esquerda e vai pra baixo");
                        Results[i] = 2;
                    }
                    if (Positions[i].x == Positions[i-1].x && Positions[i].x < Positions[i-2].x && Positions[i].y > Positions[i-1].y && Positions[i].y > Positions[i-2].y)
                    {
                        Debug.Log("Vem da direita e vai pra cima");
                        Results[i] = 1;
                    }
                    if (Positions[i].x < Positions[i-1].x && Positions[i].x < Positions[i-2].x && Positions[i].y == Positions[i-1].y && Positions[i].y > Positions[i-2].y)
                    {
                        Debug.Log("Vem de baixo e vai pra esquerda");
                        Results[i] = 2;
                    }
                    if (Positions[i].x < Positions[i-1].x && Positions[i].x < Positions[i-2].x && Positions[i].y == Positions[i - 1].y && Positions[i].y < Positions[i-2].y)
                    {
                        Debug.Log("Vem de cima e vai pra esquerda");
                        Results[i] = 2;
                    }
                }
                
            }

        }
        put.Place();
    }
}
