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
    public Save( MapDisplay tiles, MapGenerator TrackInfo)
    {
        isLoop = TrackInfo.isLoop;
        numOfLaps = TrackInfo.numOfLapsOfTheTrack;
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
    
}
