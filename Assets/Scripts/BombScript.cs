using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Explode(GameObject ball, float radius=3) {
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
