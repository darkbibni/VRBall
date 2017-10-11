using UnityEngine;

namespace VRBall
{
    public class BallBowling : ObjSpawnable
    {
        [Header("Bowling ball")]
        public GameObject quillPrefab;
        private GameObject[] marqueurPos;
        public Vector3 hauteur;

        private GameObject quillSpawned;

        protected new void Awake()
        {
            base.Awake();
            marqueurPos = GameObject.FindGameObjectsWithTag("markers");
        }

        // Use this for initialization
        void Start()
        {
            // TODO fix Quill cannot spawned because prefab don't know the bowling markers.
            Vector3 pos = marqueurPos[Random.Range(0, marqueurPos.Length - 1)].transform.position + hauteur;
            

            quillSpawned = Instantiate(quillPrefab, pos, Quaternion.identity);
        }

        protected new void Despawn()
        {
            Destroy(quillSpawned);

            base.Despawn();
        }
    }
}
