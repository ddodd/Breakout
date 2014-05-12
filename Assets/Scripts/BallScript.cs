using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {
	public float speedLimit;
	public float speedMin = 1;
	public GameObject bounceSoundPrefab;
	private static SoundManagerScript soundManagerScript;
	private GameObject mySound;

	void Start () {
		soundManagerScript = GameObject.Find ("SoundManager").GetComponent<SoundManagerScript> ();
	}
	
	void Update () {
		rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, speedLimit);
		if (rigidbody.velocity.magnitude > speedLimit)
			Debug.Log ("+ + + velocity =" + rigidbody.velocity.magnitude);
		if (rigidbody.velocity.magnitude < speedMin)
			rigidbody.velocity *= 2;
	}

	public void Boost (float value)
	{
		rigidbody.AddForce (rigidbody.velocity * value);
	}

	public void Die()
	{
		Destroy (gameObject);
		PaddleScript paddleScript = GameObject.Find ("paddle").GetComponent<PaddleScript>();
		paddleScript.LoseLife ();
	}

	void OnCollisionEnter( Collision col)
	{
		if (col.collider.gameObject.name.Substring (0, 5) == "brick")
			return;
		soundManagerScript.PlayGameSound (gameObject);
//		mySound.transform.position = transform.position;
//		mySound.audio.Play();
	}
}
