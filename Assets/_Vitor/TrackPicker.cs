using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackPicker : MonoBehaviour
{
    public Text _display;

    public Object[] _prefabs;

    int _selected = 0;
    
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            if (_selected < _prefabs.Length - 1)
            {
                _selected++;

                _display.text = _selected.ToString();
            }
            else
            {
                _selected = 0;

                _display.text = _selected.ToString();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit rayHit;

            if (Physics.Raycast(ray, out rayHit, 100.0f))
            {
                if (rayHit.collider.tag == "Slot")
                {
                    TrackSlot _target;

                    _target = rayHit.collider.gameObject.GetComponent<TrackSlot>();

                    _target.ClickAction(_prefabs[_selected], 0);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit rayHit;

            if (Physics.Raycast(ray, out rayHit, 100.0f))
            {
                if (rayHit.collider.tag == "Slot")
                {
                    TrackSlot _target;

                    _target = rayHit.collider.gameObject.GetComponent<TrackSlot>();

                    _target.ClickAction(_prefabs[_selected], 1);
                }
            }
        }
    }

    public void SwitchPiece(int piece)
    {

    }
}
