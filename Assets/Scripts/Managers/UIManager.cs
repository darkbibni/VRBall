using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace VRBall
{
    [System.Serializable]
    public class ScoreClass
    {
        public TextMeshProUGUI text;
        public Animator anim;
    }

    public class UIManager : MonoBehaviour
    {

        #region inspector attributes

        public UnityEvent pointMarked;
        public ScoreClass[] scores;

        #endregion

        #region Private attributes

        #endregion

        #region Unity methods

        // Use this for initialization
        void Awake()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion

        /// <summary>
        /// Update all the scores displayed.
        /// </summary>
        /// <param name="newScore"></param>
        public void UpdateScore(int newScore)
        {
            for (int i = 0; i < scores.Length; i++)
            {
                pointMarked.Invoke();
                scores[i].anim.SetTrigger("PointsMarked");
                scores[i].text.text = newScore.ToString();
            }
        }
    }

}
