using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpManager : MonoBehaviour
{

    public Sounds_Manager audioBump;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            Debug.Log("BUMP");

            audioBump.PlaySoundBump(collision.transform);
        }
    }
}
