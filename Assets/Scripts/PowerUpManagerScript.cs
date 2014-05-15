using UnityEngine;
using System.Collections;

public class PowerUpManagerScript : MonoBehaviour {
	public GameObject PowerUpPrefab_width;
	public GameObject PowerUpPrefab_life;

	private GameObject mySound;

	public enum PowerUpType 
	{
		WIDTH, LIFE
	}
	
	void Start () {
		mySound = GameObject.Find ("powerUp_sound");
		Debug.Log (this+".Start:"+mySound);
	}
	
	public GameObject GetPowerUp(PowerUpType type){
		GameObject powerUpPreFab = null;
		switch(type){
		case PowerUpType.WIDTH:
			powerUpPreFab = PowerUpPrefab_width;
			break;
		case PowerUpType.LIFE:
			powerUpPreFab = PowerUpPrefab_life;
			break;
		}
		return powerUpPreFab;
	}
	
	public GameObject GetRandomPowerUp(){
		GameObject powerUpPreFab = null;
		float m = Random.value;
		if(m <= .5)
			powerUpPreFab = PowerUpPrefab_width;
		else
			powerUpPreFab = PowerUpPrefab_life;
		
		return powerUpPreFab;
		
	}
	
	public void PlayPowerUpSound(GameObject powerUp){
		Debug.Log (mySound+" PlayPowerUpSound:"+powerUp);
		mySound.transform.position = powerUp.transform.position;
		mySound.audio.Play();
	}
	

	
}
