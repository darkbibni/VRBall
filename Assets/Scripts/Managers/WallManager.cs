using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace VRBall {

    public class WallManager : MonoBehaviour {

        public float delayWall = 60;
        public Transform[] walls;

        private float timer;
        private int numWall;

        // Use this for initialization
        void Start() {
            timer = 0;
            numWall = 0;
            StartCoroutine("TimerWall");
        }

        // Remove Walls every 60 seconds.
        private IEnumerator TimerWall()
        {
            // TODO bug : third set of walls don't move at same speed.

            yield return new WaitForSeconds(delayWall);

            if (numWall != walls.Length)
            {
                walls[numWall].DOLocalMoveY(-5, 2);

                numWall++;
                GameManager.instance.RoomUnlocked++;
                StartCoroutine("TimerWall");
            }

            // don't destroy to resetup the scene.
        }

        public void ResetWalls()
        {
            // TODO !
        }
    }
}
