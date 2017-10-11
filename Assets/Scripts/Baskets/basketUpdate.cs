using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basketUpdate : MonoBehaviour 
{
	Transform thisTrans;

	void Awake ( )
	{
		thisTrans = transform;
	}

	void OnTriggerEnter(Collider other) 
	{
		if ( other.tag == "littleBalls");
		{
			VRBall.GameManager.instance.Score += (int) Vector3.Distance ( VRBall.GameManager.instance.GetPlayer.position, thisTrans.position ) * 50;

			Destroy(other.gameObject);
		}
	}
}
