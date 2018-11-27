using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getDayLengthInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

public void runGetDayLengthInput(){
GameManager.dayLength = float.Parse( GetComponentInChildren<InputField>().text); // again would be better static and then triggered on button press

	}
}
