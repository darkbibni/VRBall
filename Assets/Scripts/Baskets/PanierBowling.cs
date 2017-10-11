using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanierBowling : MonoBehaviour 
{

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "bowling")
        {
            Destroy(collision.gameObject);
            // ajout de point
            Destroy(GameObject.FindGameObjectsWithTag("quill")[0]);
        }
    }
}
