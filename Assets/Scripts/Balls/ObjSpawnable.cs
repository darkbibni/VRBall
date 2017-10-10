using UnityEngine;

namespace VRBall
{
    public class ObjSpawnable : MonoBehaviour
    {
        [Header("Object configuration")]
        public float TimeEnable = 5;

        float saveTime;
        bool OnHand = false;

        protected void Awake()
        {
            saveTime = TimeEnable;
        }

        void Update()
        {
            if (!OnHand)
            {
                if (TimeEnable <= 0)
                {
                    Despawn();
                    return;
                }

                TimeEnable -= Time.deltaTime;
            }
        }

        protected void Despawn()
        {
            GameManager.instance.LifePoints--;
            Destroy(gameObject);
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
    }
}
