using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// I can't run this in start // how do i initialise it // if no instance redPShip like when I had it in thisPlayerPairSettings how can i store it
//so i need it to persist
//but can still make everything static so easy to access as this just means instances all share the value

public class GameManager : MonoBehaviour {

//does gameManager need to be on an object or if use statics methods and variables can it be independant

	//gameManager is going to holdsettings info
	//gameManager is going to handle arduino inputs
	//gameManager is going to initate and end game stages,

    //gameManager not going to spawn but instead instruct the spawner instead playersettings doing it directly
    //gameManager willset the game time before draw

	//need to carry this colour to the ship sails
	//maybe game manager could create per pair ppl a player enum holding numbers which gives to what instantiates
	//later options to numEnemies with health, ramming behaviour, speed and make an enum

//on scene where set up enemies etc need to remember than cant have more ships than number spawn point so limit it to 21 ships total

//public enum[] enemiesShipSettings;

//public static float timeForPlayerResponse = 4f;

//currently most this code could be on the spawner
//should game manager just hold setting info

	


//spawning settings
public static int numberAIs;
public static int[] enemiesShipSettings; //this is only an int array because later will be a struct array of settings for 
public GameObject enemyShipToSpawn;

public static int numberDays = 3;
public static int dayLength = 120; 
public static int gameLength {get{return numberDays*dayLength;}} //does th 
	
//public enum[] teamPairShipSettings;

	//will become an array of thisPlayerPairSettings[] // and instantiate

public int[] teamPairShipSettings;

public GameObject playerShipToSpawn;
private Transform[] spawnPoints;
private spawnpoint[] SpawnPointsAvialableScripts;

//inputs
public static thisPlayerPairSettings redPShip;
public static thisPlayerPairSettings yelPShip;
public static thisPlayerPairSettings grePShip;
public static thisPlayerPairSettings bluPShip;

public static thisPlayerPairSettings[]  shipPlayerSettingsAr =  {redPShip,yelPShip,grePShip,bluPShip};

private void Start(){
		settingUpPlayerSettingAr ();

}


private void settingUpPlayerSettingAr ()  //dont think i can run without an instance as without an instance dont think i can store values
	{
	 //code seems to need this block but not sure why as can use the dot to find what looking for
		redPShip = new thisPlayerPairSettings {};
		yelPShip = new thisPlayerPairSettings {};
		grePShip = new thisPlayerPairSettings {};
		bluPShip = new thisPlayerPairSettings {};

	
		redPShip.SetShipPairName(" MondleBrot's Wives ");
		yelPShip.SetShipPairName(" Brownian's Movement ");
		grePShip.SetShipPairName(" Marie's Glow ");
		bluPShip.SetShipPairName(" Dabloon 's Good Booty! ");

		redPShip.setShipPairColor("RED");
		yelPShip.setShipPairColor("YELLOW");
		grePShip.setShipPairColor("GREEN");
		bluPShip.setShipPairColor("BLUE");


	}











public void startGameScene(){

SceneManager.LoadScene(2);

}



	public void prepareToSpawn () { //change to on scene load game scene
		
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

		//part enemy enum is if they target other enemies


		//needs taking out of start and changing to on scene load and specify scene
	
		GameObject SpawnPointsGO  = GameObject.Find("SpawnPoints");
		SpawnPointsAvialableScripts = SpawnPointsGO.GetComponentsInChildren<spawnpoint>();
		spawnPoints = new Transform[SpawnPointsGO.transform.childCount]; // may not be necessary
		for(int i = 0; i<spawnPoints.Length; i++){spawnPoints[i] = SpawnPointsAvialableScripts[i].gameObject.transform;}

		//foreach(spawnpoint thisSpawnPoint in SpawnPointsChildrensScript){spawnPoint[i]thisSpawnPoint.gameObject.transform;} // should this just be 
		





	}
	


	
	public void spawnPlayersAndEnemies(){
	
	//PlayersShips - get first dibs
		enemiesShipSettings = new int[numberAIs];//this will be changed when passing more info but for now


		if(teamPairShipSettings.Length + enemiesShipSettings.Length>spawnPoints.Length){Debug.Log("more ships to be made than spawnlocations");Debug.DebugBreak();}

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

	//maybe an if here to check there are someavailable
	//could do 
	//	for (int i = 0; i< SpawnPointsAvailableScripts.Length; i++){if(SpawnPointsAvialableScripts[i].availableSpawnPoint == true){SPscriptIsCurrentlyAvailable[] = SpawnPointsAvialableScripts[i].availableSpawnPoint}}
		// // but using a list as wont know how many till done it. then just chose the random location from the tures and if the length of availableSpawnPoints is less than one the dont run

	
		while(foundAvailableSpawn == false){
			int random = Random.Range(0,SpawnPointsAvialableScripts.Length-1);
			if(SpawnPointsAvialableScripts[random].availableSpawnPoint == true){
				locationToInstantiate = SpawnPointsAvialableScripts[random].gameObject.transform; // note the random number isnt being recalculated so it is same spawnscript as line above
				SpawnPointsAvialableScripts[random].availableSpawnPoint = false; // make spawn point unavailable to others
				foundAvailableSpawn = true;}
		}

	
	return locationToInstantiate;
	}
}
