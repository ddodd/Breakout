using UnityEngine;
using System.Collections;

public class BrickScript : MonoBehaviour {

	static PaddleScript paddleScript;
	static int numBricks = 0;
	static int level = 0;

	public float fadeDuration = .5f;
	public float value = 1;
	public int pointValue = 5;
	public int hitPoints = 1;

	private static PowerUpManagerScript powerUpManagerScript;
	private static SoundManagerScript soundManagerScript;
	private static int nextPowerUp;
	private static BrickFragManagerScript brickFragManagerScript;

	private GameObject ball;

	private float fadeAmount;

	private Color colorStart;
	private Color colorEnd;

	private bool markedForDeath = false;
	private bool bombed = false;

	public GameObject brick_fragPrefab;
	private GameObject brickFrags;
	private BrickFragScript brickFragScript;
	public enum BrickSize 
	{
		FULL, HALF
	}

	public BrickSize brickSize;
	
	public enum BrickType 
	{
		NORMAL, SPEEDUP
	}
	
	public BrickType type;
	
	void Start () {
		//static properties added only once
		if (numBricks == 0) {
			paddleScript = GameObject.Find ("paddle").GetComponent<PaddleScript> ();
			GetNextPowerUp();
			powerUpManagerScript = GameObject.Find ("PowerUpManager").GetComponent<PowerUpManagerScript>();
			soundManagerScript = GameObject.Find ("SoundManager").GetComponent<SoundManagerScript> ();
			brickFragManagerScript = GameObject.Find ("BrickFragManager").GetComponent<BrickFragManagerScript> ();
		}
		numBricks++;
	}
	
	void Update () {
		if (markedForDeath){
			fadeAmount -= Time.deltaTime;
			if (fadeAmount <= 0){
				renderer.enabled = false;
				markedForDeath = false;
				Die ();
			}else{
				float value = fadeAmount/fadeDuration; // value goes from 1 to 0 over time
				renderer.material.color = Color.Lerp (colorEnd, colorStart, value);
			}
		}
	}

	void GetNextPowerUp(){
		nextPowerUp = Random.Range (10, 30);
	}

	void OnCollisionEnter( Collision col ){
		Debug.Log (this + " was hit by "+col.collider.gameObject.name);
		soundManagerScript.PlayGameSound( gameObject );
		if (col.collider.gameObject.name.Substring (0, 4) == "ball") {
			ball = col.collider.gameObject;
			Vector3 ballVelocity = ball.rigidbody.velocity;
			hitPoints--;
			switch(type){
				case BrickType.SPEEDUP:
					paddleScript.BoostSpeed(value);
				break;

				case BrickType.NORMAL:
				default:
				break;
			}

			if (hitPoints <= 0) {
				Hit (ballVelocity);
			}
		}
	}

	public void Bomb(GameObject ball) {
		bombed = true;
		this.ball = ball;
		Hit (ball.rigidbody.velocity, 750f);
	}

	public void Hit(Vector3 ballVelocity, float power=20f){
		if (bombed) {
			collider.isTrigger = true;
			markedForDeath = true;
			fadeAmount = fadeDuration;
			colorStart = renderer.material.color;			
			colorEnd = colorStart;
			colorEnd.a = 0;
			gameObject.AddComponent<Rigidbody> ();
			rigidbody.mass = 1;
			rigidbody.AddForce(ballVelocity);
			float bombPower = power * Random.Range(.5f,2f);
			rigidbody.AddExplosionForce(power, ball.transform.position, 4f);
		}
		else
		{
			brickFragManagerScript.Hit(gameObject, brickSize, ball, power * ballVelocity);
			nextPowerUp--;
			if (nextPowerUp <= 0)
				DeployPowerUp ();
			paddleScript.AddPoints (pointValue);
			Die ();
		}
	}

	void DeployPowerUp(){
		Debug.Log ("----------- Brick " + name + " deploying powerUp");
		GameObject powerUpPrefab = powerUpManagerScript.GetRandomPowerUp();
		GameObject powerUp = (GameObject)Instantiate (powerUpPrefab, transform.position, Quaternion.identity);
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
			if(level <= 8)
				Application.LoadLevel("Breakout_0"+level);
			else
				Application.LoadLevel("BreakoutGameOver");
		}
	}

//	void FadeOut ()		
//	{
//		float duration = 1;
//		for (float t = 0f; t < duration; t += Time.deltaTime) {			
//			renderer.material.color = Color.Lerp (colorStart, colorEnd, t/duration);
//		}
//	}
}
