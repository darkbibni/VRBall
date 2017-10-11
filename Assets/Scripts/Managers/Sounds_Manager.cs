using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds_Manager : MonoBehaviour {

    public AudioSource[] audioBump;
    public AudioSource[] audioPoint;
    public AudioSource[] audioNet;
    public AudioSource ambiantNoise;
    public AudioSource ambiantMusic;
    

    private int numBump;
    private int numPoint;
    private int numNet;

	void Start () {
        ambiantMusic.Play();
        ambiantNoise.Play();
        //Debug.Log(mySounds.Length);
    }
	
	void Update () {
		
	}

    public void PlaySoundPoint(Transform pos)
    {
        RandomPoint();
        if (audioPoint[numPoint].isPlaying == false)
        {
            audioPoint[numPoint].gameObject.transform.position = new Vector3(pos.position.x, pos.position.y, pos.position.z);
            audioPoint[numPoint].Play();
        }
        else
        {
            RandomPoint();
            PlaySoundPoint(pos);
        }
    }

    public void PlaySoundNet(Transform pos)
    {
        RandomNet();
        if (audioNet[numNet].isPlaying == false)
        {
            audioNet[numNet].gameObject.transform.position = new Vector3(pos.position.x, pos.position.y, pos.position.z);
            audioNet[numNet].Play();
        }
        else
        {
            RandomNet();
            PlaySoundNet(pos);
        }
    }

    public void PlaySoundBump(Transform pos)
    {
        RandomBump();
        if(audioBump[numBump].isPlaying == false)
        {
            audioBump[numBump].gameObject.transform.position = new Vector3(pos.position.x, pos.position.y, pos.position.z);
            audioBump[numBump].Play();
        }
        else
        {
            RandomBump();
            PlaySoundBump(pos);
        }
    }

    public void RandomBump()
    {
        numBump = Random.Range(0, 10);
    }

    public void RandomNet()
    {
        numNet = Random.Range(0, 10);
    }

    public void RandomPoint()
    {
        numPoint = Random.Range(0, 10);
    }
}
