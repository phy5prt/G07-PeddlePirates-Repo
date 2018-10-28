using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	
	//Later replace ints with enum have used int arrays just to ease transistion to enum later
	//enum array of player pairs and each one assigned a ship and instantiated to a place and the ships enum set to = enum of player
	//enum will need to include which inputs to get info from the max speed value set and colour of player and health  at start
	//should make clear in first scene which colour who so know which ship controlling
	//need to carry this colour to the ship sails
	//maybe game manager could create per pair ppl a player enum holding numbers which gives to what instantiates
	//later options to numEnemies with health, ramming behaviour, speed and make an enum

//on scene where set up enemies etc need to remember than cant have more ships than number spawn point so limit it to 21 ships total

//public enum[] enemiesShipSettings;
public int[] enemiesShipSettings;
public GameObject enemyShipToSpawn;

//public enum[] teamPairShipSettings;
public int[] teamPairShipSettings;
public GameObject playerShipToSpawn;
private Transform[] spawnPoints;
private spawnpoint[] SpawnPointsAvialableScripts;




	void Start () { //change to on scene load game scene
		
		// this is a massive hack because get componenent in children includes parents and dont want it to, dont want it to because i intend to disable childred
		//as i use them so i dont spawn two things in same place if i deactivated parent would deactivate all children too
		//however may have to redefine array each time [think this is a ref verse value thing i dont get yet] - need to test if this is the case - it is
		//doing so would be sloppy
		//could child objects to their spawnpoints and then only spawn again if spawn point has no children
		//but is that where i want them in hierachy
		
		//could put on spawn script place taken and check for it
		//just keep going through the random spaces until empty one
		//not that inefficient really - other solve could be an array of bools each array index represent a transform, randomly chose from true ones somehow
		//with current code could remake the array each time so it just of the ones whose script is true
		//a list would be an option too if i new them better an use a foreach or just measure list length and remove items as go
		
		
		GameObject SpawnPointsGO  = GameObject.Find("SpawnPoints");
		SpawnPointsAvialableScripts = SpawnPointsGO.GetComponentsInChildren<spawnpoint>();
		spawnPoints = new Transform[SpawnPointsGO.transform.childCount]; // may not be necessary
		for(int i = 0; i<spawnPoints.Length; i++){spawnPoints[i] = SpawnPointsAvialableScripts[i].gameObject.transform;}
		//foreach(spawnpoint thisSpawnPoint in SpawnPointsChildrensScript){spawnPoint[i]thisSpawnPoint.gameObject.transform;} // should this just be 
		

		spawnPlayersAndEnemies(); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	
	private void spawnPlayersAndEnemies(){
	
	//PlayersShips - get first dibs
	
	if(teamPairShipSettings.Length + enemiesShipSettings.Length>spawnPoints.Length){Debug.LogWarning("more ships to be made than spawnlocations");}

	foreach(int playerPair in teamPairShipSettings){
			Transform locationToInstantiate;
			locationToInstantiate = LocationToinstantiate();
			Instantiate (playerShipToSpawn, locationToInstantiate);
			
			
			
			
	
	}
	
		foreach(int enemyShip in enemiesShipSettings){
			Transform locationToInstantiate;
			locationToInstantiate = LocationToinstantiate();
			Instantiate (enemyShipToSpawn, locationToInstantiate);
			
			//change the above to this when intergrate enum
			//GameObject Ship = Instantiate (enemyShipToSpawn, locationToInstantiate);
			//Ship.GetComponentInChildren<Health>().startHealth = //enum start health setting
			//Ship.GetComponentInChildren<>(). Some Script maybe player set their public variable by enum = //enum colour, max speed, ram setting, health drop setting etc
			
	}
	}
	
	private Transform LocationToinstantiate(){
	Transform locationToInstantiate = this.transform;     //code makes me instantiate the transform with location because doesnt realize should never go through while loop
	//without creating one so ive just given it the gameObject transform for no other reason than its there // will check it doesnt get through with a debuglog
	
	bool foundAvailableSpawn = false;
		while(foundAvailableSpawn == false){
			int random = Random.Range(0,SpawnPointsAvialableScripts.Length-1);
			if(SpawnPointsAvialableScripts[random].availableSpawnPoint == true){
				locationToInstantiate = SpawnPointsAvialableScripts[random].gameObject.transform; // note the random number isnt being recalculated so it is same spawnscript as line above
				SpawnPointsAvialableScripts[random].availableSpawnPoint = false; // make spawn point unavailable to others
				foundAvailableSpawn = true;}else{Debug.Log ("going to look again because that spawn locatin wasnt available");}
		}

	if(locationToInstantiate == this.transform){Debug.Log("either the  while statement did not asisng spawn location or one spawn location is the same as gamemanage location");}
	return locationToInstantiate;
	}
}
