using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotSize : MonoBehaviour
{
    public BoxCollider collider;

    public Transform mesh;

    public void Resize(float size)
    { 
        mesh.localScale = new Vector3(size, transform.localScale.y, size);
    
        collider = gameObject.GetComponent<BoxCollider>();
        collider.size = new Vector3(size, collider.size.y, size);
    }
}