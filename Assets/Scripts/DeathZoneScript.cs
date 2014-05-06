using UnityEngine;
using System.Collections;

public class DeathZoneScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter( Collider other) 
	{
		Debug.Log (other.gameObject+" DeathZone Trigger");
		if(other.gameObject.name == "ball"){
			Debug.Log ("ball!");
			BallScript ballScript = other.GetComponent<BallScript> ();
			if(ballScript)
			{
				ballScript.Die();
			}
		}
		else if(other.gameObject.name == "powerUp"){
			Debug.Log ("powerUp!");
			PowerUpScript powerUpScript = other.GetComponent<PowerUpScript>();
			if(powerUpScript)
			{
				powerUpScript.Die();
			}
		}
	}
}
