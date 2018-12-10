using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//remember to find out how to acknowledge the ship asset



//current code says if something left i stop shooting if something entered i start if two things enter and one leaves i stop, so if i count entered vs left wouldnt risk this

//if i didnt use tags as they change but instead the floats sorting layer, then i could count every tag in out and shoot based on sorting layer - floating or sunk
//then i could just count what leaves and what enters and while enters - left is greater than 0 i should shoot
//another option is when something dies it tells everything its colliding with to stop firing but im already saying stop firing when something dead leaves trigger

//really i should count in and out portential targets going through the collider, stop shooting when none in there
//and if worried the counter will get confused just reset by doing a count of whats in collider to check its right now and again - though i dont know how to
//could check foreach current transform on all ships to see if they are in it not sure if this would be slow.

//code doesnt work because does not fire in the trajectory of the local position
//maybe use transform.forward to make sure cannon ball fires in right direction
//or just trajectory = transform right or left depending on a bool for left or right cannon

// cannon ball vector minus the cannon vector would result in the direction vector then could just multiply
//or try using transform point
//or use transform left right and just alter it

//if checked ships alive or dead not by tag but by if they are floating then they would stay called ship
//see if it can count number of times entered and left to solve the shooting issue


//code needs to not fire on friendly
//code needs the cannon balls to by parented to spawn position so dont move with ship
//repeated triggering shouldnt mean fast firing
//so all cannon balls just childed transform of the spawn point at top

//got lots cannons maybe should just fire when pass through and then have cool down so only using on trigger enter
//only if moved identically in step would this result in not firing when should and because of boats rocking ot sure this would happen

public class Cannon : MonoBehaviour {

public GameObject cannonBall;
public bool forceFire = false;
private bool switchWas = false;
public float ShotsPS = 1f;
public float cyclePercWeapons = 0f;
public float powerOfCannonBall = 150f;
public Vector3 trajectoryCannonBallWorldSpace = new Vector3(1000f,2500f,1000f); // need to depend on boat direction so relative to cannon
private Vector3 trajectoryCannonBall = new Vector3(); //make it local
private bool startFiring = false;
private float timeLastFired;
public float numberOfVolleysBeforeCheck = 5f;
private float reloadTime;
private bool alreadyFiring = false;
private float timeStartedInvokeRepeatingFiring =0f;
private  Transform spawnPoint;

//private Vector3 randomnessToTragectory;
//private float rangeRandomFireDelay //maybe not necessary as each cannon will triggered one at a time
	// Use this for initialization
	void Start () {
	spawnPoint = this.transform.root.GetComponentInChildren<spawnpoint>().gameObject.transform;
		reloadTime = 1f/ShotsPS;
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

	trajectoryCannonBall = transform.TransformDirection(trajectoryCannonBallWorldSpace); //if knew how to do a reference think wouldnt need to update
	//ShotsPS = 1f+ 2f*cyclePercWeapons;

	//need to be on enter collider bool on on exit bool off


		if(forceFire == true && (switchWas != forceFire)){
		switchWas = forceFire;
		startFiring = true;

		}

		if(forceFire == false && (switchWas != forceFire)){switchWas=forceFire;startFiring=false;}
		//just for testing

	

		if(startFiring && (alreadyFiring==false) && (  (Time.timeSinceLevelLoad - timeLastFired)>(reloadTime) ) ){
		alreadyFiring = true;
		timeStartedInvokeRepeatingFiring = Time.timeSinceLevelLoad;
		InvokeRepeating("Firing" , 0f  , (reloadTime)); }else if (startFiring==false){alreadyFiring=false; CancelInvoke();  }



		if(  (alreadyFiring == true) && Time.timeSinceLevelLoad > ( (numberOfVolleysBeforeCheck +0.5f)*reloadTime + timeStartedInvokeRepeatingFiring  )){
		checkStillShootingATarget();}
		 // so been firing this long check there is something to fire at still

	}
	private void checkStillShootingATarget(){


	bool aTargetIsInCollider = false;

	//no idea how to check TODO

	//CapsuleCollider myCapsColl = GetComponent<CapsuleCollider>();
	//myCapsColl.

		
		alreadyFiring=false;
		startFiring = false; 
		CancelInvoke();

	}


	private void Firing(){

			//cannonBall = GetComponent<GameObject>();
		//cannon position transform
		Transform cannonPosition = GetComponentInParent<Transform>().gameObject.transform;
		GameObject cannonBallFired = Instantiate(cannonBall,cannonPosition.position,Quaternion.identity, spawnPoint);

	//	cannonBallFired.transform.parent = gameObject.transform.parent; // works but unneccesary

	//not necessary at moment but for if want to make balls do more damage if cycling to use weapons not movement
	//may want to change size colour and range too
	cannonBallFired.GetComponent<CannonBall>().baseDamage = cannonBallFired.GetComponent<CannonBall>().baseDamage*System.Convert.ToInt32((1+cyclePercWeapons));



	cannonBallFired.GetComponent<Rigidbody>().AddForce(trajectoryCannonBall*powerOfCannonBall, ForceMode.Impulse); //only fires in fixed world direction not local direction
	timeLastFired = Time.timeSinceLevelLoad;
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
	//if(transform.root == coll.transform.root){return;} //is it my collider
	//	if(transform == coll.transform){return;} //they share the spawn parent now and health is on collider so changed this
	//Debug.Log(transform.root + " " + coll.transform.root);



	startFiring = true; 

			 //can it take a negative number


	}
	private void OnTriggerExit(Collider coll){
	//	Debug.Log(name + "out of range of " + coll.name);


		//Debug.Log(name + "in range of " + coll.name);
		//messy because do these lines in both cannon and cannon ball really should make a shared method
	//if change one remember to change both


		if( (coll.tag == "Ship") || (coll.tag == "Destroyed")  )  //so this will turn it off for destroyed ships even if they werent counted on the way in but will do for now
   //is it a ship 

	//if(coll.gameObject.GetComponent<Health>() == null){return;} //is the collider one for taking damage
	//if(transform.root == coll.transform.root){return;} // are they all the same root now theyre in spawner but now health on collider may work just asking is same game object
//		if(transform == coll.transform){return;}
		    //if it dies or is deleted it never leaves there is no on trigger empty could count no. entering leaving? TODO
		{startFiring = false;}
	}

	//code i could use 

	/*
	GameObject newProjectile = projectilePool.GetPooledObject();
        newProjectile.transform.position = transform.position + transform.up *1f;
        newProjectile.transform.rotation = transform.rotation;
        newProjectile.SetActive(true);
        newProjectile.GetComponent<Rigidbody2D>().AddForce(transform.up * projectileSpeed);
     
        coolDown = Time.time + attackSpeed;
        */

	}

