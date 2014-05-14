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
		brickFragPool = new GameObject[poolSize];
		for(int i=0; i<poolSize; i++){
			brickFrag = (GameObject)Instantiate (brickFragPrefab);
			DontDestroyOnLoad(brickFrag);
			brickFragPool[i] = brickFrag;		}
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
		return brickFragPool[pool_hPointer++%pool_hSize];
	}

	public void Hit(GameObject brick, BrickScript.brickTypes brickType, GameObject ball, float power){
		Debug.Log ("brick:" + brick + "brickType:" + brickType + " ball:" + ball + " power:" + power );
		GameObject BrickFrag;
		switch(brickType){
		case BrickScript.brickTypes.Half:
			BrickFrag = getBrick_hFrag();
			break;

		case BrickScript.brickTypes.Full:
		default:
			BrickFrag = getBrickFrag();
			break;
		}
		BrickFragScript brickFragScript = BrickFrag.GetComponent<BrickFragScript> ();
//		brickFragScript.setBrick (brick);
		brickFragScript.hit (brick, ball, power);
	}

}
