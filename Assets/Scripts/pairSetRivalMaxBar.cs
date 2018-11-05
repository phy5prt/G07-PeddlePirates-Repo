using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pairSetRivalMaxBar : MonoBehaviour {

//need to know best way to code something that runs once for an amount of time.


//dont really know when else gonna need two bikes in a paired bar so maybe this code should just be enabled and disabled
//and operate entirely in the update
//no bool for turning on because this is the only time however how will it no to send its data
//need a non void update

//it needs to pull from current players array
//set colour background to rival (next entry thats bool isnt deactivated)// and maybe even text




	
	[SerializeField] GameObject GetVoltL;
	[SerializeField] GameObject GetVoltR;
	private float voltCurrentL;
	private float voltCurrentR;


//public float GetVolt = 100f;
	private float pairVoltCurrent =20f;
	[SerializeField] float voltPerc =1f;
	[SerializeField] float voltMax = 100f;
private Image percBar;
	[SerializeField] float proportion200Bar;
	[SerializeField] Text CurrentHighestVolt;
	[SerializeField] Text CurrentVoltOutput;

	private string myShipPairColor;
	private PlayerSetupManager PSM;
	private int myShipPairArIndex;
	private int myRivalIndex;
	private Color rivalColor;
	private int checkArrayAt;
	private Image myRivalBGPanel;
	private string myRivalName;
	private string sabotageRunTxt;
	private Text sabotageRunTXTComp;
	private bool runSetMax;
	// Use this for initialization

	//del later
	private thisPlayerPairSettings rivalOrOwnPair;

	void Start () {


	this.enabled = true;


	percBar = gameObject.GetComponent<Image>();

	PSM = GameObject.Find("PlayerSetUpManager").GetComponent<PlayerSetupManager>();  
	updateTxtAndDisplayWithRivalInfo(); //should be fed this in a method

	}


	void Update () {

		//it should delay displaying this so can exceded previous
		//should display volt number
		//or could reach the top95 % change colour when you hit it
		//line shows when you reach 90%
		if(runSetMax){

		powerBarDisplayAndSetValues ();
		setVoltsToGet100PercSpeed ();

		//send Volt Max put in it 
			
		

			}
	}

	void powerBarDisplayAndSetValues ()
	{
		CurrentVoltOutput.text = "Current Volts: " + "\r\n" + pairVoltCurrent;


		voltCurrentL = GetVoltL.GetComponent<SemiRandomNumberGenerator> ().theRandomNumber;
		voltCurrentR = GetVoltR.GetComponent<SemiRandomNumberGenerator> ().theRandomNumber;
		pairVoltCurrent = (voltCurrentL + voltCurrentR)/2;
		if (voltMax > 0) {
			voltPerc = pairVoltCurrent / voltMax;
		}
		proportion200Bar = voltPerc;
		//so can get up to 90% of bar
		percBar.fillAmount = proportion200Bar;
		//goes up to 200% at moment hence divide by two
		if (proportion200Bar >= 0.9) {
			percBar.color = Color.cyan;
		}
		if (proportion200Bar < 0.8 && proportion200Bar >= 0.7) {
			percBar.color = Color.green;
		}
		if (proportion200Bar < 0.7 && proportion200Bar >= 0.5) {
			percBar.color = Color.yellow;
		}
		if (proportion200Bar < 0.5) {
			percBar.color = Color.red;
		}
	}

	private void setVoltsToGet100PercSpeed ()
	{

	//use rivalOrOwnPair's colour to set a colour indicator of who's max your setting so visually clear


		//if too fast put on coroutine

		//voltCurrent = GetVolt;
		if (pairVoltCurrent > voltMax) {
			voltMax = pairVoltCurrent * 1.3f;
			CurrentHighestVolt.text = "Max Volt Set: " + "\r\n" + voltMax;
			PSM.shipPlayerSettingsAr[myRivalIndex].SetVolt100Perc(voltMax);
		}
	
	}


	public void runSetMaxFor(thisPlayerPairSettings givenThisPlayerSettingsRivalOrPair){ /// if you want to feed something a static how do you do it as they dont have an instance

	rivalOrOwnPair = givenThisPlayerSettingsRivalOrPair;
	updateTxtAndDisplayWithRivalInfo();
	runSetMax = true;

	}

	private void updateTxtAndDisplayWithRivalInfo(){
		
	myShipPairColor = tag;
	myRivalBGPanel = GetComponentInChildren<Image>();
	sabotageRunTXTComp = GetComponentInChildren<Text>(); 

	//which one am I.So I know which one is next along
	//match the color to your back panel and then find that ones index

		if(PSM.shipPlayerSettingsAr[myRivalIndex].getShipPairColor() == "RED"){rivalColor = Color.red;}
			else if(PSM.shipPlayerSettingsAr[myRivalIndex].getShipPairColor() == "YELLOW"){rivalColor = Color.yellow;}
				else if(PSM.shipPlayerSettingsAr[myRivalIndex].getShipPairColor() == "GREEN"){rivalColor = Color.green;}
					else if(PSM.shipPlayerSettingsAr[myRivalIndex].getShipPairColor() == "BLUE"){rivalColor = Color.blue;}
						 else{Debug.Log("color not assigned? or string miss match");}


		myRivalName = PSM.shipPlayerSettingsAr[myRivalIndex].GetShipPairName();
		if(myRivalName == PSM.shipPlayerSettingsAr[myShipPairArIndex].GetShipPairName()){
			sabotageRunTxt = "rowing for their lives";}

			//need to add in incase they have been asigned a friend that "is towing team mates X as fast as they can before the docks blow"

		else{sabotageRunTxt = "sabotaging " + PSM.shipPlayerSettingsAr[myRivalIndex].GetShipPairName();}
		sabotageRunTXTComp.text = sabotageRunTxt;
		myRivalBGPanel.color = rivalColor;

		
	}

}
