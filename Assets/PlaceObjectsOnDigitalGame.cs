using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjectsOnDigitalGame : MonoBehaviour
{
    int rand = 0;
    int last = 1;
    int lastStreat;
    Vector3 lastPos = new Vector3(0, 0, 0);
    Vector3 newPos = new Vector3(0, 0, 0);
    public GameObject[] ObjectsToPlace;
    public GameObject YourCar;
    public GameObject AICAR;
    bool rightAndUp = true;
    float distance = 4.5f;
    Vector3 startPos;
    public int direction = 1;

    GameObject[] IndividualTracks;
    int IndividualTracksIndex = 0;

    int trackLenght;
    public int[] course;
    ReadInput Input;
    // Start is called before the first frame update
    void Awake()
    {
        
        Input = GameObject.FindGameObjectWithTag("Input").GetComponent<ReadInput>();
        course = new int[Input.numOfTracks];
        course = Input.Results;
        // rand = 0 mantem a reta como esta
        // rand = 1 curva (1,-1)  quando vertical vira pra esquerda quando horizontal vira pra cima
        // rand = 2 curva (2,-2)  quando vertical vira pra direita quando horizontal vira pra baixo
        trackLenght = course.Length + 1;


        IndividualTracks = new GameObject[trackLenght];
        startPos = transform.position;
        lastPos = startPos;
        // Instancia o Inicio
        Instantiate(ObjectsToPlace[3], new Vector3(0, 0, 0) + startPos, Quaternion.identity);


        decifre();


        Instantiate(YourCar, new Vector3(0, 0.5f, -1) + startPos, Quaternion.Euler(0, 90, 0));
        for (int i = 0; i < 3; i++)
        {
            Instantiate(AICAR, new Vector3(0, 0.5f, i) + startPos, Quaternion.identity);
        }
        CheckForOverLaps();
    }

    // Update is called once per frame
    void ColocarCurvaDe90(int direcao, Vector3 toAdd, int ultimaReta)
    {
        //Debug.Log("PreCurva " + direction);
        //Debug.Log("Curva direcao " + direcao);
        //Debug.Log("ultimaReta " + ultimaReta);

        if (direcao == 1 || direcao == -1)
        {
            if (direction == -1)
            {
                direcao = 0;
            }
            if (ultimaReta == 1)
            {
                direcao = -1;
            }
            if (direction == -1)
            {
                if (ultimaReta == 1)
                {
                    direcao = 0;
                }
            }

            direction = 1;
        }

        if (direcao == 2 || direcao == -2)
        {
            if (direction == 1)
            {
                if (ultimaReta == 0)
                {
                    Debug.Log(direcao);
                    direcao = 2;
                }
                if (ultimaReta == 1)
                {
                    Debug.Log("d-2");
                    direcao = -2;
                }

            }

            if (direction == -1)
            {
                if (ultimaReta == 1)
                {
                    Debug.Log("d1");
                    direcao = 1;
                }
                if (ultimaReta == 0)
                {
                    Debug.Log("d-1");
                    direcao = -1;
                }
            }
            direction = -1;
        }

        //Debug.Log("Curva direcao Pos Ajuste " + direction);
        newPos = lastPos + toAdd;
        IndividualTracks[IndividualTracksIndex] = Instantiate(ObjectsToPlace[2], newPos, Quaternion.Euler(0, 90 * direcao, 0));
        IndividualTracksIndex++;
        lastPos = newPos;
        last = 2;
    }

    void ColocarHorizontais(int direcao, Vector3 toAdd)
    {
        //Debug.Log("Reta direcao " + direcao);
        newPos = lastPos + toAdd;
        IndividualTracks[IndividualTracksIndex] = Instantiate(ObjectsToPlace[0], newPos, Quaternion.identity);
        IndividualTracksIndex++;
        lastPos = newPos;
        lastStreat = 0;
        last = 0;
    }

    void ColocarVerticais(int direcao, Vector3 toAdd)
    {
        //Debug.Log("Reta direcao " + direcao);
        newPos = lastPos + toAdd;
        IndividualTracks[IndividualTracksIndex] = Instantiate(ObjectsToPlace[1], newPos, Quaternion.identity);
        IndividualTracksIndex++;
        lastPos = newPos;
        lastStreat = 1;
        last = 1;
    }

    void CheckForOverLaps()
    {
        for (int i = 0; i < IndividualTracks.Length; i++)
        {

            for (int z = 0; z < IndividualTracks.Length; z++)
            {

                if (IndividualTracks[i].transform.position == IndividualTracks[z].transform.position && i != z)
                {

                }
            }

        }
    }

    void decifre()
    {
        for (int i = 0; i < course.Length; i++)
        {

            //rand = (int) Random.Range(0, 10);
            rand = course[i];

            // Se a ultima foi uma pista horizontal   
            if (last == 0)
            {

                // Instancia a "distance" de distancia uma nova pista horizontal
                if (rand == 0)
                {

                    ColocarHorizontais(direction, new Vector3(0, 0, distance * direction));
                }
                // Instancia a "distance" de distancia uma nova curva de 90
                if (rand > 0)
                {
                    if (rand == 1)
                    {

                        ColocarCurvaDe90(1, new Vector3(0, 0, distance * direction), lastStreat);
                    }
                    if (rand == 2)
                    {

                        ColocarCurvaDe90(2, new Vector3(0, 0, distance * direction), lastStreat);
                    }
                }
            }



            else if (last == 2)
            {

                if (lastStreat == 0)
                {

                    ColocarVerticais(direction, new Vector3(distance * direction, 0, 0));
                }
                else if (lastStreat == 1)
                {

                    ColocarHorizontais(direction, new Vector3(0, 0, distance * direction));
                }
            }




            else if (last == 1)
            {

                if (rand == 0)
                {

                    ColocarVerticais(direction, new Vector3(distance * direction, 0, 0));

                }

                if (rand > 0)
                {
                    if (rand == 1)
                    {

                        ColocarCurvaDe90(-1, new Vector3(distance * direction, 0, 0), lastStreat);
                    }
                    if (rand == 2)
                    {

                        ColocarCurvaDe90(-2, new Vector3(distance * direction, 0, 0), lastStreat);
                    }
                }
            }

        }
    }
}
