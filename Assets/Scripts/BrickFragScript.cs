using UnityEngine;
using System.Collections;

public class BrickFragScript : MonoBehaviour {
	private Vector3 startPosition;
	private GameObject[] fragments;
	private Vector3[] fragPositions;
	private GameObject brick;
	private GameObject ball;
	private float power;
	private bool isConstructed = false;
	private bool wasHit = false;
	private BrickFragManagerScript brickFragManagerScript;

	// Use this for initialization
	public void Start () {
		brickFragManagerScript = GameObject.Find ("BrickFragManager").GetComponent<BrickFragManagerScript> ();
		Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
		int length = renderers.Length;
		fragments = new GameObject[length];
		fragPositions = new Vector3[length];
		for(int i=0; i<length; i++){
			fragments[i] = renderers[i].gameObject;
			fragPositions[i] = fragments[i].transform.position;
		}
		startPosition = transform.position;
		isConstructed = true;
		Debug.Log ("fragments:" + fragments.Length );
	}

//	public void setBrick(GameObject brick){
//		this.brick = brick;
//		material = brick.renderer.material;
//		transform.position = brick.transform.position;
//		Paint ();
//	}
	
	public void hit(GameObject brick, GameObject ball, float power){
		if(wasHit) return;
		wasHit = true;
		this.brick = brick;
		this.ball = ball;
		this.power = power;
		Debug.Log (this+" hit by " + ball+",power:" + power+",position:"+ball.transform.position);
		Rigidbody rb;
		transform.position = brick.transform.position;
		foreach (GameObject frag in fragments) {
			frag.renderer.material = brick.renderer.material;
			rb = frag.rigidbody;
			rb.isKinematic = false;
			rb.AddExplosionForce(power+Random.Range(-power,power), ball.transform.position, 10);
			rb.useGravity = true;
		}	
		iTween.FadeTo(gameObject,iTween.Hash("alpha",0,"time",1,"oncomplete","OnComplete"));
	}
	
	// Update is called once per frame
//	void Update () {
//		Debug.Log (this+".Update isConstructed:" + isConstructed +",wasHit:"+ wasHit +",isPainted"+ isPainted +",brick:"+  brick);
//
//		if(isConstructed && wasHit && !isPainted && brick != null){
//			//Debug.Log ("update:" + renderers.Length );
//			transform.position = brick.transform.position;
//			Rigidbody rb;
//			foreach (GameObject frag in fragments) {
//				frag.renderer.material = brick.renderer.material;
//				rb = frag.rigidbody;
//				rb.isKinematic = false;
//				rb.useGravity = true;
//				rb.AddExplosionForce(power+Random.Range(-power,power), ball.transform.position, 10);
//			}	
//			isPainted = true;
//		}
//	}
	
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
		brick = null;
		ball = null;
	}
}
