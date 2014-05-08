using UnityEngine;
using System.Collections;

public class PaddleScript : MonoBehaviour {
	public GameObject ballPrefab;
	public GameObject bombPrefab;
	public int lives = 6;
	public GUISkin guiSkin;

	public float power = 20f;
	private GameObject attachedBall;
	private GameObject ball;
	private GameObject bomb;

	private GUIText guiLives;
	private float xMax;
	private float xMin;
	private Vector3 scaleNormal;

	int score = 0;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		DontDestroyOnLoad(GameObject.Find ("PlayField"));

		guiLives = GameObject.Find ("guiLives").GetComponent<GUIText> ();
		updateGUI();
		scaleNormal = gameObject.transform.localScale;
		GameObject wall = GameObject.Find ("wall_left");
		xMin = wall.transform.position.x;
		wall = GameObject.Find ("wall_right");
		xMax = wall.transform.position.x;

		SpawnBall ();
	}

	public void OnLevelWasLoaded( int level )
	{
		Debug.Log ("level " + level + " loaded");
		ResetScale();
		SpawnBall ();
	}

	public void SpawnBall()
	{
		attachedBall = (GameObject)Instantiate (ballPrefab, transform.position + new Vector3 (0, 0.75f, 0), Quaternion.identity);
		attachedBall.name = "ball";
    }

	public void LoseLife()
	{
		lives--;
		updateGUI();
		if (lives > 0) 
		{
			SpawnBall();
		}
		else
		{
			Destroy(gameObject);
			Application.LoadLevel("BreakoutGameOver");
		}
		
	}
	
	public void GainLife()
	{
		lives++;
		updateGUI();
	}

	void updateGUI(){
		guiLives.text = "Lives:" + lives;
	}
	
	void OnGUI()
	{
		GUI.skin = guiSkin;
		GUI.Label (new Rect (10, 10, 300, 100), "Score: " + score);
	}

	public void AddPoints(int value)
	{
		score += value;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float h = Input.GetAxis ("Horizontal");
		if( h!=0 )
		{
			float dx = power * h * Time.deltaTime;
			float x = transform.position.x + dx;
			if(x < xMin || x > xMax) dx = 0;
			//Debug.Log ("x " + x);
			transform.Translate (dx, 0, 0);
		}

		if(attachedBall){
			// make ball track paddle
			Rigidbody ballRigidbody = attachedBall.rigidbody;
			ballRigidbody.position = transform.position + new Vector3 (0, 0.75f, 0);
			if(Input.GetButtonDown("Jump")){
				ballRigidbody.isKinematic = false;
				ballRigidbody.AddForce( h*500f, 1000f, 0 );
				ball = attachedBall;
				attachedBall = null;
			}
		}
		if(Input.GetKeyDown (KeyCode.LeftControl)){
			Debug.Log ("BOMB!");
			DropBomb();
		}		
	}

	public void ActivatePowerUp(GameObject powerUp){
		Debug.Log (this +" ActivatePowerUp recieved: "+powerUp.name);
		//		PowerUpScript powerUpScript = powerUp.GetComponent<PowerUpScript>();
		//		powerUpScript.Die();
	}

	public void BoostSpeed(float value){
		Debug.Log (this +" BoostSpeed: ");
		ball.GetComponent<BallScript>().Boost(value);
	}
		
	public void ResetScale(){
		Debug.Log (this +" resetScale");
		gameObject.transform.localScale = scaleNormal;
	}

	public void DropBomb(){
		bomb = (GameObject)Instantiate (bombPrefab, ball.transform.position, Quaternion.identity);
		BombScript bombScript = bomb.GetComponent<BombScript> ();
		bombScript.Explode ();
	}
	
	void OnCollisionEnter( Collision col)
	{
		Debug.Log (this.gameObject + " collided with "+col.collider.gameObject.name);
		foreach (ContactPoint contact in col.contacts) {
			if(contact.thisCollider == collider){
				float english = contact.point.x - transform.position.x;
				contact.otherCollider.rigidbody.AddForce ( 1000f * english, 200f, 0);
				break;
			}
		}
	}
}
