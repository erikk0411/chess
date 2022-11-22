using Codice.Client.Common;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Unity.Android.Types;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

public class ChessAI : MonoBehaviour
{

    bool[] EnPassantSquares;


    
    
    
    public static List<int> OldGetComputerMoves(string gameString, bool WhiteCanCastleKs, bool WhiteCanCastleQs, bool BlackCanCastleKs, bool BlackCanCastleQs, bool[] EnPassantSquares, bool WhiteToMove)
    {

        List<int> CurrentLegalMoves = new();

        List<List<int>> AllMoves = new();
        List<int> ReturnMove;
        int value;
        CurrentLegalMoves.Clear();
        AllMoves.Clear();

        if (WhiteToMove)
        {

            for (int i = 0; i < gameString.Length; i++)
            {


                if (PieceColour(gameString[i]) == "White")
                {
                    CurrentLegalMoves = LegalMoves.LegalMovesList(i, gameString[i], gameString, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, WhiteToMove, true);
                    if (CurrentLegalMoves.Count > 0)
                    {
                        for (int j = 0; j < CurrentLegalMoves.Count; j++)
                        {
                            List<int> CurrentMove = new();
                            CurrentMove.Add(i);

                            CurrentMove.Add(CurrentLegalMoves[j]);
                            value = OldValuateMove(gameString, i, CurrentLegalMoves[j], WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, WhiteToMove, true);
                            CurrentMove.Add(value);
                            if (CurrentMove.Count == 3)
                            {


                                AllMoves.Add(CurrentMove);



                            }
                            else
                            {
                                continue;
                            }
                        }





                    }

                }



            }

            ReturnMove = OldChooseMove(AllMoves, WhiteToMove);

            return ReturnMove;
        }
        if (!WhiteToMove)
        {
            for (int i = 0; i < gameString.Length; i++)
            {
                if (PieceColour(gameString[i]) == "Black")
                {


                    CurrentLegalMoves = LegalMoves.LegalMovesList(i, gameString[i], gameString, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, WhiteToMove, true);
                    if (CurrentLegalMoves.Count > 0)
                    {
                        for (int j = 0; j < CurrentLegalMoves.Count; j++)
                        {
                            List<int> CurrentMove = new();
                            CurrentMove.Add(i);
                            CurrentMove.Add(CurrentLegalMoves[j]);
                            value = OldValuateMove(gameString, i, CurrentLegalMoves[j], WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, WhiteToMove, true);
                            CurrentMove.Add(value);

                            if (CurrentMove.Count == 3)
                            {

                                AllMoves.Add(CurrentMove);

                            }
                            else
                            {
                                continue;
                            }

                        }


                    }

                }


            }

            ReturnMove = OldChooseMove(AllMoves, WhiteToMove);

            return ReturnMove;
        }
        return new List<int> { 6, 21 };
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
    public static Vector3Int GetVectorFromIndex(int Index)
    {

        int y = 8 - Mathf.CeilToInt((Index) / 8);
        int x = Mathf.CeilToInt((Index) % 8);
        return new Vector3Int(x, y, 0);


    }
    public static List<int> OldChooseMove(List<List<int>> LegalMoves, bool WhiteToMove)
    {
        List<int> ReturnMove = new();
        List<List<int>> GoodMoves = new();
        int Index = 0;
        int HighestCurrentValue = 0;
        int LowestCurrentValue = 0;
        int count = 0;
        if (WhiteToMove)
        {
            HighestCurrentValue = LegalMoves[0][2];
            for (int i = 0; i < LegalMoves.Count; i++)
            {

                if (LegalMoves[i][2] >= HighestCurrentValue)
                {
                    if ((LegalMoves[i][2] > HighestCurrentValue))
                    {
                        HighestCurrentValue = LegalMoves[i][2];
                        Index = i;
                        count += 1;
                    }
                    else if (UnityEngine.Random.Range(0, 10) > 5)
                    {
                        HighestCurrentValue = LegalMoves[i][2];
                        Index = i;
                        count += 1;
                    }


                }
                else if (LegalMoves[i][2] < LowestCurrentValue)
                {
                    LowestCurrentValue = LegalMoves[i][2];

                }
                else
                {
                    GoodMoves.Add(LegalMoves[i]);
                }

            }


            if (count == 0)
            {
                if (GoodMoves.Count > 0)
                {

                    Index = UnityEngine.Random.Range(0, GoodMoves.Count);
                    ReturnMove.Add(GoodMoves[Index][0]);
                    ReturnMove.Add(GoodMoves[Index][1]);

                }
                else
                {

                    Index = UnityEngine.Random.Range(0, LegalMoves.Count);
                    ReturnMove.Add(LegalMoves[Index][0]);
                    ReturnMove.Add(LegalMoves[Index][1]);

                }
            }
            else if (count > 0)
            {
                ReturnMove.Add(LegalMoves[Index][0]);
                ReturnMove.Add(LegalMoves[Index][1]);


            }
        }
        else if (!WhiteToMove)
        {
            HighestCurrentValue = LegalMoves[0][2];
            for (int i = 0; i < LegalMoves.Count; i++)
            {

                if (LegalMoves[i][2] <= HighestCurrentValue)
                {

                    if (LegalMoves[i][2] < HighestCurrentValue)
                    {
                        HighestCurrentValue = LegalMoves[i][2];
                        Index = i;
                        count += 1;
                    }
                    else if (UnityEngine.Random.Range(0, 10) > 5)
                    {
                        HighestCurrentValue = LegalMoves[i][2];
                        Index = i;
                        count += 1;
                    }

                }
                else if (LegalMoves[i][2] > LowestCurrentValue)
                {
                    LowestCurrentValue = LegalMoves[i][2];
                }
                else
                {
                    GoodMoves.Add(LegalMoves[i]);
                }

            }
            if (count == 0)
            {
                if (GoodMoves.Count > 0)
                {
                    Index = UnityEngine.Random.Range(0, GoodMoves.Count);
                    ReturnMove.Add(GoodMoves[Index][0]);
                    ReturnMove.Add(GoodMoves[Index][1]);

                }
                else
                {
                    Index = UnityEngine.Random.Range(0, LegalMoves.Count);
                    ReturnMove.Add(LegalMoves[Index][0]);
                    ReturnMove.Add(LegalMoves[Index][1]);

                }
            }
            else if (count > 0)
            {
                ReturnMove.Add(LegalMoves[Index][0]);
                ReturnMove.Add(LegalMoves[Index][1]);


            }
        }
        Debug.Log(HighestCurrentValue + "currentvalue");
        Debug.Log((Mathf.CeilToInt((63 - ReturnMove[1]) / 8 + 1) + "rad"));
        Debug.Log((Mathf.CeilToInt((63 - ReturnMove[1]) % 8 + 1) + "column"));
        return ReturnMove;

    }
    public static string UpdateString(string gamesstring, int index, char NewChar)
    {

        StringBuilder sb = new(gamesstring);
        sb[index] = NewChar;
        gamesstring = sb.ToString();
        return gamesstring;
    }

    public static int PieceValue(char Piece, int index)
    {
        /*const int PawnValue = 100;
        const int KnightValue = 300;
        const int BishopValue = 300;
        const int RookValue = 500;
        const int queenValue = 900;*/

        if (Piece == 'p' || Piece == 'P')
        {

            if (Piece == 'p')
            {
                float Row = Mathf.CeilToInt((index) / 8) + 1;
                int x = Mathf.CeilToInt(Mathf.Pow(1.05F, Row));
                return 100 + x;
            }
            if (Piece == 'P')
            {
                float Row = Mathf.CeilToInt((63 - index) / 8) + 1;
                int x = Mathf.CeilToInt(Mathf.Pow(1.05F, Row));
                return 100 + x;
            }
            else return 100;

        }
        else if (Piece == 'n' || Piece == 'N')
        {
            return 300;


        }
        else if (Piece == 'b' || Piece == 'B')
        {
            return 300;
        }
        else if (Piece == 'r' || Piece == 'R')
        {
            return 500;
        }
        else if (Piece == 'q' || Piece == 'Q')
        {
            return 900;
        }
        else
        {
            return 0;
        }

    }

    public static int OldValuation(string gamestring, int Index, int Move, bool WhiteCanCastleKs, bool WhiteCanCastleQs, bool BlackCanCastleKs, bool BlackCanCastleQs, bool[] EnPassantSquares, bool WhiteToMove)
    {





        int value = 0;
        string possiblegameString;
        char Piece = gamestring[Index];
        int valueGained = PieceValue(gamestring[Move], Move);
        int valueLost = 0;
        int positionValueGained = PieceValue(gamestring[Index], Move);
        possiblegameString = UpdateString(gamestring, Index, '.');
        possiblegameString = UpdateString(possiblegameString, Move, Piece);
        string CurrentColour = PieceColour(Piece);
        bool localWhitetoMove = WhiteToMove;
        string CurrentPossibleGamestring = gamestring;
        if (WhiteToMove)
        {
            value += (PieceValue(Piece, Index) - positionValueGained);
        }
        if (!WhiteToMove)
        {
            value -= (PieceValue(Piece, Index) - positionValueGained);
        }


        if (WhiteToMove)
        {
            if (PieceMovement.WhiteOpeningMoves.ContainsKey(Move))
            {
                if (PieceMovement.WhiteOpeningMoves[Move] == Piece)
                {
                    value += 75;
                }
            }
        }
        else if (!WhiteToMove)
        {
            if (PieceMovement.BlackOpeningMoves.ContainsKey(Move))
            {
                if (PieceMovement.BlackOpeningMoves[Move] == Piece)
                {
                    value -= 75;
                }
            }
        }




        for (int i = 0; i <= 3; i++)
        {
            int CurrentvalueLost = 0;
            int currentbestmove;
            bool[] currentEnPassantSquares = EnPassantSquares;


            for (int j = 0; j < CurrentPossibleGamestring.Length; j++)
            {
                List<int> CurrentLegalMoves = LegalMoves.LegalMovesList(j, CurrentPossibleGamestring[j], CurrentPossibleGamestring, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, currentEnPassantSquares, !WhiteToMove, true);

                if (CurrentLegalMoves.Count > 0)
                {

                    CurrentvalueLost = 0;
                    for (int z = 0; z < CurrentLegalMoves.Count; z++)
                    {



                        if (localWhitetoMove)
                        {
                            if (PieceValue(CurrentPossibleGamestring[CurrentLegalMoves[z]], CurrentLegalMoves[z]) > CurrentvalueLost)
                            {
                                CurrentPossibleGamestring = UpdateString(CurrentPossibleGamestring, CurrentLegalMoves[z], CurrentPossibleGamestring[CurrentLegalMoves[z]]);
                                CurrentPossibleGamestring = UpdateString(CurrentPossibleGamestring, j, '.');
                                CurrentvalueLost = (PieceValue(CurrentPossibleGamestring[CurrentLegalMoves[z]], CurrentLegalMoves[z]));
                            }
                        }
                        else if (!localWhitetoMove)
                        {
                            if (PieceValue(CurrentPossibleGamestring[CurrentLegalMoves[z]], CurrentLegalMoves[z]) > CurrentvalueLost)
                            {
                                CurrentPossibleGamestring = UpdateString(CurrentPossibleGamestring, CurrentLegalMoves[z], CurrentPossibleGamestring[CurrentLegalMoves[z]]);
                                CurrentPossibleGamestring = UpdateString(CurrentPossibleGamestring, j, '.');
                                CurrentvalueLost = (PieceValue(CurrentPossibleGamestring[CurrentLegalMoves[z]], CurrentLegalMoves[z]));
                            }
                        }
                    }
                }


            }
            if (i % 2 == 0)
            {
                valueLost -= CurrentvalueLost;
            }
            if (i % 2 != 0)
            {
                valueLost += CurrentvalueLost;
            }
            localWhitetoMove = !localWhitetoMove;
        }





        if (CurrentColour == "White")
        {
            value += (valueGained - valueLost);
        }
        else if (CurrentColour == "Black")
        {
            value -= (valueGained - valueLost);
        }
        if (LegalMoves.NoLegalMoves(possiblegameString, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, !WhiteToMove) && LegalMoves.IsCheck(possiblegameString, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, !WhiteToMove))
        {//checkmate

            if (CurrentColour == "White")
            {
                value += 1000000;
            } else value -= 1000000;

        }

        if (LegalMoves.NoLegalMoves(possiblegameString, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, !WhiteToMove) && !LegalMoves.IsCheck(possiblegameString, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, !WhiteToMove))
        { //stalemate
            if (CurrentColour == "White")
            {
                value -= 10000;
            }
            else value += 10000;
        }




        if (Piece == 'k')
        {
            if ((Mathf.CeilToInt(Move / 8) + 1) != 1)
            {
                value += 25;
            }
            else
            {
                value += 10;
            }
        }
        if (Piece == 'K')
        {
            if ((Mathf.CeilToInt((Move) / 8) + 1) != 8)
            {
                value -= 25;
            }
            else
            {
                value -= 10;
            }
        }
        if (Piece == 'r' && Index - Move == 1 || Piece == 'r' && Move - Index == 1 || Piece == 'r' && Index - Move == 8 || Piece == 'r' && Move - Index == 8)
        {
            value += 10;
        }
        if (Piece == 'R' && Index - Move == 1 || Piece == 'R' && Move - Index == 1 || Piece == 'R' && Index - Move == 8 || Piece == 'R' && Move - Index == 8)
        {
            value -= 10;
        }

        if (Piece == 'p' && Mathf.CeilToInt((Move) / 8 + 1) == 8)
        {
            value -= 200;
        }
        if (Piece == 'P' && Mathf.CeilToInt((Move) / 8 + 1) == 1)
        {
            value += 200;
        }




        return value;





    }
    public static int OldValuateMove(string gamestring, int Index, int Move, bool WhiteCanCastleKs, bool WhiteCanCastleQs, bool BlackCanCastleKs, bool BlackCanCastleQs, bool[] EnPassantSquares, bool WhiteToMove, bool firstvaluation)
    {

        //posetive = good for white
        //negative = good for black
        int value = OldValuation(gamestring, Index, Move, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, WhiteToMove);

        char Piece = gamestring[Index];




        return value;

    }
   
    
   
    public static List<List<int>> TestValue(int ply, string gamestring,List<List<int>> allMoves, bool WhiteCanCastleKs, bool WhiteCanCastleQs, bool BlackCanCastleKs, bool BlackCanCastleQs, bool[] EnPassantSquares, bool WhiteToMove)
    {
      
        for(int i = 0; i < allMoves.Count; i++)
        {
            string PossibleGamestring = MakeMoveEval(gamestring, allMoves[i][1], allMoves[i][0]);
            allMoves[i].Add(deepSearch(PossibleGamestring, 1, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, WhiteToMove,valuateBoard(PossibleGamestring)));
        }
        return allMoves;
    }


    public static int valuateBoard(string gamestring)
    {
        int value = 0;

        for (int i = 0; i < gamestring.Length; i++)
        {
            if (PieceColour(gamestring[i]) == "White")
            {
                value += PieceValue(gamestring[i], i);
            }
            else if (PieceColour(gamestring[i]) == "Black")
            {
                value -= PieceValue(gamestring[i], i);
            }

            else
            {
                continue;
            }

        }
        return value;
    }
    public List<List<int>> GetMoves(string gameString, bool WhiteCanCastleKs, bool WhiteCanCastleQs, bool BlackCanCastleKs, bool BlackCanCastleQs, bool[] EnPassantSquares, bool WhiteToMove)
    {
       
        List<int> CurrentLegalMoves = new();

        List<List<int>> AllMoves = new();
        
        CurrentLegalMoves.Clear();
        AllMoves.Clear();
        if (WhiteToMove)
        {

            for (int i = 0; i < gameString.Length; i++)
            {


                if (PieceColour(gameString[i]) == "White")
                {
                    CurrentLegalMoves = LegalMoves.LegalMovesList(i, gameString[i], gameString, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, WhiteToMove, true);
                    if (CurrentLegalMoves.Count > 0)
                    {
                        for (int j = 0; j < CurrentLegalMoves.Count; j++)
                        {
                            List<int> CurrentMove = new();
                            CurrentMove.Add(i);

                            CurrentMove.Add(CurrentLegalMoves[j]);
                            AllMoves.Add(CurrentMove);
                           
                            
                          
                        }
                    }
                }
            }
            return AllMoves;
        }
        if (!WhiteToMove)
        {
            for (int i = 0; i < gameString.Length; i++)
            {
                if (PieceColour(gameString[i]) == "Black")
                {


                    CurrentLegalMoves = LegalMoves.LegalMovesList(i, gameString[i], gameString, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, WhiteToMove, true);
                    if (CurrentLegalMoves.Count > 0)
                    {
                        for (int j = 0; j < CurrentLegalMoves.Count; j++)
                        {
                            List<int> CurrentMove = new();
                            CurrentMove.Add(i);

                            CurrentMove.Add(CurrentLegalMoves[j]);
                            AllMoves.Add(CurrentMove);
                        }
                    }
                }
            }
            return AllMoves;   
        }
     return new List<List<int>>(); 
    }

    
    public static List<int> Main(string gameString, bool WhiteCanCastleKs, bool WhiteCanCastleQs, bool BlackCanCastleKs, bool BlackCanCastleQs, bool[] EnPassantSquares, bool WhiteToMove)
    {
        int BestIndex = 0;
        int BestValue=0;
        List<List<int>>AllMoves = GetAllMoves(gameString, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, WhiteToMove);
        AllMoves = TestValue(3, gameString, AllMoves, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, WhiteToMove);
        for(int i = 0; i < AllMoves.Count; i++)
        {
            if (i == 0)
            {
                 BestValue = AllMoves[0][2];
            }
            if (AllMoves[i][2] > BestValue)
            {
                BestValue=AllMoves[i][2];
                BestIndex = i;
            }

        }
        List<int> ReturnMove = AllMoves[BestIndex];
        ReturnMove.RemoveAt(2);
        return ReturnMove;

    }
    public static int deepSearch(string Gamestring,int ply, bool WhiteCanCastleKs,bool  WhiteCanCastleQs, bool BlackCanCastleKs,bool  BlackCanCastleQs, bool[]EnPassantSquares, bool WhiteToMove,int LowestValue)
    {   
        int CurrentValue= valuateBoard(Gamestring);
       
        List<List<int>> AllMoves = GetAllMoves(Gamestring, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, WhiteToMove);
        if(ply > 0)
        {
            if (CurrentValue< LowestValue)
            {
                LowestValue= CurrentValue;
            }
            for (int i = 0; i < AllMoves.Count; i++)
            {
                Gamestring = MakeMoveEval(Gamestring, AllMoves[i][1], AllMoves[i][0]);
                CurrentValue = deepSearch(Gamestring, ply - 1, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, !WhiteToMove, LowestValue);
            }
        }
        return LowestValue;
    }
    public static List<List<int>> GetAllMoves(string gameString, bool WhiteCanCastleKs, bool WhiteCanCastleQs, bool BlackCanCastleKs, bool BlackCanCastleQs, bool[] EnPassantSquares, bool WhiteToMove)
    {

        List<int> CurrentLegalMoves = new();

        List<List<int>> AllMoves = new();


        CurrentLegalMoves.Clear();
        AllMoves.Clear();

        if (WhiteToMove)
        {

            for (int i = 0; i < gameString.Length; i++)
            {


                if (PieceColour(gameString[i]) == "White")
                {
                    CurrentLegalMoves = LegalMoves.LegalMovesList(i, gameString[i], gameString, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, WhiteToMove, true);
                    if (CurrentLegalMoves.Count > 0)
                    {
                        for (int j = 0; j < CurrentLegalMoves.Count; j++)
                        {
                            List<int> CurrentMove = new();
                            CurrentMove.Add(i);

                            CurrentMove.Add(CurrentLegalMoves[j]);

                            if (CurrentMove.Count == 2)
                            {


                                AllMoves.Add(CurrentMove);



                            }
                            else
                            {
                                continue;
                            }
                        }





                    }

                }



            }
            return AllMoves;

        }
        if (!WhiteToMove)
        {
            for (int i = 0; i < gameString.Length; i++)
            {
                if (PieceColour(gameString[i]) == "Black")
                {


                    CurrentLegalMoves = LegalMoves.LegalMovesList(i, gameString[i], gameString, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, WhiteToMove, true);
                    if (CurrentLegalMoves.Count > 0)
                    {
                        for (int j = 0; j < CurrentLegalMoves.Count; j++)
                        {
                            List<int> CurrentMove = new();
                            CurrentMove.Add(i);
                            CurrentMove.Add(CurrentLegalMoves[j]);


                            if (CurrentMove.Count == 2)
                            {

                                AllMoves.Add(CurrentMove);

                            }
                            else
                            {
                                continue;
                            }

                        }


                    }

                }


            }



            return AllMoves;
        }
        return AllMoves;


    }

    public static string MakeMoveEval(string gamestring,int squareIndex,int OldsquareIndex)
    {
        Char currentGameStringChar = gamestring[OldsquareIndex];

     
        if (currentGameStringChar == 'p' && Mathf.CeilToInt((squareIndex) / 8 + 1) == 8 )
        {//promoting
           

                gamestring = UpdateString(gamestring, OldsquareIndex, '.');
                gamestring = UpdateString(gamestring, squareIndex, 'q');
                Debug.Log(currentGameStringChar);


            
         



        }
        else if (currentGameStringChar == 'P' && Mathf.CeilToInt((squareIndex) / 8 + 1) == 1 )
        {//promoting
      

                gamestring = UpdateString(gamestring, OldsquareIndex, '.');
                gamestring = UpdateString(gamestring, squareIndex, 'Q');
                Debug.Log(currentGameStringChar);



        }
        else if (currentGameStringChar == 'k' && squareIndex == OldsquareIndex - 3 || currentGameStringChar == 'k' && squareIndex == OldsquareIndex + 2)
        {
            if (squareIndex == OldsquareIndex + 2)
            {//BlackKingsside
                gamestring = UpdateString(gamestring, 4, '.');
                gamestring = UpdateString(gamestring, 7, '.');
                gamestring = UpdateString(gamestring, 5, 'r');
                gamestring = UpdateString(gamestring, 6, 'k');
      

            }
            else if (squareIndex == OldsquareIndex - 3)
            {//BlackQueensside
                gamestring = UpdateString(gamestring, 4, '.');
                gamestring = UpdateString(gamestring, 0, '.');
                gamestring = UpdateString(gamestring, 1, '.');
                gamestring = UpdateString(gamestring, 3, 'r');
                gamestring = UpdateString(gamestring, 2, 'k');
            }
        }
        else if (currentGameStringChar == 'K' && squareIndex == OldsquareIndex + 2 || currentGameStringChar == 'K' && squareIndex == OldsquareIndex - 3)
        {
            if (squareIndex == OldsquareIndex + 2)
            {//WhiteKingsside
                gamestring = UpdateString(gamestring, 60, '.');
                gamestring = UpdateString(gamestring, 63, '.');
                gamestring = UpdateString(gamestring, 61, 'R');
                gamestring = UpdateString(gamestring, 62, 'K');
             

            }
            else if (squareIndex == OldsquareIndex - 3)
            {//WhiteQueensside
                gamestring = UpdateString(gamestring, 56, '.');
                gamestring = UpdateString(gamestring, 60, '.');
                gamestring = UpdateString(gamestring, 57, '.');
                gamestring = UpdateString(gamestring, 59, 'R');
                gamestring = UpdateString(gamestring, 58, 'K');

            }
        }
        else if (PieceMovement.IsEnPassant(OldsquareIndex, squareIndex, gamestring, currentGameStringChar))
        {
            gamestring = UpdateString(gamestring, OldsquareIndex, '.');
            gamestring = UpdateString(gamestring, squareIndex, currentGameStringChar);
            if (currentGameStringChar == 'p')
            {//black
                if (squareIndex - OldsquareIndex == 7)
                {
                    gamestring = UpdateString(gamestring, OldsquareIndex - 1, '.');
                }
                else if (squareIndex - OldsquareIndex == 9)
                {
                    gamestring = UpdateString(gamestring, OldsquareIndex + 1, '.');
                }

            }
            else if (currentGameStringChar == 'P')
            {//white
                if (OldsquareIndex - squareIndex == 7)
                {
                    gamestring = UpdateString(gamestring, squareIndex + 8, '.');

                }
                else if (OldsquareIndex - squareIndex == 9)
                {
                    gamestring = UpdateString(gamestring, squareIndex + 8, '.');

                }
            }


        }
        else
        {//Normal Moves

            gamestring = UpdateString(gamestring, OldsquareIndex, '.');
            gamestring = UpdateString(gamestring, squareIndex, currentGameStringChar);
            //EnPassantSquares = LegalMoves.IsEnPassantable(OldsquareIndex, squareIndex, currentGameStringChar);


        }
        return gamestring;
    }
  
    



}

