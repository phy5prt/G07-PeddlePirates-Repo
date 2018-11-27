using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO.Ports;
using System;






//issue because gamemanager not running all its code before other things do maybe need to set things to awake for game manager rather than start

// I can't run this in start // how do i initialise it // if no instance redPShip like when I had it in thisPlayerPairSettings how can i store it
//so i need it to persist
//but can still make everything static so easy to access as this just means instances all share the value

//this player wants a ship holds the min volt for activation later this maybe a event setup option 


//later when putting more choices in event setup may want to put this in
//arrowTeamSelector : MonoBehaviour {
//[SerializeField] float totVoltTriggerMove = 100;

public class GameManager : MonoBehaviour {

	[SerializeField] bool useArduinoData = false;

	/* The serial port where the Arduino is connected. */
	[Tooltip("The serial port where the Arduino is connected")]
	public string port = "COM3";
	/* The baudrate of the serial port. */
	[Tooltip("The baudrate of the serial port")]
	public int baudrate = 9600;

	private SerialPort stream;
	//if this was an enum[] could i have each word allocated to its volt value so i feed in the word and it updates its value?
	string[] pins = {"DATA0", "DATA1", "DATA2", "DATA3", "DATA4", "DATA5", "DATA6", "DATA7"};




	
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

//inputs





//spawning settings
public static int numberAIs;
public static int[] enemiesShipSettings; //this is only an int array because later will be a struct array of settings for 


public static float numberDays = 3f;
public static float dayLength = 120f; 
public static float gameLength {get{return numberDays*dayLength;}} //does th 
private float endGameTime = 9999999999999999f; //trying to keep gm from holding stuff
private bool gameActive=false;	


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
//made it static?!?!?
public SemiRandomNumberGenerator fakeRedLeftArduinoVolt, fakeRedRightArduinoVolt, fakeYelLeftArduinoVolt, fakeYelRightArduinoVolt, fakeGreLeftArduinoVolt, fakeGreRightArduinoVolt, fakeBluLeftArduinoVolt, fakeBluRightArduinoVolt;

public static int stage = -1;
private static string gameEndResult; //later make this a struct holding various game information
private Camera fourthRectFor3playerCam;

	private int scenePersisting;    
	private GameObject spawner; //maybe just find and run and avoid having any kept instances
	 

	 //should i get a seperate script to be triggered for end game like for player setup
	 //end game and reset
	[SerializeField] float displayEndResultTimePeriod = 15f;
	private float timeToReset;

	private rigAllocateThisplayerPairSettingsToBars bikeRig;
																																															

 void Awake(){ //singleton 

  		int numSPWithThisSceneBuildNum = 0;
		scenePersisting = SceneManager.GetActiveScene().buildIndex;

		GameManager[] gM = FindObjectsOfType<GameManager>();   
			foreach(GameManager gameManager in gM){
				if(gameManager.scenePersisting ==  SceneManager.GetActiveScene().buildIndex) {numSPWithThisSceneBuildNum++;}else	{Destroy(gameManager.gameObject);} }  
		
		if (numSPWithThisSceneBuildNum>1){ Destroy(gameObject);}                              
																																					
		DontDestroyOnLoad(gameObject);																				




		settingUpPlayerSettingAr (); //this needs to run before everything else in game
 
}



private void Start(){
		


		bikeRig = GetComponentInChildren<rigAllocateThisplayerPairSettingsToBars>();
		bikeRig.setupTheRigVoltBars();

		OpenMyArduinoStream();

	
}

	private string arduinoTest (string data)
	{
		WriteToArduino (data);
		//WriteToArduino (number.ToString());
		 string ardionoSays = ReadFromArduino (1);
		return ardionoSays;
		//Debug.Log (" pin " + data + " says " + ardionoSays);
	}

	private float arduinoGetVolt (string data)
	{
		WriteToArduino (data);
		string ardionoSays = ReadFromArduino (1000);
		Debug.Log (" pin " + data + " says " + ardionoSays);
		return float.Parse (ardionoSays);
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
	private void feedRealArduino(){

		redPShip.SetmyLeftVolt(arduinoGetVolt(pins[0]));



		redPShip.SetmyRightVolt(arduinoGetVolt(pins[1]));




		yelPShip.SetmyLeftVolt(arduinoGetVolt(pins[2]));


		yelPShip.SetmyRightVolt(arduinoGetVolt(pins[3]));

	

		grePShip.SetmyLeftVolt(arduinoGetVolt(pins[4]));


		grePShip.SetmyRightVolt(arduinoGetVolt(pins[5]));
	

		bluPShip.SetmyLeftVolt(arduinoGetVolt(pins[6]));
	

		bluPShip.SetmyRightVolt(arduinoGetVolt(pins[7]));
	
	}

private void feedFakeArduino (){ //made it static ?!?

		//Debug.Log("redship " + redPShip + " fake red arduino volt " + fakeRedLeftArduinoVolt.theRandomNumber);
		//constant errors saying not set to object - buy not always! due to code being called out of order>
		//would it helped if pulled it off the GO

		//only happens occassionally so is it if this fires first then it fumbles and then this never runs and fromthen
		//on fails that whole game

		//does just seem to lose it at points, should i give it an instance of it instead
		//if the shipstatic is updated and read at the same time does that cause the issue?

		//try{ //not used before hoping will just not update when an error and carry on but still send me someinfo


		redPShip.SetmyLeftVolt(fakeRedLeftArduinoVolt.getTheRandomNumber());

//		Debug.Log(redPShip.getShipPairColor() + "    " + redPShip.GetmyLeftVolt());

		redPShip.SetmyRightVolt(fakeRedRightArduinoVolt.getTheRandomNumber());

	//	Debug.Log(redPShip.getShipPairColor() + "    " + redPShip.GetmyRightVolt());




		yelPShip.SetmyLeftVolt(fakeYelLeftArduinoVolt.getTheRandomNumber());

	//	Debug.Log(yelPShip.getShipPairColor() + "    " + yelPShip.GetmyLeftVolt());
		yelPShip.SetmyRightVolt(fakeYelRightArduinoVolt.getTheRandomNumber());

//		Debug.Log(yelPShip.getShipPairColor() + "    " + yelPShip.GetmyRightVolt());




		grePShip.SetmyLeftVolt(fakeGreLeftArduinoVolt.getTheRandomNumber());

	//	Debug.Log(grePShip.getShipPairColor() + "    " + grePShip.GetmyLeftVolt());
		grePShip.SetmyRightVolt(fakeGreRightArduinoVolt.getTheRandomNumber());
		//Debug.Log(grePShip.getShipPairColor() + "    " + grePShip.GetmyRightVolt());





		bluPShip.SetmyLeftVolt(fakeBluLeftArduinoVolt.getTheRandomNumber());
	//	Debug.Log(bluPShip.getShipPairColor() + "    " + bluPShip.GetmyLeftVolt());

		bluPShip.SetmyRightVolt(fakeBluRightArduinoVolt.getTheRandomNumber());
	//	Debug.Log(bluPShip.getShipPairColor() + "    " + bluPShip.GetmyRightVolt());
		//}catch{}


}









public  void startGameScene(){ //cant be static because button uses it

//take some things of player setup and put them in here

SceneManager.LoadScene(2);
//Debug.Log("calling setup scene 2");
	// think its firing out of order so seperated out the method
SceneManager.sceneLoaded += OnSceneLoaded; //using this because i think its running things before theyre there
}
	void OnSceneLoaded(Scene scene, LoadSceneMode mode){
		bikeRig.setupTheRigVoltBars();//seems daft to have to refeed it in
stage = 0;
gameActive = false;
//		Debug.Log("spawnpoints is " + GameObject.Find("SpawnPoints"));
spawner = GameObject.Find("SpawnPoints"); // may do this in player setup too dont want to double up
//		Debug.Log("spawner ship count is " + spawner.GetComponent<shipCounts>());
spawner.GetComponent<shipCounts>().enabled=false;
		
fourthRectFor3playerCam = GameObject.Find("temp3Player4thRectCam").GetComponentInChildren<Camera>();
			fourthRectFor3playerCam.enabled = false;
}

public void startGame(){ //why does this need to be static then everything interacts with need to be

//how do coroutines work with statics surely it wouldnt make sense to start a coroutine with a delay of the game play time
//then run the end game screen?
//we have an instance so should work
//if they win before time up will need to 
//cancel coroutine and run the method, but think that happens anyway

//Debug.Log("finding spawn points and tell the script to spawn players and enemies");
//could have gamemanager here apply the screen split rects however i feel playersetup is the one to do the work gamemanager gives the instructions

//spawn the ships has nothing in update so should start on the count though could be turned on later. must have time to run start before does anything though

spawner.GetComponent<spawnTheShips>().enabled=true;
spawner.GetComponent<shipCounts>().enabled=true;
spawner.GetComponent<spawnTheShips>().spawnPlayersAndEnemies();
endGameTime = Time.timeSinceLevelLoad + gameLength;
gameActive=true;
//bikeRig.setupTheRigVoltBars(); //shouldnt have to run this twice but seems to lose its static with scene change
//Debug.Log(" just set game active to  " + gameActive + " and endGameTime is " + endGameTime + " Which is Timesince level load ish " + Time.timeSinceLevelLoad + " plus gameLenght " + gameLength);
}



	

	 public void endGame(){//being called before game started


	 //be able to call for win condition
	 //at moment dont have options for winning so lets say last one standing including ai
	 //if ai get stuck in terrain alot etc then will change

	 //if there are players and ai left - its draw scenario - screen saying foods out time to parlay
	 //if all players dead and ai left - you were defeated by the rogue pirates // unaligned (current only fight each other)
	 //if noone left at all - you died but at least you took em with yah! no ones getting your treasure except your beloved mistress the sea
	 //if team only left then you defeated all your foes the seas are yours, time to rebuild and become the pirate kind of pirate island pirate lord of the sea and pirate pirate pirate of pirating

	 //if reuse the old boxes will feel anticlimatic i want it to spin nearly instantly into playing again but want to feel like acheivement
	 //treasure chest opens and star comes out with text ??? naf

	 //use the pirate dialogue boxes and the highscores slide in and record and high light scenario and outcome
	 //revert ready for the next game

	
	gameEndResult = spawner.gameObject.GetComponent<shipCounts>().currentWinLoseDrawState();
	spawner.GetComponent<shipCounts>().enabled=false;

		//be nicer if had script on itself turns itself on and is fed the string
	Canvas resultsCanvas = GameObject.Find("HolderForGameResult").GetComponentInChildren<Canvas>(true);
	resultsCanvas.gameObject.SetActive(true);
	Text resultText = resultsCanvas.gameObject.GetComponentsInChildren<Text>()[1]; //very sloppy but this code going to change when have rea end screen
	resultText.text = gameEndResult;
	Debug.Log("GameManager says its time to run a method for " + gameEndResult);


	timeToReset = Time.timeSinceLevelLoad + displayEndResultTimePeriod;

	 } 


		

	 

	private void Update(){ 

	//	foreach (String pin in pins) {arduinoTest (pin);}
		//arduinoIsCommunicating();
	//think feed arduino is running before all of start is finished sometimes hence errors
	//doesnt need to be solved yet as will be replaced by aduino but
	//if(SceneManager.GetActiveScene().buildIndex != 2){return;} //- this will reduce errors temporarily until running in awake
		if(useArduinoData &&          (arduinoTest(pins[0]) != null)   ){Debug.Log("using real arduino feed");feedRealArduino();}else{feedFakeArduino();}

	if(SceneManager.GetActiveScene().buildIndex != 2){return;}// - this will reduce errors temporarily until running in awake
	//the should only be run once playerSetup finisher
	

		//this is triggering instantly!
		if(gameActive){
	if(Time.timeSinceLevelLoad>endGameTime){endGame();
				Debug.Log (" current time is " + Time.timeSinceLevelLoad + " end game time is " + endGameTime + " day length " + dayLength + " number of days  " + numberDays);
				endGameTime = 99999999999f;}
			if(Time.timeSinceLevelLoad>timeToReset){startGameScene();}


	}}


	public void OpenMyArduinoStream () {
		// Opens the serial port
		stream = new SerialPort(port, baudrate);
		stream.ReadTimeout = 50;
		stream.Open();
		//this.stream.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
	}

	public void WriteToArduino(string message)
	{
		// Send the request
		stream.WriteLine(message);
		stream.BaseStream.Flush();
	}

	public string ReadFromArduino(int timeout = 0)
	{
		stream.ReadTimeout = timeout;
		try
		{
			return stream.ReadLine();
		}
		catch (TimeoutException e)
		{
			return null;
		}
	}


	public IEnumerator AsynchronousReadFromArduino(Action<string> callback, Action fail = null, float timeout = float.PositiveInfinity)
	{
		DateTime initialTime = DateTime.Now;
		DateTime nowTime;
		TimeSpan diff = default(TimeSpan);

		string dataString = null;

		do
		{
			// A single read attempt
			try
			{
				dataString = stream.ReadLine();
			}
			catch (TimeoutException)
			{
				dataString = null;
			}

			if (dataString != null)
			{
				callback(dataString);
				yield return null;
			} else
				yield return new WaitForSeconds(0.05f);

			nowTime = DateTime.Now;
			diff = nowTime - initialTime;

		} while (diff.Milliseconds < timeout);

		if (fail != null)
			fail();
		yield return null;
	}

	public void Close()
	{
		stream.Close();
	}

	private void arduinoIsCommunicating(){ // make bool when works
		float delay;
		float timeAsked = Time.timeSinceLevelLoad;

		float timeReplied = arduinoTest2 ();
		delay = timeReplied - timeAsked;
//		Debug.Log (" delay in arduino reply " + delay);


	}

	private float arduinoTest2 (){






			WriteToArduino ("DATA0");
			//WriteToArduino (number.ToString());
		ReadFromArduino (1000);

		return Time.timeSinceLevelLoad;
			



	}
}