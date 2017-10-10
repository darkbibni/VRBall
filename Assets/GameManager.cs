﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace VRBall
{
	public class GameManager : MonoBehaviour {

        #region Inspector attributes

        public static GameManager instance;
        public int playerLifePoints = 3;

        public Transform grounds;
        public SpawnManager spawners;

        #endregion

        #region Private attributes

        private int score = 0;
        public int Score
        {
            get { return score; }
            set {
                score = value;
                // TODO UpdateScore();
            }
        }

        private int lifePoints;
        public int LifePoints
        {
            get { return lifePoints; }
            set
            {
                if (value <= 0)
                {
                    FinishGame();
                    return;
                }

                lifePoints = value;
                // TODO UpdateLifePointsUI();
            }
        }

        private bool isGameOver = false;
        public bool IsGameOver
        {
            get { return isGameOver; }
        }

        #endregion

        #region Unity methods

        // Use this for initialization
        void Awake () {
            SingletonThis();
        }

        private void Start()
        {
            ResetGame();
        }

        #endregion

        // Singleton this class.
        private void SingletonThis()
        {
            if (instance == null)
                instance = this;

            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }

        public void ResetGame()
        {
            score = 0;
            lifePoints = playerLifePoints;
            isGameOver = false;

            // TODO clean all existing objects.
            // TODO recolor ground with normal color.

            spawners.enabled = true;
        }

        private void FinishGame()
        {
            if (isGameOver)
                return;

            Debug.Log("GAME OVER");
            isGameOver = true;
            spawners.enabled = false;

            foreach (Transform t in grounds)
            {
                t.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            }

            // TODO Feedback ground shake coloration and SOUND
        }
    }
}