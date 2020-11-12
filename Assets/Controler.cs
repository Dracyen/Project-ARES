using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controler : MonoBehaviour
{
    public GameObject PlayerCar;
    MovimentTest PlayerMoviment;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
       
            PlayerCar = GameObject.FindGameObjectWithTag("PlayerCar");
            PlayerMoviment = PlayerCar.GetComponent<MovimentTest>();
        
    }

    // Update is called once per frame
    public void RightButton()
    {
        PlayerMoviment.MoveRight();
    }
    public void LeftButton()
    {
        PlayerMoviment.MoveLeft();
    }
}
