using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TrackSlot : MonoBehaviour
{
    public enum State { EMPTY, READY, FULL }

    public string Sufix = "(Clone)";

    public State CurrentState;

    public TrackBuilder Grid;

    public bool hasPiece = false;

    public GameObject Piece;

    public SlotSize Slot;

    public Vector2 pos;

    public bool multiTile = false;

    public TrackPicker.TrackInfo Holder;

    TrackSlot()
    {
        hasPiece = false;
        CurrentState = State.EMPTY;
    }

    public void SetPosition(float coordX, float coordY)
    {
        transform.localPosition = new Vector3(coordX, transform.position.y, coordY);

        DeletePiece();

        //Grid.SwitchStart();
        //Grid.SwitchFinish();

        CurrentState = State.EMPTY;
    }

    public void SetMultiFull()
    {
        multiTile = true;
        CurrentState = State.FULL;

        Debug.Log("x: " + pos.x + " / y: " + pos.y + " MultiTile is true? " + multiTile);

        Slot.mesh.gameObject.SetActive(false);
    }

    public void SetMultiReady()
    {
        multiTile = false;
        CurrentState = State.READY;

        //Debug.Log("x: " + pos.x + " / y: " + pos.y + " MultiTile is False");

        Slot.mesh.gameObject.SetActive(true);
    }

    public void SetReady()
    {
        DeletePiece();
    }

    // SINGLE PIECE CODE

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
            else if(piece.name == "Finish")
            {
                if (!Grid.hasFinish)
                {
                    Grid.SwitchFinish();

                    PlacePiece(piece);
                }
            }
            else
            {
                PlacePiece(piece);
            }
        }
    }

    void PlacePiece(Object piece)
    {
        Slot.mesh.gameObject.SetActive(false);
        Piece = (GameObject)Instantiate(piece, transform);
        hasPiece = true;
        CurrentState = State.FULL;
    }

    void RotatePiece() //Needs Work
    {
        Piece.transform.Rotate(new Vector3(0, 90, 0), Space.Self);
    }

    public void DeletePiece()
    {
        if(Piece != null && Piece.name == "Start" + Sufix)
        {
            Grid.SwitchStart();
        }

        if (Piece != null && Piece.name == "Finish" + Sufix)
        {
            Grid.SwitchFinish();
        }

        if (Piece != null)
        {
            Destroy(Piece);
        }

        Slot.mesh.gameObject.SetActive(true);
        hasPiece = false;
        CurrentState = State.READY;
    }


    // MULTI PIECE CODE

    public void ClickAction(TrackPicker.TrackInfo piece, int button)
    {
        if (CurrentState == State.FULL)
        {
            switch (button)
            {
                case 0:
                    if (hasPiece)
                        RotatePiece(Holder);
                    break;

                case 1:
                    if (hasPiece)
                        DeletePiece(Holder);
                    break;
            }
        }
        else if (CurrentState == State.READY && button != 1)
        {
            Holder = piece;

            if (piece.Mesh.name == "Start")
            {
                if (!Grid.hasStart)
                {
                    Grid.SwitchStart();
                }
            }

            if (piece.Mesh.name == "Finish")
            {
                if (!Grid.hasFinish)
                {
                    Grid.SwitchFinish();
                }
            }

            PlacePiece(Holder);
        }
    }

    void PlacePiece(TrackPicker.TrackInfo piece)
    {
        int i = 0;

        bool o = true;

        while(o)
        {
            if (Grid.CheckSlots(pos, Holder.CurrentSquares))
            {
                SubPlacePiece(piece);

                Debug.Log("PP - Placed the Piece Normally");

                o = false;
            }
            else if (i < 4)
            {
                switch (piece.CurrentRotation)
                {
                    case TrackPicker.TrackInfo.Rotation.NORTH:
                        Holder.Rotate(TrackPicker.TrackInfo.Rotation.EAST);
                        break;

                    case TrackPicker.TrackInfo.Rotation.WEST:
                        Holder.Rotate(TrackPicker.TrackInfo.Rotation.NORTH);
                        break;

                    case TrackPicker.TrackInfo.Rotation.EAST:
                        Holder.Rotate(TrackPicker.TrackInfo.Rotation.SOUTH);
                        break;

                    case TrackPicker.TrackInfo.Rotation.SOUTH:
                        Holder.Rotate(TrackPicker.TrackInfo.Rotation.WEST);
                        break;
                }

                //Piece.transform.eulerAngles = new Vector3(0, Holder.pieceRotation, 0);

                Debug.Log("PP - Rotated the Piece " + i + " times");

                i++;
            }
            else
            {
                DeletePiece(Holder);

                Debug.LogError("PP - No space in the grid.");

                o = false;
            }
        }
        
        Debug.Log("PP - Finished Placing");
    }

    void SubPlacePiece(TrackPicker.TrackInfo piece)
    {
        Slot.mesh.gameObject.SetActive(false);
        Piece = (GameObject)Instantiate(piece.Mesh, transform);

        if (Piece.name == "Ponte" + Sufix)
        {
            switch (Holder.pieceRotation)
            {
                case 0:
                    Piece.transform.localEulerAngles = new Vector3(0, 270, 0);
                    break;

                case 90:
                    Piece.transform.localEulerAngles = new Vector3(0, 0, 0);
                    break;

                case 180:
                    Piece.transform.localEulerAngles = new Vector3(0, 90, 0);
                    break;

                case 270:
                    Piece.transform.localEulerAngles = new Vector3(0, 180, 0);
                    break;
            }
        }
        else
        {
            Piece.transform.localEulerAngles = new Vector3(0, Holder.pieceRotation, 0);
        }

        Grid.SetFull(pos, Holder.CurrentSquares);
        hasPiece = true;
        CurrentState = State.FULL;

        Debug.Log("Slots are Full");
    }

    void RotatePiece(TrackPicker.TrackInfo piece) //Needs Work
    {
        Grid.SetEmpty(pos, Holder.CurrentSquares);

        RotationSwitch(piece);

        if(Grid.CheckSlots(pos, Holder.CurrentSquares) == false)
        {
            while(Grid.CheckSlots(pos, Holder.CurrentSquares) == false)
            {
                RotationSwitch(piece);
            }
        }

        if (Piece.name == "Ponte" + Sufix)
        {
            switch (Holder.pieceRotation)
            {
                case 0:
                    Piece.transform.localEulerAngles = new Vector3(0, 270, 0);
                    break;

                case 90:
                    Piece.transform.localEulerAngles = new Vector3(0, 0, 0);
                    break;

                case 180:
                    Piece.transform.localEulerAngles = new Vector3(0, 90, 0);
                    break;

                case 270:
                    Piece.transform.localEulerAngles = new Vector3(0, 180, 0);
                    break;
            }
        }
        else
        {
            Piece.transform.localEulerAngles = new Vector3(0, Holder.pieceRotation, 0);
        }


        Grid.SetFull(pos, Holder.CurrentSquares);
    }

    void RotationSwitch(TrackPicker.TrackInfo piece)
    {
        switch (piece.CurrentRotation)
        {
            case TrackPicker.TrackInfo.Rotation.NORTH:
                Holder.Rotate(TrackPicker.TrackInfo.Rotation.EAST);
                break;

            case TrackPicker.TrackInfo.Rotation.WEST:
                Holder.Rotate(TrackPicker.TrackInfo.Rotation.NORTH);
                break;

            case TrackPicker.TrackInfo.Rotation.EAST:
                Holder.Rotate(TrackPicker.TrackInfo.Rotation.SOUTH);
                break;

            case TrackPicker.TrackInfo.Rotation.SOUTH:
                Holder.Rotate(TrackPicker.TrackInfo.Rotation.WEST);
                break;
        }
    }

    void DeletePiece(TrackPicker.TrackInfo piece)
    {
        if (Piece.name == "Start" + Sufix)
        {
            Grid.SwitchStart();
        }

        if (Piece.name == "Finish" + Sufix)
        {
            Grid.SwitchFinish();
        }

        if (Piece != null)
        {
            Destroy(Piece);
        }

        Debug.Log("Try Delete");

        Grid.SetEmpty(pos, Holder.CurrentSquares);
        Holder.Rotate(TrackPicker.TrackInfo.Rotation.EAST);

        hasPiece = false;
        CurrentState = State.READY;
    }
}
