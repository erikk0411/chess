using Codice.Client.BaseCommands.EventTracking;
using Codice.Client.Common.GameUI;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class LegalMoves : MonoBehaviour
{
    PieceMovement pieceMovement;
    


    public Vector3Int[] Positions;

    

    public static List<int> LegalMovesList(int Index, char Piece, string gamestring, bool WhiteCanCastleKs, bool WhiteCanCastleQs, bool BlackCanCastleKs, bool BlackCanCastleQs, bool[] EnPassantSquares, bool WhiteToMove,bool LookForChecks)
    {
        int row;
        int column;
        List<int> Moves = new();
        row = Mathf.CeilToInt((Index) / 8 + 1);
        column = Mathf.CeilToInt((Index) % 8 + 1);
        Moves.Clear();
       
        
        if (LookForChecks)
        {
        if (char.IsLower(Piece) && WhiteToMove)
        {
            return Moves;
        }
        else if (char.IsUpper(Piece) && !WhiteToMove)
        {
            return Moves;
        }
        }
       
        if (Piece == 'k' || Piece == 'K')
        {
            if (Piece == 'k')
            {//blackKing

                //castle
                if (Index + 2 <64)
                {
                    if (gamestring[Index + 1] == '.' && gamestring[Index + 2] == '.' && BlackCanCastleKs && row == 1 && gamestring[7]=='r')
                    {
                        Moves.Add(Index + 2);
                    }
                }
                if (Index - 3 > 0 )
                {
                    if (gamestring[Index - 1] == '.' && gamestring[Index - 2] == '.' && gamestring[Index - 3] == '.' && BlackCanCastleQs && row == 1 && gamestring[0] == 'r')
                    {
                        Moves.Add(Index - 3);
                    }
                }
                //NormalMoves



                if (Index - 1 >= 0 && Mathf.CeilToInt((Index - 1) / 8 + 1) == row)
                {
                    if (gamestring[Index - 1] == '.' || PieceColour(gamestring[Index - 1]) == "White")
                    {

                        Moves.Add(Index - 1);
                    }
                }
                if (Index + 1 < 64 && Mathf.CeilToInt((Index + 1) / 8 + 1) == row)
                {
                    if (gamestring[Index + 1] == '.' || PieceColour(gamestring[Index + 1]) == "White")
                    {

                        Moves.Add(Index + 1);
                    }
                }
                if (Index + 7 < 64 && Mathf.CeilToInt((Index + 7) / 8 + 1) == row + 1)
                {
                    if (gamestring[Index + 7] == '.' || PieceColour(gamestring[Index + 7]) == "White")
                    {

                        Moves.Add(Index + 7);
                    }
                }
                if (Index + 8 < 64 && Mathf.CeilToInt((Index + 8) / 8 + 1) == row + 1)
                {
                    if (gamestring[Index + 8] == '.' || PieceColour(gamestring[Index + 8]) == "White")
                    {

                        Moves.Add(Index + 8);
                    }
                }
                if (Index + 9 < 64 && Mathf.CeilToInt((Index + 9) / 8 + 1) == row + 1)
                {
                    if (gamestring[Index + 9] == '.' || PieceColour(gamestring[Index + 9]) == "White")
                    {

                        Moves.Add(Index + 9);
                    }
                }
                if (Index - 7 >= 0 && Mathf.CeilToInt((Index - 7) / 8 + 1) == row - 1)
                {
                    if (gamestring[Index - 7] == '.' || PieceColour(gamestring[Index - 7]) == "White")
                    {

                        Moves.Add(Index - 7);
                    }
                }
                if (Index - 8 >= 0 && Mathf.CeilToInt((Index - 8) / 8 + 1) == row - 1)
                {
                    if (gamestring[Index - 8] == '.' || PieceColour(gamestring[Index - 8]) == "White")
                    {

                        Moves.Add(Index - 8);
                    }
                }
                if (Index - 9 >= 0 && Mathf.CeilToInt((Index - 9) / 8 + 1) == row - 1)
                {
                    if (gamestring[Index - 9] == '.' || PieceColour(gamestring[Index - 9]) == "White")
                    {

                        Moves.Add(Index - 9);
                    }
                }

            }
            if (Piece == 'K')
            {//WhiteKing
             //castle

                if (Index + 2 < 64 )
                {
                    if (gamestring[Index + 1] == '.' && gamestring[Index + 2] == '.' && WhiteCanCastleKs && row == 8 && gamestring[63] == 'R')
                    {
                        Moves.Add(Index + 2);
                    }
                }
                if (Index - 3 > 0)
                {
                    if (gamestring[Index - 1] == '.' && gamestring[Index - 2] == '.' && gamestring[Index - 3] == '.' && WhiteCanCastleQs && row == 8 && gamestring[56] == 'R')
                    {
                        Moves.Add(Index - 3);
                    }
                }
                //NormalMoves

                if (Index - 1 >= 0 && Mathf.CeilToInt((Index - 1) / 8 + 1) == row)
                {
                    if (gamestring[Index - 1] == '.' || PieceColour(gamestring[Index - 1]) == "Black")
                    {

                        Moves.Add(Index - 1);
                    }
                }
                if (Index + 1 < 64 && Mathf.CeilToInt((Index + 1) / 8 + 1) == row)
                {
                    if (gamestring[Index + 1] == '.' || PieceColour(gamestring[Index + 1]) == "Black")
                    {

                        Moves.Add(Index + 1);
                    }
                }
                if (Index + 7 < 64 && Mathf.CeilToInt((Index + 7) / 8 + 1) == row + 1)
                {
                    if (gamestring[Index + 7] == '.' || PieceColour(gamestring[Index + 7]) == "Black")
                    {

                        Moves.Add(Index + 7);
                    }
                }
                if (Index + 8 < 64 && Mathf.CeilToInt((Index + 8) / 8 + 1) == row + 1)
                {
                    if (gamestring[Index + 8] == '.' || PieceColour(gamestring[Index + 8]) == "Black")
                    {

                        Moves.Add(Index + 8);
                    }
                }
                if (Index + 9 < 64 && Mathf.CeilToInt((Index + 9) / 8 + 1) == row + 1)
                {
                    if (gamestring[Index + 9] == '.' || PieceColour(gamestring[Index + 9]) == "Black")
                    {

                        Moves.Add(Index + 9);
                    }
                }
                if (Index - 7 >= 0 && Mathf.CeilToInt((Index - 7) / 8 + 1) == row - 1)
                {
                    if (gamestring[Index - 7] == '.' || PieceColour(gamestring[Index - 7]) == "Black")
                    {

                        Moves.Add(Index - 7);
                    }
                }
                if (Index - 8 >= 0 && Mathf.CeilToInt((Index - 8) / 8 + 1) == row - 1)
                {
                    if (gamestring[Index - 8] == '.' || PieceColour(gamestring[Index - 8]) == "Black")
                    {

                        Moves.Add(Index - 8);
                    }
                }
                if (Index - 9 >= 0 && Mathf.CeilToInt((Index - 9) / 8 + 1) == row - 1)
                {
                    if (gamestring[Index - 9] == '.' || PieceColour(gamestring[Index - 9]) == "Black")
                    {

                        Moves.Add(Index - 9);
                    }
                }

            }

        }
        if (Piece == 'q' || Piece == 'Q')
        {
            if (Piece == 'q')
            {//BlackQueen
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 64; j += 9)
                    {
                        if (j + Index < 64 && Mathf.CeilToInt((Index + j) % 8 + 1) > Mathf.CeilToInt((Index) % 8 + 1))
                        {
                            if (gamestring[j + Index] == '.' || gamestring[j + Index] == 'q' && j == 0)
                            {
                                if (j != 0)
                                {
                                    Moves.Add(Index + j);
                                }

                            }
                            else if (PieceColour(gamestring[j + Index]) == "White")
                            {
                                Moves.Add(Index + j);
                                break;
                            }
                            else
                            {

                                break;
                            }
                        }
                    }
                    for (int j = 0; j < 64; j += 7)
                    {
                        if (j + Index < 64 && Mathf.CeilToInt((Index + j) % 8 + 1) < Mathf.CeilToInt((Index) % 8 + 1))
                        {
                            if (gamestring[j + Index] == '.' || gamestring[j + Index] == 'q' && j == 0)
                            {
                                if (j != 0)
                                {
                                    Moves.Add(Index + j);
                                }

                            }
                            else if (PieceColour(gamestring[j + Index]) == "White")
                            {
                                Moves.Add(Index + j);
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    for (int j = 0; j + Index >= 0; j -= 9)
                    {
                        if (j + Index >= 0 && Mathf.CeilToInt((Index + j) % 8 + 1) < Mathf.CeilToInt((Index) % 8 + 1))
                        {

                            if (gamestring[j + Index] == '.' || gamestring[j + Index] == 'q' && j == 0)
                            {
                                if (j != 0)
                                {
                                    Moves.Add(Index + j);
                                }
                            }
                            else if (PieceColour(gamestring[j + Index]) == "White")
                            {
                                Moves.Add(Index + j);
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    for (int j = 0; j + Index >= 0; j -= 7)
                    {
                        if (j + Index >= 0 && Mathf.CeilToInt((Index + j) % 8 + 1) > Mathf.CeilToInt((Index) % 8 + 1))
                        {
                            if (gamestring[j + Index] == '.' || gamestring[j + Index] == 'q' && j == 0)
                            {
                                if (j != 0)
                                {
                                    Moves.Add(Index + j);
                                }
                            }
                            else if (PieceColour(gamestring[j + Index]) == "White")
                            {
                                Moves.Add(Index + j);
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    for (int j = 0; (j + Index) < 64; j += 8)
                    {

                        if (gamestring[j + Index] == '.' || gamestring[j + Index] == 'q' && j == 0)
                        {
                            if (j != 0)
                            {
                                Moves.Add(Index + j);
                            }




                        }
                        else if (PieceColour(gamestring[j + Index]) == "White")
                        {
                            Moves.Add(j + Index);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int j = 0; j + Index >= 0; j -= 8)
                    {
                        if (gamestring[j + Index] == '.' || gamestring[j + Index] == 'q' && j == 0)
                        {
                            if (j != 0)
                            {
                                Moves.Add(Index + j);
                            }

                        }
                        else if (PieceColour(gamestring[j + Index]) == "White")
                        {
                            Moves.Add(j + Index);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int j = 0; j + column < 9; j += 1)
                    {
                        if (gamestring[j + Index] == '.' || gamestring[j + Index] == 'q' && j == 0)
                        {
                            if (j != 0)
                            {
                                Moves.Add(Index + j);
                            }

                        }
                        else if (PieceColour(gamestring[j + Index]) == "White")
                        {
                            Moves.Add(Index + j);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int j = 0; j + column > 0; j -= 1)
                    {
                        if (gamestring[j + Index] == '.' || gamestring[j + Index] == 'q' && j == 0)
                        {
                            if (j != 0)
                            {
                                Moves.Add(Index + j);
                            }

                        }
                        else if (PieceColour(gamestring[j + Index]) == "White")
                        {
                            Moves.Add(Index + j);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            if (Piece == 'Q')
            {//WhiteQueen
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 64; j += 9)
                    {
                        if (j + Index < 64 && Mathf.CeilToInt((Index + j) % 8 + 1) > Mathf.CeilToInt((Index) % 8 + 1))
                        {
                            if (gamestring[j + Index] == '.' || gamestring[j + Index] == 'Q' && j == 0)
                            {
                                if (j != 0)
                                {
                                    Moves.Add(Index + j);
                                }

                            }
                            else if (PieceColour(gamestring[j + Index]) == "Black")
                            {
                                Moves.Add(Index + j);
                                break;
                            }
                            else
                            {

                                break;
                            }
                        }
                    }
                    for (int j = 0; j < 64; j += 7)
                    {
                        if (j + Index < 64 && Mathf.CeilToInt((Index + j) % 8 + 1) < Mathf.CeilToInt((Index) % 8 + 1))
                        {
                            if (gamestring[j + Index] == '.' || gamestring[j + Index] == 'Q' && j == 0)
                            {
                                if (j != 0)
                                {
                                    Moves.Add(Index + j);
                                }

                            }
                            else if (PieceColour(gamestring[j + Index]) == "Black")
                            {
                                Moves.Add(Index + j);
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    for (int j = 0; j + Index >= 0; j -= 9)
                    {
                        if (j + Index >= 0 && Mathf.CeilToInt((Index + j) % 8 + 1) < Mathf.CeilToInt((Index) % 8 + 1))
                        {

                            if (gamestring[j + Index] == '.' || gamestring[j + Index] == 'Q' && j == 0)
                            {
                                if (j != 0)
                                {
                                    Moves.Add(Index + j);
                                }
                            }
                            else if (PieceColour(gamestring[j + Index]) == "Black")
                            {
                                Moves.Add(Index + j);
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    for (int j = 0; j + Index >= 0; j -= 7)
                    {
                        if (j + Index >= 0 && Mathf.CeilToInt((Index + j) % 8 + 1) > Mathf.CeilToInt((Index) % 8 + 1))
                        {
                            if (gamestring[j + Index] == '.' || gamestring[j + Index] == 'Q' && j == 0)
                            {
                                if (j != 0)
                                {
                                    Moves.Add(Index + j);
                                }
                            }
                            else if (PieceColour(gamestring[j + Index]) == "Black")
                            {
                                Moves.Add(Index + j);
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    for (int j = 0; (j + Index) < 64; j += 8)
                    {

                        if (gamestring[j + Index] == '.' || gamestring[j + Index] == 'Q' && j == 0)
                        {
                            if (j != 0)
                            {
                                Moves.Add(Index + j);
                            }




                        }
                        else if (PieceColour(gamestring[j + Index]) == "Black")
                        {
                            Moves.Add(j + Index);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int j = 0; j + Index >= 0; j -= 8)
                    {
                        if (gamestring[j + Index] == '.' || gamestring[j + Index] == 'Q' && j == 0)
                        {
                            if (j != 0)
                            {
                                Moves.Add(Index + j);
                            }

                        }
                        else if (PieceColour(gamestring[j + Index]) == "Black")
                        {
                            Moves.Add(j + Index);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int j = 0; j + column < 9; j += 1)
                    {
                        if (gamestring[j + Index] == '.' || gamestring[j + Index] == 'Q' && j == 0)
                        {
                            if (j != 0)
                            {
                                Moves.Add(Index + j);
                            }

                        }
                        else if (PieceColour(gamestring[j + Index]) == "Black")
                        {
                            Moves.Add(Index + j);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int j = 0; j + column > 0; j -= 1)
                    {
                        if (gamestring[j + Index] == '.' || gamestring[j + Index] == 'Q' && j == 0)
                        {
                            if (j != 0)
                            {
                                Moves.Add(Index + j);
                            }
                        }
                        else if (PieceColour(gamestring[j + Index]) == "Black")
                        {
                            Moves.Add(Index + j);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

        }
        if (Piece == 'b' || Piece == 'B')
        {
            if (Piece == 'b')
            {//BlackBishop
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 64; j += 9)
                    {
                        if (j + Index < 64 && Mathf.CeilToInt((Index + j) % 8 + 1) > Mathf.CeilToInt((Index) % 8 + 1))
                        {
                            if (gamestring[j + Index] == '.' || gamestring[j + Index] == 'b' && j == 0)
                            {
                                if (j != 0)
                                {
                                    Moves.Add(Index + j);
                                }

                            }
                            else if (PieceColour(gamestring[j + Index]) == "White")
                            {
                                Moves.Add(Index + j);
                                break;
                            }
                            else
                            {

                                break;
                            }
                        }
                    }
                    for (int j = 0; j < 64; j += 7)
                    {
                        if (j + Index < 64 && Mathf.CeilToInt((Index + j) % 8 + 1) < Mathf.CeilToInt((Index) % 8 + 1))
                        {
                            if (gamestring[j + Index] == '.' || gamestring[j + Index] == 'b' && j == 0)
                            {
                                if (j != 0)
                                {
                                    Moves.Add(Index + j);
                                }

                            }
                            else if (PieceColour(gamestring[j + Index]) == "White")
                            {
                                Moves.Add(Index + j);
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    for (int j = 0; j + Index >= 0; j -= 9)
                    {
                        if (j + Index >= 0 && Mathf.CeilToInt((Index + j) % 8 + 1) < Mathf.CeilToInt((Index) % 8 + 1))
                        {

                            if (gamestring[j + Index] == '.' || gamestring[j + Index] == 'b' && j == 0)
                            {
                                if (j != 0)
                                {
                                    Moves.Add(Index + j);
                                }
                            }
                            else if (PieceColour(gamestring[j + Index]) == "White")
                            {
                                Moves.Add(Index + j);
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    for (int j = 0; j + Index >= 0; j -= 7)
                    {
                        if (j + Index >= 0 && Mathf.CeilToInt((Index + j) % 8 + 1) > Mathf.CeilToInt((Index) % 8 + 1))
                        {
                            if (gamestring[j + Index] == '.' || gamestring[j + Index] == 'b' && j == 0)
                            {
                                if (j != 0)
                                {
                                    Moves.Add(Index + j);
                                }
                            }
                            else if (PieceColour(gamestring[j + Index]) == "White")
                            {
                                Moves.Add(Index + j);
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            else if (Piece == 'B')
            {//WhiteBishop
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 64; j += 9)
                    {
                        if (j + Index < 64 && Mathf.CeilToInt((Index + j) % 8 + 1) > Mathf.CeilToInt((Index) % 8 + 1))
                        {
                            if (gamestring[j + Index] == '.' || gamestring[j + Index] == 'B' && j == 0)
                            {
                                if (j != 0)
                                {
                                    Moves.Add(Index + j);
                                }

                            }
                            else if (PieceColour(gamestring[j + Index]) == "Black")
                            {
                                Moves.Add(Index + j);
                                break;
                            }
                            else
                            {

                                break;
                            }
                        }
                    }
                    for (int j = 0; j < 64; j += 7)
                    {
                        if (j + Index < 64 && Mathf.CeilToInt((Index + j) % 8 + 1) < Mathf.CeilToInt((Index) % 8 + 1))
                        {
                            if (gamestring[j + Index] == '.' || gamestring[j + Index] == 'B' && j == 0)
                            {
                                if (j != 0)
                                {
                                    Moves.Add(Index + j);
                                }

                            }
                            else if (PieceColour(gamestring[j + Index]) == "Black")
                            {
                                Moves.Add(Index + j);
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    for (int j = 0; j + Index >= 0; j -= 9)
                    {
                        if (j + Index >= 0 && Mathf.CeilToInt((Index + j) % 8 + 1) < Mathf.CeilToInt((Index) % 8 + 1))
                        {

                            if (gamestring[j + Index] == '.' || gamestring[j + Index] == 'B' && j == 0)
                            {
                                if (j != 0)
                                {
                                    Moves.Add(Index + j);
                                }
                            }
                            else if (PieceColour(gamestring[j + Index]) == "Black")
                            {
                                Moves.Add(Index + j);
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    for (int j = 0; j + Index >= 0; j -= 7)
                    {
                        if (j + Index >= 0 && Mathf.CeilToInt((Index + j) % 8 + 1) > Mathf.CeilToInt((Index) % 8 + 1))
                        {
                            if (gamestring[j + Index] == '.' || gamestring[j + Index] == 'B' && j == 0)
                            {
                                if (j != 0)
                                {
                                    Moves.Add(Index + j);
                                }
                            }
                            else if (PieceColour(gamestring[j + Index]) == "Black")
                            {
                                Moves.Add(Index + j);
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }

            }
        }
        if (Piece == 'n' || Piece == 'N')
        {

            if (Piece == 'n')
            {//blackKnight

                if (Index - 10 >= 0 && Mathf.CeilToInt((Index-10) / 8 + 1)==row-1)
                {
                    if (gamestring[Index - 10] == '.' || PieceColour(gamestring[Index - 10]) == "White")
                    {

                        Moves.Add(Index - 10);
                    }
                }
                if (Index + 6 < 64 && Mathf.CeilToInt((Index +6) / 8 + 1) == row + 1)
                {
                    if (gamestring[Index + 6] == '.' || PieceColour(gamestring[Index + 6]) == "White")
                    {

                        Moves.Add(Index + 6);
                    }
                }
                if (Index + 15 < 64 && Mathf.CeilToInt((Index +15) / 8 + 1) == row +2)
                {
                    if (gamestring[Index + 15] == '.' || PieceColour(gamestring[Index + 15]) == "White")
                    {

                        Moves.Add(Index + 15);
                    }
                }
                if (Index + 17 < 64 && Mathf.CeilToInt((Index + 17) / 8 + 1) == row +2)
                {
                    if (gamestring[Index + 17] == '.' || PieceColour(gamestring[Index + 17]) == "White")
                    {

                        Moves.Add(Index + 17);
                    }
                }
                if (Index + 10 < 64 && Mathf.CeilToInt((Index + 10) / 8 + 1) == row + 1)
                {
                    if (gamestring[Index + 10] == '.' || PieceColour(gamestring[Index + 10]) == "White")
                    {

                        Moves.Add(Index + 10);
                    }
                }
                if (Index - 6 >= 0 && Mathf.CeilToInt((Index - 6) / 8 + 1) == row - 1)
                {
                    if (gamestring[Index - 6] == '.' || PieceColour(gamestring[Index - 6]) == "White")
                    {

                        Moves.Add(Index - 6);
                    }
                }
                if (Index - 15 >= 0 && Mathf.CeilToInt((Index - 15) / 8 + 1) == row - 2)
                {
                    if (gamestring[Index - 15] == '.' || PieceColour(gamestring[Index - 15]) == "White")
                    {

                        Moves.Add(Index - 15);
                    }
                }
                if (Index - 17 >= 0 && Mathf.CeilToInt((Index - 17) / 8 + 1) == row - 2)
                {
                    if (gamestring[Index - 17] == '.' || PieceColour(gamestring[Index - 17]) == "White")
                    {

                        Moves.Add(Index - 17);
                    }
                }

            }
            if (Piece == 'N')
            {//WhiteKnight

                if (Index - 10 >= 0 && Mathf.CeilToInt((Index - 10) / 8 + 1) == row - 1)
                {
                    if (gamestring[Index - 10] == '.' || PieceColour(gamestring[Index - 10]) == "Black")
                    {

                        Moves.Add(Index - 10);
                    }
                }
                if (Index + 6 < 64 && Mathf.CeilToInt((Index + 6) / 8 + 1) == row + 1)
                {
                    if (gamestring[Index + 6] == '.' || PieceColour(gamestring[Index + 6]) == "Black")
                    {

                        Moves.Add(Index + 6);
                    }
                }
                if (Index + 15 < 64 && Mathf.CeilToInt((Index + 15) / 8 + 1) == row + 2)
                {
                    if (gamestring[Index + 15] == '.' || PieceColour(gamestring[Index + 15]) == "Black")
                    {

                        Moves.Add(Index + 15);
                    }
                }
                if (Index + 17 < 64 && Mathf.CeilToInt((Index + 17) / 8 + 1) == row + 2)
                {
                    if (gamestring[Index + 17] == '.' || PieceColour(gamestring[Index + 17]) == "Black")
                    {

                        Moves.Add(Index + 17);
                    }
                }
                if (Index + 10 < 64 && Mathf.CeilToInt((Index + 10) / 8 + 1) == row + 1)
                {
                    if (gamestring[Index + 10] == '.' || PieceColour(gamestring[Index + 10]) == "Black")
                    {

                        Moves.Add(Index + 10);
                    }
                }
                if (Index - 6 >= 0 && Mathf.CeilToInt((Index - 6) / 8 + 1) == row - 1)
                {
                    if (gamestring[Index - 6] == '.' || PieceColour(gamestring[Index - 6]) == "Black")
                    {

                        Moves.Add(Index - 6);
                    }
                }
                if (Index - 15 >= 0 && Mathf.CeilToInt((Index - 15) / 8 + 1) == row - 2)
                {
                    if (gamestring[Index - 15] == '.' || PieceColour(gamestring[Index - 15]) == "Black")
                    {

                        Moves.Add(Index - 15);
                    }
                }
                if (Index - 17 >= 0 && Mathf.CeilToInt((Index - 17) / 8 + 1) == row - 2)
                {
                    if (gamestring[Index - 17] == '.' || PieceColour(gamestring[Index - 17]) == "Black")
                    {

                        Moves.Add(Index - 17);
                    }
                }

            }

        }
        if (Piece == 'r' || Piece == 'R')
        {
            
            if (Piece == 'r')
            {//BlackRook
                
                for (int j = 0; j < 8; j++)
                {
                    for (int i = 0; (i + Index) < 64; i += 8)
                    {

                        if (gamestring[i + Index] == '.' || gamestring[i + Index] == 'r' && i == 0)
                        {
                            if (i != 0)
                            {
                                Moves.Add(Index + i);
                            }




                        }
                        else if (PieceColour(gamestring[i + Index]) == "White")
                        {
                            if (i != 0)
                            {
                                Moves.Add(i + Index);
                            break;   
                            }
                         
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int i = 0; i + Index >= 0; i -= 8)
                    {
                        if (gamestring[i + Index] == '.' || gamestring[i + Index] == 'r' && i==0)
                        {
                            if (i != 0)
                            {
                                Moves.Add(Index + i);
                            }

                        }
                        else if (PieceColour(gamestring[i + Index]) == "White")
                        {
                            Moves.Add(i + Index);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int i = 0; i + column < 9; i += 1)
                    {
                        if (gamestring[i + Index] == '.' || gamestring[i + Index] == 'r' && i == 0)
                        {
                            if (i != 0)
                            {
                                Moves.Add(Index + i);
                            }
                            

                        }
                        else if (PieceColour(gamestring[i + Index]) == "White")
                        {
                            Moves.Add(Index + i);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int i = 0; i + column > 0; i -= 1)
                    {
                        if (gamestring[i + Index] == '.' || gamestring[i + Index] == 'r' && i == 0)
                        {
                            if (i != 0)
                            {
                             Moves.Add(Index + i);
                            }
                           
                        }
                        else if (PieceColour(gamestring[i + Index]) == "White")
                        {
                            Moves.Add(Index + i);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            if (Piece == 'R')
            {//WhiteRook
                for (int j = 0; j < 8; j++)
                {
                    for (int i = 0; (i + Index) < 64; i += 8)
                    {

                        if (gamestring[i + Index] == '.' || gamestring[i + Index] == 'R' && i == 0)
                        {
                            if (i != 0)
                            {
                                Moves.Add(Index + i);
                            }




                        }
                        else if (PieceColour(gamestring[i + Index]) == "Black")
                        {
                            Moves.Add(i + Index);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int i = 0; i + Index >= 0; i -= 8)
                    {
                        if (gamestring[i + Index] == '.' || gamestring[i + Index] == 'R' && i == 0)
                        {
                            if (i != 0)
                            {
                                Moves.Add(Index + i);
                            }

                        }
                        else if (PieceColour(gamestring[i + Index]) == "Black")
                        {
                            Moves.Add(i + Index);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int i = 0; i + column < 9; i += 1)
                    {
                        if (gamestring[i + Index] == '.' || gamestring[i + Index] == 'R' && i == 0)
                        {
                            if (i != 0)
                            {
                                Moves.Add(Index + i);
                            }

                        }
                        else if (PieceColour(gamestring[i + Index]) == "Black")
                        {
                            Moves.Add(Index + i);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int i = 0; i + column > 0; i -= 1)
                    {
                        if (gamestring[i + Index] == '.' || gamestring[i + Index] == 'R' && i == 0)
                        {
                            if (i != 0)
                            {
                             Moves.Add(Index + i);
                            }
                           
                        }
                        else if (PieceColour(gamestring[i + Index]) == "Black")
                        {
                            Moves.Add(Index + i);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }

                }



            }
        }
        if (Piece == 'p')
        {
            //BlackPawn

            if (Index + 8 < 64 && gamestring[Index + 8] == '.' && row < 8)
            {
                Moves.Add(Index + 8);

            }
            if (row == 2 && gamestring[Index + 16] == '.' && gamestring[Index + 8] == '.')
            {

                Moves.Add(Index + 16);
            }
            if (Index + 7 < 64)
            {
                if (PieceColour(gamestring[Index + 7]) == "White" && column > 1)
                {
                    Moves.Add(Index + 7);

                }
            }
            if (Index + 9 < 64)
            {
                if (PieceColour(gamestring[Index + 9]) == "White" && column < 8)
                {
                    Moves.Add(Index + 9);
                }
            }

            //enPassant
             if (Index + 9 < 64)
            {
                if (PieceColour(gamestring[Index + 1]) == "White" && column <= 8 && EnPassantSquares[Index + 1] && gamestring[Index + 9] == '.')
                {

                    Moves.Add(Index + 9);

                }
            }
             if (Index + 7 < 64)
            {
               if(PieceColour(gamestring[Index - 1]) == "White" && column >= 1 && EnPassantSquares[Index - 1] && gamestring[Index + 7] == '.')
               {
                   
                        Moves.Add(Index + 7);
                    
               } 
            }
        }
        if (Piece == 'P')
        {
            // WhitePawn

            if (Index - 8 >= 0 && gamestring[Index - 8] == '.' && row > 1)
            {
                Moves.Add(Index - 8);
            }
            if (row == 7 && gamestring[Index - 8] == '.' && gamestring[Index - 16] == '.')
            {

                Moves.Add(Index - 16);
            }
            if (Index - 7 >= 0)
            {
                if (PieceColour(gamestring[Index - 7]) == "Black" && column < 8)
                {
                    Moves.Add(Index - 7);

                }
            }
            if (Index - 9 >= 0)
            {
                if (PieceColour(gamestring[Index - 9]) == "Black" && column > 1)
                {
                    Moves.Add(Index - 9);
                }
            }
            //enPassant
            if (Index - 7 >= 0)
            {
                if (PieceColour(gamestring[Index + 1]) == "Black" && column <= 8 && EnPassantSquares[Index + 1] && gamestring[Index - 7] == '.')
                {
                    if (Index - 7 >= 0)
                    {
                        Moves.Add(Index - 7);
                    }
                }
            }
           if (Index - 9 >= 0)
            {
                if (PieceColour(gamestring[Index - 1]) == "Black" && column >= 1 && EnPassantSquares[Index - 1] && gamestring[Index - 9] == '.')
                {
                    if (Index - 7 >= 0)
                    {
                        Moves.Add(Index - 9);
                    }

                }   
            }
        }

      
        List<int> ValidMoves = new();
        ValidMoves.Clear();
        if (LookForChecks)
        {
            

            for (int x = 0; x < Moves.Count; x++)
            {
               
                if (!CanGetChecked(Index, gamestring, Piece, Moves[x], WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, WhiteToMove))
                {
                    if (Moves[x] == Index + 2 && Piece == 'k')
                    {

                        if (CanCastle("BKS", gamestring, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, WhiteToMove))
                        {
                            ValidMoves.Add(Moves[x]);
                        }
                    }
                    else if (Moves[x] == Index - 3 && Piece == 'k')
                    {

                        if (CanCastle("BQS", gamestring, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, WhiteToMove))
                        {
                            ValidMoves.Add(Moves[x]);
                        }
                    }
                    else if (Moves[x] == Index + 2 && Piece == 'K')
                    {

                        if (CanCastle("WKS", gamestring, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, WhiteToMove))
                        {
                            ValidMoves.Add(Moves[x]);
                        }
                    }
                    else if (Moves[x] == Index - 3 && Piece == 'K')
                    {
                        if (CanCastle("WQS", gamestring, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, WhiteToMove))
                        {
                            ValidMoves.Add(Moves[x]);

                        }
                    }
                    else
                    {
                        ValidMoves.Add(Moves[x]);
                        
                    }
                    

                }
            }



            return ValidMoves;
        }
        

        return Moves;
        

    }
    public static string PieceColour(char piece)
    {
        string t = piece switch
        {
            'r' => "Black",
            'n' => "Black",
            'b' => "Black",
            'q' => "Black",
            'k' => "Black",
            'p' => "Black",
            'R' => "White",
            'N' => "White",
            'B' => "White",
            'Q' => "White",
            'K' => "White",
            'P' => "White",
            '.' => "empty",
            _ => throw new Exception("Unknown char" + piece),
        };
        return t;
    }

    public static bool[] IsEnPassantable(int OldSquareIndex, int NewSquareIndex, char Piece)
    {
        bool[] IsEnPassantableSquare = new bool[63];
        Array.Fill(IsEnPassantableSquare, false);
        if (NewSquareIndex - OldSquareIndex == 16 && Piece == 'p')
        {
            IsEnPassantableSquare[NewSquareIndex] = true;
            
        }
        if (NewSquareIndex - OldSquareIndex == -16 && Piece == 'P')
        {
            IsEnPassantableSquare[NewSquareIndex] = true;
        }

        return IsEnPassantableSquare;
    }

    public static bool CanGetChecked(int Index,string gameString, char piece, int Move, bool WhiteCanCastleKs, bool WhiteCanCastleQs, bool BlackCanCastleKs, bool BlackCanCastleQs, bool[] EnPassantSquares, bool WhiteToMove)
    {
        string possiblegameString;
        possiblegameString = UpdateString(gameString, Index, '.');
        possiblegameString = UpdateString(possiblegameString, Move, piece);
        
        
        if (PieceColour(piece) == "Black")
        {

            int KingIndex = possiblegameString.IndexOf('k');

            for (int i = 0; i < possiblegameString.Length; i++)
            {
                if (PieceColour(possiblegameString[i]) == "White")
                {
                
                    if (LegalMovesList(i, possiblegameString[i], possiblegameString, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, !WhiteToMove,false).Contains(KingIndex))
                     {
                        
                        return true;
                    
                    }
                    else
                    {
                        
                    }
                }

            }
            
            return false;
        }
        else if (PieceColour(piece) == "White")
        {
            int KingIndex = possiblegameString.IndexOf('K');
            
            for (int i = 0; i < possiblegameString.Length; i++)
            {
                if (PieceColour(possiblegameString[i]) == "Black")
                {


                    if (LegalMovesList(i, possiblegameString[i], possiblegameString, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, !WhiteToMove,false).Contains(KingIndex))
                    {

                        
                        return true;
                    }
                    else
                    {
                      
                    }
                }


            }
            
            return false;
        }
        else
        {
            
            return false;
        }

       

    }


    public static bool IsCheck(string gameString, bool WhiteCanCastleKs, bool WhiteCanCastleQs, bool BlackCanCastleKs, bool BlackCanCastleQs, bool[] EnPassantSquares, bool WhiteToMove)
    {
        string possiblegameString = gameString;


        if (WhiteToMove)
        {
           
            int KingIndex = possiblegameString.IndexOf('K');
            for(int i = 0; i < possiblegameString.Length; i++)
            {
               
                if (PieceColour(possiblegameString[i])== "Black")
                {
                    
                    if (LegalMovesList(i, possiblegameString[i], possiblegameString, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, !WhiteToMove, false).Contains(KingIndex))
                    {
                        
                        return true;
                    }
                }
            
            }
        return false;
        }
        else if (!WhiteToMove)
        {
            
            int KingIndex = possiblegameString.IndexOf('k');
            for (int i = 0; i < possiblegameString.Length; i++)
            {
               
                if (PieceColour(possiblegameString[i]) == "White")
                {
                    
                    if (LegalMovesList(i, possiblegameString[i], possiblegameString, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, !WhiteToMove, false).Contains(KingIndex))
                    {
                        
                        return true;
                    }
                }
            }
        return false;
        }
        else return false;

    }
    public static bool NoLegalMoves(string gamestring,bool WhiteCanCastleKs,bool WhiteCanCastleQs,bool BlackCanCastleKs,bool BlackCanCastleQs,bool[] EnPassantSquares, bool WhiteToMove)
    {
        
        if (WhiteToMove)
        {
            for(int i = 0; i < gamestring.Length; i++)
            {
                if (PieceColour(gamestring[i])=="White")
                {
                    if (LegalMovesList(i, gamestring[i],gamestring,WhiteCanCastleKs,WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, WhiteToMove, true).Count > 0)
                    {
                        
                        return false;
                    }
                }
            }
            
        }
        else if (!WhiteToMove)
        {
            for (int i = 0; i < gamestring.Length; i++)
            {
                
                if (PieceColour(gamestring[i]) == "Black")
                {  
                    if (LegalMovesList(i, gamestring[i], gamestring, WhiteCanCastleKs, WhiteCanCastleQs, WhiteCanCastleQs, BlackCanCastleQs, EnPassantSquares, WhiteToMove, true).Count > 0)
                    {
                        
                        return false;
                    }
                }
            }
           
        }
        return true;
    }
    public static bool CanCastle(string side, string gameString, bool WhiteCanCastleKs, bool WhiteCanCastleQs, bool BlackCanCastleKs, bool BlackCanCastleQs, bool[] EnPassantSquares, bool WhiteToMove)
    {
        string possiblegameString = gameString;
        if (side == "BQS")
        {//blackqueenside
            //60-56
            
            if (!WhiteToMove)
            {


                for (int i = 0; i < possiblegameString.Length; i++)
                {

                    if (PieceColour(possiblegameString[i]) == "White")
                    {

                        if (LegalMovesList(i, possiblegameString[i], possiblegameString, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, !WhiteToMove, false).Contains(4) || LegalMovesList(i, possiblegameString[i], possiblegameString, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, !WhiteToMove, false).Contains(3))
                        {

                            return false;
                        }
                    }


                }
            }       
           
        }
        if (side == "BKS")
        {//blackkingside
            if (!WhiteToMove)
            {
                
                for (int i = 0; i < possiblegameString.Length; i++)
                {

                    if (PieceColour(possiblegameString[i]) == "White")
                    {

                        if (LegalMovesList(i, possiblegameString[i], possiblegameString, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, !WhiteToMove, false).Contains(4) || LegalMovesList(i, possiblegameString[i], possiblegameString, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, !WhiteToMove, false).Contains(5))
                        {

                            return false;
                        }
                    }


                }
            }
            
        }
        if (side == "WQS")
        {//whitequeenside
            
            if (WhiteToMove)
            {


                for (int i = 0; i < possiblegameString.Length; i++)
                {

                    if (PieceColour(possiblegameString[i]) == "Black")
                    {

                        if (LegalMovesList(i, possiblegameString[i], possiblegameString, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, !WhiteToMove, false).Contains(60) || LegalMovesList(i, possiblegameString[i], possiblegameString, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, !WhiteToMove, false).Contains(59))
                        {

                            return false;
                        }
                    }


                }
            }

        }
        if (side == "WKS")
        {//whitekingsside
            if (WhiteToMove)
            {
                


                for (int i = 0; i < possiblegameString.Length; i++)
                {

                    if (PieceColour(possiblegameString[i]) == "Black")
                    {

                        if (LegalMovesList(i, possiblegameString[i], possiblegameString, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, !WhiteToMove, false).Contains(60) || LegalMovesList(i, possiblegameString[i], possiblegameString, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, !WhiteToMove, false).Contains(61))
                        {

                            return false;
                        }
                    }


                }



               
            }
           
        }
        return true;
    }

    public static string UpdateString(string gamesstring, int index, char NewChar)
    {

        StringBuilder sb = new(gamesstring);
        sb[index] = NewChar;
        gamesstring = sb.ToString();
        return gamesstring;
    }
    
}

