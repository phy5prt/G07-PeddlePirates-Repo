using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//should this be a public static class?
// no but the four instances of it should be!
//where i set the team number i need to set the team name (not ship name team name)
//need structs for the fixed info i think this would be nice
//should change everything to get set

//current sets from random number generator

public class thisPlayerPairSettings : MonoBehaviour {

	// Use this for initialization
	private bool bikePairSetAsAvailableOnEventSetup = true; //set to false after finished setting to scene

	//later add health and inputVoltMultiplier as things for the event setting page

	private bool werePlaying = true; //will need to be false in future
	private string shipPairColor;
	private float volt100Perc = 100f;
	private string shipPairName = "no name yet";
	private int teamNumber;
	private string teamName;


//using get and setting it to the arduino 
	private float myLeftVolt;    
	private float myRightVolt;




	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	//shouldnt all these be static?

	public bool getBikePairSetAsAvailableOnEventSetup(){
		return bikePairSetAsAvailableOnEventSetup;
	}
	public void setBikePairSetAsAvailableOnEventSetup(bool YN){
			bikePairSetAsAvailableOnEventSetup = YN;
	}




	public bool getWerePlaying(){
	return werePlaying;
	}
	public void setWerePlaying(bool YN){
	Debug.Log(shipPairColor + "set were playing as " + YN);
	werePlaying = YN;
	}
	public string getShipPairColor(){
	return shipPairColor;
	}
	public void setShipPairColor(string shipPTColor){
	shipPairColor = shipPTColor;
	Debug.Log(shipPairColor + " is now our color ");
	}

	public float GetVolt100Perc(){
	return volt100Perc;
	}
	public void SetVolt100Perc(float MPercVolt){
	Debug.Log(shipPairColor + " our volt max is " + volt100Perc);
	volt100Perc = MPercVolt;
	}

	public string GetShipPairName(){
		return shipPairName;
	}
	public void SetShipPairName(string shipName){
	Debug.Log(shipPairColor + " our name is now " + shipName);
	shipPairName = shipName;
	}

	public float GetmyLeftVolt(){
		return myLeftVolt;
	}
	public void SetmyLeftVolt(float myLVolt){
		myLeftVolt =  myLVolt;
	}

	public float GetmyRightVolt(){
		return myRightVolt;
	}
	public void SetmyRightVolt(float myRVolt){
		myRightVolt =  myRVolt;
	}





	public int GetTeamNumber(){
		return teamNumber;
	}
	public void SetTeamNumber(int thisTeamNumber){
	teamNumber = thisTeamNumber;
	Debug.Log(shipPairColor + " our team number is now " + teamNumber);
	
	}

	public string GetTeamName(){
		return teamName;
	}
	public void SetTeamName(string thisTeamName){

		

	teamName = thisTeamName;
	}
}
