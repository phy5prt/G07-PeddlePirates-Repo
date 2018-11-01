using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class arrowTeamSelector : MonoBehaviour {

[SerializeField] float totVoltTriggerBool = 100;
[SerializeField] float totVoltThisPeriodRight = 0;

public float voltInLeft;  //this will become set to the arduino input from the red port left e.g.
public float voltInRight; //

private Text textTimer;
[SerializeField] float currentTimerTime;
[SerializeField] float startTime;
private Image arrowImage;
[SerializeField] int numberPositionMovesLeft = 6; //there and back

[SerializeField] bool runStageTimer;

[SerializeField] float endTime;

[SerializeField] float time = Time.time; //not need just to help me calibrate

[SerializeField] float arrowChoosingTime = 5;
[SerializeField] bool runArrowTimer;
private Vector3 startScale;

public Transform[] gizmoSittingPositions; //should code this better

	// Use this for initialization
	void Start () {




	arrowImage = GetComponentInChildren<Image>();
	arrowImage.color = Color.red;//should get first
	startScale =	arrowImage.gameObject.GetComponent<RectTransform>().localScale;
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
			moveArrow();
			if(numberPositionMovesLeft>0){startArrowTimer(arrowChoosingTime);}	else{}} //set team // if dont move set bool inactive for their ship

	}


	void setArrowScaleColor ()
	{
		totVoltThisPeriodRight += voltInRight - voltInLeft;
		if (totVoltThisPeriodRight < -totVoltTriggerBool) {
			arrowImage.color = Color.green;
			arrowImage.gameObject.GetComponent<RectTransform> ().localScale = new Vector3 (-1 * startScale.x, startScale.y, startScale.z);
		}
		else
			if (totVoltThisPeriodRight > totVoltTriggerBool) {
				arrowImage.color = Color.green;
				arrowImage.gameObject.GetComponent<RectTransform> ().localScale = startScale;
			}
			else {
				arrowImage.gameObject.GetComponent<RectTransform> ().localScale = new Vector3 ((startScale.x * totVoltThisPeriodRight / totVoltTriggerBool), startScale.y, startScale.z);
			}
	}

	private void moveArrow(){

		if (totVoltThisPeriodRight < -totVoltTriggerBool) {
			
		}
		else
			if (totVoltThisPeriodRight > totVoltTriggerBool) {


	}
}}



