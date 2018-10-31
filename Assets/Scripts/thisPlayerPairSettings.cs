using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thisPlayerPairSettings : MonoBehaviour {

	// Use this for initialization

	private bool werePlaying = false;
	private string shipPairColor;
	private float volt100Perc = 100f;
	private string shipPairName = "no name yet";


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool getWerePlaying(){
	return werePlaying;
	}
	public void setWerePlaying(bool YN){
	werePlaying = YN;
	}
	public string getShipPairColor(){
	return shipPairColor;
	}
	public void setShipPairColor(string shipPTColor){
	shipPairColor = shipPTColor;
	}

	public float GetVolt100Perc(){
	return volt100Perc;
	}
	public void SetVolt100Perc(float MPercVolt){
	volt100Perc = MPercVolt;
	}

	public string GetShipPairName(){
		return shipPairName;
	}
	public void SetShipPairName(string shipName){
	shipPairName = shipName;
	}
}
