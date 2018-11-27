using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class giveGMCommInfo : MonoBehaviour {

public Text baudInputTxt;
public Text commInputTxt;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void giveGMCommInfoMethod(){

	GameManager.port = commInputTxt.text;
	GameManager.baudrate = int.Parse(baudInputTxt.text);
		GameObject.Find("Game Manager").GetComponent<GameManager>().OpenMyArduinoStream ();

	}
}
