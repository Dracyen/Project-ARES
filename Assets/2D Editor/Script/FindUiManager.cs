using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindUiManager : MonoBehaviour
{
   public void Tap()
    {
        //Debug.Log("Tap");
        UiManager Ui;
        Ui = FindObjectOfType<UiManager>();
        Ui.SelectPlacedTrack(new Vector2(transform.position.x, transform.position.y));
        Ui.SelectPosToPlace(new Vector2(transform.position.x, transform.position.y), GetComponent<FindYourSelfInGrid>().posX, GetComponent<FindYourSelfInGrid>().posX);
        
    }
}
