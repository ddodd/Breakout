using UnityEngine;
using System.Collections;

public class SoundScript : MonoBehaviour {
	public AudioClip myClip;
	// Use this for initialization
	void Start () {
		audio.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlaySound(AudioClip clip){
		audio.PlayOneShot (clip);
	}
}
