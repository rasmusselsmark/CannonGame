using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour
{
	public Transform GunBarrel;
	public GameObject CannonBallPrefab;
	public GameObject ExplosionPrefab;
	public UnityEngine.UI.Slider CannonBallPowerBar;
	public UnityEngine.UI.Slider CannonBallPowerBarLast;
	public UnityEngine.UI.Text ScoreText;

	public KeyCode KeyUp;
	public KeyCode KeyDown;
	public KeyCode KeyFire;

	private int _score;

	public int Score
	{
		get { return _score; }
		set
		{
			_score = value;

			ScoreText.text = _score.ToString();
		}
	}

	void Start ()
	{
		Reset ();
	}

	public void Reset()
	{
		gameObject.SetActive (true);
		GunBarrel.localRotation = Quaternion.identity;
		CannonBallPowerBar.gameObject.SetActive (false);
		CannonBallPowerBarLast.gameObject.SetActive (false);
	}

	void Update ()
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
			CannonBallPowerBar.gameObject.SetActive (true);
		}

		if (Input.GetKey (KeyFire))
		{
			CannonBallPowerBar.value += 0.1f;
		}
		else if (Input.GetKeyUp (KeyFire))
		{
			FireCannon (CannonBallPowerBar.value);

			CannonBallPowerBarLast.gameObject.SetActive (true);
			CannonBallPowerBarLast.value = CannonBallPowerBar.value;
		}
	}

	private void RotateBarrel (int degrees)
	{
		GunBarrel.Rotate(new Vector3(0, 0, degrees));
	}

	private void FireCannon (float power)
	{
		Vector2 position = GunBarrel.position + GunBarrel.right * 0.8f;

		GameObject cannonBall = Instantiate (CannonBallPrefab, position, GunBarrel.rotation) as GameObject;
		cannonBall.GetComponent<CannonBall> ().FiringPlayer = this;
		cannonBall.GetComponent<Rigidbody2D> ().AddForce (GunBarrel.right * power, ForceMode2D.Impulse);
		Destroy (cannonBall.gameObject, 30);
	}

	public void GotHit ()
	{
		GameObject explosion = Instantiate (ExplosionPrefab, this.transform.position, Quaternion.identity) as GameObject;
		Destroy (explosion, 1f);

		this.gameObject.SetActive (false);
		Invoke ("Start", 2);
	}
}
