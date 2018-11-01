using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class arrowTeamSelector : MonoBehaviour {

[SerializeField] float totVoltTriggerMove = 100;
[SerializeField] float totVoltThisPeriodRight = 0;

public float voltInLeft;  //this will become set to the arduino input from the red port left e.g.
public float voltInRight; //

private Text textTimer;
[SerializeField] float currentTimerTime;
[SerializeField] float startTime;
private Image arrowImage;
[SerializeField] int numberPositionMovesLeft = 6; //there and back



[SerializeField] float endTime;

[SerializeField] float time = Time.time; //not need just to help me calibrate

[SerializeField] float arrowChoosingTime = 5;
[SerializeField] bool runArrowTimer;
private Vector3 startScaleArrow;

public Transform[] gizmoSittingPositions; //should code this better
private Vector3 gizmoPosition;
	// Use this for initialization
	void Start () {




	arrowImage = GetComponentInChildren<Image>();
	arrowImage.color = Color.red;//should get first
	startScaleArrow =	arrowImage.gameObject.GetComponent<RectTransform>().localScale;
	gizmoPosition = GetComponent<RectTransform>().localPosition;
	textTimer = GetComponentInChildren<Text>();

	}
	
	// Update is called once per frame
	void Update () {
	time = Time.time;


	if(runArrowTimer){arrowCountDownNow();}
			
	}






	public void startArrowTimer(float setArrowTimerTime){
		totVoltThisPeriodRight = 0;
		startTime = Time.time;
		endTime = startTime+setArrowTimerTime;
	runArrowTimer = true;


	}

	private void arrowCountDownNow(){


	setArrowScaleColor ();
		
		currentTimerTime = endTime - Time.time;

		if(currentTimerTime<0){textTimer.text = "";}else{textTimer.text = (currentTimerTime).ToString("0.0");}

		if(Time.time>endTime){
			runArrowTimer=false;
			numberPositionMovesLeft--;
			moveTeamSelectGizmo();
			if(numberPositionMovesLeft>0){startArrowTimer(arrowChoosingTime);}	else{}} //set team // if dont move set bool inactive for their ship

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

		if (totVoltThisPeriodRight < -totVoltTriggerMove) {

			if(gizmoPosition.x> gizmoSittingPositions[0].localPosition.x ){
			int indexForMostRightPositionLeftOfMe = -1;
			float currentSmallestPositionalDifference = 10000000f; 
			for(int i = 0; i<3; i++){
					if(gizmoSittingPositions[i].localPosition.x  < gizmoPosition.x && Mathf.Abs(gizmoPosition.x -gizmoSittingPositions[i].localPosition.x )<currentSmallestPositionalDifference){
						currentSmallestPositionalDifference = gizmoPosition.x -gizmoSittingPositions[i].localPosition.x;
						indexForMostRightPositionLeftOfMe = i;}}
				gizmoPosition = new Vector3 (gizmoSittingPositions[indexForMostRightPositionLeftOfMe].localPosition.x, gizmoPosition.y, gizmoPosition.z); }
			
		}
		else
			if (totVoltThisPeriodRight > totVoltTriggerMove) {
				if(gizmoPosition.x< gizmoSittingPositions[3].localPosition.x ){
					int indexForMostLeftPositionRightOfMe = -1;
					float currentSmallestPositionalDifference = 10000000f; 
							for(int i = 0; i<4; i++){
								if(gizmoSittingPositions[i].localPosition.x  > gizmoPosition.x && Mathf.Abs(gizmoPosition.x -gizmoSittingPositions[i].localPosition.x )<currentSmallestPositionalDifference){
									currentSmallestPositionalDifference = gizmoPosition.x -gizmoSittingPositions[i].localPosition.x;
									indexForMostLeftPositionRightOfMe = i;}}
										gizmoPosition = new Vector3 (gizmoSittingPositions[indexForMostLeftPositionRightOfMe].localPosition.x, gizmoPosition.y, gizmoPosition.z); }
			
		}


	}
}



