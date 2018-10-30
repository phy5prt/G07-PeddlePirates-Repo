using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//What we need
//each team with a max, colour, a teams layer, left right row input set, team name(is string so keep an index to a static string array with player numbnumber) 
//Ai infor to food to computer
//no need number players just have a ship per colour, it gets assigned a layer and ones allocated get put in player array and created // use colour to border their screen partician they
//dont need where they are in array

//or do teams need their own class - that overloads player?

//going to make a player override class
//  Player : myPlayerSetup overidestart

public class PlayerSetupManager : MonoBehaviour {

private thisPlayerPairSettings[] shipPlayerSettingsAr;

//in future may want to set max's per bike
//this would mean a child and parent could have different maxes
//there could be an option for set your own max's


private thisPlayerPairSettings redPShip;
private thisPlayerPairSettings yelPShip;
private thisPlayerPairSettings grePShip;
private thisPlayerPairSettings bluPShip;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void HowManyBikePairs ()
	{
		redPShip = new thisPlayerPairSettings {};
		yelPShip = new thisPlayerPairSettings {};
		grePShip = new thisPlayerPairSettings {};
		bluPShip = new thisPlayerPairSettings {};

		shipPlayerSettingsAr = new thisPlayerPairSettings[] {redPShip,yelPShip,grePShip,bluPShip};

	}
}
