using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookPlayer : MonoBehaviour 
{
	Transform currPlayer;
	Transform thisBall;

	void Awake ( )
	{
		thisBall = transform;
	}

    private void Start()
    {
        currPlayer = VRBall.GameManager.instance.GetPlayer;
    }

    // Update is called once per frame
    void Update ( ) 
	{
		thisBall.LookAt ( currPlayer );
	}
}
