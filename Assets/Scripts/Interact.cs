using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class Interact : MonoBehaviour {

        #region Inspector attributes

        #endregion

        #region Private attributes

        private SteamVR_TrackedObject trackedObject;

        private SteamVR_Controller.Device Device {
            get { return SteamVR_Controller.Input((int) trackedObject.index); }
        }


        private GameObject collidingObject;
        private GameObject objectInHand;
        private Rigidbody rgbd;

		#endregion
		
		#region Unity methods
		
		// Use this for initialization
		void Awake () {
            trackedObject = GetComponent<SteamVR_TrackedObject>();
		}
		
		// Update is called once per frame
		void Update () {

            if (collidingObject)
            {
                if (Device.GetHairTriggerDown())
                {
                    GrabObject();
                }
            }

            if (Device.GetHairTriggerUp())
            {
                if (objectInHand)
                {
                    ReleaseObject();
                }
            }
		}
        
        private void OnTriggerEnter(Collider other)
        {
            SetCollidingObject(other.gameObject);
        }

        private void OnTriggerStay(Collider other)
        {
            SetCollidingObject(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if (collidingObject)
                collidingObject = null;
        }

        #endregion

        #region Interaction with balls

        private void SetCollidingObject(GameObject collidingObject)
        {
            if (collidingObject == null || rgbd == null)
                return;

            this.collidingObject = collidingObject;
        }

        private void GrabObject()
        {
            objectInHand = collidingObject;
            collidingObject = null;

            var joint = AddFixedJoint();
            joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
        }

        private FixedJoint AddFixedJoint()
        {
            FixedJoint fx = gameObject.AddComponent<FixedJoint>();
            fx.breakForce = 20000;
            fx.breakTorque = 20000;
            return fx;
        }

        private void ReleaseObject()
        {
            FixedJoint joint = GetComponent<FixedJoint>();

            if (joint)
            {
                joint.connectedBody = null;
                Destroy(joint);

                objectInHand.GetComponent<Rigidbody>().velocity = Device.velocity;
                objectInHand.GetComponent<Rigidbody>().angularVelocity = Device.angularVelocity;
            }

            objectInHand = null;
        }

        #endregion
    }
}