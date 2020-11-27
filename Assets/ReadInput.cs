using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadInput : MonoBehaviour
{
    public GameObject canvas;
    public Vector2[] Positions;
    int index = 0;
    public int width;
    public int height;

    

    public int[] Results;

    public ADD put;

    public int subDivisions;
    public GameObject Dots;

    public Transform Canvas;

    GameObject[] trackDrawing;
    public int indexDots = 0;

    public int numOfTracks;
    // Start is called before the first frame update
    private void Awake()
    {

        width = Screen.width;
        height = Screen.height;

        trackDrawing = new GameObject[subDivisions * subDivisions];
        
        //Debug.Log("TESTE1");
        for (int x = (width / subDivisions); x < width - (width / subDivisions) * 2; x += width / subDivisions)
        {
            for (int y = (height / subDivisions); y < height - (height / subDivisions) * 2; y += height / subDivisions)
            {

                trackDrawing[indexDots] = Instantiate(Dots, new Vector3(x+(width / subDivisions)/2, y + (height / subDivisions) / 2, 1), Quaternion.identity, Canvas);
                trackDrawing[indexDots].transform.localScale = new Vector3((width / subDivisions) - 1, (height / subDivisions)-1, 1);
                if(x < (width - (width / subDivisions) * 2) - 1)
                {
                    indexDots++;
                }
                
                
            }
        }
    }

    void Start()
    {
        Positions = new Vector2[numOfTracks];
        Results = new int[numOfTracks];

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (index == 0)
            {


                Positions[index] = Input.mousePosition;
                changeColor(Positions[index], trackDrawing);

                index++;
            }

            if(Vector2.Distance(Positions[index-1],Input.mousePosition) > (height / subDivisions) / 2.5f)
            {
                FarDots();
            }
            else
            {
                Debug.Log("f");
                Positions[index] = Input.mousePosition;
                changeColor(Positions[index], trackDrawing);
                index++;
            }
  

        }

        // Debug.Log("Click");
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            FarDots();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

            //Debug.Log("Click");
            Positions[index] = Input.mousePosition;
            changeColor(Positions[index], trackDrawing);
            index++;
        }

        width = Screen.width;
        height = Screen.height;

        if (Input.GetKeyDown(KeyCode.A))
        {

            Debug.Log("Click");
            Read();
            canvas.SetActive(false);
        }
    }

    private void Read()
    {
        for (int i = 0; i < Positions.Length; i++)
        {
            for (int sub = 1; sub < subDivisions; sub++)
            {
                
                if (Positions[i].x < (width / subDivisions) * (sub+1) && Positions[i].x > (width / subDivisions) * sub)
                {
                    Positions[i].x = (width / subDivisions) * (sub+1);
                }
            }
        }

        for (int i = 0; i < Positions.Length; i++)
        {
            for (int sub = 1; sub < subDivisions; sub++)
            {

                if (Positions[i].y < (height / subDivisions) * (sub + 1) && Positions[i].y > (height / subDivisions) * sub)
                {
                    Positions[i].y = (height / subDivisions) * (sub + 1);
                }
            }
        }
        for (int i = 0; i < Positions.Length; i++)
        {
            Debug.Log(Positions[i]);
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
                if (Positions[i].x == Positions[i - 2].x || Positions[i].y == Positions[i - 2].y)
                {
                    Results[i] = 0;
                    //Debug.Log("0");
                }
                if (Positions[i].x != Positions[i - 2].x && Positions[i].y != Positions[i - 2].y)
                {
                    if (Positions[i].x > Positions[i - 1].x && Positions[i].x > Positions[i - 2].x && Positions[i].y == Positions[i - 1].y && Positions[i].y > Positions[i - 2].y)
                    {
                        Debug.Log("Vem d baixo e vai pra direita");
                        Results[i] = 1;
                    }
                    if (Positions[i].x > Positions[i - 1].x && Positions[i].x > Positions[i - 2].x && Positions[i].y == Positions[i - 1].y && Positions[i].y < Positions[i - 2].y)
                    {
                        Debug.Log("Vem d cima e vai pra direita");
                        Results[i] = 1;
                    }
                    if (Positions[i].x == Positions[i - 1].x && Positions[i].x < Positions[i - 2].x && Positions[i].y < Positions[i - 1].y && Positions[i].y < Positions[i - 2].y)
                    {

                        Debug.Log("Vem da direita e vai pra baixo");
                        Results[i] = 2;
                    }
                    if (Positions[i].x == Positions[i - 1].x && Positions[i].x > Positions[i - 2].x && Positions[i].y > Positions[i - 1].y && Positions[i].y > Positions[i - 2].y)
                    {
                        Debug.Log("Vem da esquerda e vai pra cima");
                        Results[i] = 1;
                    }
                    if (Positions[i].x == Positions[i - 1].x && Positions[i].x > Positions[i - 2].x && Positions[i].y < Positions[i - 1].y && Positions[i].y < Positions[i - 2].y)
                    {

                        Debug.Log("Vem da esquerda e vai pra baixo");
                        Results[i] = 2;
                    }
                    if (Positions[i].x == Positions[i - 1].x && Positions[i].x < Positions[i - 2].x && Positions[i].y > Positions[i - 1].y && Positions[i].y > Positions[i - 2].y)
                    {
                        Debug.Log("Vem da direita e vai pra cima");
                        Results[i] = 1;
                    }
                    if (Positions[i].x < Positions[i - 1].x && Positions[i].x < Positions[i - 2].x && Positions[i].y == Positions[i - 1].y && Positions[i].y > Positions[i - 2].y)
                    {
                        Debug.Log("Vem de baixo e vai pra esquerda");
                        Results[i] = 2;
                    }
                    if (Positions[i].x < Positions[i - 1].x && Positions[i].x < Positions[i - 2].x && Positions[i].y == Positions[i - 1].y && Positions[i].y < Positions[i - 2].y)
                    {
                        Debug.Log("Vem de cima e vai pra esquerda");
                        Results[i] = 2;
                    }
                }

            }

        }

        put.Place();
    }


    void FarDots()
    {
        float CountX = 0;
        float CountY = 0;
        int XD = 1;
        int YU = 1;
        if (index > 0)
        {
            if (Positions[index - 1].x > Input.mousePosition.x)
            {
                CountX = (int)(Positions[index - 1].x - Input.mousePosition.x);
                XD = -1;
            }
            else if (Positions[index - 1].x < Input.mousePosition.x)
            {
                CountX = (int)(Input.mousePosition.x - Positions[index - 1].x);
            }
            else if (Positions[index - 1].x == Input.mousePosition.x)
            {
                CountX = 0;
            }

            if (Positions[index - 1].y > Input.mousePosition.y)
            {
                CountY = (int)(Positions[index - 1].y - Input.mousePosition.y);
                YU = -1;
            }
            else if (Positions[index - 1].y < Input.mousePosition.y)
            {
                CountY = (int)(Input.mousePosition.y - Positions[index - 1].y);
            }
            else if (Positions[index - 1].y == Input.mousePosition.y)
            {
                CountY = 0;
            }


            if (CountX / (width / subDivisions) - (int)(CountX / (width / subDivisions)) > 0.5f)
            {
                CountX = (int)(CountX / (width / subDivisions)) + 1;

            }
            else if (CountX / (width / subDivisions) - (int)(CountX / (width / subDivisions)) < 0.5f)
            {

                CountX = (int)(CountX / (width / subDivisions));
            }
            if (CountY / (height / subDivisions) - (int)(CountY / (height / subDivisions)) > 0.5f)
            {
                CountY = (int)(CountY / (height / subDivisions)) + 1;
            }
            else if (CountY / (height / subDivisions) - (int)(CountY / (height / subDivisions)) < 0.5f)
            {
                CountY = (int)(CountY / (height / subDivisions));
            }

            Vector3 Inp;
            //
            Inp = Input.mousePosition;
            for (int x = 0; x < CountX; x++)
            {
                
                Positions[index + x].y = Positions[index - 1].y;
                Positions[index + x].x = (Input.mousePosition.x - ((width / subDivisions) * (CountX - x)) * XD);
                
                changeColor(Positions[index + x], trackDrawing);
                
            }
            index += (int)CountX;
            for (int y = 0; y < CountY; y++)
            {
                Positions[index + y].x = Positions[(int)(index - CountX)].x;
                Positions[index + y].y = (Input.mousePosition.y - ((height / subDivisions) * (CountY - y)) * YU);
                Debug.Log(".");
                changeColor(Positions[index + y], trackDrawing);
            }
            for (int i = 0; i < CountY + CountX; i++)
            {
                Debug.Log(Positions[i]);

            }
            index += (int)CountY;
            
            //Read();
            //
        }

        if (index == 0)
        {
            Positions[index] = Input.mousePosition;
            index++;
        }




    }

    void changeColor(Vector2 Pos, GameObject[] TilesPos)
    {
       


        for (int i = 0; i < indexDots; i++)
        {
           
            

            

            if (Vector2.Distance(TilesPos[i].transform.position, Pos) < (height / subDivisions)/2.5f)
            {
                TilesPos[i].GetComponent<Image>().color = Color.black;
            }
                    
                
            
            
        }
    }
}

