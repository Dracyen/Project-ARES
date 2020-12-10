using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile
{
    public int index;
    public Vector2[] occupPos;
    public enum RotationState {Up,Right,Down,Left};
    public RotationState rotation = RotationState.Left;
    public RotationState outPutRot;
    public Vector3 rot;
    public Vector3 Scale = new Vector3(1,1,1);

    public Vector2 entrancePos;
    public Vector2 exitPos;

    public Vector2 size;
    public Vector2 step;
    Vector3 stepV3;
    public Vector2 direction = new Vector2(1, 0);

    Transform pointA;
    Transform pointB;

    public bool isCurved;
    bool ToSpinOrNotToSpin = false;

    public MapGenerator.TrackTile originalInfo;


    RotationState rotAnterior;
    bool isTheLastACurve;
    public void AddFirstTile(Vector2 entPos, MapGenerator.TrackTile tile, Vector2 direct, RotationState rotState, bool isCurve)
    {
        direction = direct;
        originalInfo = tile;
        index = tile.index;
        entrancePos = entPos;
        size = new Vector2(tile.SizeX, tile.SizeY);
        step = new Vector2(tile.StepX, tile.StepY);
        
        rotation = rotState;
        outPutRot = RotationState.Left;
        isCurved = tile.isCurve;
        ExitPos();
    }
    public void AddTile(MapGenerator.TrackTile tile, Tile LastTile)
    {
        entrancePos = LastTile.exitPos;
        rotAnterior = LastTile.outPutRot;
        isTheLastACurve = LastTile.isCurved;
        index = tile.index;
        isCurved = tile.isCurve;
        size = new Vector2(tile.SizeX, tile.SizeY);
        step = new Vector2(tile.StepX, tile.StepY);
       
        originalInfo = tile;
        
            AjustRotation();
        
    }
    void AjustRotation()
    {
        if (!isCurved)
        {
            //Debug.Log("Last Rotation: " + rotAnterior);
            rotation = rotAnterior;
            outPutRot = rotation;
        }
        else
        {         
            if (originalInfo.changeAxis)
            {
                // se ultimo output foi Up curva 90 so pode ser Down ou Rigth
                // se ultimo output foi Left curva 90 so pode ser left ou Down
                // se ultimo output foi Right curva 90 so pode ser UP ou Rigth
                // se ultimo output foi Down curva 90 so pode ser Up ou left
                if (rotAnterior == RotationState.Up)
                {
                    rotation = RotationState.Right;
                    outPutRot = RotationState.Left;
                }
                if (rotAnterior == RotationState.Right)
                {
                    rotation = RotationState.Up;
                    outPutRot = RotationState.Up;
                }
                if (rotAnterior == RotationState.Down)
                {
                    rotation = RotationState.Left;
                    outPutRot = RotationState.Right;
                }
                if (rotAnterior == RotationState.Left)
                {
                    rotation = RotationState.Down;
                    outPutRot = RotationState.Down;
                }

            }
            else
            {
                if (rotAnterior == RotationState.Up)
                {
                    //rotation = RotationState.Right;
                    outPutRot = RotationState.Down;
                }
                if (rotAnterior == RotationState.Right)
                {
                    //rotation = RotationState.Up;
                    outPutRot = RotationState.Left;
                }
                if (rotAnterior == RotationState.Down)
                {
                    //rotation = RotationState.Left;
                    outPutRot = RotationState.Up;
                }
                if (rotAnterior == RotationState.Left)
                {
                    //rotation = RotationState.Down;
                    outPutRot = RotationState.Right;
                }
            }
            
            
        }

        RotateTile();
        //Debug.Log("Used rotation: " + rotation);
        AjustStep(rotation);
        ExitPos();
    }
  
    void AjustStep(RotationState CurrRotation)
    {
        if (originalInfo.changeAxis)
        {
            if (CurrRotation == RotationState.Up)
            {
                step = new Vector2(-originalInfo.StepX, originalInfo.StepY);
            }
            if (CurrRotation == RotationState.Right)
            {
                step = new Vector2(originalInfo.StepY, originalInfo.StepX);
            }
            if (CurrRotation == RotationState.Down)
            {
                step = new Vector2(originalInfo.StepX, -originalInfo.StepY);
            }
            if (CurrRotation == RotationState.Left)
            {
                step = new Vector2(-originalInfo.StepY, -originalInfo.StepX);
            }
        }
        else
        {
            if (CurrRotation == RotationState.Up)
            {
                step = new Vector2(originalInfo.StepY, originalInfo.StepX);
            }
            if (CurrRotation == RotationState.Right)
            {
                step = new Vector2(-originalInfo.StepX, originalInfo.StepY);
            }
            if (CurrRotation == RotationState.Down)
            {
                step = new Vector2(originalInfo.StepY, -originalInfo.StepX);
            }
            if (CurrRotation == RotationState.Left)
            {
                step = new Vector2(originalInfo.StepX, originalInfo.StepY);
            }
        }
        
        //Debug.Log("Step: " + step);
    }
    void RotateTile()
    {
        
       

            if (rotation == RotationState.Up)
            {
                rot = new Vector3(0, 0, -90);
            }
            if (rotation == RotationState.Right)
            {
                rot = new Vector3(0, 0, -180);
            }
            if (rotation == RotationState.Down)
            {
                rot = new Vector3(0, 0, -270);
            }
            if (rotation == RotationState.Left)
            {
                rot = new Vector3(0, 0, 0);
            }
        
        
    }
    public void FillGrid()
    {

    }
    public void BeSelected()
    {
        if (isCurved)
        {            
          Spin(); 
        }
       
    }
    public void AjustScaleToSpin()
    {
        if (rotation == RotationState.Up)
        {
            Scale = new Vector3(Scale.x * -1, 1, 1);

        }
        else if (rotation == RotationState.Right)
        {
            Scale = new Vector3(Scale.x * -1, 1, 1);
        }
        else if (rotation == RotationState.Down)
        {
            Debug.Log("When rotation is Down Scale is: " + Scale.x);
            Scale = new Vector3(Scale.x * -1, 1, 1);
            Debug.Log("When rotation is Down Scale is: " + Scale.x);
        }
        else if (rotation == RotationState.Left)
        {
            Scale = new Vector3(Scale.x * -1, 1,1);
        }

        if (rotation == RotationState.Up)//Not Ok
        {
            Debug.Log("U");
            step = new Vector2(originalInfo.StepY, -originalInfo.StepX);
        }
        if (rotation == RotationState.Right)//Not Ok
        {
            Debug.Log("R");
            step = new Vector2(-originalInfo.StepX, -originalInfo.StepY);
        }
        if (rotation == RotationState.Down)//OK
        {
            Debug.Log("D");
            step = new Vector2(-originalInfo.StepY, originalInfo.StepX);
        }
        if (rotation == RotationState.Left)//ok
        {
            Debug.Log("L");
            step = new Vector2(originalInfo.StepX, originalInfo.StepY);
        }
    }

    public void Spin()
    {

        if (originalInfo.changeAxis)
        {
            
            // se ultimo output foi Up curva 90 so pode ser Down ou Rigth
            // se ultimo output foi Left curva 90 so pode ser left ou Down
            // se ultimo output foi Right curva 90 so pode ser UP ou Rigth
            // se ultimo output foi Down curva 90 so pode ser Up ou left
            if (rotAnterior == RotationState.Up)
            {
                if (!ToSpinOrNotToSpin)
                {
                    rotation = RotationState.Down;
                    outPutRot = RotationState.Right;
                    ToSpinOrNotToSpin = true;
                }
                
                else if (ToSpinOrNotToSpin) 
                {
                    rotation = RotationState.Right;
                    outPutRot = RotationState.Left;
                    ToSpinOrNotToSpin = false;
                }


            }
            else if (rotAnterior == RotationState.Right)
            {
                if (!ToSpinOrNotToSpin)
                {
                    rotation = RotationState.Right;
                    outPutRot = RotationState.Down;
                    ToSpinOrNotToSpin = true;
                }
                else if(ToSpinOrNotToSpin)
                {
                    rotation = RotationState.Up;
                    outPutRot = RotationState.Up;
                    ToSpinOrNotToSpin = false;
                }

            }
            else if (rotAnterior == RotationState.Down)
            {
                if (!ToSpinOrNotToSpin)
                {
                    rotation = RotationState.Up;
                    outPutRot = RotationState.Left;
                    ToSpinOrNotToSpin = true;
                }
                else if(ToSpinOrNotToSpin)
                {
                    rotation = RotationState.Left;
                    outPutRot = RotationState.Right;
                    ToSpinOrNotToSpin = false;
                }


            }
            else if (rotAnterior == RotationState.Left)
            {
                if (!ToSpinOrNotToSpin)
                {
                    rotation = RotationState.Left;
                    outPutRot = RotationState.Up;
                    ToSpinOrNotToSpin = true;
                }
                else if(ToSpinOrNotToSpin)
                {
                    rotation = RotationState.Down;
                    outPutRot = RotationState.Down;
                    ToSpinOrNotToSpin = false;
                }
                
            }
            if (size.x == 1 || size.y == 1)
            {
                if (rotation == RotationState.Up)
                {
                    step = new Vector2(originalInfo.StepY, originalInfo.StepX);
                }
                if (rotation == RotationState.Right)
                {

                    step = new Vector2(originalInfo.StepX, -originalInfo.StepY);
                }
                if (rotation == RotationState.Down)
                {
                    step = new Vector2(-originalInfo.StepY, -originalInfo.StepX);
                }
                if (rotation == RotationState.Left)
                {
                    step = new Vector2(originalInfo.StepX, originalInfo.StepY);
                }
            }
               
        }
        else 
        { 

        }
        //Debug.Log(rotation);
        if (size.x > 1 || size.y > 1)
        {
            AjustScaleToSpin();
        }
        else
        {
            RotateTile();
        }
        
        ExitPos();
    }
    void ExitPos()
    {
       
        Debug.Log(rotation);
        exitPos.x = entrancePos.x + (GameObject.FindObjectOfType<MapDisplay>().Step * step.x);
        exitPos.y = entrancePos.y + (GameObject.FindObjectOfType<MapDisplay>().Step * step.y);
    }
}
