﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitBump : MonoBehaviour
{
    public float timeBeforeBump = 1;
    public bool bumpReady = true;
    public Sounds_Manager audioBump;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator DoCheck()
    {
        yield return new WaitForSeconds(timeBeforeBump);
        bumpReady = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (bumpReady)
        {
            if (collision.gameObject.tag == "Wall")
            {
                audioBump.PlaySoundBump(collision.transform);
                bumpReady = false;
                StartCoroutine(DoCheck());
            }
            else if (collision.gameObject.tag == "Point")
            {
                audioBump.PlaySoundPoint(collision.transform);
                bumpReady = false;
                StartCoroutine(DoCheck());
            }
            else if (collision.gameObject.tag == "Net")
            {
                audioBump.PlaySoundNet(collision.transform);
                bumpReady = false;
                StartCoroutine(DoCheck());
            }
        }
    }
}
