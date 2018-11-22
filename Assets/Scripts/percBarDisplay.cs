using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class percBarDisplay : MonoBehaviour {

//I am leftBool ?
//does it find its partner and work it out that way with local transform

public GameObject GetVolt;
public float voltCurrent;
public float voltPerc;
public float voltMax;
private Image percBar;
public float proportion200Bar;
public Text CurrentHighestVolt;
public Text CurrentVoltOutput;


	// Use this for initialization
	void Start () {



	percBar = gameObject.GetComponent<Image>();

	}
	void Update () {

	CurrentVoltOutput.text = "Current Volts: " + "\r\n" + voltCurrent; //if too fast put on coroutine

	voltCurrent = GetVolt.GetComponent<GetBikeFloatsHere>().bikeVolts;

	if(voltCurrent>voltMax){
	voltMax=voltCurrent*1.3f;
	CurrentHighestVolt.text = "Max Volt Set: " + "\r\n" + voltMax;

	} //it should delay displaying this so can exceded previous

	//should display volt number
	//or could reach the top95 % change colour when you hit it
	//line shows when you reach 90%

	voltPerc = voltCurrent/voltMax;
	proportion200Bar =	voltPerc; //so can get up to 90% of bar
	percBar.fillAmount = proportion200Bar; //goes up to 200% at moment hence divide by two


	if(proportion200Bar >= 0.9){percBar.color = Color.cyan;}      
	if(proportion200Bar <  0.8 && proportion200Bar>=0.7){percBar.color = Color.green;} 
	if(proportion200Bar <  0.7 && proportion200Bar>=0.5){percBar.color = Color.yellow;} 
	if(proportion200Bar <  0.5){percBar.color = Color.red;} 


	}

	/* for comparison

	void powerBarDisplayAndSetValues ()
	{
		CurrentVoltOutput.text = "Current Volts: " + "\r\n" + pairVoltCurrent;


		voltCurrentL = myThisPlayerPairSettings.GetmyLeftVolt();
		voltCurrentR = myThisPlayerPairSettings.GetmyRightVolt();
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

	*/

}
