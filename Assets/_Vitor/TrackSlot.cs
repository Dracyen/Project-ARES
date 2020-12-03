using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSlot : MonoBehaviour
{
    enum State { EMPTY, READY, FULL }

    State CurrentState = State.EMPTY;

    bool hasPiece = false;

    GameObject Piece;

    TrackSlot()
    {
        hasPiece = false;
    }

    void ClickAction(/*outerPiece*/)
    {
        if(hasPiece)
        {
            //Ask to delete
        }
        else
        {
            //PlacePiece()
        }

    }

    void PlacePiece()
    {

    }

    void DeletePiece()
    {
        Destroy(Piece);
        hasPiece = false;
    }
}
