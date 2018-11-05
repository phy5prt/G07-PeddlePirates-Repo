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

//https://docs.unity3d.com/ScriptReference/MonoBehaviour.StartCoroutine.html
//https://unity3d.com/learn/tutorials/topics/scripting/coroutines
//return yield = coroutinestart(method (time)) -- is this the same as run the method over the amount of frames it needs then return the result
//maybe every satge should be a coroutine rather than a method.
//try this at refactor stage i currently want minimal product


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
private bool[] someoneIsAlreadyGoingToSetMyMax = {false,false,false,false};


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

//can i make this and this whole script static?
[SerializeField] static int stage = 0;

//move the match mechanics to own script and object?
[SerializeField] float rateMatchReturnsToStart = 75f;
private float matchPosDeg = 0f;
[SerializeField] float matchTurnDegreesPerDeltaTime = 60f; //a circle in a minute assuming the person cycle out put creates a float of one per second)
	private float matchDegreeStart = 0f ;              //2550f        // 90f; due to scaling using different numbers but leaving this here so know intended behaviour
	private float matchDegreesStrike = 240f;         //2305         //350f;
private float minVoltDefineActivePedaling = 1f; //minimum volts in to register bike use
private GameObject matchObj;

private bool strikeMatch =false;
private float timeMatchStruck = 0f;

private Quaternion startRotationPosMatch;

private GameObject	stageCountDownImageGO;
private GameObject	chooseTeamStageGO;
private GameObject setCrewsAndShrinkLeft;
private MXSBCtagSloppy[] maxSetRivalssGOTagSC;
private arrowTeamSelector[] arrowTeamSelectGizmos;
private pairSetRivalMaxBar[] pairSetRivalMaxes;

[SerializeField] float stage1Time = 10f;
private float stage1EndTime = 1000f;

[SerializeField] float stage2Time = 10f;
private float stage2EndTime = 1000f;


[SerializeField] float stage2ShrinkTime = 0.7f;

	// Use this for initialization


private thisPairWantsAship[] currentlySelectedsGOs; // only need turning on and off not individually assigning because they using the static can individually assign to the thisPlayerPairSettingPair static instance themselves

[SerializeField] int numberPositionMovesStage2 = 6;
[SerializeField] float arrowJumpChoiceTimeStage2 = 3f; 



					void Start () {

	matchObj = GameObject.Find("Match").gameObject;    //when restructure try do so so dont have to do this
	startRotationPosMatch =	matchObj.transform.rotation;
	matchPosDeg = matchDegreeStart;

	setCrewsAndShrinkLeft = GetComponentInChildren<shrinkLeft>().gameObject;

	stageCountDownImageGO = transform.Find("stageCountDownImage").gameObject;
	stageCountDownImageGO.SetActive(false);
		chooseTeamStageGO = transform.Find("chooseTeamStage").gameObject;
	chooseTeamStageGO.transform.GetChild(0).gameObject.SetActive(false);

	currentlySelectedsGOs = GetComponentsInChildren<thisPairWantsAship> (true);
	foreach(thisPairWantsAship gizmo in currentlySelectedsGOs){gizmo.gameObject.SetActive(false);}


		maxSetRivalssGOTagSC = GetComponentsInChildren<MXSBCtagSloppy>();
		foreach(MXSBCtagSloppy maxSetter in maxSetRivalssGOTagSC){maxSetter.gameObject.SetActive(false);}
		arrowTeamSelectGizmos = GetComponentsInChildren<arrowTeamSelector>();
		foreach(arrowTeamSelector teamSelector in arrowTeamSelectGizmos){teamSelector.gameObject.SetActive(false);}

		pairSetRivalMaxes =  GetComponentsInChildren<pairSetRivalMaxBar>();

	settingUpPlayerSettingAr ();

		}



	

	
	// Update is called once per frame
	void Update () {

	//stage 0 is do people want to play
	//stage 1 is everyone who wants to play select
	//stage 2 chose team 




	if(stage == 0){ // when ready make this a method in another class that returns the stage int it adds 1 if it is time to move on
		stage = stage0Method ();}else 
	if (stage == 1 && Time.timeSinceLevelLoad > stage1EndTime){ //this is begging for a coroutine or something that says runs this and when this done run this or run this and we knoew it takes this so after this amoutn of time do this
		//stage = initiateShrinkStage2();}else
			initiateShrinkStage2(stage2ShrinkTime);
			Invoke("setupStage2AfterShrink",stage2ShrinkTime);
			//need to be run in a coroutine and only once


			}else
	if (stage == 2){initiateStage3();


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

	private int stage0Method (){

		//issue with this if is that if a pair is cycling 3 singles can still contribut  but not sure it is a problem 
		//could make them ifs in the if
		if ((redLeftVolt > minVoltDefineActivePedaling && redRightVolt > minVoltDefineActivePedaling) || (yelLeftVolt > minVoltDefineActivePedaling && yelRightVolt > minVoltDefineActivePedaling) || (greLeftVolt > minVoltDefineActivePedaling && greRightVolt > minVoltDefineActivePedaling) || (bluLeftVolt > minVoltDefineActivePedaling && bluRightVolt > minVoltDefineActivePedaling)) {
			matchPosDeg += (redLeftVolt + redRightVolt + yelLeftVolt + yelRightVolt + greRightVolt + greLeftVolt + bluLeftVolt + bluRightVolt) * matchTurnDegreesPerDeltaTime * Time.deltaTime;
		}
		else
			if (matchPosDeg > matchDegreeStart) {
				matchPosDeg -= rateMatchReturnsToStart * matchTurnDegreesPerDeltaTime * Time.deltaTime;
			}
		if (matchPosDeg > matchDegreesStrike) {
			matchPosDeg = matchDegreesStrike;
		}
		else
			if (matchPosDeg < matchDegreeStart) {
				matchPosDeg = matchDegreeStart;
			}
		//Debug.Log(" before matchObj.transform.eulerAngles.Set = " + matchObj.transform.eulerAngles + " setting z to matchPosDeg which is = " + matchPosDeg);
		//matchObj.transform.eulerAngles = new Vector3 (matchObj.transform.eulerAngles.x, matchObj.transform.eulerAngles.x ,matchPosDeg); //euler angles works
		matchObj.transform.rotation = startRotationPosMatch;
		matchObj.transform.Rotate (Vector3.back * matchPosDeg);
		//Debug.Log(" after matchObj.transform.eulerAngles.Set = " + matchObj.transform.eulerAngles);
		if (matchPosDeg == matchDegreesStrike) {
			if (strikeMatch == false) {
				strikeMatch = true;
				timeMatchStruck = Time.timeSinceLevelLoad;
			}
			//the code make match wait 2 seconds in lit position
			else
				if (strikeMatch == true && Time.timeSinceLevelLoad > timeMatchStruck + 1f) {
					stage = 1;
					matchObj.SetActive (false);
					//startCountDownAndPowderKegAnimation();//Todo
					initiateStage1();
				}
		}
		else {
			strikeMatch = false;
		}
		return stage;
	}


	private void initiateStage1(){

	stageCountDownImageGO.SetActive(true);
	stageCountDownImageGO.GetComponent<redCountDownTimer>().startStageTimer(stage1Time);
	foreach(thisPairWantsAship gizmo in currentlySelectedsGOs){gizmo.gameObject.SetActive(true);}
	stage1EndTime = Time.timeSinceLevelLoad + stage1Time;
	}

	private void initiateShrinkStage2(float theShrinkTime){			//startShrinking triggers its own arrows and bars
		foreach(thisPairWantsAship gizmo in currentlySelectedsGOs){gizmo.gameObject.SetActive(false);}
		//this triggers stage 2
	//in their code the enabled selectors will have set the static instances so check which are enabled if zero ...

	//check static instances playerSettings to see if they are enabled // if not run the reset show an explosion

	//if more than zero just deactivate the gizmos that are not selected

		setCrewsAndShrinkLeft.GetComponent<shrinkLeft>().startShrinking(theShrinkTime); 

	}

	private void setupStage2AfterShrink(){


	if(stage!=1){return;} // wont need if using coroutine to just run it once

	chooseTeamStageGO.transform.GetChild(0).gameObject.SetActive(true);
	foreach(arrowTeamSelector teamSelector in arrowTeamSelectGizmos){teamSelector.gameObject.SetActive(true);}
	foreach(arrowTeamSelector teamSelector in arrowTeamSelectGizmos){teamSelector.runThisArrowTimer(numberPositionMovesStage2,arrowJumpChoiceTimeStage2);}

	stage2Time = numberPositionMovesStage2*arrowJumpChoiceTimeStage2 + 1f; //1f just incase lose some time in processing // seems go a little long maybe -1 on the stages but not sure why

	stageCountDownImageGO.SetActive(true);
	stageCountDownImageGO.GetComponent<redCountDownTimer>().startStageTimer(stage2Time);
	stage2EndTime = Time.timeSinceLevelLoad + stage2Time;

	stage = 2;
	}



	private void initiateStage3(){	


			//i could send the method an index or which player prefs to use

		//moving the gizmos	 - they move themselves
		//they set themselves so just need to wait
	
		//or reset if they dies in set up by not chooseing a team show rest
	if(Time.timeSinceLevelLoad> stage2EndTime){
			foreach(arrowTeamSelector teamSelector in arrowTeamSelectGizmos){teamSelector.gameObject.SetActive(false);}
			foreach(MXSBCtagSloppy maxSetter in maxSetRivalssGOTagSC){maxSetter.gameObject.SetActive(true);}

			//this loop prioritizes setting for an enemy then for yourself then if you must for an ally, however would be better if when you set who you will be assigning the max to then
			//it assigns them back to you, so more likely that you would end up being assigned yourself if run out of enemies
			//also if more allies than enemies would be nice if instead of setting for an ally which you will be tempted to let off lightly but instead you joint contributed to an enemy
			//this system would have you just peddle to set everyone elses maxes except your own and if you are the only player then you set your own

			//this all risk infinite loop if we dont hit the breaks 

			for(int i=0; i<4; i++ ){someoneIsAlreadyGoingToSetMyMax[i]= shipPlayerSettingsAr[i].getWerePlaying();}

			for(int i = 0; i<4; i++){
			if(pairSetRivalMaxes[i].gameObject.activeInHierarchy != false){

			int checkArrayAt=0;
			int numberChecked = 0;

					for(int j = i; j< 4; j++){
						numberChecked ++;
						if(numberChecked <6){

													if(j+1>= 4){checkArrayAt = 0; j = 0;}else{checkArrayAt = j+1;}    //should plus one so not do zero twice


													if (someoneIsAlreadyGoingToSetMyMax[i] == false && i==j){pairSetRivalMaxes[i].runSetMaxFor(shipPlayerSettingsAr[i]);		someoneIsAlreadyGoingToSetMyMax[i] = true; break;}		//set my rival as myself if been through all the options // only works if i have put them in the array in same  order found the gizmos
														else if(someoneIsAlreadyGoingToSetMyMax[checkArrayAt] == false &&  																												//thearray has already excluded non players
																shipPlayerSettingsAr[checkArrayAt].GetTeamNumber()!=shipPlayerSettingsAr[i].GetTeamNumber())																			//make sure on opposing team

																{pairSetRivalMaxes[i].runSetMaxFor(shipPlayerSettingsAr[checkArrayAt]);		someoneIsAlreadyGoingToSetMyMax[checkArrayAt] = true; break;		}						//assign rival
																													
															

						}else{if(j+1>= 4){checkArrayAt = 0; j = 0;}else{checkArrayAt = j+1;}    //should plus one so not do zero twice


						if (someoneIsAlreadyGoingToSetMyMax[checkArrayAt] == false){pairSetRivalMaxes[i].runSetMaxFor(shipPlayerSettingsAr[checkArrayAt]);		someoneIsAlreadyGoingToSetMyMax[checkArrayAt] = true; break;}		//set my rival as myself if been through all the options // only works if i have put them in the array in same  order found the gizmos
														
			}
			} 

			}

		
			  


		stage=3; 
		}
		}
}
