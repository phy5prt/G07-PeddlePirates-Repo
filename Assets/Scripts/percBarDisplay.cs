using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class percBarDisplay : MonoBehaviour {


//does it find its partner and work it out that way with local transform


private float voltCurrent;
private float voltPerc;
private float volt100Perc;

private Image percBar;
private float proportion200Bar;

private Text hundredPercVolt;
private Text CurrentVoltOutput;

[SerializeField] bool leftSideBar = true; //would be better if set self in code rather than me saying if left
private thisPlayerPairSettings myPlayerPairSettings;
private bool iHaveTextBoxes = false;




	// Use this for initialization
	void Start () {








	percBar = gameObject.GetComponent<Image>();

	//this needs to find the text boxes and if non return text boxes

	if(gameObject.transform.childCount>0){

	iHaveTextBoxes = true;

	//dont like code because if move them around it goes arry
	hundredPercVolt = GetComponentsInChildren<Text>()[0];
	CurrentVoltOutput = GetComponentsInChildren<Text>()[1];
	}//just check for children if there is set the bool and get the texts 



	}
	void Update () {

	if(myPlayerPairSettings != null){

	if(leftSideBar){voltCurrent = myPlayerPairSettings.GetmyLeftVolt();	}else{voltCurrent = myPlayerPairSettings.GetmyRightVolt();	} // if using get set could just say getset


	if(iHaveTextBoxes){
	CurrentVoltOutput.text = "Current Volts: " + "\r\n" + voltCurrent; //if too fast put on coroutine
	hundredPercVolt.text = "100 Perc: " + "\r\n" + volt100Perc; //need max here
	}





	voltPerc = voltCurrent/volt100Perc;
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
	volt100Perc = myPlayerPairSettings.GetVolt100Perc(); //probs only need calling once unless being set
	}



}
