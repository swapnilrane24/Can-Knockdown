using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to handle sound of the game
/// </summary>
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource fxSource;      //reference to audiosource which we will use for FX
    [SerializeField] private AudioClip canHitFx, gameOverFx, gameWinFx, ballSpawnFx, ballThrowFx;   //fx audio clips

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
    /// Method which plays the required sound fx
    /// </summary>
    /// <param name="fxType"></param>
    public void PlayFx(FxTypes fxType)
    {
        switch (fxType)                                 //switch case is used to run respective logic for respective FxType
        {
            case FxTypes.CANHIT:                        //if its CanHit
                fxSource.PlayOneShot(canHitFx);         //play canhit fx
                break;
            case FxTypes.GAMEOVER:                      //if its GAMEOVER
                fxSource.PlayOneShot(gameOverFx);       //play GAMEOVER fx
                break;
            case FxTypes.GAMEWIN:                       //if its GAMEWIN
                fxSource.PlayOneShot(gameWinFx);        //play GAMEWIN fx
                break;
            case FxTypes.BALLTHROW:
                fxSource.PlayOneShot(ballThrowFx);      
                break;
            case FxTypes.BALLSPAWN:                     
                fxSource.PlayOneShot(ballSpawnFx);      
                break;
        }
    }
}

/// <summary>
/// Enum to differ fx types, you can add as many fx types as possible
/// </summary>
public enum FxTypes
{
    CANHIT,
    GAMEOVER,
    GAMEWIN,
    BALLTHROW,
    BALLSPAWN
}