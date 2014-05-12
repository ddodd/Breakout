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

	private GUIText infoText;

	private float xMax;
	private float xMin;
	private Vector3 scaleNormal;
	private float paddleWidth;
	private Vector3 paddlePosition;
	private Vector3 paddleVelocity;
	int score = 0;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		DontDestroyOnLoad(GameObject.Find ("PlayField"));

		infoText = GameObject.Find ("info").GetComponent<GUIText> ();
		infoText.text = "Mouse moves paddle, lmb launches ball, rmb explodes ball";
		scaleNormal = gameObject.transform.localScale;
		GameObject wall = GameObject.Find ("wall_left");
		xMin = wall.transform.position.x;
		wall = GameObject.Find ("wall_right");
		xMax = wall.transform.position.x;
		paddlePosition = transform.position;

		SpawnBall ();
	}

	public void OnLevelWasLoaded( int level )
	{
		Debug.Log ("level " + level + " loaded");
		ResetScale();
		if(!ball)
			SpawnBall ();
	}

	public void SpawnBall()
	{
		attachedBall = (GameObject)Instantiate (ballPrefab, transform.position + new Vector3 (0, 0.75f, 0), Quaternion.identity);
		attachedBall.name = "attachedBall";
		attachedBall.collider.isTrigger = true;
		Debug.Log ("ball spawned");
    }

	public void LoseLife()
	{
		lives--;
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
	}

	void OnGUI()
	{
		GUI.skin = guiSkin;
		GUI.Label (new Rect (10, 10, 300, 100), "Score: " + score);
		GUI.Label (new Rect (10, 1000, 300, 100), "Lives: " + lives);
	}

	public void AddPoints(int value)
	{
		score += value;
	}

	// Update is called once per frame
	void Update () 
	{
		//Debug.Log ("paddle.Update");
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePosition.z = 0;
		transform.position = keepInBounds (mousePosition);
		paddleVelocity = (transform.position - paddlePosition)/Time.deltaTime;
		paddlePosition = transform.position;
		//Debug.Log ("paddle.transform.position"+transform.position);
		if(attachedBall){
			// make ball track paddle
			Rigidbody ballRigidbody = attachedBall.rigidbody;
			Vector3 position = transform.position;
			//Debug.Log ("position:"+position);
			transform.position = keepInBounds(position);
			//Debug.Log ("t.position:"+transform.position);
			ballRigidbody.position = transform.position + new Vector3 (0, 0.75f, 0);
			if(Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0)){
				LaunchBall();
			}
		}else{
			if(Input.GetKeyDown (KeyCode.LeftControl) || Input.GetMouseButtonDown(1)){
				Debug.Log ("BOMB!");
				DropBomb();
			}
		}
	}

	void LaunchBall(){
		Debug.Log ("paddleVelocity:" + paddleVelocity);
		ball = attachedBall;
		DontDestroyOnLoad(ball);
		attachedBall = null;
		AddPaddleForce ();
		ball.rigidbody.isKinematic = false;
		ball.name = "ball";
		ball.collider.isTrigger = false;
	}

	void AddPaddleForce(){
		float vx = Mathf.Min (Mathf.Max (paddleVelocity.x, -10),10);
		float vy = Mathf.Min (Mathf.Max (paddleVelocity.y, 0),10);
		
		Vector3 paddleForce = new Vector3 (vx * power, (100 + vy) * power, 0);
		ball.rigidbody.AddForce( paddleForce );

	}

	Vector3 keepInBounds(Vector3 position, float margin=0f){
		if (position.x < xMin + margin)
			position.x = xMin + margin;
		else if(position.x > xMax - margin) 
			position.x = xMax - margin;	
		return position;
	}

	public void ActivatePowerUp(GameObject powerUp){
		Debug.Log (this +" ActivatePowerUp recieved: "+powerUp.name);
		//		PowerUpScript powerUpScript = powerUp.GetComponent<PowerUpScript>();
		//		powerUpScript.Die();
	}

	public void BoostSpeed(float value){
		Debug.Log (this +" BoostSpeed: ");
		if(ball)
			ball.GetComponent<BallScript>().Boost(value);
	}
		
	public void ResetScale(){
		Debug.Log (this +" resetScale");
		gameObject.transform.localScale = scaleNormal;
	}

	public void DropBomb(){
		if (!ball)
			return;
		Debug.Log (this +".DropBomb:ball="+ball);
		bomb = (GameObject)Instantiate (bombPrefab, ball.transform.position, Quaternion.identity);
		BombScript bombScript = bomb.GetComponent<BombScript> ();
		bombScript.Explode (ball);
		Destroy (ball);
		LoseLife ();
	}
	
	void OnCollisionExit( Collision col)
	{
		Debug.Log (this.gameObject + " collided with "+col.collider.gameObject.name);
		if(col.collider.gameObject.name=="ball"){
			paddleWidth = renderer.bounds.extents.x;
			foreach (ContactPoint contact in col.contacts) {
				if(contact.thisCollider == collider){
					float english = (contact.point.x - transform.position.x)/paddleWidth;
					Debug.Log ("english is "+english);
					col.collider.gameObject.rigidbody.AddForce ( 1000f * english, 0, 0);
					AddPaddleForce ();
					break;
				}
			}
		}
	}
}
