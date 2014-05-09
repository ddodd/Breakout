using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour {
	public GameObject BombSoundPrefab;
	private GameObject mySound;

	// Use this for initialization
	void Start () {
		Debug.Log (this+".Start:");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Explode(GameObject ball, float radius=3) {
		//Debug.Log (this+ ".Explode: ball="+ball);
		mySound = (GameObject)Instantiate (BombSoundPrefab, ball.transform.position, Quaternion.identity);
		mySound.audio.Play ();
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
		Debug.Log (hitColliders.Length + " bricks effected");
		int i = 0;
		while (i < hitColliders.Length) {
			// get brick script and call Die()
			BrickScript brickScript = hitColliders[i].gameObject.GetComponent<BrickScript>();
			if(brickScript)
				brickScript.Bomb(ball);
			i++;
		}
	}
}
