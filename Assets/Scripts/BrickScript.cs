using UnityEngine;
using System.Collections;

public class BrickScript : MonoBehaviour {

	static PaddleScript paddleScript;
	static int numBricks = 0;
	static int level = 0;
	static string NORMAL = "NORMAL";
	static string SPEEDUP = "SPEEDUP";

	public string type = NORMAL;
	public float fadeDuration = .5f;
	public float value = 1;
	public int pointValue = 5;
	public int hitPoints = 1;
	public GameObject SoundPrefab;

	private static PowerUpManagerScript powerUpManagerScript;
	private static int nextPowerUp;

	private GameObject ball;

	private float fadeAmount;
	static Vector3 originalScale;
	private Color colorStart;
	private Color colorEnd;

	private GameObject mySound;
	private bool markedForDeath = false;

	void Start () {
		//Debug.Log ("sound is " + audio.clip);
		if (numBricks == 0) {
			paddleScript = GameObject.Find ("paddle").GetComponent<PaddleScript> ();
			GetNextPowerUp();
			powerUpManagerScript = GameObject.Find ("PowerUpManager").GetComponent<PowerUpManagerScript>();
			originalScale = transform.localScale;
		}
		mySound = (GameObject)Instantiate (SoundPrefab, transform.position, Quaternion.identity);
		numBricks++;
	}
	
	void Update () {
		if (markedForDeath){
			if(mySound && !mySound.audio.isPlaying)
				Destroy (mySound);
			fadeAmount -= Time.deltaTime;
			if (fadeAmount <= 0){
				renderer.enabled = false;
				markedForDeath = false;
				Die ();
			}else{
				float value = fadeAmount/fadeDuration; // value goes from 1 to 0 over time
				renderer.material.color = Color.Lerp (colorEnd, colorStart, value);
				//transform.localScale = originalScale * (1+value)/2;
			}
			//Debug.Log ("fadeAmount:"+fadeAmount);
		}
	}

	void GetNextPowerUp(){
		nextPowerUp = Random.Range (5, 20);
	}

	void OnCollisionEnter( Collision col ){
		Debug.Log (this + " was hit by "+col.collider.gameObject.name);
		mySound.audio.Play ();
		if (col.collider.gameObject.name.Substring (0, 4) == "ball") {
			hitPoints--;
			ball = col.collider.gameObject;
			if (type == SPEEDUP) {
				paddleScript.BoostSpeed(value);
			}
			
			if (hitPoints <= 0) {
				Hit ();
			}
		}
	}

	public void Bomb(GameObject ball) {
		this.ball = ball;
		Hit (500f);
	}

	public void Hit(float power=200f){
		nextPowerUp--;
		if (nextPowerUp <=0)
			DeployPowerUp ();
		
		paddleScript.AddPoints (pointValue);
		collider.isTrigger = true;
		markedForDeath = true;
		fadeAmount = fadeDuration;
		colorStart = renderer.material.color;			
		colorEnd = colorStart;
		colorEnd.a = 0;
		gameObject.AddComponent<Rigidbody> ();
		rigidbody.mass = 1;
		rigidbody.AddExplosionForce(power, ball.transform.position, 10);
	}

	void DeployPowerUp(){
		Debug.Log ("----------- Brick " + name + " deploying powerUp");
		GameObject powerUpPrefab = powerUpManagerScript.GetRandomPowerUp();
		GameObject powerUp = (GameObject)Instantiate (powerUpPrefab, transform.position, Quaternion.identity);
		powerUpManagerScript.PlayPowerUpSound(powerUp);
		powerUp.rigidbody.AddTorque (0, 0, 50);
		powerUp.rigidbody.AddForce (0, 500f, 0);
		GetNextPowerUp();
	}

	public void Die(){
		numBricks--;
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

	void FadeOut ()		
	{

		float duration = 1;
		for (float t = 0f; t < duration; t += Time.deltaTime) {			
			renderer.material.color = Color.Lerp (colorStart, colorEnd, t/duration);
		}
		
	}
}
