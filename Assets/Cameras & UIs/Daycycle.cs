using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Daycycle : MonoBehaviour {

//[Tooltip ("Number of minutes per second that pass, try 60")]
//public float timeScale = 60f;
private float minutesPerDay = GameManager.dayLength/60;
private Quaternion startRotation;


	// Use this for initialization
	void Start () {
		startRotation = transform.rotation;

	}
	
	// Update is called once per frame
	void Update () {

	//float angleTheFrame = Time.deltaTime/360*timeScale; // their code

	//float angleTheFrame = Time.time/(timeScale/120f); //fudge factor of 120f to get 3 sunsets in a 3 min games

	//if we want to set how many cycles a minute which seems more intuitable then
	//so number of second times 6 so 60 seconds equals 360 and then divde by how many minutes you want a cycle to take, this is your angle, if it goes over 360 thats fine
	//just need to ensure every time it translate from the start position so every time just reassigning the x value through a method or reseting the transform before re translating

	float angleTheFrame = Time.timeSinceLevelLoad*6f/minutesPerDay;//yeah this works great time scale should be renamed minutesPerDay it was now changed again
	transform.rotation = startRotation;
	transform.RotateAround(transform.position, Vector3.forward, angleTheFrame);
	//transform.rotation = startRotation;
	//transform.RotateAround(transform.position, Vector3.forward, angleTheFrame);

	/*if((Math.Round(Time.time, 0))%timeScale == 0){
	//if(Time.time%timeScale == 0){
			//Debug.Log("time is =" + Time.time + " Rounded is " + (Math.Round(Time.time, 0)) + " so I am incrementing rotation ");
			Debug.Log("time is =" + Time.time + " transform is " + this.gameObject.transform.localRotation.x);

			this.transform.localRotation = Quaternion.Euler(this.transform.localRotation.x+1f,this.transform.localRotation.y, this.transform.localRotation.z);
		Debug.Log("now it is " + this.transform.localRotation.x );
		}
	*/	
	}
}
