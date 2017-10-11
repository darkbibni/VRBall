using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace VRBall {

    public class WallManager : MonoBehaviour {

        public float delayWall = 60;
        public Transform[] walls;

        private bool[] wallDown;
        private int numWall;

        private void Awake()
        {
            wallDown = new bool[walls.Length];
            for (int i = 0; i < walls.Length; i++) {
                wallDown[i] = false;
            }
        }

        // Use this for initialization
        void Start() {
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
                wallDown[numWall] = true;

                numWall++;
                GameManager.instance.RoomUnlocked++;
                StartCoroutine("TimerWall");
            }

            // don't destroy to resetup the scene.
        }

        public void ResetWalls()
        {
            for(int i = 0; i < walls.Length; i++)
            {
                if(wallDown[i])
                    walls[i].DOMoveY(1, 2);
            }

            numWall = 0;
            StartCoroutine("TimerWall");
        }
    }
}
