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



[SerializeField] float arrowChoosingTime = 5;
[SerializeField] bool runArrowTimer;
private Vector3 startScaleArrow;

public GameObject[] gizmoSittingPositions; //should code this better
public GameObject gizmoRT;

	// Use this for initialization
	void Start () {




	arrowImage = GetComponentInChildren<Image>();
	arrowImage.color = Color.red;//should get first
	startScaleArrow =	arrowImage.gameObject.GetComponent<RectTransform>().localScale;
	textTimer = GetComponentInChildren<Text>();

	}
	
	// Update is called once per frame
	void Update () {



	if(runArrowTimer){arrowCountDownNow();}
			
	}






	public void startArrowTimer(){
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
			if(numberPositionMovesLeft>0){startArrowTimer();}	else{}} //set team // if dont move set bool inactive for their ship

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

			if(gizmoRT.transform.position.x> gizmoSittingPositions[0].transform.position.x ){
			int indexForMostRightPositionLeftOfMe = -1;
			float currentSmallestPositionalDifference = 10000000f; 
			for(int i = 0; i<3; i++){
					if(gizmoSittingPositions[i].transform.position.x < gizmoRT.transform.position.x && Mathf.Abs(gizmoRT.transform.position.x-gizmoSittingPositions[i].transform.position.x )<currentSmallestPositionalDifference){
						currentSmallestPositionalDifference = Mathf.Abs(gizmoRT.transform.position.x -gizmoSittingPositions[i].transform.position.x);
						indexForMostRightPositionLeftOfMe = i;}}
				gizmoRT.transform.position = new Vector3 (gizmoSittingPositions[indexForMostRightPositionLeftOfMe].transform.position.x, gizmoRT.transform.position.y, gizmoRT.transform.position.z); }
			
		}
		else
			if (totVoltThisPeriodRight > totVoltTriggerMove) {
		
				
				if(gizmoRT.transform.position.x< gizmoSittingPositions[3].transform.position.x ){

					int indexForMostLeftPositionRightOfMe = -1;
					float currentSmallestPositionalDifference = 10000000f; 
							for(int i = 0; i<4; i++){
						
						if(gizmoSittingPositions[i].transform.position.x  > gizmoRT.transform.position.x && Mathf.Abs(gizmoRT.transform.position.x -gizmoSittingPositions[i].transform.position.x )<currentSmallestPositionalDifference){
							currentSmallestPositionalDifference = Mathf.Abs(gizmoRT.transform.position.x -gizmoSittingPositions[i].transform.position.x);
									indexForMostLeftPositionRightOfMe = i;}}
					Debug.Log("indexForMostLeftPositionRightOfMe is " + indexForMostLeftPositionRightOfMe + " gizmoSittingPositions[indexForMostLeftPositionRightOfMe].localPosition.x is " + gizmoSittingPositions[indexForMostLeftPositionRightOfMe].transform.position.x + " mine currently is " + gizmoRT.transform.position.x + " next line is change position ");
					gizmoRT.transform.position = new Vector3 (gizmoSittingPositions[indexForMostLeftPositionRightOfMe].transform.position.x, gizmoRT.transform.position.y, gizmoRT.transform.position.z); }
			
		}


	}
}



