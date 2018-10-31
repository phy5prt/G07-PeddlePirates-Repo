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




	[SerializedField] float myRunTime;
	private float finishTime;
	[SerializeField] GameObject GetVoltL;
	[SerializeField] GameObject GetVoltR;
	[SerializeField] float voltCurrentL;
	[SerializeField] float voltCurrentR;


//public float GetVolt = 100f;
	[SerializeField] float pairVoltCurrent =20f;
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
	// Use this for initialization
	void Start () {


	this.enabled;


	percBar = gameObject.GetComponent<Image>();
	finishTime = Time.timeSinceLevelLoad + myRunTime;
	PSM = GameObject.Find("PlayerSetUpManager").GetComponent<PlayerSetupManager>();  //there will only ever be one so research is something like this always called to should be just made a static or something else findable

	setMyRival();

	}

	private void setMyRival(){

	myShipPairColor = tag;
	myRivalBGPanel = GetComponentInChildren<Image>();
	sabotageRunTXTComp = GetComponentInChildren<Text>(1); //can you do this to say second text file?

	//which one am I.So I know which one is next along
	//match the color to your back panel and then find that ones index
		for(int i =0; i< PSM.shipPlayerSettingsAr.Length; i++){
		if(PSM.shipPlayerSettingsAr[i].getShipPairColor == myShipPairColor){myShipPairArIndex=i; break;}}


		//careful of crash coz messing with loop im using in loop
		for(int j = myShipPairArIndex; j< PSM.shipPlayerSettingsAr.Length; j++){

			if(j+1>= PSM.shipPlayerSettingsAr.Length){checkArrayAt = 0;}else{checkArrayAt = j+1;j = 0;}    //should plus one so not do zero twice
			if(PSM.shipPlayerSettingsAr[checkArrayAt].getWerePlaying == true){myRivalIndex = checkArrayAt; break;}
			} //for should never get to the end without finding something as eventually will find itself

		if(PSM.shipPlayerSettingsAr[myRivalIndex].getShipPairColor == "RED"){rivalColor = Color.red;}
			else if(PSM.shipPlayerSettingsAr[myRivalIndex].getShipPairColor == "YELLOW"){rivalColor = Color.yellow;}
				else if(PSM.shipPlayerSettingsAr[myRivalIndex].getShipPairColor == "GREEN"){rivalColor = Color.green;}
					else if(PSM.shipPlayerSettingsAr[myRivalIndex].getShipPairColor == "BLUE"){rivalColor = Color.blue;}
						 else{Debug.Log("color not assigned? or string miss match");}


		myRivalName = PSM.shipPlayerSettingsAr[myRivalIndex].GetShipPairName;
		if(myRivalName == PSM.shipPlayerSettingsAr[myShipPairArIndex].GetShipPairName){
			sabotageRunTxt = "rowing for their lives";}
		else{sabotageRunTxt = "sabotaging " + PSM.shipPlayerSettingsAr[myRivalIndex].GetShipPairName;}
		sabotageRunTXTComp.text = sabotageRunTXTComp;
		myRivalBGPanel = rivalColor;

		
	}
	void Update () {

		//it should delay displaying this so can exceded previous
		//should display volt number
		//or could reach the top95 % change colour when you hit it
		//line shows when you reach 90%


		powerBarDisplayAndSetValues ();
		setVoltsToGet100PercSpeed ();
		if(Time.timeSinceLevelLoad > finishTime){
		//send Volt Max
			PSM.shipPlayerSettingsAr[myRivalIndex].SetVolt100Perc = voltMax;
			this.enabled = false;}


	}

	void powerBarDisplayAndSetValues ()
	{
		voltCurrentL = GetVoltL.GetComponent<SemiRandomNumberGenerator> ().theRandomNumber;
		voltCurrentR = GetVoltR.GetComponent<SemiRandomNumberGenerator> ().theRandomNumber;
		pairVoltCurrent = voltCurrentL + voltCurrentR;
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

		CurrentVoltOutput.text = "Current Volts: " + "\r\n" + pairVoltCurrent;
		//if too fast put on coroutine

		//voltCurrent = GetVolt;
		if (pairVoltCurrent > voltMax) {
			voltMax = pairVoltCurrent * 1.3f;
			CurrentHighestVolt.text = "Max Volt Set: " + "\r\n" + voltMax;
		}

	}
}
