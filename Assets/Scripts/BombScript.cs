using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour {
	public GameObject BombSoundPrefab;
	public GameObject ExplosionPrefab;

	private GameObject sound;
	private GameObject explosion;

	// Use this for initialization
	void Start () {
		Debug.Log (this+".Start:");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Explode(GameObject ball, float radius=3) {
		//Debug.Log (this+ ".Explode: ball="+ball);
		explosion = (GameObject)Instantiate (ExplosionPrefab, ball.transform.position, Quaternion.identity);
		sound = (GameObject)Instantiate (BombSoundPrefab, ball.transform.position, Quaternion.identity);
		sound.audio.Play ();
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
		Destroy(gameObject);
		Destroy(sound);
	}
}
