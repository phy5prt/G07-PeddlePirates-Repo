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
[SerializeField] int numberPositionMovesLeft = 6; //there and back //only runs once so can alter directly



[SerializeField] float endTime;

[SerializeField] float time = Time.time; //not need just to help me calibrate

[SerializeField] float arrowChoosingTime = 5;
[SerializeField] bool runArrowTimer;
private Vector3 startScaleArrow;

public RectTransform[] gizmoSittingPositions; //should code this better
public RectTransform gizmoRT;

	// Use this for initialization
	void Start () {




	arrowImage = GetComponentInChildren<Image>();
	arrowImage.color = Color.red;//should get first
	startScaleArrow =	arrowImage.gameObject.GetComponent<RectTransform>().localScale;
	textTimer = GetComponentInChildren<Text>();

	}
	
	// Update is called once per frame
	void Update () {
	time = Time.time;


	if(runArrowTimer){arrowCountDownNow();}
			
	}






	public void startArrowTimer(float arrowChoosingTime){
		totVoltThisPeriodRight = 0;
		startTime = Time.time;
		endTime = startTime+arrowChoosingTime;
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

	//code looks a bit funny because x actually decrease left to right in the hierachy object dont know why


	Debug.Log("run move teams select gizmo");

		if (totVoltThisPeriodRight < -totVoltTriggerMove) {

			if(Mathf.Abs(gizmoRT.localPosition.x)> Mathf.Abs(gizmoSittingPositions[0].localPosition.x) ){
			int indexForMostRightPositionLeftOfMe = -1;
			float currentSmallestPositionalDifference = 10000000f; 
			for(int i = 0; i<3; i++){
					if(Mathf.Abs(gizmoSittingPositions[i].localPosition.x) <  Mathf.Abs(gizmoRT.localPosition.x) && Mathf.Abs(gizmoRT.localPosition.x-gizmoSittingPositions[i].localPosition.x )<currentSmallestPositionalDifference){
						currentSmallestPositionalDifference = Mathf.Abs(gizmoRT.localPosition.x -gizmoSittingPositions[i].localPosition.x);
						indexForMostRightPositionLeftOfMe = i;}}
				gizmoRT.localPosition = new Vector3 (gizmoSittingPositions[indexForMostRightPositionLeftOfMe].localPosition.x, gizmoRT.localPosition.y, gizmoRT.localPosition.z); }
			
		}
		else
			if (totVoltThisPeriodRight > totVoltTriggerMove) {
			Debug.Log("should move right");
				if(Mathf.Abs(gizmoRT.localPosition.x)< Mathf.Abs(gizmoSittingPositions[3].localPosition.x )){
					Debug.Log("Im not in the most right position");
					int indexForMostLeftPositionRightOfMe = -1;
					float currentSmallestPositionalDifference = 10000000f; 
							for(int i = 0; i<4; i++){
						Debug.Log("i is " +i+ " this if statement is " + (Mathf.Abs(gizmoSittingPositions[i].localPosition.x)  > Mathf.Abs(gizmoRT.localPosition.x) && Mathf.Abs(gizmoRT.localPosition.x -gizmoSittingPositions[i].localPosition.x )<currentSmallestPositionalDifference) );
						if(Mathf.Abs(gizmoSittingPositions[i].localPosition.x)  > Mathf.Abs(gizmoRT.localPosition.x) && Mathf.Abs(gizmoRT.localPosition.x -gizmoSittingPositions[i].localPosition.x )<currentSmallestPositionalDifference){
							currentSmallestPositionalDifference = Mathf.Abs(gizmoRT.localPosition.x -gizmoSittingPositions[i].localPosition.x);
									indexForMostLeftPositionRightOfMe = i;}}
					Debug.Log("indexForMostLeftPositionRightOfMe is " + indexForMostLeftPositionRightOfMe + " gizmoSittingPositions[indexForMostLeftPositionRightOfMe].localPosition.x is " + gizmoSittingPositions[indexForMostLeftPositionRightOfMe].localPosition.x + " mine currently is " + gizmoRT.localPosition.x + " next line is change position ");
					gizmoRT.localPosition = new Vector3 (gizmoSittingPositions[indexForMostLeftPositionRightOfMe].localPosition.x, gizmoRT.localPosition.y, gizmoRT.localPosition.z); }
			
		}


	}
}



