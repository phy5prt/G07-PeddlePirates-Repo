﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;






//https://docs.unity3d.com/ScriptReference/MonoBehaviour.StartCoroutine.html
//https://unity3d.com/learn/tutorials/topics/scripting/coroutines
//return yield = coroutinestart(method (time)) -- is this the same as run the method over the amount of frames it needs then return the result
//maybe every satge should be a coroutine rather than a method.
//try this at refactor stage i currently want minimal product

//later need a screen that summarizes teams, players and the maxes, and gives chance to get out - use the slide out menu holding high scores
// so can see setting mirrored - maybe slides up over team warf - it is unclear what end result is - or at end of the setting replace gizmo with a clear image and the max
//or even a spikey star bubble with info in like a sale sticker

public class PlayerSetupManager : MonoBehaviour {





private bool[] someoneIsAlreadyGoingToSetMyMax = {false,false,false,false}; // make it part of player settings




//                                                            MATCH                                                                                  //

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


//                                                      Finding Components                                                                    //


private GameObject	stageCountDownImageGO;
private GameObject	chooseTeamStageGO;
private GameObject setCrewsAndShrinkLeft;
private MXSBCtagSloppy[] maxSetRivalssGOTagSC;
private arrowTeamSelector[] arrowTeamSelectGizmos;
private pairSetRivalMaxBar[] pairSetRivalMaxes;
private PirateInstruction switchInstructionTexts;
private selectorPBGizmo[] gizmoAr;


//													Setting the Stages                                                                     //


[SerializeField] static int stage = 0;

[SerializeField] float stage1Time = 10f;
private float stage1EndTime = 1000f;

[SerializeField] float stage2Time = 10f;
private float stage2EndTime = 1000f;

[SerializeField] float  stage3Time = 10f;
private float stage3EndTime = 1000f;




//                                                    Stage 2                                                                            //

[SerializeField] float stage2ShrinkTime = 0.7f;

private thisPairWantsAship[] currentlySelectedsGOs; // only need turning on and off not individually assigning because they using the static can individually assign to the thisPlayerPairSettingPair static instance themselves

[SerializeField] int numberPositionMovesStage2 = 6;
[SerializeField] float arrowJumpChoiceTimeStage2 = 3f; 









	void Start () {



	matchObj = GameObject.Find("Match").gameObject;    //when restructure try do so so dont have to do this
	startRotationPosMatch =	matchObj.transform.rotation;
	matchPosDeg = matchDegreeStart;

	setCrewsAndShrinkLeft = GetComponentInChildren<shrinkLeft>().gameObject;
	gizmoAr = setCrewsAndShrinkLeft.GetComponentsInChildren<selectorPBGizmo>();	

	stageCountDownImageGO = transform.Find("stageCountDownImage").gameObject;
	stageCountDownImageGO.SetActive(false);
		chooseTeamStageGO = transform.Find("chooseTeamStage").gameObject;
	chooseTeamStageGO.transform.GetChild(0).gameObject.SetActive(false);

	currentlySelectedsGOs = GetComponentsInChildren<thisPairWantsAship> (true);
	foreach(thisPairWantsAship gizmo in currentlySelectedsGOs){gizmo.gameObject.SetActive(false);}

		switchInstructionTexts = GetComponentInChildren<PirateInstruction>();
		maxSetRivalssGOTagSC = GetComponentsInChildren<MXSBCtagSloppy>();
		foreach(MXSBCtagSloppy maxSetter in maxSetRivalssGOTagSC){maxSetter.gameObject.SetActive(false);}
		arrowTeamSelectGizmos = GetComponentsInChildren<arrowTeamSelector>();
		foreach(arrowTeamSelector teamSelector in arrowTeamSelectGizmos){teamSelector.gameObject.SetActive(false);}

		pairSetRivalMaxes =  gameObject.GetComponentsInChildren<pairSetRivalMaxBar>(true);




		}



	void Update () { // using coroutine could i avoid using update or using switch statements // dont think i need the elses either

	//stage 0 is do people want to play
	//stage 1 is everyone who wants to play select
	//stage 2 chose team 




	if(stage == 0){ 
		stage = stage0Method ();}else 
	if (stage == 1 && Time.timeSinceLevelLoad > stage1EndTime){
			
			initiateShrinkStage2(stage2ShrinkTime);
			Invoke("setupStage2AfterShrink",stage2ShrinkTime);
			}else
	if (stage == 2){initiateStage3();


	}else
	if(stage==3 && Time.timeSinceLevelLoad> stage3EndTime){setupCompleteStartGame();};


			
	}





	private int stage0Method (){

		//issue with this if is that if a pair is cycling 3 singles can still contribut  but not sure it is a problem 
		//could make them ifs in the if


		if ((GameManager.redPShip.GetmyLeftVolt() > minVoltDefineActivePedaling && GameManager.redPShip.GetmyRightVolt() > minVoltDefineActivePedaling) || (GameManager.yelPShip.GetmyLeftVolt() > minVoltDefineActivePedaling && GameManager.yelPShip.GetmyRightVolt() > minVoltDefineActivePedaling) || (GameManager.grePShip.GetmyLeftVolt() > minVoltDefineActivePedaling && GameManager.grePShip.GetmyRightVolt() > minVoltDefineActivePedaling) || (GameManager.bluPShip.GetmyLeftVolt() > minVoltDefineActivePedaling && GameManager.bluPShip.GetmyRightVolt() > minVoltDefineActivePedaling)) {
			matchPosDeg += (GameManager.redPShip.GetmyLeftVolt() + GameManager.redPShip.GetmyRightVolt() + GameManager.yelPShip.GetmyLeftVolt() + GameManager.yelPShip.GetmyRightVolt() + GameManager.grePShip.GetmyRightVolt() + GameManager.grePShip.GetmyLeftVolt() + GameManager.bluPShip.GetmyLeftVolt() + GameManager.bluPShip.GetmyRightVolt()) * matchTurnDegreesPerDeltaTime * Time.deltaTime;
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

		matchObj.transform.rotation = startRotationPosMatch;
		matchObj.transform.Rotate (Vector3.back * matchPosDeg);

		if (matchPosDeg == matchDegreesStrike) {
			if (strikeMatch == false) {
				strikeMatch = true;
				timeMatchStruck = Time.timeSinceLevelLoad;
			}
			//the code makes the match wait 2 seconds in lit position
			else
				if (strikeMatch == true && Time.timeSinceLevelLoad > timeMatchStruck + 1f) {
					stage = 1;
					matchObj.SetActive (false);
					//startCountDownAndPowderKegAnimation();//Todo
					initiateStage1();
					switchInstructionTexts.updatePirateText(1);
				}
		}
		else {
			strikeMatch = false;
		}
		return stage;
	}


	private void initiateStage1(){ //the code that does the selection is the thispairwantsaship

	stageCountDownImageGO.SetActive(true);
	stageCountDownImageGO.GetComponent<redCountDownTimer>().startStageTimer(stage1Time);
	foreach(thisPairWantsAship gizmo in currentlySelectedsGOs){gizmo.gameObject.SetActive(true);}
	stage1EndTime = Time.timeSinceLevelLoad + stage1Time;
	}



	private void initiateShrinkStage2(float theShrinkTime){			//startShrinking triggers its own arrows and bars



		removeGizmosWithNoPlayers(); //think deletes unwanted gizmos too! relabel
		switchInstructionTexts.updatePirateText(2);
		foreach(thisPairWantsAship gizmo in currentlySelectedsGOs){gizmo.gameObject.SetActive(false);}
		//this triggers stage 2

	

	//check static instances playerSettings to see if they are enabled // if not run the reset show an explosion

	//if more than zero just deactivate the gizmos that are not selected

		setCrewsAndShrinkLeft.GetComponent<shrinkLeft>().startShrinking(theShrinkTime); 

	}

	private void setupStage2AfterShrink(){


	if(stage!=1){return;} // wont need if using coroutine to just run it once

	chooseTeamStageGO.transform.GetChild(0).gameObject.SetActive(true);
	foreach(arrowTeamSelector teamSelector in arrowTeamSelectGizmos){teamSelector.gameObject.SetActive(true);}
	foreach(arrowTeamSelector teamSelector in arrowTeamSelectGizmos){teamSelector.runThisArrowTimer(numberPositionMovesStage2,arrowJumpChoiceTimeStage2);}

	stage2Time = numberPositionMovesStage2*arrowJumpChoiceTimeStage2 + 1f; //1f just incase lose some time in processing

	stageCountDownImageGO.SetActive(true);
	stageCountDownImageGO.GetComponent<redCountDownTimer>().startStageTimer(stage2Time);
	stage2EndTime = Time.timeSinceLevelLoad + stage2Time;

	stage = 2;
	}



	private void initiateStage3(){	 //currently not destroying ship in the move out warf or get blown up section

				
	
		//or reset if they die in set up by not choosing a team show rest
	if(Time.timeSinceLevelLoad> stage2EndTime){

			removeGizmosWithNoPlayers();
	//another example where if i used a list things would be good here if i made a list and checked if that number war already in it


	//calculates if more than one team 
	//could just get the shiparray and count if greater than zero
			int[] teamNumberHasTeam = {0,0,0,0};
			foreach(thisPlayerPairSettings playerWithTeam in GameManager.shipPlayerSettingsAr){
				if(playerWithTeam.getWerePlaying()==true){
				Debug.Log(" should always be over zero "+ playerWithTeam.GetTeamNumber()); //error cause beacuse was zero should of been 4 i think
				teamNumberHasTeam[playerWithTeam.GetTeamNumber()-1]=1;}}

			int numberOfTeams = 0;
			foreach(int thisInt in teamNumberHasTeam){numberOfTeams += thisInt;}
			if(numberOfTeams>1){switchInstructionTexts.updatePirateText(3);}
			else{switchInstructionTexts.updatePirateText(4);}






			stageCountDownImageGO.SetActive(true);
			stageCountDownImageGO.GetComponent<redCountDownTimer>().startStageTimer(stage3Time);
			stage3EndTime = Time.timeSinceLevelLoad + stage3Time;


			foreach(arrowTeamSelector teamSelector in arrowTeamSelectGizmos){teamSelector.gameObject.SetActive(false);}
			foreach(MXSBCtagSloppy maxSetter in maxSetRivalssGOTagSC){maxSetter.gameObject.SetActive(true);}

			//this loop prioritizes setting for an enemy then for yourself then if you must for an ally, however would be better if when you set who you will be assigning the max to then
			//it assigns them back to you, so more likely that you would end up being assigned yourself if run out of enemies
			//also if more allies than enemies would be nice if instead of setting for an ally which you will be tempted to let off lightly but instead you joint contributed to an enemy
			//this system would have you just peddle to set everyone elses maxes except your own and if you are the only player then you set your own



			for(int i=0; i<4; i++ ){someoneIsAlreadyGoingToSetMyMax[i]= !GameManager.shipPlayerSettingsAr[i].getWerePlaying();}

		
			for(int i = 0; i<4; i++){
	
			if(pairSetRivalMaxes[i].gameObject.activeInHierarchy != false){

			int checkArrayAt=0;
			int numberChecked = 0;

					// to avoid dirty infinite looping maybe checkArray at should = i+j+1 and if that is > 4 the checkedArray = i+j+1 -4, 
					//and if j = 4 if j and have an if j4-8 you run the same loop and select an ally if get to 8 and found nothing it will just run ahead but do a debug.


					for(int j = i; j< 4; j++){ //changed j to -1 because it added as it circles
					//	Debug.Log(gameObject.tag + " start loop j " + j);
				

						numberChecked ++;
						if(numberChecked <4){

													if(j+1>= 4){checkArrayAt = 0; j = -1;}else{checkArrayAt = j+1;}    //should plus one so not do zero twice


													//this code never actually triggers the else if sets it to itself first dont know why it doesnt work. 
												Debug.Log(gameObject.tag + " check array at "  +checkArrayAt);
							if (someoneIsAlreadyGoingToSetMyMax[i] == false && i==checkArrayAt){pairSetRivalMaxes[i].runSetMaxFor(GameManager.shipPlayerSettingsAr[checkArrayAt]);		someoneIsAlreadyGoingToSetMyMax[checkArrayAt] = true; /*Debug.Log(gameObject.tag + " setting as myself");*/ break; }		//set my rival as myself if been through all the options // only works if i have put them in the array in same  order found the gizmos


														else if(someoneIsAlreadyGoingToSetMyMax[checkArrayAt] == false &&  																												//thearray has already excluded non players
																GameManager.shipPlayerSettingsAr[checkArrayAt].GetTeamNumber()!=GameManager.shipPlayerSettingsAr[i].GetTeamNumber())																			//make sure on opposing team

							{pairSetRivalMaxes[i].runSetMaxFor(GameManager.shipPlayerSettingsAr[checkArrayAt]);		someoneIsAlreadyGoingToSetMyMax[checkArrayAt] = true; /*Debug.Log(gameObject.tag + " setting as enemy");*/ break;		}						//assign rival
																													
															

						}else if (numberChecked <8){if(j+1>= 4){checkArrayAt = 0; j = -1;}else{checkArrayAt = j+1;} 
						Debug.Log(gameObject.tag + " check array at "  +checkArrayAt);   //should plus one so not do zero twice
						if (someoneIsAlreadyGoingToSetMyMax[checkArrayAt] == false){pairSetRivalMaxes[i].runSetMaxFor(GameManager.shipPlayerSettingsAr[checkArrayAt]);		someoneIsAlreadyGoingToSetMyMax[checkArrayAt] = true; /*Debug.Log(gameObject.tag + " setting as my team mate");*/ break;}		//set my rival as my team mate// only works if i have put them in the array in same  order found the gizmos
														
						}else{Debug.Log(gameObject.tag + " not been given a rival or self or team mate so returning" + " availablilty aray reads " +someoneIsAlreadyGoingToSetMyMax[0]+someoneIsAlreadyGoingToSetMyMax[1]+someoneIsAlreadyGoingToSetMyMax[2]+someoneIsAlreadyGoingToSetMyMax[3]); return;} // ha ha this is where my error occurs, hmm it triggers when only one player!
			} 

			}

		
			  


		}stage=3; 
		}
		}

		private void setupCompleteStartGame(){

			GameManager.startGame();

			Debug.Log(" about to set playersetup GO false ");
			this.gameObject.SetActive(false);

			//probably just run GameManager from here as all info should be in statics, then turn off the setup

		}

		private void removeGizmosWithNoPlayers(){ 

			int countPlayerPair = 0;




			foreach(thisPlayerPairSettings playerPair in GameManager.shipPlayerSettingsAr){if (playerPair.getWerePlaying() == false)
			{foreach(selectorPBGizmo gizmoSc in gizmoAr){if(gizmoSc.gameObject.tag == playerPair.getShipPairColor()){gizmoSc.gameObject.SetActive(false);}}}else{countPlayerPair++;}

			}
			if(countPlayerPair<1){resetSetUp();}

			}

		private void resetSetUp(){//todo

		Debug.Log(" tried to trigger reset on setup but we dont have that option ");
		//speed up fuse animation
		//set text to something like too slow your not gonna make it
		//do an explosion bubble
		//reset everything ... will have to come through and see whats changed

		}
}
