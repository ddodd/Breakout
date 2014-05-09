using UnityEngine;
using System.Collections;

public class SoundScript : MonoBehaviour {

	public GameObject brick_5_sound_prefab;
	public GameObject brick_10_sound_prefab;
	public GameObject brick_25_sound_prefab;
	public GameObject brick_100_sound_prefab;
	public GameObject brick_power_sound_prefab;

	public GameObject brick_5_sound;
	public GameObject brick_10_sound;
	public GameObject brick_25_sound;
	public GameObject brick_100_sound;
	public GameObject brick_power_sound;


	// Use this for initialization
	void Start () {
		audio.Play();
		brick_5_sound = (GameObject)Instantiate (brick_5_sound_prefab);
		brick_10_sound = (GameObject)Instantiate (brick_10_sound_prefab);
		brick_25_sound = (GameObject)Instantiate (brick_25_sound_prefab);
		brick_100_sound = (GameObject)Instantiate (brick_100_sound_prefab);
		brick_power_sound = (GameObject)Instantiate (brick_power_sound_prefab);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlaySound(AudioClip clip){
		audio.PlayOneShot (clip);
	}
}
