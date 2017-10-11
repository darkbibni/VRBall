using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookPlayer : MonoBehaviour 
{
	Transform currPlayer;
	Transform thisBall;
	Transform getParent;

	void Start ( )
	{
		currPlayer = VRBall.GameManager.instance.GetPlayer;
		thisBall = transform;
		getParent = thisBall.parent;
	}
	
	// Update is called once per frame
	void Update ( ) 
	{
		thisBall.position = getParent.position + new Vector3 ( 0, 0.75f * getParent.localScale.y, 0 );
		thisBall.LookAt ( currPlayer );
	}
}
