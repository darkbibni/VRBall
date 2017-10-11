using UnityEngine;
using UnityEngine.SceneManagement;

namespace VRBall
{
	public class GameManager : MonoBehaviour {

        #region Inspector attributes

        public static GameManager instance;
		public Transform GetPlayer;
        public int playerLifePoints = 3;

        public Transform grounds;

        [Header("Managers")]
        public SpawnManager spawnMgr;
        public WallManager wallMgr;
        public UIManager ui;

        #endregion

        #region Private attributes

        private int score = 0;
        public int Score
        {
            get { return score; }
            set {
                score = value;
                ui.UpdateScore(score);
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
                // TODO UpdateLifePointsUI(lifePoints);
            }
        }

        private bool isGameOver = false;
        public bool IsGameOver
        {
            get { return isGameOver; }
        }

        private int roomUnlocked = 1;
        public int RoomUnlocked
        {
            get { return roomUnlocked; }
            set
            {
                roomUnlocked = value;
            }
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
        
        private void FinishGame()
        {
            if (isGameOver)
                return;
            
            isGameOver = true;
            spawnMgr.enabled = false;
            wallMgr.StopAllCoroutines();
            wallMgr.enabled = false;

            ColorGround(Color.red);

			spawnMgr.ClearObj ();

            // TODO Feedback ground shake or coloration and SOUND
        }

        public void ResetGame()
        {
            Score = 0;
            lifePoints = playerLifePoints;
            isGameOver = false;
            
            ColorGround(Color.cyan);

            if (spawnMgr)
                spawnMgr.enabled = true;
            if (wallMgr)
                wallMgr.enabled = true;

            spawnMgr.ClearObj();
        }
        
        private void ColorGround(Color newColor)
        {
            foreach (Transform t in grounds)
            {
                t.gameObject.GetComponent<MeshRenderer>().material.color = newColor;
            }
        }
    }
}