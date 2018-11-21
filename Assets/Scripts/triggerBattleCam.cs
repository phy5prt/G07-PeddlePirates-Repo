using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class triggerBattleCam : MonoBehaviour { 


private float lastCamActivationTime;
[SerializeField] float delayBeforeBattleCamTurnsOff = 3f;
private RawImage tacticalScreen;

	void Start () {
		tacticalScreen = GetComponentInChildren<RawImage>();
		tacticalScreen.enabled = false;
	}


	
	// Update is called once per frame
	void Update () {

	if(Time.timeSinceLevelLoad - lastCamActivationTime > delayBeforeBattleCamTurnsOff){
	tacticalScreen.enabled = false;


		}
	}

	void OnTriggerStay(Collider ship){

//		Debug.Log(" battle cam trigger triggered ");
	
		if(ship.tag == "Ship"){
			
//			Debug.Log("ship detetected");
				tacticalScreen.enabled = true;
			
//				Debug.Log("applying raw image");
			
				lastCamActivationTime = Time.timeSinceLevelLoad;

				}
		}
		}
									

