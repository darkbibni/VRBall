using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quill : MonoBehaviour {

    public float delay = 2, forceProjection = 1.5f;


    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "bowling")
        {
            GetComponent<Rigidbody>().AddForce(forceProjection * collision.rigidbody.velocity, ForceMode.Impulse);
            Destroy(collision.gameObject);
            // add score
            StartCoroutine("Timer");
        }
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(delay);
    }
}
