using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script attached on ball to detect collision
/// </summary>
public class BallControl : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Can")                //check if the tag of collided object is Can
        {
            SoundManager.instance.PlayFx(FxTypes.CANHIT);   //If we hit the can then me play canhit sound fx
        }
    }
}
