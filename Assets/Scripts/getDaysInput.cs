using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getDaysInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void runGetDaysInput(){
//		Debug.Log("changing number of days from " + GameManager.numberDays + " game time was  " + GameManager.gameLength);
	GameManager.numberDays = float.Parse( GetComponentInChildren<InputField>().text); // again would be better static and then triggered on button press
//		Debug.Log("to " + GameManager.numberDays + " game time is  " + GameManager.gameLength);
	}
}
