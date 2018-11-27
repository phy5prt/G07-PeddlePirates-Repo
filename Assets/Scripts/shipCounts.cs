using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

//later have set of conditions for game type so can have just ai play each other in background etc
//any cycling instantly finishes battle takes to setup screen

//could be nice to count teams at start and teams at end so can say which teams lost
//may be able to do this by using were playing if were playing but dead then they were defeated

public class shipCounts : MonoBehaviour {

public int ShipsCount;

public int ShipsDestroyed;
public int ShipsActive;

public int aIShips;
public int aIShipsSunk;
public int aIShipsActive;

public int playerShips;
public int playerShipsSunk;
public int playerShipsActive;

//there is no zero team but could make that team the ai's in future if making them work together!
public int[] shipsActivePerTeamCount = {9,9,9,9,9};//just to initialise
public int numberOfTeamsActive = 9;
public bool[] teamActive = {true,true,true,true,true};

//active teams
public bool aiRogueShipsActive = true;
public bool playerShipsActiveBool = true;



public GameObject[] ShipsGOs;

//public GameObject[] aIShips;
//public GameObject[] aIShipsSunk;
//public GameObject[] aIShipsActive;

//public GameObject[] playerShips;
//public GameObject[] playerShipsSunk;
//public GameObject[] playerShipsActive;


private Health[] ShipsHealths;

//could use broadcastmessage and just tell spawner when to decrement its count from the health sink script
//because search by tag this could be done anywhere no advantage to running it here
//could write a method that looks for the tags looking at each child to be more efficient
 



	// Use this for initialization
	void Start () { //this starts before the ships out is it supposed to be disabled and enabled on gamestart

		InvokeRepeating("UpdateShipCounts",30f,3f);
		
	}
	
	// Update is called once per frame
	void Update () {

	//search by rigidbody because not deactivated and some ppl say although get component gets deactivated componetnet get componentin children does not
		
	}
	public void UpdateShipCounts(){

	ShipsHealths = GetComponentsInChildren<Health>(); 

	ShipsCount = ShipsHealths.Length;

	ShipsGOs = new GameObject[ShipsCount]; 
	for(int i = 0; i<ShipsGOs.Length; i++){ShipsGOs[i] = ShipsHealths[i].gameObject;}


//aIShips
aIShips = 0;
foreach(GameObject Ship in ShipsGOs){if(Ship.GetComponent<AICharacterControl>() != null){aIShips++;}}; //check works when deactivated if not it doesnt need deactivating now anyway

aIShipsSunk = 0;
		foreach(GameObject Ship in ShipsGOs){if(Ship.GetComponent<AICharacterControl>() != null && Ship.tag == "Destroyed" ){aIShipsSunk++;}};

aIShipsActive=0;
		foreach(GameObject Ship in ShipsGOs){if(Ship.GetComponent<AICharacterControl>() != null && Ship.tag == "Ship" ){aIShipsActive++;}};
			if(aIShipsActive>0){aiRogueShipsActive =true;}else{aiRogueShipsActive =false;}

playerShips = 0;
		foreach(GameObject Ship in ShipsGOs){if(Ship.GetComponent<MyPlayer>() != null){playerShips++;}};


playerShipsSunk=0;
		foreach(GameObject Ship in ShipsGOs){if(Ship.GetComponent<MyPlayer>() != null && Ship.tag == "Destroyed" ){playerShipsSunk++;}};



//should list players active
//then list teams active
//could feed the team who died and survived on it their colour and ship names
//should monitor the win condition and trigger gm to trigger it if met otherwise gm will just pull the info
playerShipsActive=0;
numberOfTeamsActive = 0;
teamActive = new bool[]{false,false,false,false,false};
shipsActivePerTeamCount = new int[]{0,0,0,0,0}; //if was a list could just check length of list for how many teams active
		foreach(GameObject Ship in ShipsGOs){if(Ship.GetComponent<MyPlayer>() != null && Ship.tag == "Ship" ){    //foreach using ship but an array of ourShipsPlayer and checking allive may be better than a tag check
			playerShipsActive++;
//				Debug.Log(" Ship.GetComponent<MyPlayer>().ourShipsPlayerPairSettings.GetTeamNumber() is " + Ship.GetComponent<MyPlayer>().ourShipsPlayerPairSettings.GetTeamNumber());
			shipsActivePerTeamCount[Ship.GetComponent<MyPlayer>().ourShipsPlayerPairSettings.GetTeamNumber()]++; //getting array out of limits error presume this is knock on from the ship losing its connection to the static
			}
		}for(int i=0; i<shipsActivePerTeamCount.Length;i++){if(shipsActivePerTeamCount[i]>0){teamActive[i] = true;}else{teamActive[i]= false;}};
		foreach(bool teamIsActive in teamActive){if(teamIsActive == true){numberOfTeamsActive++;}}


			if(playerShipsActive>0){playerShipsActiveBool =true;}else{playerShipsActiveBool =false;}


ShipsDestroyed = playerShipsSunk+aIShipsSunk;
ShipsActive = playerShipsActive + aIShipsActive;

//gamemanager will run end game and find a draw when time runs out otherwise want to continue playing
//therefore currently mutual annilation or ai winning is a lose state
		if("DRAW" != currentWinLoseDrawState()){
		Debug.Log("currentWinLoseState is not returning draw when shipsCount checked if should end the game so were telling gm to end game");
		GameObject.Find("Game Manager").GetComponent<GameManager>().endGame();};

//run method here to check if should trigger game manager end games

	}
	public string currentWinLoseDrawState(){ //later should be a struct of all the info and processed so it can be displayed

	//in future will have set a game type and depending on that depends what condition checking

	//not having ai just playing selves at the moment
	//would be fun later to have a win condition where the ai wins and gets a win screen, instead of a player lose screen 
	if(playerShips<1){Debug.Log("currentWinLoseState is returning LOSE"); return "LOSE";}
		if(aIShipsActive <1 && numberOfTeamsActive ==1){Debug.Log("currentWinLoseState is returning LOSE"); return "WIN";}

//Debug.Log("returning draw from current win lose draw state method");
	return "DRAW";
	}
}
