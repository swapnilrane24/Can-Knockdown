using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script credit:- Alexander Zotov
/// Video Link:- https://www.youtube.com/watch?v=7O9bAFyGvH8
/// </summary>

public class SwipeControl : MonoBehaviour
{
    public static SwipeControl instance;

    [SerializeField] private float throwForce = 1f;             //force which act in x and y direction
    [SerializeField] private float throwForceZ = 50f;           //force which act in z direction

    private Vector2 startPos, endPos, direction;                //vector variables to store important information
    private float tapStartTime, tapEndTime, totalTime;          //variables to claculate time required for swipe
    private GameObject ballObj;                                 //ref to target object on which we apply force

    public GameObject BallObj { get => ballObj; set => ballObj = value; }

    private bool canCheckSwipe = false;

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

    // Update is called once per frame
    void Update()
    {
        //if ball object is null or game status is not playing return from the method
        if (BallObj == null || GameManager.instance.GameStatus != GameStatus.PLAYING) return;

        if (Input.GetMouseButtonDown(0))                                    //if our mouse is down
        {            
            tapStartTime = Time.time;                                       //get the start time
            startPos = Input.mousePosition;                                 //get the start position
            canCheckSwipe = true;                                           //set canCheckSwipe to true
        }

        if (Input.GetMouseButtonUp(0) && canCheckSwipe)                     //if we lift mouse click and canCheckSwipe is true
        {
            tapEndTime = Time.time;                                         //get the end time
            totalTime = tapEndTime - tapStartTime;                          //calculate the total time 
            endPos = Input.mousePosition;                                   //get the end position
            direction = startPos - endPos;                                  //find the direction

            BallObj.GetComponent<Rigidbody>().isKinematic = false;          //get is ball kinematics to false
                                                                            //add force to the ball rigidbody
            BallObj.GetComponent<Rigidbody>().AddForce(-direction.x * throwForce, -direction.y * throwForce, throwForceZ / totalTime);
            BallObj = null;                                                 //we make ballobj null as we no more need reference to it
            canCheckSwipe = false;                                          //set canCheckSwipe to false
            SoundManager.instance.PlayFx(FxTypes.BALLTHROW);                //play ball throw animation
        }


    }
}
