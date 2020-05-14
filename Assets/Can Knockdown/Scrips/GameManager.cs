using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton sript used to keep track of important game data: game status, level, etc
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;                                 //static make sure we have only one copy of it at any given time

    private GameStatus gameStatus = GameStatus.NONE;                    //by default gameStatus is none

    public GameStatus GameStatus { get { return gameStatus; } }         //getter for gameStatus

    private int currentLevel = 0;                                       //int variable to store current level index
    public int CurrentLevel { get { return currentLevel; } set { currentLevel = value; } }  //getter and setter for current level

    private void Awake()
    {
        if (instance == null)                                           //we check if instance in null
        {   
            instance = this;                                            //if yes we set this script reference inside the instance
            DontDestroyOnLoad(gameObject);                              //and ask unity to dont destroy the gameobject when we change scenes
        }
        //the below code is to prevent multiple objects with GameManager on it, we want only one GameManager throughout the game
        else                                                            //if instance is not null, as instance is static and only one can be there
        {
            Destroy(gameObject);                                        //we destroy the current gameobject
        }

        //currentLevel = PlayerPrefs.GetInt("LevelIndex", 0);              
    }

    /// <summary>
    /// Method to set the status of game
    /// </summary>
    /// <param name="gameStatus"></param>
    public void SetGameStatus(GameStatus gameStatus)
    {
        this.gameStatus = gameStatus;

        if (gameStatus == GameStatus.COMPLETE)                          //if game status is complete
        {
            if (currentLevel < LevelManager.instance.levelDatas.Count)  //if currentLevel is less than total levels we have
            {
                currentLevel++;                                         //we increase the currentLevel count
            }
            //PlayerPrefs.SetInt("LevelIndex", currentLevel);
        }
    }

}

[System.Serializable]
public enum GameStatus  //enum to set the status of game
{
    NONE,
    PLAYING,
    COMPLETE,
    FAILED
}