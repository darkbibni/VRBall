using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRBall;

public class QuillGoal : Goal {

    public float delay = 2, forceProjection = 2.5f;
    private bool active;

    void Awake()
    {
        active = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!active)
            return;
        
        if(collision.gameObject.tag == "Bowling") {
            active = false;

            GetComponent<Rigidbody>().AddForce(forceProjection * collision.rigidbody.velocity, ForceMode.Impulse);
            Destroy(collision.gameObject);

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
