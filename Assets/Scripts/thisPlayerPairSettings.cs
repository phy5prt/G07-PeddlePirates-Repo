using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thisPlayerPairSettings : MonoBehaviour {

	// Use this for initialization

	private bool werePlaying = false;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool getWerePlaying(){
	return werePlaying;
	}
	public void setWerePlaying(bool YN){
	werePlaying = YN;
	}

}
