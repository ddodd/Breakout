using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour {
	private GameObject mySound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter( Collision col)
	{
		if(col.collider.gameObject.name == "brick_fragment"){
//			Debug.Log ("fragment hit wall!");
			Destroy (col.collider.gameObject);
		}
	}

	void OnCollisionExit( Collision col)
	{
		if (col.collider.gameObject.name != "ball")
			return;

		GameObject ball = col.collider.gameObject;
		Vector3 velocity = ball.rigidbody.velocity;
//		Debug.Log ("bounce velocity=" + velocity);
		float xSpeed = velocity.x;
		float ySpeed = velocity.y;
		if (xSpeed == 0)
			xSpeed = Random.Range (-2f, 2f);
//		Debug.Log ("ySpeed=" + ySpeed);
		if (Mathf.Abs (ySpeed) < 4f) {
			if (Mathf.Abs (ySpeed) < 1f) {
				ySpeed = Mathf.Sign(ySpeed)*2;
//				Debug.Log ("ySpeed is now " + ySpeed);
			}
			ySpeed *= 1.5f;
		}
		velocity.x = xSpeed * Random.Range (1f, 1.5f);
		velocity.y = ySpeed * Random.Range (1f, 1.5f);
//		Debug.Log ("velocity.x =" + velocity.x+", velocity.y =" + velocity.y);
		ball.rigidbody.velocity = velocity;
//		Debug.Log ("velocity is now " + ball.rigidbody.velocity);
	}
}
