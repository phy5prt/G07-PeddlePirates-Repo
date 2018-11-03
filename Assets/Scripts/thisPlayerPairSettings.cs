using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//should this be a public static class?
// no but the four instances of it should be!

public class thisPlayerPairSettings : MonoBehaviour {

	// Use this for initialization

	private bool werePlaying = false;
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
	public void SetTeamNumber(float thisTeamNumber){
	teamNumber = thisTeamNumber;
	}

	public string GetTeamName(){
		return teamName;
	}
	public void SetTeamName(float thisTeamName){
	teamName = thisTeamName;
	}
}
