using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stageCountDownTimer : MonoBehaviour {

private Text textStageTimer;
[SerializeField] float currentTimerTime;
[SerializeField] float startTime;
[SerializeField] Image timerImage;
[SerializeField] bool runTimer;

[SerializeField] float endTime;

[SerializeField] float time = Time.time; //not need just to help me calibrate
	// Use this for initialization
	void Start () {

	timerImage = GetComponent<Image>();
	textStageTimer = GetComponentInChildren<Text>();

	}
	
	// Update is called once per frame
	void Update () {
	time = Time.time;
	if(runTimer){stageCountDownNow();}
		
	}

	public void startStageTimer(float setStageTimerTime){
		startTime = Time.time;
		endTime = startTime+setStageTimerTime;
	runTimer = true;


	}

	private void stageCountDownNow(){

		timerImage.fillAmount = 1- (Time.time-startTime)/(endTime - startTime);
		currentTimerTime = endTime - Time.time;
		if(currentTimerTime<0){textStageTimer.text = "";}else{textStageTimer.text = (currentTimerTime).ToString("0.00");}
		if(Time.time>endTime){runTimer=false;}
	}
}
