using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSounds : MonoBehaviour
{
    public float timeBeforeSound = 1;
    public bool bumpReady = true;
    public Sounds_Manager soundMgr;

    IEnumerator CooldownSound()
    {
        yield return new WaitForSeconds(timeBeforeSound);
        bumpReady = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (bumpReady)
        {
            if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Ground")
            {
                bumpReady = false;
                soundMgr.PlaySound(collision.transform, soundMgr.audioBump);
                StartCoroutine(CooldownSound());
            }

            else if (collision.gameObject.tag == "Basket")
            {
                bumpReady = false;
                soundMgr.PlaySound(collision.transform, soundMgr.audioNet);
                StartCoroutine(CooldownSound());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goal")
        {
            soundMgr.PlaySound(other.transform, soundMgr.audioPoint);
            bumpReady = false;
        }
    }
}
