using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class WallManager : MonoBehaviour {

    public float delayWall = 60;
    private float timer;
    private int numWall;
    public Transform[] walls;

	// Use this for initialization
	void Start () {
        timer = 0;
        numWall = 0;
        StartCoroutine("TimerWall");
	}

    private IEnumerator TimerWall()
    {
        yield return new WaitForSeconds(delayWall);
        if (numWall != 3)
        {
            walls[numWall].DOLocalMoveY(-5, 2);
            numWall++;
        } else if (numWall == 3)
        {
            Destroy(this.gameObject);
        }
        StartCoroutine("TimerWall");
    }

}
