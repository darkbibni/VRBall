using UnityEngine;

namespace VRBall
{
    public class BallBowling : ObjSpawnable
    {
        [Header("Bowling ball")]
        public GameObject quillPrefab;
        public GameObject[] marqueurPos;

        private GameObject quillSpawned;

        protected override void Awake()
        {
            base.Awake();
        }

        // Use this for initialization
        void Start()
        {
            marqueurPos = GameObject.FindGameObjectsWithTag("markers");
            Vector3 pos = marqueurPos[Random.Range(0, marqueurPos.Length - 1)].transform.position;
            pos.y += 3.0f;
            
            quillSpawned = Instantiate(quillPrefab, pos, Quaternion.identity);
        }

		protected override void Despawn()
        {
            Destroy(quillSpawned);
            base.Despawn();
        }

        public override void Clean()
        {
            Destroy(quillSpawned);
            base.Clean();
        }
    }
}
