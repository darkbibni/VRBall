using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookPlayer : MonoBehaviour 
{
	Transform currPlayer;
	Transform thisBall;

	void Awake ( )
	{
		currPlayer = VRBall.GameManager.instance.GetPlayer;
		thisBall = transform;
	}
	
	// Update is called once per frame
	void Update ( ) 
	{
		thisBall.LookAt ( currPlayer );
	}
}
