using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class rigAllocateThisplayerPairSettingsToBars : MonoBehaviour {

//not fond of the double foreach seems wasteful especially when could just use array and fors and match array numbers

	// Use this for initialization
	void Start () {

		Invoke("setupTheVoltBars", 3f); //just giving time for gm to allocate
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void setupTheVoltBars(){

		percBarDisplay[] percBarAr = GetComponentsInChildren<percBarDisplay>();
	foreach(percBarDisplay percBarSc in percBarAr){
	foreach(thisPlayerPairSettings playerPairVolts in GameManager.shipPlayerSettingsAr){
	if(percBarSc.gameObject.tag == playerPairVolts.getShipPairColor()){percBarSc.passMeMyPlayerPairSettings(playerPairVolts);}
	}}
	}
}
