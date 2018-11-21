using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// I can't run this in start // how do i initialise it // if no instance redPShip like when I had it in thisPlayerPairSettings how can i store it
//so i need it to persist
//but can still make everything static so easy to access as this just means instances all share the value

//this player wants a ship holds the min volt for activation later this maybe a event setup option 


//later when putting more choices in event setup may want to put this in
//arrowTeamSelector : MonoBehaviour {
//[SerializeField] float totVoltTriggerMove = 100;

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


public static int numberDays = 3;
public static int dayLength = 120; 
public static int gameLength {get{return numberDays*dayLength;}} //does th 
	


//inputs
//what is the type where when you define it you have to set values
public static thisPlayerPairSettings redPShip;
public static thisPlayerPairSettings yelPShip;
public static thisPlayerPairSettings grePShip;
public static thisPlayerPairSettings bluPShip;

//public static thisPlayerPairSettings[]  shipPlayerSettingsAr =  {redPShip,yelPShip,grePShip,bluPShip}; - this doesnt work needs doing in the build array or get an error no instance
//when try and call it ... understanding why would be a useful bit of learning i think its something fundamental i have a vague but not clear idea why
	public static thisPlayerPairSettings[]  shipPlayerSettingsAr; 

//delete when go real arduino

public SemiRandomNumberGenerator fakeRedLeftArduinoVolt, fakeRedRightArduinoVolt, fakeYelLeftArduinoVolt, fakeYelRightArduinoVolt, fakeGreLeftArduinoVolt, fakeGreRightArduinoVolt, fakeBluLeftArduinoVolt, fakeBluRightArduinoVolt;





	private int scenePersisting;     
								

 void Awake(){ //singleton 



 		int numSPWithThisSceneBuildNum = 0;
		scenePersisting = SceneManager.GetActiveScene().buildIndex;



		GameManager[] gM = FindObjectsOfType<GameManager>();   
			foreach(GameManager gameManager in gM){
				if(gameManager.scenePersisting ==  SceneManager.GetActiveScene().buildIndex) {numSPWithThisSceneBuildNum++;}else	{Destroy(gameManager.gameObject);} }  

			
		if (numSPWithThisSceneBuildNum>1){ Destroy(gameObject);}                              
										
																																																		
		DontDestroyOnLoad(gameObject);																				
			
 
}



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

		redPShip.SetBattleCamRenderTextureLeft("REDLEFTRENDERTEXTURE");
		redPShip.SetBattleCamRenderTextureRight("REDRIGHTRENDERTEXTURE");
		yelPShip.SetBattleCamRenderTextureLeft("YELLEFTRENDERTEXTURE");
		yelPShip.SetBattleCamRenderTextureRight("YELRIGHTRENDERTEXTURE");
		grePShip.SetBattleCamRenderTextureLeft("GRELEFTRENDERTEXTURE");
		grePShip.SetBattleCamRenderTextureRight("GRERIGHTRENDERTEXTURE");
		bluPShip.SetBattleCamRenderTextureLeft("BLULEFTRENDERTEXTURE");
		bluPShip.SetBattleCamRenderTextureRight("BLURIGHTRENDERTEXTURE");



		shipPlayerSettingsAr  =  new thisPlayerPairSettings[] {redPShip,yelPShip,grePShip,bluPShip};

	}

private void feedFakeArduino (){

		//Debug.Log("redship " + redPShip + " fake red arduino volt " + fakeRedLeftArduinoVolt.theRandomNumber);
		//constant errors saying not set to object - buy not always! due to code being called out of order>
		redPShip.SetmyLeftVolt(fakeRedLeftArduinoVolt.theRandomNumber);
		redPShip.SetmyRightVolt(fakeRedRightArduinoVolt.theRandomNumber);

		yelPShip.SetmyLeftVolt(fakeYelLeftArduinoVolt.theRandomNumber);
		yelPShip.SetmyRightVolt(fakeYelRightArduinoVolt.theRandomNumber);

		grePShip.SetmyLeftVolt(fakeGreLeftArduinoVolt.theRandomNumber);
		grePShip.SetmyRightVolt(fakeGreRightArduinoVolt.theRandomNumber);

		bluPShip.SetmyLeftVolt(fakeBluLeftArduinoVolt.theRandomNumber);
		bluPShip.SetmyRightVolt(fakeBluRightArduinoVolt.theRandomNumber);


}









public  void startGameScene(){ //cant be static because button uses it

SceneManager.LoadScene(2);

}

public static void startGame(){

//how do coroutines work with statics surely it wouldnt make sense to start a coroutine with a delay of the game play time
//then run the end game screen?
//we have an instance so should work
//if they win before time up will need to 
//cancel coroutine and run the method, but think that happens anyway

//Debug.Log("finding spawn points and tell the script to spawn players and enemies");
//could have gamemanager here apply the screen split rects however i feel playersetup is the one to do the work gamemanager gives the instructions
GameObject.Find("SpawnPoints").GetComponent<spawnTheShips>().spawnPlayersAndEnemies();


}



	private void Update(){ 

	//
	feedFakeArduino();} // wont be need once got real arduino
}
