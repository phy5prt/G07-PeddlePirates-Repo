using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayGMGameLength : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//would be nice if this just triggered by on value changed in the other boxes
		GetComponent<InputField>().text = GameManager.gameLength.ToString(); //doesnt seem to change
	}



}
