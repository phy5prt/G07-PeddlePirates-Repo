using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class percBarDisplay : MonoBehaviour {


//does it find its partner and work it out that way with local transform


private float voltCurrent;
private float voltPerc;

private Image percBar;
private float proportion200Bar;
private Text CurrentHighestVolt;
private Text CurrentVoltOutput;
[SerializeField] bool leftSideBar = true; //would be better if set self in code rather than me saying if left
private thisPlayerPairSettings myPlayerPairSettings;



	// Use this for initialization
	void Start () {



	percBar = gameObject.GetComponent<Image>();

	}
	void Update () {

	if(myPlayerPairSettings != null){

	if(leftSideBar){voltCurrent = myPlayerPairSettings.GetmyLeftVolt();}else{voltCurrent = myPlayerPairSettings.GetmyRightVolt();} // if using get set could just say getset
	CurrentVoltOutput.text = "Current Volts: " + "\r\n" + voltCurrent; //if too fast put on coroutine







	voltPerc = voltCurrent/myPlayerPairSettings.GetVolt100Perc();
	proportion200Bar =	voltPerc; //so can get up to 90% of bar
	percBar.fillAmount = proportion200Bar; //goes up to 200% at moment hence divide by two


	if(proportion200Bar >= 0.9){percBar.color = Color.cyan;}      
	if(proportion200Bar <  0.8 && proportion200Bar>=0.7){percBar.color = Color.green;} 
	if(proportion200Bar <  0.7 && proportion200Bar>=0.5){percBar.color = Color.yellow;} 
	if(proportion200Bar <  0.5){percBar.color = Color.red;} 
	}

	}

	public void passMeMyPlayerPairSettings(thisPlayerPairSettings barsPlayerPairSettings){

	myPlayerPairSettings = barsPlayerPairSettings;

	}



}
