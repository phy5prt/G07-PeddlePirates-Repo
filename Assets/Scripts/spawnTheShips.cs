using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the spawner should hold the "blank" of the ships but GameManager should hold the specifics for the game e.g. later the gamemanager will say make 3 of your blank ais, but make
//1 like this that rams and two like this with big health

public class spawnTheShips : MonoBehaviour {

	//public enum[] teamPairShipSettings;

	//will become an array of thisPlayerPairSettings[] // and instantiate



public GameObject playerShipToSpawn;
public GameObject enemyShipToSpawn;


private Transform[] spawnPoints;
private spawnpoint[] SpawnPointsAvialableScripts;

	// Use this for initialization
	void Start () {


		SpawnPointsAvialableScripts = GetComponentsInChildren<spawnpoint>();
		spawnPoints = new Transform[transform.childCount]; // may not be necessary
		for(int i = 0; i<spawnPoints.Length; i++){spawnPoints[i] = SpawnPointsAvialableScripts[i].gameObject.transform;} 
		//the number of points in showslidervalue

		//foreach(spawnpoint thisSpawnPoint in SpawnPointsChildrensScript){spawnPoint[i]thisSpawnPoint.gameObject.transform;} // should this just be 
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	



	public void spawnPlayersAndEnemies(){

	//PlayersShips - get first dibs


		GameManager.enemiesShipSettings = new int[GameManager.numberAIs];//this will be changed when passing more info but for now
		Debug.Log(" making enemy ship int array its length is " + GameManager.enemiesShipSettings.Length);
		int noPlayerPairs=0;
		foreach(thisPlayerPairSettings playerPair in GameManager.shipPlayerSettingsAr){if(playerPair.getWerePlaying()){noPlayerPairs++;}}
		Debug.Log(" this is how many player are playing according to spawntheships " + noPlayerPairs);


		foreach(thisPlayerPairSettings playerPair in GameManager.shipPlayerSettingsAr){
				if(playerPair.getWerePlaying()){
					Transform locationToInstantiate;
					locationToInstantiate = LocationToinstantiate();

					//now should player ship have a struct required when its being instantiated
					//could the code actually have its object as part of it and make a version of the script and it creates own ship
					GameObject playerShip = Instantiate (playerShipToSpawn, locationToInstantiate);
					playerShip.GetComponent<MyPlayer>().applyPlayerSettingsToShip(playerPair);
			} }
			
			
			
	

	Debug.Log("instantiating enemies");
		foreach(int enemyShip in GameManager.enemiesShipSettings){
			Transform locationToInstantiate;
			locationToInstantiate = LocationToinstantiate();
			Instantiate (enemyShipToSpawn, locationToInstantiate);
			

			//GameObject Ship = Instantiate (enemyShipToSpawn, locationToInstantiate);
			//Ship.GetComponentInChildren<Health>().startHealth = //enum start health setting
			//Ship.GetComponentInChildren<>(). Some Script maybe player set their public variable by enum = //enum colour, max speed, ram setting, health drop setting etc
			
	}

	Debug.Log("finished spawning");

	}
	
	private Transform LocationToinstantiate(){
	Transform locationToInstantiate = this.transform;     //code makes me instantiate the transform with location because doesnt realize should never go through while loop
	//without creating one so ive just given it the gameObject transform for no other reason than its there // will check it doesnt get through with a debuglog
	
	bool foundAvailableSpawn = false;

	//maybe an if here to check there are someavailable
	//could do 
	//	for (int i = 0; i< SpawnPointsAvailableScripts.Length; i++){if(SpawnPointsAvialableScripts[i].availableSpawnPoint == true){SPscriptIsCurrentlyAvailable[] = SpawnPointsAvialableScripts[i].availableSpawnPoint}}
		// // but using a list as wont know how many till done it. then just chose the random location from the tures and if the length of availableSpawnPoints is less than one the dont run

		Debug.Log("SpawnPointsAvialableScripts.Length " + SpawnPointsAvialableScripts.Length );
		while(foundAvailableSpawn == false){     
			int random = Random.Range(0,SpawnPointsAvialableScripts.Length); //if random range is exclusive this may be the error! (im removing the -1) Yep that was it
			if(SpawnPointsAvialableScripts[random].availableSpawnPoint == true){
				locationToInstantiate = SpawnPointsAvialableScripts[random].gameObject.transform; // note the random number isnt being recalculated so it is same spawnscript as line above
				SpawnPointsAvialableScripts[random].availableSpawnPoint = false; // make spawn point unavailable to others
				foundAvailableSpawn = true;}

		//when refactor make it check there is any available spawn locations and break if there isnt as crash protection incase number spawns change, as AIs spawned second this will be fine no players will be excluded
		}

	
	return locationToInstantiate;
	}
}
