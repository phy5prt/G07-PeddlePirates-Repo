using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BikeVoltUITest1 : MonoBehaviour {

public GameObject GetVolt;
//public float GetVolt = 100f;
public float voltCurrent =20f;
public float voltPerc =1f;
public float voltMax = 100f;
private Image percBar;
public float proportion200Bar;
public Text CurrentHighestVolt;
public Text CurrentVoltOutput;


	// Use this for initialization
	void Start () {



	percBar = gameObject.GetComponent<Image>();

	}
	void Update () {

		//it should delay displaying this so can exceded previous
		//should display volt number
		//or could reach the top95 % change colour when you hit it
		//line shows when you reach 90%

		voltCurrent = GetVolt.GetComponent<SemiRandomNumberGenerator> ().theRandomNumber;

		if(voltMax>0){voltPerc = voltCurrent / voltMax;}
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

	public float setAssigntVoltsToGet100PercSpeed(thisPlayerPairSettings rivalOrOwnPair){

	//coroutine or invoke repeating would be better
	//
		//run setVoltsToGet100PercSpeed (rivalOrOwnPair)
		// for the time it takes
		//or in the update?

		//returns rivalOrOWnPair with the max now save to it or just a float made 

		return 1.0f; //for now
	}

	private void setVoltsToGet100PercSpeed (thisPlayerPairSettings rivalOrOwnPair)
	{

	//use rivalOrOwnPair's colour to set a colour indicator of who's max your setting so visually clear

		CurrentVoltOutput.text = "Current Volts: " + "\r\n" + voltCurrent;
		//if too fast put on coroutine

		//voltCurrent = GetVolt;
		if (voltCurrent > voltMax) {
			voltMax = voltCurrent * 1.3f;
			CurrentHighestVolt.text = "Max Volt Set: " + "\r\n" + voltMax;
		}

	}
}
