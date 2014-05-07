using UnityEngine;
using System.Collections;

public class BrickScript : MonoBehaviour {

	static PaddleScript paddleScript;
	static int numBricks = 0;
	static int level = 0;
	static string NORMAL = "NORMAL";
	static string SPEEDUP = "SPEEDUP";

	public string type = NORMAL;
	public float value = 1;
	public int pointValue = 5;
	public int hitPoints = 1;
	public GameObject SoundPrefab;

	private static PowerUpManagerScript powerUpManagerScript;
	private static int nextPowerUp;

	private GameObject mySound;
	private bool markedForDeath = false;

	void Start () {
		//Debug.Log ("sound is " + audio.clip);
		if (numBricks == 0) {
			paddleScript = GameObject.Find ("paddle").GetComponent<PaddleScript> ();
			GetNextPowerUp();
			powerUpManagerScript = GameObject.Find ("PowerUpManager").GetComponent<PowerUpManagerScript>();
		}
		mySound = (GameObject)Instantiate (SoundPrefab, transform.position, Quaternion.identity);
		numBricks++;
	}
	
	void Update () {
		if (markedForDeath && !mySound.audio.isPlaying) {
			Destroy (mySound);
			Die ();
		}
	}

	void GetNextPowerUp(){
		nextPowerUp = Random.Range (5, 20);
	}

	void OnCollisionEnter( Collision col ){
		Debug.Log (this + " was hit !");
		mySound.audio.Play ();
		hitPoints--;

		if (type == SPEEDUP) {
			Debug.Log ("type="+type+", value="+value);
			paddleScript.BoostSpeed(value);
		}

		if (hitPoints <= 0) {
			renderer.enabled = false;
			collider.isTrigger = true;
			markedForDeath = true;
		}
	}

	void DeployPowerUp(){
		Debug.Log ("----------- Brick " + name + " deploying powerUp");
		GameObject powerUpPrefab = powerUpManagerScript.GetRandomPowerUp();
		GameObject powerUp = (GameObject)Instantiate (powerUpPrefab, transform.position, Quaternion.identity);
		powerUpManagerScript.PlayPowerUpSound(powerUp);
		powerUp.rigidbody.AddTorque (0, 0, 50);
		powerUp.rigidbody.AddForce (0, 50, 0);
		GetNextPowerUp();
	}

	void Die(){
		numBricks--;
		nextPowerUp--;
		if (nextPowerUp <=0)
			DeployPowerUp ();

		paddleScript.AddPoints (pointValue);
		Destroy (gameObject);

		Debug.Log ("numBricks="+numBricks+", level="+level);
		if (numBricks <= 0) {
			level++;
			if(level <= 5)
			{
				Application.LoadLevel("Breakout_0"+level);
			}
			else
			{
				Application.LoadLevel("BreakoutGameOver");
			}
		}
	}
}
