using UnityEngine;

public class BasketGoal : Goal
{
	Transform thisTrans;
	public ParticleSystem ps;

	void Awake ( )
	{
		thisTrans = transform;
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "littleBalls")
		{
			VRBall.GameManager.instance.Score += (int) Vector3.Distance ( VRBall.GameManager.instance.GetPlayer.position, thisTrans.position ) * score;

			Destroy(other.gameObject);

            ps.Emit(100);
        }
	}
}
