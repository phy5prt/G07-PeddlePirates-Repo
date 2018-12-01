using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//should probably make the code also responsible for the event in inspector using a listener

public class turnOffUseArduinoToggle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void turnMeOff(){this.gameObject.GetComponent<Toggle>().interactable = false;}
}
