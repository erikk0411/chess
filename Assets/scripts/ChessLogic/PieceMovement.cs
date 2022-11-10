using Codice.CM.WorkspaceServer.DataStore.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

public class PieceMovement : MonoBehaviour
{
    public GameObject PromotingWhite;
    public GameObject PromotingBlack;
    public Tile BlackPawn;
    public Tile BlackRook;
    public Tile BlackKnight;
    public Tile BlackBishop;
    public Tile BlackQueen;
    public Tile BlackKing;
    public Tile WhitePawn;
    public Tile WhiteRook;
    public Tile WhiteKnight;
    public Tile WhiteBishop;
    public Tile WhiteQueen;
    public Tile WhiteKing;
    public bool Player1IsWhite;
    public bool BlackCanCastleKs = true;
    public bool BlackCanCastleQs = true;
    public bool WhiteCanCastleKs = true;
    public bool WhiteCanCastleQs = true;
    public Text Description;
    public GameObject GameOverUI;
    public Text GameOverText;
    public char PromotionPiece;
    int promotionIndex;
    bool PlayerPromoting = false;
   

    public bool ComputerHasMoved;

    List<int> CurrentLegalMoves;
    bool[] EnPassantSquares;

    /*0  1  2  3  4  5  6  7 Black
      8  9  10 11 12 13 14 15
      16 17 18 19 20 21 22 23
      24 25 26 27 28 29 30 31
      32 33 34 35 36 37 38 39
      40 41 42 43 44 45 46 47
      48 49 50 51 52 53 54 55
      56 57 58 59 60 61 62 63 White
    */
    Tilemap tilemap;
    string initGame;
    public string gameString;
    public string CurrentLetter;
    public char currentGameStringChar;
    public int tileindex;
    Tile selectedTile;
    private bool HaspickedPiece = false;
    int OldsquareIndex;
    public bool WhiteToMove = true;
    public Game gameScript;
    public static int LocalGameMode;

    public static Dictionary<int, char> WhiteOpeningMoves;
    public static Dictionary<int, char> BlackOpeningMoves;
    public static string WhiteOpening;
    public static string BlackOpening;
    public bool WhiteOpeningPhase = true;
    public bool BlackOpeningPhase = true;
    public int NumOfMoves;
    public bool UpdateBoard;


    public void Awake()
    {
        EnPassantSquares = LegalMoves.IsEnPassantable(1, 60, 'k');
        NumOfMoves = 0;
        WhiteOpening = GetWhiteOpening();
        Debug.Log(WhiteOpening);
        BlackOpening = GetBlackOpening();
        Debug.Log(BlackOpening);
        BlackOpeningMoves = GetOpeningMoves(BlackOpening);
        WhiteOpeningMoves = GetOpeningMoves(WhiteOpening);
        GameOverUI.SetActive(false);
        HaspickedPiece = false;
        WhiteToMove = true;
        PromotingWhite.SetActive(false);
        PromotingBlack.SetActive(false);
        initGame = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";
        //initGame = "k7/7P/8/8/8/8/p7/7K";
        BlackCanCastleKs = true;
        BlackCanCastleQs = true;
        WhiteCanCastleKs = true;
        WhiteCanCastleQs = true;
        tilemap = GetComponentInChildren<Tilemap>();
        gameString = FenUtil.FenStringToGameString(initGame);
        LoadPieces();
        
    }
    public void Update()
    {
        if (UpdateBoard)
        {
            LoadPieces();
            UpdateBoard = false;
        }
       
        if (!LegalMoves.NoLegalMoves(gameString, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, WhiteToMove) && !OnlyKingLeft(gameString)&& !PlayerPromoting)
        {   
            if (IsYourTurn(WhiteToMove))
            {
                MovePiece();
            }
            else if (!IsYourTurn(WhiteToMove))
            {
                MovePiece();
            }
        }
        if (NumOfMoves == 40)
        {
            WhiteOpeningMoves.Clear();
            BlackOpeningMoves.Clear();
        }
        
        
        if (WhiteToMove)
        {
            Description.text = "White to move";

        }
        else if(!WhiteToMove)
        {
            Description.text = "Black to move";
        }
        
        
        if(LegalMoves.NoLegalMoves( gameString,WhiteCanCastleKs,WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares,  WhiteToMove)&& !PlayerPromoting)
        {
            if(LegalMoves.IsCheck(gameString,WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, WhiteToMove))
            {
                if (!WhiteToMove)
                {
                    GameOverUI.SetActive(true);
                    GameOverText.text = "White Won \n by CheckMate ";

                }
                if (WhiteToMove)
                {
                    GameOverUI.SetActive(true);
                    GameOverText.text = "Black Won \n by CheckMate ";
                }
            }
            else
            {
                GameOverUI.SetActive(true);
                GameOverText.text = "Draw  by stalemate";
            }
            
        }
        if (OnlyKingLeft(gameString) && !PlayerPromoting)
        {
            
                GameOverUI.SetActive(true);
                GameOverText.text = "Draw ";
           
        }

    }


    public void LoadPieces()
    {
        
        for (int y = 8; y > 0; y--)
        {
            for (int x = 0; x < 8; x++)
            {   
                
                int gameStringPos = (8 - y) * 8 + x;
                char pieceChar = gameString[gameStringPos];
                Vector3Int position = new(x,y,0);
                
                Tile piece = GamestringCharToTile(pieceChar);

                tilemap.SetTile(position,piece);
                Debug.Log(gameString);
              
            }
        }
    }
    

    public bool IsYourTurn(bool WhiteToMove)
    {//if it is you or the computer to move
        if (LocalGameMode <2)
        {
            

            if (!WhiteToMove)
            {
                return true;
            }
            return false;
        }
        else if (LocalGameMode == 2)
        {
            
            if (WhiteToMove)
            {
                return true;
            }
            return false;
        }
        else if (LocalGameMode == 3)
        {
            
            return true;
        }
        else if (LocalGameMode == 4)
        {
           
            return false;
        }
        else return true;
    }
   
    public  void MovePiece()
    {
        

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int squarePosition = tilemap.WorldToCell(mousePosition);
        //Vector3Int CurrentSquarePosition;
        //Vector3Int DesiredSquarePosition;
        int computersMove;
        int ComputerOldSquareIndex;
        LegalMoves.IsCheck(gameString, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, WhiteToMove);
        if (!IsYourTurn(WhiteToMove) && !PlayerPromoting)
        {
            
            Vector3Int ComputersmoveVector;
            Vector3Int ComputersIndexVector;
            List<int>ComputersmoveList= ChessAI.OldGetComputerMoves(gameString, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, WhiteToMove);

            ComputerOldSquareIndex = ComputersmoveList[0];
            ComputersIndexVector = ChessAI.GetVectorFromIndex(ComputerOldSquareIndex);
            PlacePieces(ComputersIndexVector,true);

            computersMove = ComputersmoveList[1];
            ComputersmoveVector = ChessAI.GetVectorFromIndex(computersMove);
            PlacePieces(ComputersmoveVector,true);
           


        }

        if (Input.GetMouseButtonDown(0)&& IsYourTurn(WhiteToMove))
        {
            
            PlacePieces(squarePosition,false);
        }

    }
    public bool IsEnPassant(int OldsquareIndex,int squareIndex,string gameString,char piece)
    {
        if(piece == 'p')
        {//black
            if (squareIndex - OldsquareIndex == 7 && gameString[squareIndex-8]=='P'&& gameString[squareIndex]=='.' || squareIndex - OldsquareIndex == 9 && gameString[squareIndex - 8] == 'P' && gameString[squareIndex] == '.')
            {
               
                return true;
            }
            else 
            { 
                return false; 
            }
        }
        else if(piece == 'P')
        {//white
            if (OldsquareIndex-squareIndex  == 7 && gameString[squareIndex + 8] == 'p' && gameString[squareIndex] == '.' || OldsquareIndex - squareIndex == 9 && gameString[squareIndex + 8] == 'p' && gameString[squareIndex] == '.')
            {
               
                return true;
            }
            else { 
                
                return false;
                
            }
        }
        else
        {
           
            return false;
        }
    }
    public int GetSquareIndex(Vector3Int position)
    {
        int SquareIndex = (8 - position.y) * 8 + position.x;
        return SquareIndex;
    }
    public string UpdateString(string gamesstring,int index,char NewChar)
    {
        UpdateBoard = true;
        StringBuilder sb = new(gamesstring);
        sb[index]= NewChar;
        gamesstring = sb.ToString();
        return gamesstring;
    }

    public void PlacePieces(Vector3Int squarePosition,bool ComputersMove)
    {
        Vector3Int CurrentSquarePosition;
        Vector3Int DesiredSquarePosition;

       

        if (!HaspickedPiece)
        {
           
            selectedTile = tilemap.GetTile<Tile>(squarePosition);


            CurrentSquarePosition = squarePosition;
            OldsquareIndex = GetSquareIndex(CurrentSquarePosition);
           




            if (OldsquareIndex < 64 && OldsquareIndex >= 0)
            {
                
                currentGameStringChar = gameString[OldsquareIndex];
                CurrentLegalMoves = LegalMoves.LegalMovesList(OldsquareIndex, gameString[OldsquareIndex], gameString, WhiteCanCastleKs, WhiteCanCastleQs, BlackCanCastleKs, BlackCanCastleQs, EnPassantSquares, WhiteToMove, true);

            }




            if (selectedTile != null && CurrentLegalMoves.Count > 0)
            {
                
                HaspickedPiece = true;
            }

        }
        else if(HaspickedPiece)
        {

            
            DesiredSquarePosition = squarePosition;
            int squareIndex = GetSquareIndex(DesiredSquarePosition);
            
           
            if (CurrentLegalMoves.Contains(squareIndex))
            {
                

                if (squareIndex == 63 || OldsquareIndex == 63)
                {
                    WhiteCanCastleKs = false;
                }
                else if (squareIndex == 56 || OldsquareIndex == 56)
                {
                    WhiteCanCastleQs = false;
                }
                else if (squareIndex == 7 || OldsquareIndex == 7)
                {
                    BlackCanCastleKs = false;
                }
                else if (squareIndex == 0 || OldsquareIndex == 0)
                {
                    BlackCanCastleQs = false;
                }
                else if (OldsquareIndex == 4)
                {
                    BlackCanCastleKs = false;
                    BlackCanCastleQs = false;
                }
                else if (OldsquareIndex == 60)
                {
                    WhiteCanCastleKs = false;
                    WhiteCanCastleQs = false;
                }

                if (currentGameStringChar == 'p' && Mathf.CeilToInt((squareIndex) / 8 + 1) == 8 && CurrentLegalMoves.Contains(squareIndex))
                {//promoting
                    if (!IsYourTurn(WhiteToMove))
                    {

                        gameString = UpdateString(gameString, OldsquareIndex, '.');
                        gameString = UpdateString(gameString, squareIndex, 'q');
                        Debug.Log(currentGameStringChar);


                    }
                    else if (IsYourTurn(WhiteToMove))
                    {
                        PlayerPromoting = true;
                        promotionIndex = squareIndex;
                        BlackPromotePawn();
                        gameString = UpdateString(gameString, OldsquareIndex, '.');
                        
                    }



                }
                else if (currentGameStringChar == 'P' && Mathf.CeilToInt((squareIndex) / 8 + 1) == 1 && CurrentLegalMoves.Contains(squareIndex))
                {//promoting
                    if (!IsYourTurn(WhiteToMove))
                    {

                        gameString = UpdateString(gameString, OldsquareIndex, '.');
                        gameString = UpdateString(gameString, squareIndex, 'Q');
                        Debug.Log(currentGameStringChar);

                    }
                    else if (IsYourTurn(WhiteToMove))
                    {
                        PlayerPromoting = true;

                        promotionIndex = squareIndex;
                        WhitePromotePawn();
                        gameString = UpdateString(gameString, OldsquareIndex, '.');

                        
                    }



                }
                else if (currentGameStringChar == 'k' && squareIndex == OldsquareIndex - 3 || currentGameStringChar == 'k' && squareIndex == OldsquareIndex + 2)
                {
                    if (squareIndex == OldsquareIndex + 2)
                    {//BlackKingsside
                        gameString = UpdateString(gameString, 4, '.');
                        gameString = UpdateString(gameString, 7, '.');
                        gameString = UpdateString(gameString, 5, 'r');
                        gameString = UpdateString(gameString, 6, 'k');
                        BlackCanCastleKs = false;
                        BlackCanCastleQs = false;

                    }
                    else if (squareIndex == OldsquareIndex - 3 )
                    {//BlackQueensside
                        gameString = UpdateString(gameString, 4, '.');
                        gameString = UpdateString(gameString, 0, '.');
                        gameString = UpdateString(gameString, 1, '.');
                        gameString = UpdateString(gameString, 3, 'r');
                        gameString = UpdateString(gameString, 2, 'k');
                        BlackCanCastleKs = false;
                        BlackCanCastleQs = false;
                    }
                }
                else if (currentGameStringChar == 'K' && squareIndex == OldsquareIndex + 2 || currentGameStringChar == 'K' && squareIndex == OldsquareIndex - 3)
                {
                    if (squareIndex == OldsquareIndex + 2)
                    {//WhiteKingsside
                        gameString = UpdateString(gameString, 60, '.');
                        gameString = UpdateString(gameString, 63, '.');
                        gameString = UpdateString(gameString, 61, 'R');
                        gameString = UpdateString(gameString, 62, 'K');
                        WhiteCanCastleKs = false;
                        WhiteCanCastleQs = false;

                    }
                    else if (squareIndex == OldsquareIndex - 3 )
                    {//WhiteQueensside
                        gameString = UpdateString(gameString, 56, '.');
                        gameString = UpdateString(gameString, 60, '.');
                        gameString = UpdateString(gameString, 57, '.');
                        gameString = UpdateString(gameString, 59, 'R');
                        gameString = UpdateString(gameString, 58, 'K');
                        WhiteCanCastleKs = false;
                        WhiteCanCastleQs = false;
                    }
                }
                else if (IsEnPassant(OldsquareIndex, squareIndex, gameString, currentGameStringChar))
                {
                    gameString = UpdateString(gameString, OldsquareIndex, '.');
                    gameString = UpdateString(gameString, squareIndex, currentGameStringChar);
                    if (currentGameStringChar == 'p')
                    {//black
                        if (squareIndex - OldsquareIndex == 7)
                        {
                            gameString = UpdateString(gameString, OldsquareIndex - 1, '.');
                        }
                        else if (squareIndex - OldsquareIndex == 9)
                        {
                            gameString = UpdateString(gameString, OldsquareIndex + 1, '.');
                        }

                    }
                    else if (currentGameStringChar == 'P')
                    {//white
                        if (OldsquareIndex - squareIndex == 7)
                        {
                            gameString = UpdateString(gameString, squareIndex + 8, '.');

                        }
                        else if (OldsquareIndex - squareIndex == 9)
                        {
                            gameString = UpdateString(gameString, squareIndex + 8, '.');

                        }
                    }


                }

                else
                {//Normal Moves

                    gameString = UpdateString(gameString, OldsquareIndex, '.');
                    gameString = UpdateString(gameString, squareIndex, currentGameStringChar);
                    EnPassantSquares = LegalMoves.IsEnPassantable(OldsquareIndex, squareIndex, currentGameStringChar);

                    HaspickedPiece = false;
                }
                WhiteToMove = !WhiteToMove;
                NumOfMoves += 1;
            }
            else
            {

                HaspickedPiece = false;
            }

        }
    }












    public Tile GamestringCharToTile(char fenChar) {
     

        Tile t;
        switch(fenChar)
        {
            case 'r': 
                t = BlackRook;
                break;
            case 'n':
                t= BlackKnight;
                break;
            case 'b':
                t = BlackBishop;
                break;
            case 'q':
                t = BlackQueen;
                break;
            case 'k':
                t = BlackKing;
                break;
            case 'p':
                t = BlackPawn;
                break;
            case 'R': 
                t = WhiteRook;
                break;
            case 'N': 
                t = WhiteKnight;
                break;
            case 'B':
                t = WhiteBishop;
                break;
            case 'Q':
                t = WhiteQueen;
                break;
            case 'K':
                t = WhiteKing;
                break;
            case 'P':
                t = WhitePawn;
                break;
            case '.':
                t=null;
                break;
            
            default:
                throw new Exception("Unknown char " + fenChar);

        }
        return t;
    }
    public void WhitePromotePawn()
    {
        PromotingWhite.SetActive(true);
    }
    public void BlackPromotePawn()
    {
        PromotingBlack.SetActive(true);
    }
    public void WhitePromoteQueen()
    {
        gameString = UpdateString(gameString, promotionIndex, 'Q');
        PromotingWhite.SetActive(false);
         HaspickedPiece=false;

        PlayerPromoting = false;

    }
    public void WhitePromoteRook()
    {
        gameString = UpdateString(gameString, promotionIndex, 'R');
        PromotingWhite.SetActive(false);
        HaspickedPiece = false;

        PlayerPromoting = false;

    }
    public void WhitePromoteKnight()
    {
        gameString = UpdateString(gameString, promotionIndex, 'N');
        PromotingWhite.SetActive(false);
        HaspickedPiece = false;
        PlayerPromoting = false;
    }
    public void WhitePromoteBishop()
    {
        gameString = UpdateString(gameString, promotionIndex, 'B');
        PromotingWhite.SetActive(false);
        HaspickedPiece = false;

        PlayerPromoting = false;
    }
    public void BlackPromoteQueen()
    {
        gameString = UpdateString(gameString, promotionIndex, 'q');
        PromotingBlack.SetActive(false);
        HaspickedPiece = false;
        PlayerPromoting = false;
    }
    public void BlackPromoteRook()
    {
        gameString = UpdateString(gameString, promotionIndex, 'r');
        PromotingBlack.SetActive(false);
        HaspickedPiece = false;

        PlayerPromoting = false;
    }
    public void BlackPromoteknight()
    {
        gameString = UpdateString(gameString, promotionIndex, 'n');
        PromotingBlack.SetActive(false);
        HaspickedPiece = false;
        PlayerPromoting = false;
    }
    public void BlackPromoteBishop()
    {
        gameString = UpdateString(gameString, promotionIndex, 'b');
        PromotingBlack.SetActive(false);
        HaspickedPiece=false;
        PlayerPromoting=false;
    }
    public bool OnlyKingLeft(string gamestring)
    {
        for (int i = 0; i < gamestring.Length; i++)
        {
            if (gamestring[i] != '.')
            {
                if (gamestring[i] != 'k')
                {
                    if (gamestring[i] != 'K')
                    {
                        return false;
                    }
                }
            }
        }
        return true;

    }
    public static string GetWhiteOpening()
    {
        int OpeningInt = UnityEngine.Random.Range(1, 6);
        string Opening;
        if (OpeningInt == 1)
        {//kingsindianattack
            Opening = "KIA";
        }
        if (OpeningInt == 2)
        {//Italian Game
            Opening = "IGA";

        }
        if (OpeningInt == 3)
        {//Four Knights Opening
            Opening = "FKO";
        }
        if (OpeningInt == 4)
        {//london system
            Opening = "LSA";
        }
        else
        {//Vienna Gambit
            Opening = "VGA";
        }


        return Opening;
    }
    public static string GetBlackOpening()
    {
        int OpeningInt = UnityEngine.Random.Range(1, 6);
        string Opening;
        if (OpeningInt == 1)
        {//kings indian defence
            Opening = "KID";
        }
        if (OpeningInt == 2)
        {//Caro Kann defence
            Opening = "CAD";
        }
        if (OpeningInt == 3)
        {//Nimzo Indian
            Opening = "NID";
        }
        if (OpeningInt == 4)
        {//Random defence
            Opening = "RD";
        }
        else
        {//Dutch Defence
            Opening = "DD";
        }
        return Opening;
    }


    public static Dictionary<int, char> GetOpeningMoves(string Opening)
    {
        Dictionary<int, char> moves = new();
        if (Opening == "KIA")
        {//Kings Indian Attack
            moves.Add(36, 'P');
            moves.Add(43, 'P');
            moves.Add(46, 'P');
            moves.Add(51, 'N');
            moves.Add(45, 'N');
            moves.Add(54, 'B');
            moves.Add(62, 'K');

        }
        if (Opening == "IGA")
        {//Italian Game
            moves.Add(36, 'P');
            moves.Add(45, 'N');
            moves.Add(34, 'B');
            moves.Add(62, 'K');

        }
        if (Opening == "FKO")
        {//four knights opening
            moves.Add(36, 'P');
            moves.Add(42, 'N');
            moves.Add(45, 'N');
            moves.Add(62, 'K');
            moves.Add(34, 'B');

        }
        if (Opening == "LSA")
        {//london system
            moves.Add(45, 'N');
            moves.Add(37, 'B');
            moves.Add(35, 'P');
            moves.Add(62, 'K');


        }
        if (Opening == "VGA")
        {//Vienna Gambit

            moves.Add(36, 'P');
            moves.Add(37, 'P');
            moves.Add(42, 'N');
            moves.Add(43, 'B');
            moves.Add(62, 'K');
        }


        if (Opening == "KID")
        {//kings indian defence
            moves.Add(22, 'p');
            moves.Add(28, 'p');
            moves.Add(19, 'p');
            moves.Add(6, 'k');
            moves.Add(21, 'n');
            moves.Add(14, 'b');
        }
        if (Opening == "CAD")
        {//Caro Kann defence
            moves.Add(18, 'p');
            moves.Add(27, 'p');
            moves.Add(29, 'b');
            moves.Add(16, 'n');
            moves.Add(1, 'k');


        }
        if (Opening == "NID")
        {//Nimzo Indian
            moves.Add(33, 'b');
            moves.Add(21, 'n');
            moves.Add(20, 'p');
            moves.Add(6, 'k');

        }
        if (Opening == "RD")
        {//Random defence
            moves.Add(27, 'p');
            moves.Add(28, 'p');
            moves.Add(26, 'b');
            moves.Add(21, 'n');
            moves.Add(6, 'k');
        }
        if (Opening == "DD")
        {//Dutch Defence
            moves.Add(19, 'p');
            moves.Add(21, 'n');
            moves.Add(29, 'p');
            moves.Add(22, 'p');
            moves.Add(14, 'b');
            moves.Add(6, 'k');

        }
        else
        {

        }
        return moves;
    }



}
