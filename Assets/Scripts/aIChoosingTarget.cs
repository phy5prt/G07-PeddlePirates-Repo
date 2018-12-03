using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

//need to restructure the enemy ship so has collisions
//so labeled
//so health on top level

//ai now finds enemies and fires, ive fixed the cannons and some are still shooting funny and health not decromented but may be due to collider.root now they 
//share a parent. they dance a bit could be caused by colliders or nav mesh or targetting, so will need to start with targetting (select on the navigator and it 
//shows you the target), if targetting happening because of find targets code may just need to put bigger delay, priority for most recent target - could also be ai's
// syncing with each other causing reciprical movements as try and get to optimum distance from each other.  - either way worth testing if can find new targets without 
//the invoke repeat and if can make it into a singular script

//check ai still updates target location on its own when fed target rather than gameobject being dropped in its public inspector box


//how do i want ai to behave, chase a while, then attack the closer, attack the one coming to it, attack any in 2x firing range other wise stick to their target for x time
//???

//need put in way to differentiate if human or computer player in archetecture just check for navigator!

public class aIChoosingTarget : MonoBehaviour {


//when this works can go straight into the AI of the script

	//wont work till health is on the top level
	//wont work till network method spawns human into spawn slot

//will look through the spawnpoints and those with children will ask if it is player or ai and will determine which closest and follow them for a certain time before asking again
//it needs a trigger mechanism too where if it sinks something or what its looking at sinks // it finds new enemy - or let timer do it?
private AICharacterControl aI;
private GameObject SpawnPointsGO; 
public bool justHumans = true;

	void Start () {

		aI = gameObject.GetComponent<AICharacterControl>();

		SpawnPointsGO = GameObject.Find("SpawnPoints");	


		InvokeRepeating("findNearestTarget",0f,5f);		//dont know how would specify if humans with invoke repeating
	}
	
	// Update is called once per frame
	void Update () {


		
	}

	//public Transform findNearestTarget(bool justHumans = true){ //should i set justhumans in the class as public bool so if accessed by another script and changes it it changes the invoke repeating?

	public void findNearestTarget(){

	Transform attackThis = this.transform; //just needs inialising if it does attack self its gone wrong or no enemies

		Health[] targets = SpawnPointsGO.GetComponentsInChildren<Health>();
		float distanceClosestTarget = 100000f;

		foreach(Health potentialTarget in targets){
		//code distance between target and this target

		if(potentialTarget.gameObject != this.gameObject){   //check not targetting self
			bool isHuman; 
			if(potentialTarget.GetComponent<ThirdPersonCharacter>() == null){isHuman=true;}else{isHuman=false;} // this aint working

			if((justHumans == false) || (justHumans == isHuman)){

					if(potentialTarget.tag == "Ship"){ //because if not human and not just humans that ok too this checks if targetting ai
					float distanceAwayThisTarget;
					distanceAwayThisTarget = Vector3.Distance(potentialTarget.transform.position,this.transform.position);
			//calculate distance
					if(distanceAwayThisTarget<distanceClosestTarget){                    //if here added a fudge fact so need to be distanceAwayThisTarget+10<distanceclosestTarget may stop the potential issue of getting stuck between two targets or changing to soon?!
						distanceClosestTarget = distanceAwayThisTarget; 
						attackThis = potentialTarget.gameObject.transform;}
		}}}}

		//could have this check how far away the current target is of the ai from that script fed into method and then unless significant difference or that target dead it
		//wont change the target so that the ships are more committed or at least will chase for some time and hard to lose.


										if(attackThis == this.transform){
												Debug.Log("it has not found a target either because theyre all gone or something went wrong");
												Debug.DebugBreak();
												attackThis = null; } //maybe should return null as dont want it to target self will get confusd


												aI.SetTarget(attackThis);       //changed because wanted to invoke but when put this script in thirdperson secript wont have to
	//return attackThis;

	} 




}
