using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[System.Serializable]
public class Save
{
    public int[] index;
    public int numOfTiles;
    public float[] rotacaoEmZdeCadaTile;
    public float[] PosicoesDeEntradaX;
    public float[] PosicoesDeEntradaY;
    public float[] EscalaX;
    public float[] EscalaY;
    public float[] EscalaZ;
    public bool isLoop;
    public int numOfLaps;
    public float SizeOfTheGrid;
    public Save( MapDisplay tiles, MapGenerator TrackInfo)
    {
        isLoop = TrackInfo.isLoop;
        numOfLaps = TrackInfo.numOfLapsOfTheTrack;
        SizeOfTheGrid = TrackInfo.GridSize;
        index = new int[tiles.tileTracks.Count];
        rotacaoEmZdeCadaTile = new float[tiles.tileTracks.Count];
        PosicoesDeEntradaX = new float[tiles.tileTracks.Count];
        PosicoesDeEntradaY = new float[tiles.tileTracks.Count];
        EscalaX = new float[tiles.tileTracks.Count];
        EscalaY = new float[tiles.tileTracks.Count];
        EscalaZ = new float[tiles.tileTracks.Count];
        numOfTiles = tiles.tileTracks.Count;

        for (int i = 0; i < index.Length; i++)
        {
            index[i] = tiles.tileTracks[i].index;
            rotacaoEmZdeCadaTile[i] = tiles.tileTracks[i].rot.z;
            PosicoesDeEntradaX[i] = tiles.tileTracks[i].entrancePos.x;
            PosicoesDeEntradaY[i] = tiles.tileTracks[i].entrancePos.y;
            EscalaX[i] = tiles.tileTracks[i].Scale.x;
            EscalaY[i] = tiles.tileTracks[i].Scale.y;
            EscalaZ[i] = tiles.tileTracks[i].Scale.z;
        }
    }
    
    public Save(TrackBuilder tiles)
    {
        isLoop = tiles.isLoop;
        numOfLaps = 3;
        SizeOfTheGrid = tiles.gridSize;
        index = new int[tiles.count];
        rotacaoEmZdeCadaTile = new float[tiles.count];
        PosicoesDeEntradaX = new float[tiles.count];
        PosicoesDeEntradaY = new float[tiles.count];
        EscalaX = new float[tiles.count];
        EscalaY = new float[tiles.count];
        EscalaZ = new float[tiles.count];
        numOfTiles = tiles.count;

        for (int i = 0; i < index.Length; )
        {
            for (int x = 0; x < tiles.gridSize; x++)
            {
                for (int y = 0; y < tiles.gridSize; y++)
                {
                    if(tiles.SlotGrid[x,y].CurrentState == TrackSlot.State.FULL)
                    {
                        index[i] = tiles.SlotGrid[x,y].Holder.index;
                        rotacaoEmZdeCadaTile[i] = tiles.SlotGrid[x, y].Piece.transform.rotation.z;
                        PosicoesDeEntradaX[i] = tiles.SlotGrid[x, y].transform.position.x;
                        PosicoesDeEntradaY[i] = tiles.SlotGrid[x, y].transform.position.y;
                        EscalaX[i] = tiles.SlotGrid[x, y].transform.localScale.x;
                        EscalaY[i] = tiles.SlotGrid[x, y].transform.localScale.y;
                        EscalaZ[i] = tiles.SlotGrid[x, y].transform.localScale.z;

                        i++;
                    }
                } 
            }
        }
    }
    
}