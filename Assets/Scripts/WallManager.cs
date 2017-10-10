using System.Collections;
using UnityEngine;
using DG.Tweening;

public class WallManager : MonoBehaviour {

    public float delayWall = 60;
    public Transform[] walls;

    private float timer;
    private int numWall;

	// Use this for initialization
	void Start () {
        timer = 0;
        numWall = 0;
        StartCoroutine("TimerWall");
	}

    // Remove Walls every 60 seconds.
    private IEnumerator TimerWall()
    {
        yield return new WaitForSeconds(delayWall);

        if (numWall != walls.Length-1)
        {
            walls[numWall].DOLocalMoveY(-5, 2);
            numWall++;
        }

        else if (numWall == walls.Length - 1)
        {
            Destroy(gameObject);
        }

        StartCoroutine("TimerWall");
    }

}
