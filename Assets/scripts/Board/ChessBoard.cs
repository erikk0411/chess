using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class ChessBoard : MonoBehaviour
{
    public PieceMovement PieceMovement;
    Tilemap tilemap;
    public Tile BlackTile;
    public Tile WhiteTile;
    

    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        DrawBoard();
    }

    public void DrawBoard()
    {
        
       
        for(int y = 8; y > 0; y--)
        {
            for (int x=0; x < 8; x++)
            {
                bool isDarksquare=(x+y)%2!=0;
                Vector3Int position = new(x,y,0);


                if (isDarksquare)
                {
                    tilemap.SetTile(position, BlackTile);

                }
                else
                {
                    tilemap.SetTile(position, WhiteTile);
                }
            }
        }
    }
   

}
