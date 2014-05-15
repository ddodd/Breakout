using UnityEngine;
using System.Collections;

public class BrickFragManagerScript : MonoBehaviour {
	public GameObject brickFragPrefab;
	public int poolSize = 20;
	private GameObject[] brickFragPool;
	private int poolPointer = 0;

	public GameObject brickFrag_hPrefab;
	public int pool_hSize = 10;
	private GameObject[] brickFrag_hPool;
	private int pool_hPointer = 0;

		// Use this for initialization
	void Start () {
		//return;
		GameObject brickFrag;
		// create full size brick frag pool
		brickFragPool = new GameObject[poolSize];
		for(int i=0; i<poolSize; i++){
			brickFrag = (GameObject)Instantiate (brickFragPrefab);
			DontDestroyOnLoad(brickFrag);
			brickFragPool[i] = brickFrag;
		}
		// create half size brick frag pool
		brickFrag_hPool = new GameObject[pool_hSize];
		for(int i=0; i<pool_hSize; i++){
			brickFrag = (GameObject)Instantiate (brickFrag_hPrefab);
			DontDestroyOnLoad(brickFrag);
			brickFrag_hPool[i] = brickFrag;
		}
	}

	public GameObject getBrickFrag(){
		int index = poolPointer++ % poolSize;
		Debug.Log ("index: " + index);
		return brickFragPool[index];
	}
	
	public GameObject getBrick_hFrag(){
		int index = pool_hPointer++ % pool_hSize;
		Debug.Log ("index: " + index);
		return brickFragPool[index];
	}

	public void Hit(GameObject brick, BrickScript.BrickSize brickSize, GameObject ball, Vector3 force){
		Debug.Log ("brick:" + brick + "brick size:" + brickSize + " ball:" + ball + " force:" + force );
		GameObject BrickFrag;
		switch(brickSize){
		case BrickScript.BrickSize.HALF:
			BrickFrag = getBrick_hFrag();
			break;

		case BrickScript.BrickSize.FULL:
		default:
			BrickFrag = getBrickFrag();
			break;
		}
		BrickFragScript brickFragScript = BrickFrag.GetComponent<BrickFragScript> ();
		brickFragScript.hit (brick, ball, force);
	}

}
