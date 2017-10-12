using UnityEngine;

namespace VRBall
{
    public class LaserPointer : MonoBehaviour {

        #region Inspector attributes
        
        [Header("Laser configuration")]
        public GameObject laserPrefab;
        public LaserPointer otherHand;



        [Header("Teleport")]

        public Transform cameraRigTransform;
        public GameObject teleportReticlePrefab;
        public Transform headTransform;
        public Vector3 teleportReticleOffset;

        public LayerMask teleportMask;
        #endregion

        #region Private attributes

        private SteamVR_TrackedObject trackedObject;

        private SteamVR_Controller.Device devive
        {
            get { return SteamVR_Controller.Input((int)trackedObject.index); }
        }

        private Transform self;

        // laser attributes
        private GameObject laser;
        private Transform laserTransform;

        private Vector3 hitPoint;
        
        public bool IsUsingLaser
        {
            get { return useLaser; }
        }
        private bool useLaser = false;

        // Teleport attributes

        private GameObject reticle;
        private Transform teleportReticleTransform;
        private bool canTeleport = false;

        #endregion

        #region Unity methods

        // Use this for initialization
        void Awake () {
            self = transform;
            trackedObject = GetComponent<SteamVR_TrackedObject>();
		}

        private void Start()
        {
            laser = Instantiate(laserPrefab);
            laserTransform = laser.transform;

            reticle = Instantiate(teleportReticlePrefab);
            teleportReticleTransform = reticle.transform;
        }

        // Update is called once per frame
        void Update () {
            
            if (GameManager.instance.IsGameOver)
            {
                return;
            }

            if (devive.GetPress(SteamVR_Controller.ButtonMask.Touchpad) && !otherHand.IsUsingLaser)
            {
                useLaser = true;
                RaycastHit hit;

                if (Physics.Raycast(trackedObject.transform.position, self.forward, out hit, 100, teleportMask))
                {
                    hitPoint = hit.point;

                    ShowLaser(hit);

                    reticle.SetActive(true);
                    teleportReticleTransform.position = hitPoint + teleportReticleOffset;
                    canTeleport = true;
                }
            }

            else
            {
                useLaser = false;
                laser.SetActive(false);
                reticle.SetActive(false);
                canTeleport = false;
            }

            if(canTeleport && devive.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
            {
                Teleport();
            }
        }

        #endregion

        private void ShowLaser(RaycastHit hit)
        {
            laser.SetActive(true);
            laserTransform.position = Vector3.Lerp(trackedObject.transform.position, hitPoint, 0.5f);
            laserTransform.LookAt(hitPoint);
            laserTransform.localScale = new Vector3(
                laserTransform.localScale.x,
                laserTransform.localScale.y,
                hit.distance
                );
        }

        private void Teleport()
        {
            canTeleport = false;
            reticle.SetActive(false);

            Vector3 difference = cameraRigTransform.position - headTransform.position;
            difference.y = 0f;

            cameraRigTransform.position = hitPoint - difference;
        }
    }
}