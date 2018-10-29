using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

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
	void Start () {

		InvokeRepeating("UpdateShipCounts",10f,3f);
		
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

playerShips = 0;
		foreach(GameObject Ship in ShipsGOs){if(Ship.GetComponent<MyPlayer>() != null){playerShips++;}};


playerShipsSunk=0;
		foreach(GameObject Ship in ShipsGOs){if(Ship.GetComponent<MyPlayer>() != null && Ship.tag == "Destroyed" ){playerShipsSunk++;}};

playerShipsActive=0;
		foreach(GameObject Ship in ShipsGOs){if(Ship.GetComponent<MyPlayer>() != null && Ship.tag == "Ship" ){playerShipsActive++;}};


ShipsDestroyed = playerShipsSunk+aIShipsSunk;
ShipsActive = playerShipsActive + aIShipsActive;


	}
}
