using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script responsible for managing level, like spawning level, spawning balls, deciding game win/loss status and more
/// </summary>
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public List<LevelData> levelDatas;                      //list of all the available levels [We must use array instead of list and level count in fixed]
    public GameObject ballPrefab;                           //reference to ball prefab
    public Transform ballSpawnPos;                          //reference to spawn position of ball

    private int availableCans, availableBalls;              //count to store available cans and balls

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Method to spawn balls
    /// </summary>
    public void SpawnBall()                                        
    {
        if (availableBalls > 0)                                                 //we check if availableBalls is more than zero
        {
                                                                                //then we Instantiate the ball at spawn position
            GameObject ball = Instantiate(ballPrefab, ballSpawnPos.position, Quaternion.identity);
            SoundManager.instance.PlayFx(FxTypes.BALLSPAWN);                    //play ball spawn music
            SwipeControl.instance.BallObj = ball;                               //give ball reference to SwipeControl
            availableBalls--;                                                   //reduce availableBalls count by 1
            UIManager.instance.BallText.text = availableBalls.ToString();       //update the text
        }
        else                                                                    //if availableBalls is equal or less than zero
        {
            if (GameManager.instance.GameStatus == GameStatus.PLAYING)          //we check for game status, if its playing
            {
                GameManager.instance.SetGameStatus(GameStatus.FAILED);          //we set it to failed
                UIManager.instance.GameResult(GameManager.instance.GameStatus); //call GameResult method of UIManager
                SoundManager.instance.PlayFx(FxTypes.GAMEOVER);                 //play gameover sound
            }
        }
    }

    /// <summary>
    /// Method to  spawn respective level
    /// </summary>
    public void SpawnLevel()
    {   //we spawn the level prefab at required position and get the LevelData component attached on it
        LevelData levelData = Instantiate(levelDatas[GameManager.instance.CurrentLevel].gameObject, new Vector3(0, 2.5f, 8.5f), 
            Quaternion.identity).GetComponent<LevelData>();
        availableBalls = levelData.totalBalls;                                  //set the availableBalls
        availableCans = levelData.totalCans;                                    //set the availableCans
        UIManager.instance.CanText.text = availableCans.ToString();             //set the Cans text
        UIManager.instance.BallText.text = availableBalls.ToString();           //set the Balls text

        SpawnBall();                                                            //Spawn the ball
        GameManager.instance.SetGameStatus(GameStatus.PLAYING);                 //Set the game status to playing
    }

    /// <summary>
    /// Method used to reduce can
    /// </summary>
    public void ReduceCan()
    {
        availableCans--;                                                        //reduce the can by 1
        UIManager.instance.CanText.text = availableCans.ToString();             //set the Cans text

        if (GameManager.instance.GameStatus == GameStatus.PLAYING)          //and if gamestatus is playing
        {
            if (availableCans <= 0)                                                 //if availableCans is equal or less than 0
            {
                GameManager.instance.SetGameStatus(GameStatus.COMPLETE);        //set gamestatus to Complete
                UIManager.instance.GameResult(GameManager.instance.GameStatus); //call GameResult method of UIManager
                SoundManager.instance.PlayFx(FxTypes.GAMEWIN);                  //play win sound fx
            }
        }
    }
}