using UnityEngine;

namespace VRBall
{
    public class BallBowling : ObjSpawnable
    {
        [Header("Bowling ball")]
        public GameObject quillPrefab;
        public Transform[] marqueurPos;

        private GameObject quillSpawned;

        protected new void Awake()
        {
            base.Awake();
        }

        // Use this for initialization
        void Start()
        {
            Vector3 pos = marqueurPos[Random.Range(0, marqueurPos.Length - 1)].position;
            pos.y += 3.0f;

            quillSpawned = Instantiate(quillPrefab, pos, Quaternion.identity);
        }

        protected new void Despawn()
        {
            Destroy(quillSpawned);

            base.Despawn();
        }
    }
}
