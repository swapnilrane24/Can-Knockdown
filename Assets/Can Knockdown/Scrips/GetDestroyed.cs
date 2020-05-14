using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script attached on gameobject we want to destroy
/// </summary>
public class GetDestroyed : MonoBehaviour
{
    public ObjectType objectType = ObjectType.CAN;          //to keep track of gameobject on which this script is attached

    private void OnCollisionEnter(Collision collision)      //check for the collision
    {
        if (collision.collider.name == "Destroyer")         //if gameobject collide with object name "Destroyer"
        {
            switch (objectType)                             //switch statement to perform respective logic for the object type
            {
                case ObjectType.BALL:                       //if object type is ball
                    LevelManager.instance.SpawnBall();      //we call SpawnBall method of LevelManager
                    break;
                case ObjectType.CAN:                        //if object type is can
                    LevelManager.instance.ReduceCan();      //we call ReduceCan method of LevelManager
                    break;
            }

            Destroy(gameObject);                            //and then we destroy the gameobject
        }
    }
}

[System.Serializable]
public enum ObjectType
{
    BALL,
    CAN
}
