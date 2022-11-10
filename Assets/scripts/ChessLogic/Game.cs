using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    PieceMovement pieceMovement;
    public int gamemode;
    public static bool GameIsPaused;
    public GameObject SettingMenuUI;
    public GameObject SettingButton;
    public GameObject GameOverUi;
    public Text WhoWon;
    public GameObject PromotingWhite;
    public GameObject PromotingBlack;


    public void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1 && !GameIsPaused)
        {
            SettingMenuUI.SetActive(false);
            SettingButton.SetActive(true);
        }
            
    }
    public void GoToMainMenu()
    {
        GameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void PausGames()
    {
        GameIsPaused = true;
        SettingMenuUI.SetActive(true);
        SettingButton.SetActive(false);
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        GameIsPaused = false;
        SettingMenuUI.SetActive(false);
        SettingButton.SetActive(true);
        Time.timeScale = 1f;
    }
    public void StartGame()
    {
        GameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    public void QuitGame() 
    {
        Application.Quit();

    }
    public void GameMode(int DropDownIndex)
    {
        if(DropDownIndex == 0)
        {//BlackVsComputer
            
            Debug.Log("1");
            PieceMovement.LocalGameMode = 1;
        }
        if(DropDownIndex == 1)
        {//WhiteVsComputer
            
            Debug.Log("2");
            PieceMovement.LocalGameMode = 2;
        }
        if(DropDownIndex == 2)
        {//2players
            Debug.Log("3");
            PieceMovement.LocalGameMode = 3;
        } 
        if(DropDownIndex == 3)
        {//computerVsComputer
            Debug.Log("4");
            PieceMovement.LocalGameMode = 4;
        }

    }
   
   
}
