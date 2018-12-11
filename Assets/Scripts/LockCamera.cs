using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCamera : MonoBehaviour {

//would be better with a listner that waits for the transform to change before reseting it
//this would be constant till it hits the bottom
//then would be running less checks

private bool cameraLocked = false;
//private Transform cameraLockedTrans;
private Vector3 cameraLockPos;
private Quaternion cameraLockRot;

	// Use this for initialization
	void Start () {


		
	}
	
	// Update is called once per frame
	void Update () {

	if(cameraLocked){
	Debug.Log("putting camera in locked position");
	transform.position = cameraLockPos; transform.rotation = cameraLockRot;}
				
	}
	public void lockCamera(){
	Debug.Log("locking camera");
	cameraLockPos = this.transform.position;
	cameraLockRot = this.transform.rotation;
	cameraLocked = true;
	}

}
