using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotSize : MonoBehaviour
{
    public BoxCollider _collider;

    public Transform _mesh;

    public void Resize(float _size)
    { 
        _mesh.localScale = new Vector3(_size, transform.localScale.y, _size);
    
        _collider = gameObject.GetComponent<BoxCollider>();
        _collider.size = new Vector3(_size, _collider.size.y, _size);
    }
}