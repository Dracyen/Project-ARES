using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackPicker : MonoBehaviour
{
    public Text _display;

    public Text _display2;

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

                    _target.ClickAction(_prefabs[_selected], option, _display2);
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

        _display.text = _selected.ToString();
    }

    [System.Serializable]
    public struct TrackInfo
    {
        public string Name;

        public Object Mesh;

        public Vector2[] OriginalSquares;

        public Vector2[] CurrentSquares { get; private set; }

        public bool multiTile;

        public enum Rotation { NORTH, WEST, EAST, SOUTH }

        public Rotation CurrentRotation;

        public void Rotate(Rotation rot)
        {
            switch (CurrentRotation) //Original East - X:0 Y:3
            {
                case Rotation.NORTH:
                    for (int i = 0; i < CurrentSquares.Length; i++) // X:3 Y:0
                    {
                        CurrentSquares[i] = new Vector2(OriginalSquares[i].y, OriginalSquares[i].x);
                    }
                    break;

                case Rotation.WEST:
                    for (int i = 0; i < CurrentSquares.Length; i++) // X:0 Y:-3
                    {
                        CurrentSquares[i] = new Vector2(OriginalSquares[i].x, Mathf.Abs(OriginalSquares[i].y) * -1);
                    }
                    break;

                case Rotation.EAST:
                    for (int i = 0; i < CurrentSquares.Length; i++) // X:0 Y:3
                    {
                        CurrentSquares[i] = new Vector2(OriginalSquares[i].x, OriginalSquares[i].y);
                    }
                    break;

                case Rotation.SOUTH:
                    for (int i = 0; i < CurrentSquares.Length; i++) // X:-3 Y:0
                    {
                        CurrentSquares[i] = new Vector2(Mathf.Abs(OriginalSquares[i].y) * -1, OriginalSquares[i].x);
                    }
                    break;
            }
        }
    }
}