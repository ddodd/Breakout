using UnityEngine;
using System.Collections;

public class BrickFragScript : MonoBehaviour {
	public float duration = 1;
	private Vector3 startPosition;
	private GameObject[] fragments;
	private Vector3[] fragPositions;
	private bool wasHit = false;

	// Use this for initialization
	public void Start () {
		Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
		int length = renderers.Length;
		fragments = new GameObject[length];
		fragPositions = new Vector3[length];
		for(int i=0; i<length; i++){
			fragments[i] = renderers[i].gameObject;
			fragPositions[i] = fragments[i].transform.position;
		}
		startPosition = transform.position;
		Debug.Log ("fragments:" + fragments.Length );
	}

	public void hit(GameObject brick, GameObject ball, Vector3 force){
		if(wasHit) return;
		wasHit = true;
		//Debug.Log (this+" hit by " + ball+",force:" + force+",position:"+ball.transform.position);
		Rigidbody rb;
		float power;
		float bang;
		transform.position = brick.transform.position;
		foreach (GameObject frag in fragments) {
			frag.renderer.material = brick.renderer.material;
			rb = frag.rigidbody;
			rb.isKinematic = false;
			power = Vector3.Magnitude(force);
			bang = power+Random.Range(-power,power);
			rb.AddExplosionForce(bang, ball.transform.position, 10);
			rb.AddForce(force);
			rb.useGravity = true;
		}	
		iTween.FadeTo(gameObject,iTween.Hash("alpha",0,"time",duration,"oncomplete","OnComplete"));
	}
	
	void OnComplete () {
		Debug.Log ("OnComplete");
		Reset ();
	}
	
	public void Reset(){
		Debug.Log ("Resetting fragments:"+fragments.Length);
		iTween.FadeTo(gameObject,iTween.Hash("alpha",1,"time",0));
		transform.position = startPosition;
		for(int i=0; i<fragments.Length; i++){
			GameObject frag = fragments[i];
			frag.rigidbody.isKinematic = true;
			frag.rigidbody.useGravity = false;
			frag.transform.position = fragPositions[i];
			frag.transform.rotation = Quaternion.identity;
		}
		wasHit = false;
	}
}
