using UnityEngine;

namespace VRBall
{
    public class ObjSpawnable : MonoBehaviour
    {
        public float TimeEnable = 5;

        float saveTime;
        bool OnHand = false;

        void Awake()
        {
            saveTime = TimeEnable;
        }

        void Update()
        {
            if (!OnHand)
            {
                TimeEnable -= Time.deltaTime;

                if (TimeEnable <= 0)
                {
                    objDisable();
                }
            }
        }

        void objDisable()
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
