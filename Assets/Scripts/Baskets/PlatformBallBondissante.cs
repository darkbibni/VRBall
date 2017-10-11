using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRBall;

public class PlatformBallBondissante : MonoBehaviour {

    public int score = 100;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "BallBondissante")
        {
            Destroy(collision.gameObject);
            GameManager.instance.Score += score;
        }
    }
}
