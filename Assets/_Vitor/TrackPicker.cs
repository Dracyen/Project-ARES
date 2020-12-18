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
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        RaycastHit rayHit;
    }

    public void InteractPiece(int option)
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        RaycastHit rayHit;

        if (Physics.Raycast(ray, out rayHit, 100.0f))
        {
            if (rayHit.collider.tag == "Slot")
            {
                TrackSlot _target;

                _target = rayHit.collider.gameObject.GetComponent<TrackSlot>();

                _target.ClickAction(_prefabs[_selected], option);
            }
        }
    }

    public void SwitchPiece(int piece)
    {
        _selected = piece;

        _display.text = _selected.ToString();
    }
}
