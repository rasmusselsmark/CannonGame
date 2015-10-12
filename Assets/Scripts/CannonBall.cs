using UnityEngine;
using System.Collections;

public class CannonBall : MonoBehaviour
{
	public Cannon FiringPlayer;

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.name == "Grass")
		{
			GetComponent<Rigidbody2D> ().isKinematic = true;
			GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
		}
		else if (coll.gameObject.tag == "Cannon")
		{
			Cannon cannon = coll.gameObject.GetComponent<Cannon> ();
			cannon.GotHit ();

			if (coll.gameObject.name != FiringPlayer.name)
			{
				FiringPlayer.Score += 1;
				FiringPlayer.Reset ();
			}

			Destroy (this.gameObject);
		}
	}
}
