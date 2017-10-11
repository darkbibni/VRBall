using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRBall;

public class Quill : MonoBehaviour {

    public float delay = 2, forceProjection = 2.5f;
    private bool active;
    public int score = 300;

    void Awake()
    {
        active = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(active && collision.gameObject.tag == "bowling")
        {
            GetComponent<Rigidbody>().AddForce(forceProjection * collision.rigidbody.velocity, ForceMode.Impulse);
            Destroy(collision.gameObject);
            active = false;
            GameManager.instance.Score += score;
            StartCoroutine("Timer");
        }
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }
}
