using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace VRBall
{
    public class GameManager : MonoBehaviour {

        #region Inspector attributes

        public static GameManager instance;

		[Header("Player information")]
        public int playerLifePoints = 3;
        public Transform GetPlayer;

        [Header("Ground")]
        public MeshRenderer[] groundRenderers;
        public Color defaultGround = Color.cyan;
        public Color gameoverGround = Color.yellow;
        public Color lpLostGround = Color.red;
        public Color pointsGround = Color.green;
        public Color spawnFeedback = Color.magenta;

        [Header("Managers")]
        public SpawnManager spawnMgr;
        public WallManager wallMgr;
        public Sounds_Manager soundMgr;
        public UIManager ui;

        #endregion

        #region Private attributes

        public int Score
        {
            get { return score; }
            set {
                score = value;
                ui.UpdateScore(score);
                StartCoroutine(FeedbackGround(pointsGround));
            }
        }
        private int score = 0;

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

                StartCoroutine(FeedbackGround(lpLostGround));
                soundMgr.PlaySound(GetPlayer, soundMgr.audioLifepointLost);
            }
        }
        private int lifePoints;

        public bool IsGameOver
        {
            get { return isGameOver; }
        }
        private bool isGameOver = false;
        
        public int RoomUnlocked
        {
            get { return roomUnlocked; }
            set
            {
                roomUnlocked = value;
            }
        }
        private int roomUnlocked = 1;
        
        private bool colorGround = false;

        #endregion

        #region Unity methods

        // Use this for initialization
        void Awake () {
            SingletonThis();
        }

        private void Start()
        {
            SetupGame();
        }

        private void Update()
        {
            if(isGameOver)
            {
                if (Input.GetKeyDown(KeyCode.R))
                    ResetGame();
            }
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

        #region Game methods

        private void FinishGame()
        {
            if (isGameOver)
                return;
            
            isGameOver = true;

            // Stop all managers, feedback and clean objects.
            spawnMgr.enabled = false;

            wallMgr.StopAllCoroutines();
            wallMgr.enabled = false;
            
            spawnMgr.ClearObj ();

            soundMgr.PlaySound(soundMgr.gameover);

            StopAllCoroutines();
            ColorGround(gameoverGround, 5.0f);
        }

        // Setup or reset game.
        public void SetupGame()
        {
            score = 0;
            ui.UpdateScore(score);
            
            roomUnlocked = 0;

            lifePoints = playerLifePoints;
            // Update ui.
            isGameOver = false;

            ColorGround(defaultGround);

            if (spawnMgr)
                spawnMgr.enabled = true;
            if (wallMgr)
                wallMgr.enabled = true;
        }

        public void ResetGame()
        {
            SetupGame();

            GetPlayer.transform.position = Vector3.zero;
            wallMgr.ResetWalls();
        }

        #endregion

        #region Ground effect

        private void ColorGround(Color newColor, float newEmissive = 0.0f)
        {
            for (int i = 0; i < groundRenderers.Length; i++)
            {
                groundRenderers[i].material.SetColor("_Color", newColor);
                groundRenderers[i].material.SetFloat("_Emissive", newEmissive);
            }
        }

        private void ColorGround(int roomIndex, Color newColor, float newEmissive = 0.0f)
        {
            groundRenderers[roomIndex].material.SetColor("_Color", newColor);
            groundRenderers[roomIndex].material.SetFloat("_Emissive", newEmissive);
        }

        private IEnumerator FeedbackGround(Color feedbackColor)
        {
            if (colorGround && !IsGameOver)
                yield break;
            
            colorGround = true;
            Color mid = Color.Lerp(Color.cyan, feedbackColor, 0.5f);

            for (float emi = 0f; emi < 3.5f; emi += 0.5f)
            {
                ColorGround(mid, emi);
                yield return new WaitForSeconds(0.01f);
            }

            ColorGround(feedbackColor, 5.0f);
            yield return new WaitForSeconds(0.2f);

            for (float emi = 3.5f; emi > 0f; emi -= 0.5f)
            {
                ColorGround(mid, emi);
                yield return new WaitForSeconds(0.01f);
            }
            
            ColorGround(Color.cyan, 0.0f);
            colorGround = false;
        }

        private IEnumerator FeedbackGround(int roomIndex, Color feedbackColor)
        {
            if (colorGround && !IsGameOver)
                yield break;
            
            Color mid = Color.Lerp(Color.cyan, feedbackColor, 0.5f);

            for (float emi = 0f; emi < 3.5f; emi += 0.5f)
            {
                ColorGround(roomIndex, mid, emi);
                yield return new WaitForSeconds(0.01f);
            }

            ColorGround(roomIndex, feedbackColor, 5.0f);
            yield return new WaitForSeconds(0.2f);

            for (float emi = 3.5f; emi > 0f; emi -= 0.5f)
            {
                ColorGround(roomIndex, mid, emi);
                yield return new WaitForSeconds(0.01f);
            }

            ColorGround(roomIndex, Color.cyan, 0.0f);
            colorGround = false;
        }

        public void SpawnFeedback(int roomIndex)
        {
            StartCoroutine(FeedbackGround(roomIndex, spawnFeedback));
            soundMgr.PlaySound(soundMgr.spawnFeedback);
        }

        #endregion
    }
}