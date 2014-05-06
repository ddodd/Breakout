using UnityEngine;
using System.Collections;

public class PowerUpManagerScript : MonoBehaviour {
	public GameObject PowerUpPrefab_width;
	public GameObject PowerUpPrefab_life;

	void Start () {
	}
	
	void Update () 
	{
	}

	public GameObject GetPowerUp(string type){
		GameObject powerUpPreFab = null;
		switch(type){
		case "WIDTH":
			powerUpPreFab = PowerUpPrefab_width;
			break;
		case "LIFE":
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
	

	
}
