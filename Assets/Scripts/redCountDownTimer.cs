using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class redCountDownTimer : MonoBehaviour {

private Text textTimer;
[SerializeField] float currentTimerTime;
[SerializeField] float startTime;
[SerializeField] Image timerImage;
[SerializeField] bool runStageTimer;

[SerializeField] float endTime;

[SerializeField] float time = Time.time; //not need just to help me calibrate


[SerializeField] bool runArrowTimer;

	// Use this for initialization
	void Start () {

	timerImage = GetComponent<Image>();
	textTimer = GetComponentInChildren<Text>();

	}
	
	// Update is called once per frame
	void Update () {
	time = Time.time;

	if(runStageTimer){stageCountDownNow();}
	if(runArrowTimer){arrowCountDownNow();}
			
	}

	public void startStageTimer(float setStageTimerTime){
		startTime = Time.time;
		endTime = startTime+setStageTimerTime;
	runStageTimer = true;


	}

	private void stageCountDownNow(){

		timerImage.fillAmount = 1- (Time.time-startTime)/(endTime - startTime);
		currentTimerTime = endTime - Time.time;
		if(currentTimerTime<0){textTimer.text = "";}else{textTimer.text = (currentTimerTime).ToString("0.00");}
		if(Time.time>endTime){runStageTimer=false;}
	}



	public void startArrowTimer(float setArrowTimerTime){
		startTime = Time.time;
		endTime = startTime+setArrowTimerTime;
	runArrowTimer = true;


	}

	private void arrowCountDownNow(){


		currentTimerTime = endTime - Time.time;
		if(currentTimerTime<0){textTimer.text = "";}else{textTimer.text = (currentTimerTime).ToString("0.0");}
		if(Time.time>endTime){runArrowTimer=false;}
	}
}
