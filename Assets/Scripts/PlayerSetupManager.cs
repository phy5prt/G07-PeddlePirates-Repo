using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//What we need
//each team with a max, colour, a teams layer, left right row input set, team name(is string so keep an index to a static string array with player numbnumber) 
//Ai infor to food to computer
//no need number players just have a ship per colour, it gets assigned a layer and ones allocated get put in player array and created // use colour to border their screen partician they
//dont need where they are in array

//or do teams need their own class - that overloads player?
//move some of the setting up to gamemanager maybe as some will be set in the event set up scene
//Or not ...?


//going to make a player override class
//  Player : myPlayerSetup overidestart

public class PlayerSetupManager : MonoBehaviour {

public thisPlayerPairSettings[] shipPlayerSettingsAr;

//in future may want to set max's per bike
//this would mean a child and parent could have different maxes
//there could be an option for set your own max's

//theyre static but only accessible through PlayerSetupManager which is good because dont need instance "playerSetupManager" so dont need to find it.
//however would be beter as part of maybe the game manager script for example as that will always be active.
//so later take the storage element of working with thisPlayerPairSettings and give to game manager or the Arduino object


public static thisPlayerPairSettings redPShip;
public static thisPlayerPairSettings yelPShip;
public static thisPlayerPairSettings grePShip;
public static thisPlayerPairSettings bluPShip;



//should these be statics as want anything to be able to call them?
//for now use these but i suspect later the arduino will be static and we will call the arduino static for each one of these values and 
//not here but gamemanager in the event setup will determine which thisplayersetting instance that is created as a static receives which
public float redLeftVolt;
public float redRightVolt;

public float yelLeftVolt;
public float yelRightVolt;

public float greLeftVolt;
public float greRightVolt;

public float bluLeftVolt;
public float bluRightVolt;

[SerializeField] int stage = 0;

//move the match mechanics to own script and object?
private float rateMatchReturnsToStart = 5;
private float matchPosDeg = 0f;
private float matchTurnDegreesPerDeltaTime = 6f; //a circle in a minute assuming the person cycle out put creates a float of one per second)
private float matchDegreeStart = 2550f;                      // 90f; due to scaling using different numbers but leaving this here so know intended behaviour
private float matchDegreesStrike = 2305;                  //350f;
private float minVoltDefineActivePedaling = 1f; //minimum volts in to register bike use
private GameObject matchObj;

private bool strikeMatch =false;
private float timeMatchStruck = 0f;


	// Use this for initialization
	void Start () {

	matchObj = GameObject.Find("match");    //when restructure try do so so dont have to do this
	matchPosDeg = matchDegreeStart;
	settingUpPlayerSettingAr ();

		}
	
	// Update is called once per frame
	void Update () {


	if(stage == 0){

			Debug.Log (" before matchPosDeg = " + matchPosDeg);

			if((redLeftVolt > minVoltDefineActivePedaling && redLeftVolt > minVoltDefineActivePedaling) || 
			(yelLeftVolt > minVoltDefineActivePedaling && yelLeftVolt > minVoltDefineActivePedaling) || 
			(greLeftVolt > minVoltDefineActivePedaling && greLeftVolt > minVoltDefineActivePedaling) || 
			(bluLeftVolt > minVoltDefineActivePedaling && bluLeftVolt > minVoltDefineActivePedaling) ){

				matchPosDeg += (redLeftVolt + redRightVolt + yelLeftVolt + yelRightVolt + greRightVolt + greLeftVolt + bluLeftVolt + bluRightVolt)* matchTurnDegreesPerDeltaTime*Time.deltaTime;
			}else if (matchPosDeg > matchDegreeStart ){matchPosDeg -= rateMatchReturnsToStart*Time.deltaTime;}

		if(matchPosDeg>matchDegreesStrike){matchPosDeg = matchDegreesStrike;} else if (matchPosDeg<matchDegreeStart){matchPosDeg = matchDegreeStart;}

			Debug.Log(" before matchObj.transform.eulerAngles.Set = " + matchObj.transform.eulerAngles + " setting z to matchPosDeg which is = " + matchPosDeg);
			//matchObj.transform.eulerAngles.Set = new Vector3 (matchObj.transform.eulerAngles.x, matchObj.transform.eulerAngles.x ,matchPosDeg); //euler angles?
			matchObj.transform.eulerAngles.Set(matchObj.transform.eulerAngles.x, matchObj.transform.eulerAngles.x ,matchPosDeg); //euler angles?
			Debug.Log(" after matchObj.transform.eulerAngles.Set = " + matchObj.transform.eulerAngles);


		if(matchPosDeg == matchDegreesStrike){if (strikeMatch == false ){ strikeMatch = true; timeMatchStruck = Time.timeSinceLevelLoad;}    //the code make match wait 2 seconds in lit position
			else if (strikeMatch == true && Time.timeSinceLevelLoad > timeMatchStruck + 2f){
				stage = 1;
					matchObj.SetActive(false);
					//startCountDownAndPowderKegAnimation();//Todo
					initiateStage1();}}else{strikeMatch = false;}


				
	


	}


			
	}

	void HowManyBikePairs ()
	{
		

	}

	void settingUpPlayerSettingAr ()
	{
		redPShip = new thisPlayerPairSettings {};
		yelPShip = new thisPlayerPairSettings {};
		grePShip = new thisPlayerPairSettings {};
		bluPShip = new thisPlayerPairSettings {};

		redPShip.SetShipPairName(" MondleBrot's Wives ");
		yelPShip.SetShipPairName(" Brownian's Movement ");
		grePShip.SetShipPairName(" Marie's Glow ");
		bluPShip.SetShipPairName(" Dabloon 's Good Booty! ");

		redPShip.setShipPairColor("RED");
		yelPShip.setShipPairColor("YELLOW");
		grePShip.setShipPairColor("GREEN");
		bluPShip.setShipPairColor("BLUE");

		shipPlayerSettingsAr = new thisPlayerPairSettings[] {redPShip,yelPShip,grePShip,bluPShip};
	}

	// this is the select players who are playing stage
	private void initiateStage1(){

	Debug.Log("I want to run stage1 but dont know what it is yet");
	}
}
