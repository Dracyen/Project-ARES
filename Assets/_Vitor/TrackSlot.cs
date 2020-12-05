using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSlot : MonoBehaviour
{
    enum State { EMPTY, FULL }

    State CurrentState;

    bool hasPiece = false;

    GameObject Piece;

    public SlotSize _mesh;

    TrackSlot()
    {
        hasPiece = false;
        CurrentState = State.EMPTY;
    }

    public void ClickAction(Object piece)
    {
        if(hasPiece)
        {
            DeletePiece();
        }
        else
        {
            PlacePiece(piece);
        }

        Debug.Log(piece.name);
    }

    public void SetPosition(float coordX, float coordY)
    {
        transform.localPosition = new Vector3(coordX, transform.position.y, coordY);

        CurrentState = State.EMPTY;
    }

    void PlacePiece(Object piece)
    {
        CurrentState = State.FULL;
    }

    void DeletePiece()
    {
        Destroy(Piece);
        hasPiece = false;
        CurrentState = State.EMPTY;
    }
}
