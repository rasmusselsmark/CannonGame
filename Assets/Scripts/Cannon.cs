using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour
{
	public Transform GunBarrel;
	public GameObject CannonBallPrefab;
	public GameObject ExplosionPrefab;
	public UnityEngine.UI.Slider CannonBallPowerBar;

	public KeyCode KeyUp;
	public KeyCode KeyDown;
	public KeyCode KeyFire;

	void Start ()
	{
	
	}

	void Update()
	{
		if (Input.GetKey (KeyUp))
		{
			RotateBarrel (1);
		}
		else if (Input.GetKey (KeyDown))
		{
			RotateBarrel (-1);
		}

		if (Input.GetKeyDown (KeyFire))
		{
			// fire cannon when space has been released
			CannonBallPowerBar.value = 0f;
		}

		if (Input.GetKey (KeyFire))
		{
			CannonBallPowerBar.value += 0.1f;
		}
		else if (Input.GetKeyUp (KeyFire))
		{
			FireCannon (CannonBallPowerBar.value);
		}
	}

	private void RotateBarrel(int degrees)
	{
		GunBarrel.Rotate(new Vector3(0, 0, degrees));
	}

	private void FireCannon(float power)
	{
		Vector2 position = GunBarrel.position + GunBarrel.right * 0.8f;

		GameObject cannonBall = Instantiate (CannonBallPrefab, position, GunBarrel.rotation) as GameObject;
		cannonBall.GetComponent<Rigidbody2D> ().AddForce (GunBarrel.right * power, ForceMode2D.Impulse);
	}

	public void Hit()
	{
		GameObject explosion = Instantiate (ExplosionPrefab, this.transform.position, Quaternion.identity) as GameObject;
		Destroy (explosion, 1f);
		Destroy (this.gameObject);
	}
}
