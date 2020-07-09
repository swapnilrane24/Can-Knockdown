using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Script to control game UI
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager instance;


    [SerializeField] private Text canText, ballText;                    //reference to game texts
    [SerializeField] private GameObject mainMenu, gameMenu, gameFinish, retryBtn, nextBtn;  //reference to UI gameobjects
    [SerializeField] private GameObject container, lvlButtonPrefab;     //reference to container and level button prefab

    public Text CanText { get { return canText; } }                     //getter for can text
    public Text BallText { get { return ballText; } }                   //getter for ball text

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

    private void Start()
    {
        //we check for game status, failed or complete
        if (GameManager.instance.GameStatus == GameStatus.FAILED || GameManager.instance.GameStatus == GameStatus.COMPLETE)
        {
            mainMenu.SetActive(false);                                      //deavtivate main menu
            gameMenu.SetActive(true);                                       //activate game menu
            LevelManager.instance.SpawnLevel();                             //spawn level
        }
        else if (GameManager.instance.GameStatus == GameStatus.NONE)        //if its none
        {
            CreateLevelButtons();                                           //main main level buttons grid is create
        }
    }

    /// <summary>
    /// Method which spawn levels button
    /// </summary>
    void CreateLevelButtons()
    {
        //total count is number of level datas
        for (int i = 0; i < LevelManager.instance.levelDatas.Count; i++)
        {
            GameObject buttonObj = Instantiate(lvlButtonPrefab, container.transform);   //spawn the button prefab
            buttonObj.transform.GetChild(0).GetComponent<Text>().text = "" + (i + 1);   //set the text child
            Button button = buttonObj.GetComponent<Button>();                           //get the button componenet
            button.onClick.AddListener(() => OnClick(button));                          //add listner to button
        }
    }

    /// <summary>
    /// Method called when we click on button
    /// </summary>
    /// <param name="btn"></param>
    void OnClick(Button btn)
    {
        mainMenu.SetActive(false);                                                      //deactivate main menu
        gameMenu.SetActive(true);                                                       //activate game manu
        GameManager.instance.CurrentLevel = btn.transform.GetSiblingIndex(); ;          //set current level equal to sibling index on button
        LevelManager.instance.SpawnLevel();                                             //spawn level
    }

    /// <summary>
    /// Method call after level fail or win
    /// </summary>
    /// <param name="gameStatus"></param>
    public void GameResult(GameStatus gameStatus)
    {
        switch (gameStatus)
        {
            case GameStatus.COMPLETE:                       //if completed
                gameFinish.SetActive(true);                 //activate game finish panel
                nextBtn.SetActive(true);                    //activate next button
                break;
            case GameStatus.FAILED:                         //if failed
                gameFinish.SetActive(true);                 //activate game finish panel
                retryBtn.SetActive(true);                   //activate retry button
                break;
        }
    }

    public void HomeBtn()                                   //method to load level again and go to main menu
    {
        GameManager.instance.GameStatus = GameStatus.NONE;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextRetryBtn()                               //method to load level again without going to main menu
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
