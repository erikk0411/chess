using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FenUtil
{
    
    public Tile BlackPawn;
    public static string FenStringToGameString(string v)
    {   
        string Gamestring= "";
        
        for (int i = 0; i < v.Length; i++)
        {
            
            

            if (v[i] == '/' )
            {
                continue;
            }
            if (char.IsDigit(v,i))
            {
                int number = (int)char.GetNumericValue(v[i]);
                for (int j = 0; j < number; j++)
                {
                    Gamestring += '.';                }
                
            }
            if(char.IsLetter(v[i]))
            {
                Gamestring += v[i];
            }

            

        }

        return Gamestring;
        //"rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR"
        //rnbqkbnrpppppppp........
    }
}
