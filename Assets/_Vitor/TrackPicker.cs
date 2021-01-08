﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackPicker : MonoBehaviour
{
    public Text _display;

    //public Text _display2;

    public TrackInfo[] _prefabs;

    int _selected = 0;

    public void InteractPiece(int option)
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        RaycastHit rayHit;

        if (Physics.Raycast(ray, out rayHit, 100.0f))
        {
            if (rayHit.collider.tag == "Slot")
            {
                TrackSlot _target;

                if (_prefabs[_selected].multiTile)
                {
                    //Do Multi Tile Action = Send TrackInfo

                    _target = rayHit.collider.gameObject.GetComponent<TrackSlot>();

                    _target.ClickAction(_prefabs[_selected], option);
                }
                else
                {
                    //Do Single Tile Action = Send TrackInfo Mesh
                    _target = rayHit.collider.gameObject.GetComponent<TrackSlot>();

                    _target.ClickAction(_prefabs[_selected].Mesh, option);
                }
            }
        }
    }

    public void SwitchPiece(int piece)
    {
        _selected = piece;

        Debug.Log("Piece Number: " + _selected.ToString());
    }

    [System.Serializable]
    public struct TrackInfo
    {
        public string Name;

        public int index;

        public Object Mesh;

        public Vector2[] OriginalSquares;

        public Vector2[] CurrentSquares;

        public bool multiTile;

        public float pieceRotation;

        public enum Rotation { NORTH, WEST, EAST, SOUTH }

        public Rotation CurrentRotation;

        public void Rotate(Rotation rot)
        {
            Debug.Log("Struct - Rotate " + rot);

            CurrentRotation = rot;

            switch (CurrentRotation) //Original East - X:1 Y:3
            {
                case Rotation.NORTH:
                    for (int i = 0; i < CurrentSquares.Length; i++) // X:-3 Y:1
                    {
                        CurrentSquares[i] = new Vector2(Mathf.Abs(OriginalSquares[i].y) * -1, OriginalSquares[i].x);
                        pieceRotation = 270;
                    }
                    break;

                case Rotation.WEST:
                    for (int i = 0; i < CurrentSquares.Length; i++) // X:-1 Y:-3
                    {
                        CurrentSquares[i] = new Vector2(Mathf.Abs(OriginalSquares[i].x) * -1, Mathf.Abs(OriginalSquares[i].y) * -1);
                        pieceRotation = 180;
                    }
                    break;

                case Rotation.EAST:
                    for (int i = 0; i < CurrentSquares.Length; i++) // X:1 Y:3
                    {
                        CurrentSquares[i] = new Vector2(OriginalSquares[i].x, OriginalSquares[i].y);
                        pieceRotation = 0;
                    }
                    break;

                case Rotation.SOUTH:
                    for (int i = 0; i < CurrentSquares.Length; i++) // X:3 Y:-1
                    {
                        CurrentSquares[i] = new Vector2(OriginalSquares[i].y, Mathf.Abs(OriginalSquares[i].x) * -1);
                        pieceRotation = 90;
                    }
                    break;
            }

            Debug.Log("Struct - " + CurrentRotation + " / " + pieceRotation);
        }
    }
}