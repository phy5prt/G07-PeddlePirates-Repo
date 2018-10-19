/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;



public class playerTest : NetworkBehaviour  {

private Vector3 inputValue;
public float MovePower =1000.0f;
public float cycPercSpeedLeft = 0f;
public float cycPercSpeedRight = 0f;
public Vector3 leftCycVelAngle = new Vector3(1,0,1); 
public Vector3 rightCycVelAngle = new Vector3(1,0,-1); 
private Vector3 boatVelocityRaw = new Vector3(0,0,0);


	// Use this for initialization
	void Start () {

	//because this is in the start if in game change these angles the magnitude will not be normalized until change the setting out of game

	//just normalising but vectors tricky to code straight into something
		Vector3 leftCycVelAngleNorm = leftCycVelAngle.normalized;
		Vector3 rightCycVelAngleNorm = rightCycVelAngle.normalized;
		leftCycVelAngle = leftCycVelAngleNorm;
		rightCycVelAngle = rightCycVelAngleNorm;
	}
	
	// Update is called once per frame
	void Update () {


	if(!isLocalPlayer){return;}
	gameObject.tag = "localPlayer";

      //      float h = CrossPlatformInputManager.GetAxis("Horizontal");
      //     float v = CrossPlatformInputManager.GetAxis("Vertical");
      //      move = (v*Vector3.forward + h*Vector3.right).normalized;
          
      //should boat Vel be normalized instead
		cycPercSpeedLeft = CrossPlatformInputManager.GetAxis("XZVector");
		cycPercSpeedRight = CrossPlatformInputManager.GetAxis("X-ZVector");
		Debug.Log("left " + cycPercSpeedLeft + "right " + cycPercSpeedRight );

       boatVelocityRaw = leftCycVelAngle*cycPercSpeedLeft+ rightCycVelAngle*cycPercSpeedRight;



	 ///the old controls
	//inputValue.x = CrossPlatformInputManager.GetAxis("Horizontal");
	//inputValue.y= 0f;
	//inputValue.z = CrossPlatformInputManager.GetAxis("Vertical");
	//bike movement control
			GameObject.Find("Cylinder").GetComponent<Rigidbody>().AddForce(boatVelocityRaw*MovePower);

	}

public override void OnStartLocalPlayer()
	{this.transform.GetComponentInChildren<Camera>().enabled = true;

	}
}

*/
