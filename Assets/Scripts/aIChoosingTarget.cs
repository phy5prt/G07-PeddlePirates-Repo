using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

//need to restructure the enemy ship so has collisions
//so labeled
//so health on top level

//check ai still updates target location on its own when fed target rather than gameobject being dropped in its public inspector box


//how do i want ai to behave, chase a while, then attack the closer, attack the one coming to it, attack any in 2x firing range other wise stick to their target for x time
//???



public class aIChoosingTarget : MonoBehaviour {


//when this works can go straight into the AI of the script

	//wont work till health is on the top level
	//wont work till network method spawns human into spawn slot

//will look through the spawnpoints and those with children will ask if it is player or ai and will determine which closest and follow them for a certain time before asking again
//it needs a trigger mechanism too where if it sinks something or what its looking at sinks // it finds new enemy - or let timer do it?
private AICharacterControl aI;
private GameObject SpawnPointsGO; 

	void Start () {

		aI = gameObject.GetComponent<AICharacterControl>();

		SpawnPointsGO = GameObject.Find("SpawnPoints");	
				
	}
	
	// Update is called once per frame
	void Update () {

	//code run method every so long
		
	}

	public Transform findNearestTarget(bool justHumans = true){

	Transform attackThis; //may have to initialise not sure what would be sensible

		Health[] targets = SpawnPointsGO.GetComponentsInChildren<Health>();
		int distanceClosestTarget = 100000;
		foreach(Health potentialTarget in targets){
		//code distance between target and this target
		if(potentialTarget.gameObject != this.gameObject){
			int distanceAwayThisTarget;
			//calculate distance
			if(distanceAwayThisTarget<distanceClosestTarget){                    //if here added a fudge fact so need to be distanceAwayThisTarget+10<distanceclosestTarget may stop the potential issue of getting stuck between two targets or changing to soon?!
				distanceClosestTarget = distanceAwayThisTarget; 
				attackThis = potentialTarget.gameObject.transform;}
		}

		//could have this check how far away the current target is of the ai from that script fed into method and then unless significant difference or that target dead it
		//wont change the target so that the ships are more committed or at least will chase for some time and hard to lose.

	return attackThis;

	} 




}
