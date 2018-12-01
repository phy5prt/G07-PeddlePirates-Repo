using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkArduino : MonoBehaviour {


	public Dropdown Com;
	public Dropdown Baud;
	public Toggle useArduino;

	private Toggle thisToggle;
	private arduinoReceiver arduino;
	// Use this for initialization
	void Start () {
		arduino = GameObject.Find("ArduinoHolder").GetComponent<arduinoReceiver>();
		thisToggle = GetComponent<Toggle>();
		thisToggle.onValueChanged.AddListener(delegate{ToggleValueChanged(thisToggle);});
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void ToggleValueChanged(Toggle change){
		
	thisToggle.isOn = arduino.isBaudCommRight();
	if(thisToggle.isOn){
	thisToggle.interactable = false; 
	Com.interactable = false; 
	Baud.interactable=false;
	useArduino.interactable = true;}

	}
}
