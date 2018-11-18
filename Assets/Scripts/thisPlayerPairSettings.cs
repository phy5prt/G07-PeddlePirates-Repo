using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//should this be a public static class?
// no but the four instances of it should be!
//where i set the team number i need to set the team name (not ship name team name)
//need structs for the fixed info i think this would be nice
//should change everything to get set

public class thisPlayerPairSettings : MonoBehaviour {

	// Use this for initialization
	private bool bikePairSetAsAvailableOnEventSetup = false;

	//later add health and inputVoltMultiplier as things for the event setting page

	private bool werePlaying = true; //will need to be false in future
	private string shipPairColor;
	private float volt100Perc = 100f;
	private string shipPairName = "no name yet";
	private int teamNumber;
	private string teamName;


	/// 
	/// 
	/// so the shipModel will get its colour volt max etc and strip them off this static
	//it will also get the (address, so instantiate, or pass an array number for which arduino volt static to call on? arduino changes the static)
	private GameObject myLeftVolt;     //i think gameObject because storing the address (could be the comm port number) of where to find the thing really
	private GameObject myRightVolt;

	//public static float testVoltStatic; // accessible without an instance


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

	public GameObject GetmyLeftVolt(){
		return myLeftVolt;
	}
	public void SetmyLeftVolt(GameObject myLVolt){
		myLeftVolt =  myLVolt;
	}

	public GameObject GetmyRightVolt(){
		return myRightVolt;
	}
	public void SetmyRightVolt(GameObject myRVolt){
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
