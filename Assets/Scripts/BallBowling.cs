using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBowling : MonoBehaviour {

    public Transform[] marqueurPos;
    public GameObject Quill;

	// Use this for initialization
	void Start () {
        Instantiate(Quill, marqueurPos[Random.Range(0, marqueurPos.Length - 1)].position, Quaternion.identity);
        // timer Game over
	}
	
	
}
