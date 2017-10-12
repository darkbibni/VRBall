using System.Collections;
using UnityEngine;

namespace VRBall
{
    public class ObjSpawnable : MonoBehaviour
    {
        [Header("Object configuration")]
        public float TimeEnable = 5;
		public Vector2 minMaxMass = new Vector2 ( 0.1f, 1 );

        public float forceScale = 1.0f;
        
		MeshRenderer getMesh;

        float savedTime;
        bool OnHand = false;

        // Couroutine booleans.
        bool dispawing = false;

        protected virtual void Awake()
        {
			getMesh = GetComponent<MeshRenderer> ( );
			GetComponent<Rigidbody> ( ).mass = Random.Range ( minMaxMass.x, minMaxMass.y );
            
            savedTime = TimeEnable;
        }

        void Update()
        {
            if (GameManager.instance.IsGameOver)
                return;

            // Don't doing anything if catched by player.
            if (OnHand)
                return;

            // Trigger Death when time is out.
            if (TimeEnable <= 0f && !dispawing)
            {
                dispawing = true;
                TimeEnable = 0;

                StopAllCoroutines();
                Despawn();

                return;
            }
            
            TimeEnable -= Time.deltaTime;
            getMesh.material.SetFloat("_Timer", 1f - (TimeEnable / savedTime));
        }

		public void CheckDest ( )
		{
			if ( !dispawing )
			{
				dispawing = true;

				Despawn();
			}
		}

        /// <summary>
        /// Inflict one damage to player and dissapear.
        /// </summary>
        protected virtual void Despawn()
        {
            GameManager.instance.LifePoints--;
            
            if (!GameManager.instance.IsGameOver)
            {
                GameManager.instance.spawnMgr.RemoveObj(gameObject);
                StartCoroutine(FadeThenDestroy());
            }
        }

        #region Object effects

        // Direct dissapear.
        public virtual void Clean()
        {
            StartCoroutine(FadeThenDestroy());
        }
        
        private IEnumerator FadeThenDestroy()
        {
            Color c = getMesh.material.GetColor("_Color");

            for (float alpha = 1.0f; alpha > 0.0f; alpha -= 0.1f)
            {
                c.a = alpha;
                getMesh.material.SetColor("_Color", c);
                yield return new WaitForSeconds(0.02f);
            }
            
            Destroy(gameObject);
        }

        #endregion

        #region Vive Interaction

        public void CatchObject()
        {
            OnHand = true;
            TimeEnable = savedTime;
            getMesh.material.SetFloat("_Timer", 0.0f);
            
            dispawing = false;
        }

        public void ReleaseObject()
        {
            OnHand = false;
        }

        #endregion
    }
}
