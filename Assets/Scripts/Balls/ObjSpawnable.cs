using System.Collections;
using UnityEngine;

namespace VRBall
{
    public class ObjSpawnable : MonoBehaviour
    {
        [Header("Object configuration")]
        public float TimeEnable = 5;
        
        float saveTime;
        bool OnHand = false;

        // Couroutine booleans.
        bool blinking = false;
        bool dispawing = false;

        protected void Awake()
        {
            saveTime = TimeEnable;
        }

        void Update()
        {
            // Don't doing anything if catched by player.
            if (OnHand)
                return;
            
            // Trigger Death when time is out.
            if (TimeEnable <= 0f && !dispawing)
            {
                dispawing = true;

                StopAllCoroutines();
                Despawn();

                return;
            }

            // Trigger blink effect when object will despawn.
            else if (TimeEnable <= 5f && !blinking)
            {
                blinking = true;
                StartCoroutine(Blink());
            }
            
            TimeEnable -= Time.deltaTime;
        }

        private void UpdateTime() { 
}

        protected void Despawn()
        {
            GameManager.instance.LifePoints--;

            StartCoroutine(FadeThenDestroy());
        }

        public void TakeOnHand(bool isOnHand)
        {
            if (isOnHand)
            {
                OnHand = true;
                TimeEnable = saveTime;
            }
            else
            {
                OnHand = false;
            }
        }

        private IEnumerator Blink()
        {
            // TODO FEEDBACK AUDIO AND GRAPHIC ?

            yield return new WaitForSeconds(1.0f);
        }

        /// <summary>
        /// Destroy item on a fade.
        /// </summary>
        /// <returns></returns>
        public IEnumerator FadeThenDestroy()
        {
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            Color c = renderer.material.color;

            for (float alpha = 1.0f; alpha > 0.0f; alpha -= 0.1f)
            {
                c.a = alpha;
                renderer.material.color = c;
                yield return new WaitForSeconds(0.01f);
            }
            
            Destroy(gameObject);
        }

        public void CatchObject()
        {
            OnHand = true;

            // Reset timer ?
            TimeEnable = saveTime;
        }

        public void ReleaseObject()
        {
            OnHand = false;
        }
    }
}
