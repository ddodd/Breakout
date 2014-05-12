using UnityEngine;
using System.Collections;

public class SoundManagerScript : MonoBehaviour {
	private GameObject ball_bounce;
	private GameObject brick_5_sound;
	private GameObject brick_10_sound;
	private GameObject brick_25_sound;
	private GameObject brick_100_sound;
	private GameObject brick_power_sound;


	// Use this for initialization
	void Start () {
		audio.Play();
		ball_bounce = GameObject.Find ("ball_bounce");
		brick_5_sound = GameObject.Find ("brick_5_sound");
		brick_10_sound = GameObject.Find ("brick_10_sound");
		brick_25_sound = GameObject.Find ("brick_25_sound");
		brick_100_sound = GameObject.Find ("brick_100_sound");
		brick_power_sound = GameObject.Find ("brick_power_sound");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayGameSound(GameObject go){
		Vector3 brickPosition = go.transform.position;
		string goName = go.name;
		Debug.Log (this+".PlayBrickSound:" + goName);
		GameObject goSound = ball_bounce;
		switch (goName) {
		case "ball":
			goSound = ball_bounce;
			break;
		case "brick_5":
			goSound = brick_5_sound;
			break;
		case "brick_10":
			goSound = brick_10_sound;
			break;
		case "brick_25":
			goSound = brick_25_sound;
			break;
		case "brick_100":
			goSound = brick_100_sound;
			break;
		case "brick_power":
			goSound = brick_power_sound;
			break;
		}
		goSound.transform.position = brickPosition;
		goSound.audio.Play ();

	}
}
