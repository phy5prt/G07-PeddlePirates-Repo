using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//was working seems upset now changed the location to the bars it should be their herachy positin
//maybe something isnt reseting
//no idea why but this is the case
//actually works when all triggered may be due to sharing value!? maybe

//something is wrong with the code doesnt always make the jump

//is it running off two timers so does it not move empty its resevoir of needs this much to jump then when resevoir tested not jump
//write a list of what happens what order

public class arrowTeamSelector : MonoBehaviour {

[SerializeField] float totVoltTriggerMove = 100;
[SerializeField] float totVoltThisPeriodRight = 0;

	[SerializeField] float voltInLeft = 0;  //this will become set to the arduino input from the red port left e.g.
	[SerializeField] float voltInRight = 0; //

private Text textTimer;
[SerializeField] float currentTimerTime;
[SerializeField] float startTime;
private Image arrowImage;
private int numberPositionMovesRemaining = 6; //there and back //only runs once so can alter directly



[SerializeField] float endTime;



private float arrowJumpChoiceTime = 5;
private bool runArrowTimer;
private Vector3 startScaleArrow;

[SerializeField] GameObject[] gizmoSittingPositions; //should code this better
[SerializeField] GameObject gizmoRT;

private PlayerSetupManager PSM; //replace this with the direct addresses to statics of the input comms later
private int selectedTeamNumber;
	// Use this for initialization
	void Start () {


	PSM = GameObject.Find("PlayerStartSetUp").GetComponent<PlayerSetupManager>();

	arrowImage = GetComponentInChildren<Image>();
	arrowImage.color = Color.red;//should get first
	startScaleArrow =	arrowImage.gameObject.GetComponent<RectTransform>().localScale;
	textTimer = GetComponentInChildren<Text>();

	}
	
	// Update is called once per frame
	void Update () {



	if(runArrowTimer){arrowCountDownNow();}
			
	}

	public void runThisArrowTimer(int numberOfJumps, float countDownPerJump){

	numberPositionMovesRemaining = numberOfJumps;
	arrowJumpChoiceTime = countDownPerJump;
	runArrowTimer = true;
	}






	public void startArrowTimer(){
		totVoltThisPeriodRight = 0;
		startTime = Time.timeSinceLevelLoad;
		endTime = startTime+arrowJumpChoiceTime;
	runArrowTimer = true;


	}

	private void arrowCountDownNow(){


	setArrowScaleColor ();
		
		currentTimerTime = endTime - Time.timeSinceLevelLoad;

		if(currentTimerTime<0){textTimer.text = "";}else{textTimer.text = (currentTimerTime).ToString("0.0");}

		if(Time.timeSinceLevelLoad>endTime){
			runArrowTimer=false;
			numberPositionMovesRemaining--;
			moveTeamSelectGizmo();
			if(numberPositionMovesRemaining>=0){startArrowTimer();}	else{setTeam();}} //set team //if enabled but didnt chose a team put up team name and colour didnt get to warf in time and unenable them

			// team names "The ARRR ARRRMAARRRRDAAAARRR", "The fleet Fleet" "The Cod Sqod" "The fancy flottila " or " th''ard t' sa' arg's'y"
	}


	void setArrowScaleColor ()
	{
		totVoltThisPeriodRight += voltInRight - voltInLeft;
		if (totVoltThisPeriodRight < -totVoltTriggerMove) {
			arrowImage.color = Color.green;
			arrowImage.gameObject.GetComponent<RectTransform> ().localScale = new Vector3 (-1 * startScaleArrow.x, startScaleArrow.y, startScaleArrow.z);
		}
		else
			if (totVoltThisPeriodRight > totVoltTriggerMove) {
				arrowImage.color = Color.green;
				arrowImage.gameObject.GetComponent<RectTransform> ().localScale = startScaleArrow;
			}
			else {
				arrowImage.gameObject.GetComponent<RectTransform> ().localScale = new Vector3 ((startScaleArrow.x * totVoltThisPeriodRight / totVoltTriggerMove), startScaleArrow.y, startScaleArrow.z);
			}
	}

	private void moveTeamSelectGizmo(){

	//code looks a bit funny because x actually decrease left to right in the hierachy object dont know why


//	Debug.Log("run move teams select gizmo");

		if (totVoltThisPeriodRight < -totVoltTriggerMove) {

			if(gizmoRT.transform.position.x> gizmoSittingPositions[0].transform.position.x ){
			int indexForMostRightPositionLeftOfMe = -1;
			float currentSmallestPositionalDifference = -10000000f; 
			for(int i = 0; i<3; i++){
					if(gizmoSittingPositions[i].transform.position.x < gizmoRT.transform.position.x && (gizmoSittingPositions[i].transform.position.x )>currentSmallestPositionalDifference){
						currentSmallestPositionalDifference = gizmoSittingPositions[i].transform.position.x;
						indexForMostRightPositionLeftOfMe = i;}}
				gizmoRT.transform.position = new Vector3 (gizmoSittingPositions[indexForMostRightPositionLeftOfMe].transform.position.x, gizmoRT.transform.position.y, gizmoRT.transform.position.z); }
			
		}
		else
			if (totVoltThisPeriodRight > totVoltTriggerMove) {
		
				
				if(gizmoRT.transform.position.x< gizmoSittingPositions[3].transform.position.x ){

					int indexForMostLeftPositionRightOfMe = -1;
					float currentSmallestPositionalDifference = 10000000f; 
							for(int i = 0; i<4; i++){
						
						if(gizmoSittingPositions[i].transform.position.x  > gizmoRT.transform.position.x && (gizmoSittingPositions[i].transform.position.x )<currentSmallestPositionalDifference){
							currentSmallestPositionalDifference = gizmoSittingPositions[i].transform.position.x;
									indexForMostLeftPositionRightOfMe = i;}}
					
					gizmoRT.transform.position = new Vector3 (gizmoSittingPositions[indexForMostLeftPositionRightOfMe].transform.position.x, gizmoRT.transform.position.y, gizmoRT.transform.position.z); }
			
		}


		}
		private void setTeam(){
		for(int i =0; i<4;i++){
		//works if it connects which doesnt seem to do for first position
		//will try rounding	
			if(Mathf.Round(gizmoRT.transform.position.x) == Mathf.Round(gizmoSittingPositions[i].transform.position.x)){selectedTeamNumber = i+1; Debug.Log(" set my team number selection "); break;}}

		foreach(thisPlayerPairSettings thisPlayerPSettings in PSM.shipPlayerSettingsAr){if(thisPlayerPSettings.getShipPairColor() == tag ){thisPlayerPSettings.SetTeamNumber(selectedTeamNumber);}}

		}
	}




