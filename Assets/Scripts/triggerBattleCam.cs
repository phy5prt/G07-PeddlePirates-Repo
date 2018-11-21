using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking;
using UnityEngine.UI;

public class triggerBattleCam : MonoBehaviour { //NetworkBehaviour
//turns on and off the texture on canvas showing battle cam as is needed
	// Use this for initialization

private float lastCamActivationTime;
[SerializeField] float delayBeforeBattleCamTurnsOff = 3f;

	void Start () {
		transform.GetComponentInParent<RawImage>().enabled = false;
		
	}
	
	// Update is called once per frame
	void Update () {

	if(Time.timeSinceLevelLoad - lastCamActivationTime > delayBeforeBattleCamTurnsOff){
	transform.GetComponentInParent<RawImage>().enabled = false;


		}
	}

	void OnTriggerStay(Collider ship){

		Debug.Log(" battle cam trigger triggered ");
	//we have labeled the ship localPlayer on start hopefully only to self
	//may not be the case and can't check untill in multiplayer or read up
	//so going to make this monobehaviour a network script
	//alternate solution make more triggers so there is a gap in the middle for boat
	//or stick 2 triggers seperate a bit but camera will go on and off as pass through centre
	//this its could be overcome by having a delay for camera deactivating


	//if i need to directly child the texture to the canvas to get it to snap this will have to change a bit
	//Debug.Log("triggerd"); // it triggers itself
		if(ship.tag == "Ship"){
			
			Debug.Log("ship detetected");
				this.gameObject.transform.GetComponentInParent<RawImage>().enabled = true;
				Debug.Log("applying raw image");
			
				lastCamActivationTime = Time.timeSinceLevelLoad;

				}
		}
		}
									

