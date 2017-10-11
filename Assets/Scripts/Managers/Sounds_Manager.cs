using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds_Manager : MonoBehaviour {

    public AudioSource[] audioBump;
    public AudioSource[] audioPoint;
    public AudioSource[] audioNet;
    public AudioSource[] audioLifepointLost;
    public AudioSource ambiantNoise;
    public AudioSource ambiantMusic;

    public AudioSource gameover;

    private int audioSrcIndex;

	void Start () {
        ambiantMusic.Play();
        ambiantNoise.Play();
    }

    public void PlaySound(AudioSource source)
    {
        source.Play();
    }

    public void PlaySound(Transform pos, AudioSource[] audioSources)
    {
        if (audioSources.Length <= 0)
            return;

        bool soundPlayed = false;
        int timeout = 5;
        do
        {
            RandomInt(audioSources.Length);

            if(!audioSources[audioSrcIndex].isPlaying)
            {
                audioSources[audioSrcIndex].gameObject.transform.position = new Vector3(pos.position.x, pos.position.y, pos.position.z);
                audioSources[audioSrcIndex].Play();
                soundPlayed = true;
            }
            timeout--;

        } while (!soundPlayed && timeout > 0);
    }

    public void RandomInt(int lenght)
    {
        audioSrcIndex = Random.Range(0, lenght);
    }
}
