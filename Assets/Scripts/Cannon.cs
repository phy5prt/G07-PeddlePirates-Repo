using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//remember to find out how to acknowledge the ship asset



public class Cannon : MonoBehaviour {

public GameObject cannonBall;
public bool forceFire = false;
public float ShotsPS = 5f;
public float cyclePercWeapons = 0f;
public float powerOfCannonBall = 150f;
public Vector3 trajectoryCannonBall = new Vector3(0f,0.25f,1f);
private bool isFiring = false;


//private Vector3 randomnessToTragectory;
//private float rangeRandomFireDelay //maybe not necessary as each cannon will triggered one at a time
	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
	//take cycle input and map it 0-1
	//if cycling cannons then collider for range is longer
	//if cycling for cannons fire rate increase

	//cycleForWeaponsRaw -> make 0 to 1 become cycle percent
	//could be they cycle as hard as they can for the other player
	//that is set as 100% so they can go over it
	//OtherTeamsPlayerMaxAverage over thirty seconds = currentPlayerInput/Max = cyclePerecent
	//then have a button so they can change it to and from weapons and velocity
	//do this in another script


	ShotsPS = 1f + 2f*cyclePercWeapons;

	//need to be on enter collider bool on on exit bool off


		if(forceFire && Time.timeSinceLevelLoad%5f==0){Firing();}//just for testing	

	
	}


	void Firing(){

			//cannonBall = GetComponent<GameObject>();
		//cannon position transform
		Transform cannonPosition = GetComponentInParent<Transform>().gameObject.transform;
		GameObject cannonBallFired = Instantiate(cannonBall,cannonPosition.position,Quaternion.identity, gameObject.transform );
	//	cannonBallFired.transform.parent = gameObject.transform.parent; // works but unneccesary

	//not necessary at moment but for if want to make balls do more damage if cycling to use weapons not movement
	//may want to change size colour and range too
	cannonBallFired.GetComponent<CannonBall>().baseDamage = cannonBallFired.GetComponent<CannonBall>().baseDamage*System.Convert.ToInt32((1+cyclePercWeapons));



	cannonBallFired.GetComponent<Rigidbody>().AddForce(trajectoryCannonBall*powerOfCannonBall, ForceMode.Impulse);
	}


	//this code will work for one boat but once there are more will need to code
	//that when any number boats are that you fire and only when no boats are in stop
	//so a bool and when the bool changes then invoke repeating turns on or off
	//dont want invoke repeating called multiple times

	private void OnTriggerEnter(Collider coll){

	//Debug.Log(name + "in range of " + coll.name);
		//messy because do these lines in both cannon and cannon ball really should make a shared method
	//if change one remember to change both
	if(coll.tag != "Ship" ){return;}     //is it a ship
	if(coll.gameObject.GetComponent<Health>() == null){return;} //is the collider one for taking damage
	if(transform.root == coll.transform.root){return;} //is it my collider
	Debug.Log(transform.root + " " + coll.transform.root);
	if(!isFiring){isFiring = true; InvokeRepeating("Firing" , 0f , (1f/ShotsPS));}


	}
	private void OnTriggerExit(Collider coll){
	//	Debug.Log(name + "out of range of " + coll.name);


		//Debug.Log(name + "in range of " + coll.name);
		//messy because do these lines in both cannon and cannon ball really should make a shared method
	//if change one remember to change both


	if(coll.tag != "Ship" ){return;}     //is it a ship
	if(coll.gameObject.GetComponent<Health>() == null){return;} //is the collider one for taking damage
	if(transform.root == coll.transform.root){return;}

		CancelInvoke();      //if it dies or is deleted it never leaves there is no on trigger empty could count no. entering leaving? TODO
		isFiring = false;
	}


	}

