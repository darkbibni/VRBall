using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds_Manager : MonoBehaviour {

    public AudioSource[] myAudio;

    private int numSound;
    private Transform newPos;

	void Start () {
        //Debug.Log(mySounds.Length);
	}
	
	void Update () {
		
	}

    public void PlaySoundBump(Transform pos)
    {
        newPos = pos;
        RandomMusic();
        if(myAudio[numSound].isPlaying == false)
        {
            myAudio[numSound].gameObject.transform.position = new Vector3(newPos.position.x, newPos.position.y, newPos.position.z);
            myAudio[numSound].Play();
        }
        else
        {
            RandomMusic();
            PlaySoundBump(newPos);
        }
    }

    public void RandomMusic()
    {
        numSound = Random.Range(0, 10);
    }
}
