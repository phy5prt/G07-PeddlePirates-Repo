using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCamera : MonoBehaviour {

//would be better with a listner that waits for the transform to change before reseting it
//this would be constant till it hits the bottom
//then would be running less checks

private bool cameraLocked = false;
private Transform cameraLockedTrans;

	// Use this for initialization
	void Start () {


		
	}
	
	// Update is called once per frame
	void Update () {

	if(cameraLocked){transform.position = cameraLockedTrans.position; transform.rotation = cameraLockedTrans.rotation;}
				
	}
	public void lockCamera(){
	cameraLockedTrans = this.transform;
	cameraLocked = true;
	}

}
