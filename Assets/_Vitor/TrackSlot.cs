using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackSlot : MonoBehaviour
{
    public enum State { EMPTY, READY, FULL }

    public string Sufix = "(Clone)";

    public State CurrentState { get; private set; }

    public TrackBuilder Grid;

    bool hasPiece = false;

    public GameObject Piece { get; private set; }

    public SlotSize Slot;

    public Vector2 pos;

    bool multiTile = false;

    TrackPicker.TrackInfo Holder;

    public Text _display;

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

        CurrentState = State.EMPTY;
    }

    public void SetMultiFull()
    {
        multiTile = true;
        CurrentState = State.FULL;

        Slot.mesh.gameObject.SetActive(false);
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

        if(Piece != null)
        {
            Destroy(Piece);
        }

        Slot.mesh.gameObject.SetActive(true);
        hasPiece = false;
        CurrentState = State.READY;
    }


    // MULTI PIECE CODE

    public void ClickAction(TrackPicker.TrackInfo piece, int button, Text it)
    {
        _display = it;

        if (CurrentState == State.FULL && !multiTile)
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

            PlacePiece(Holder);
        }
    }

    void PlacePiece(TrackPicker.TrackInfo piece)
    {
        //Holder = piece;

        int i = 0;

        if (Grid.CheckSlots(pos, Holder.OriginalSquares))
        {
            Slot.mesh.gameObject.SetActive(false);
            Piece = (GameObject)Instantiate(piece.Mesh, transform);

            _display.text = "Gonna Full";

            Grid.SetFull(pos, Holder.OriginalSquares);
            hasPiece = true;
            CurrentState = State.FULL;

            _display.text = "Is Full";
        }
        else
        {
            if(i < 4)
            {
                RotatePiece(Holder);
                i++;

                PlacePiece(Holder);
            }
            else
            {
                DeletePiece(Holder);

                Debug.LogError("No space in the grid.");
            }
        }
    }

    void RotatePiece(TrackPicker.TrackInfo piece) //Needs Work
    {
        Piece.transform.Rotate(new Vector3(0, 90, 0), Space.Self);

        switch (piece.CurrentRotation)
        {
            case TrackPicker.TrackInfo.Rotation.NORTH:
                for (int i = 0; i < piece.CurrentSquares.Length; i++)
                {
                    Holder.Rotate(TrackPicker.TrackInfo.Rotation.EAST);
                }
                break;

            case TrackPicker.TrackInfo.Rotation.WEST:
                for (int i = 0; i < piece.CurrentSquares.Length; i++)
                {
                    Holder.Rotate(TrackPicker.TrackInfo.Rotation.NORTH);
                }
                break;

            case TrackPicker.TrackInfo.Rotation.EAST:
                for (int i = 0; i < piece.CurrentSquares.Length; i++)
                {
                    Holder.Rotate(TrackPicker.TrackInfo.Rotation.SOUTH);
                }
                break;

            case TrackPicker.TrackInfo.Rotation.SOUTH:
                for (int i = 0; i < piece.CurrentSquares.Length; i++)
                {
                    Holder.Rotate(TrackPicker.TrackInfo.Rotation.WEST);
                }
                break;
        }

    }

    void DeletePiece(TrackPicker.TrackInfo piece)
    {
        if (Piece.name == "Start" + Sufix)
        {
            Grid.SwitchStart();
        }

        if (Piece != null)
        {
            Destroy(Piece);
        }

        Grid.SetEmpty(pos, Holder.OriginalSquares);

        Slot.mesh.gameObject.SetActive(true);
        hasPiece = false;
        CurrentState = State.READY;
    }
}
