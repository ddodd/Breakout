using UnityEngine;
using System.Collections;

public class SoundManagerScript : MonoBehaviour {

	public GameObject brick_5_sound_prefab;
	public GameObject brick_10_sound_prefab;
	public GameObject brick_25_sound_prefab;
	public GameObject brick_100_sound_prefab;
	public GameObject brick_power_sound_prefab;

	private GameObject brick_5_sound;
	private GameObject brick_10_sound;
	private GameObject brick_25_sound;
	private GameObject brick_100_sound;
	private GameObject brick_power_sound;


	// Use this for initialization
	void Start () {
		audio.Play();
//		brick_5_sound = (GameObject)Instantiate (brick_5_sound_prefab);
//		brick_10_sound = (GameObject)Instantiate (brick_10_sound_prefab);
//		brick_25_sound = (GameObject)Instantiate (brick_25_sound_prefab);
//		brick_100_sound = (GameObject)Instantiate (brick_100_sound_prefab);
//		brick_power_sound = (GameObject)Instantiate (brick_power_sound_prefab);
		brick_5_sound = GameObject.Find ("brick_5_sound");
		brick_10_sound = GameObject.Find ("brick_10_sound");
		brick_25_sound = GameObject.Find ("brick_25_sound");
		brick_100_sound = GameObject.Find ("brick_100_sound");
		brick_power_sound = GameObject.Find ("brick_power_sound");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayBrickSound(GameObject brick){
		Vector3 brickPosition = brick.transform.position;
		string brickName = brick.name;
		Debug.Log (this+".PlayBrickSound:" + brickName);
		GameObject brickSound = brick_5_sound;
		switch (brickName) {
		case "brick_5":
			brickSound = brick_5_sound;
			break;
		case "brick_10":
			brickSound = brick_10_sound;
			break;
		case "brick_25":
			brickSound = brick_25_sound;
			break;
		case "brick_100":
			brickSound = brick_100_sound;
			break;
		case "brick_power":
			brickSound = brick_power_sound;
			break;
		}
		brickSound.transform.position = brickPosition;
		brickSound.audio.Play ();

	}
}
