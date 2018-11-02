using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class redCountDownTimer : MonoBehaviour {

private Text textTimer;
private float currentTimerTime;
private float startTime;
[SerializeField] Image timerImage;
private bool runStageTimer;
[SerializeField] bool delMethodtrigger = false;
[SerializeField] float	setStageTimerTime = 10f;

[SerializeField] float endTime;

//not need just to help me calibrate




	// Use this for initialization
	void Start () {

	timerImage = GetComponent<Image>();
	textTimer = GetComponentInChildren<Text>();

	}
	
	// Update is called once per frame
	void Update () {

		if(delMethodtrigger){delMethodtrigger=false; startStageTimer();} //delete this when ready to trigger through code rather than inspector

	if(runStageTimer){stageCountDownNow();}

			
	}



	public void startStageTimer(){
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




}
