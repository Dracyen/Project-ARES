using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSlot : MonoBehaviour
{
    public enum State { EMPTY, READY, FULL }

    public string Sufix = "(Clone)";

    public State CurrentState { get; private set; }

    public TrackBuilder Grid;

    bool hasPiece = false;

    GameObject Piece;

    public SlotSize Slot;

    TrackSlot()
    {
        hasPiece = false;
        CurrentState = State.EMPTY;
    }

    public void ClickAction(Object piece, int button)
    {
        if (CurrentState == State.FULL)
        {
            switch (button)
            {
                case 0:
                    if (hasPiece)
                        RotatePiece();
                    break;

                case 1:
                    if (hasPiece)
                        DeletePiece();
                    break;
            }
        }
        else if (CurrentState == State.READY && button != 1)
        {
            if (piece.name == "Start")
            {
                if (!Grid.hasStart)
                {
                    Grid.SwitchStart();

                    PlacePiece(piece);
                }
            }
            else
            {
                PlacePiece(piece);
            }
        }
    }

    public void SetPosition(float coordX, float coordY)
    {
        transform.localPosition = new Vector3(coordX, transform.position.y, coordY);

        CurrentState = State.EMPTY;
    }

    void PlacePiece(Object piece)
    {
        Debug.Log("Place");

        Slot.mesh.gameObject.SetActive(false);
        Piece = (GameObject)Instantiate(piece, transform);
        hasPiece = true;
        CurrentState = State.FULL;
    }

    void RotatePiece() //Needs Work
    {
        Debug.Log("Rotate");

        Piece.transform.Rotate(new Vector3(0, 90, 0), Space.Self);
    }

    void DeletePiece()
    {
        Debug.Log("Delete");

        if(Piece.name == "Start" + Sufix)
        {
            Grid.SwitchStart();
        }

        Destroy(Piece);
        Slot.mesh.gameObject.SetActive(true);
        hasPiece = false;
        CurrentState = State.READY;
    }

    public void SetReady()
    {
        CurrentState = State.READY;
    }
}
