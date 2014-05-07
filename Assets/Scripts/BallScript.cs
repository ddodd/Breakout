using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {
	public float speedLimit;
	public GameObject bounceSoundPrefab;
	private GameObject mySound;

	void Start () {
		mySound = (GameObject)Instantiate (bounceSoundPrefab, transform.position, Quaternion.identity);
	}
	
	void Update () {
		rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, speedLimit);
		if (rigidbody.velocity.magnitude > speedLimit)
			Debug.Log ("+ + + velocity =" + rigidbody.velocity.magnitude);
//		if (rigidbody.velocity.magnitude > speedLimit) {
//			Debug.Log ("1 velocity=" + rigidbody.velocity.magnitude);
//			rigidbody.velocity *= speedLimit/rigidbody.velocity.magnitude;
//			Debug.Log ("2 velocity=" + rigidbody.velocity.magnitude);
//		}
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

		mySound.transform.position = transform.position;
		mySound.audio.Play();
	}
}
