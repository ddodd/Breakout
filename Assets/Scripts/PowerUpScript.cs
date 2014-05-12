using UnityEngine;
using System.Collections;

public class PowerUpScript : MonoBehaviour {

	public float duration = 5;
	public string type="WIDTH";
	private bool triggered = false;
	private GameObject paddle;
	private PaddleScript paddleScript;
	private PowerUpManagerScript powerUpManagerScript;

	private Vector3 extraWide = new Vector3(1,0,0);

	void Start () {
		paddle = GameObject.Find ("paddle");
		paddleScript = paddle.GetComponent<PaddleScript> ();
		powerUpManagerScript = GameObject.Find ("PowerUpManager").GetComponent<PowerUpManagerScript> ();
	}
	
	void Update () 
	{
		if (triggered) {
//			Debug.Log (this.gameObject + " TRIGGERED");
			duration -= Time.deltaTime;
			if (duration <= 0){
				triggered = false;
				Die ();
			}
		}
	}

	public bool isTriggered()
	{
		return triggered;
	}

	void OnCollisionEnter( Collision col ){
		Debug.Log (this.gameObject + " collided with "+col.collider.gameObject);
		if (col.collider.gameObject == paddle) {
			Debug.Log (this.gameObject + " hit paddle");
			rigidbody.isKinematic = true;
			renderer.enabled = false;
			collider.isTrigger = true;
			ApplyPowerUp(this.gameObject);
		}
	}

	void ApplyPowerUp(GameObject powerup){
		powerUpManagerScript.PlayPowerUpSound (powerup);
		switch(type){
		case "WIDTH":
			//paddleScript.ResetScale();
			paddle.transform.localScale += extraWide;
			triggered = true;
			break;
		case "LIFE":
			paddleScript.GainLife();
			Die();
			break;
		}
		paddleScript.ActivatePowerUp(powerup);		
	}
	
	public void Die()
	{
		Debug.Log (this.gameObject + " death");
		switch(type){
		case "WIDTH":
			paddle.transform.localScale -= extraWide;
			break;
		case "LIFE":
			break;
		}
		Destroy (gameObject);
	}
}
