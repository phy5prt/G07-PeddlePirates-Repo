using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//on scene change loses its connection to myPlayerPairsettings dont know why as we keep gm and its a child
//is this script getting reassigned somehwere and not actually losing its link
public class percBarDisplay : MonoBehaviour {


//does it find its partner and work it out that way with local transform
	[SerializeField] bool leftSideBar = true; //would be better if set self in code rather than me saying if left


	//later all should be private
	[SerializeField] float voltCurrent;
	[SerializeField] float voltPerc;
	[SerializeField] float volt100Perc;

	private Image percBar;
	private float proportion200Bar;

	private Text hundredPercVolt;
private Text CurrentVoltOutput;


	private thisPlayerPairSettings myPPSettings; //loses this over scene change so may need singleton design pattern //static seems to solve it but get orther errors
	private bool iHaveTextBoxes = false;
	private bool receivedMyPlayerPairSettings = false;
	private float originalAlpha;


	// Use this for initialization
	void Start () {








	percBar = this.gameObject.GetComponent<Image>();
	originalAlpha = percBar.color.a;
	//this needs to find the text boxes and if non return text boxes

	if(gameObject.transform.childCount>0){

	iHaveTextBoxes = true;

	//dont like code because if move them around it goes arry
	hundredPercVolt = GetComponentsInChildren<Text>()[0];
	CurrentVoltOutput = GetComponentsInChildren<Text>()[1];
	}//just check for children if there is set the bool and get the texts 



	}
	void Update () {
	//messy




	if(receivedMyPlayerPairSettings){//it loses its static on scene change no idea how to help it keep it

	if(leftSideBar){voltCurrent = myPPSettings.GetmyLeftVolt();	}else{voltCurrent = myPPSettings.GetmyRightVolt();	} // if using get set could just say getset
	//Debug.Log("mycurrentvolt is" + voltCurrent);

	if(iHaveTextBoxes){
	CurrentVoltOutput.text = "Current Volts: " + "\r\n" + voltCurrent; //if too fast put on coroutine
	hundredPercVolt.text = "100 Perc: " + "\r\n" + volt100Perc; //need max here
	}





	voltPerc = voltCurrent/volt100Perc;
	proportion200Bar =	voltPerc; //so can get up to 90% of bar
	//Debug.Log("proportion 200bar" +proportion200Bar);
	percBar.fillAmount = proportion200Bar; //goes up to 200% at moment hence divide by two


	if(proportion200Bar >= 0.9){percBar.color = Color.cyan;}      
	if(proportion200Bar <  0.8 && proportion200Bar>=0.7){percBar.color = Color.green;} 
	if(proportion200Bar <  0.7 && proportion200Bar>=0.5){percBar.color = Color.yellow;} 
	if(proportion200Bar <  0.5){percBar.color = Color.red;} 

	//reapplying alpha
	percBar.color = new Color(percBar.color.r, percBar.color.g, percBar.color.b, originalAlpha );

	}

	}

	public void passMeMyPlayerPairSettings(thisPlayerPairSettings barsPlayerPairSettings){
	//Debug.Log("im receiring my player pairsettings");

	this.myPPSettings = barsPlayerPairSettings;

	volt100Perc = this.myPPSettings.GetVolt100Perc(); //probs only need calling once unless being set
		receivedMyPlayerPairSettings=true;
		//Debug.Log(" I am perc bar " + this.gameObject.tag +"I have received " + myPPSettings.getShipPairColor()+ " settings ");
	}



}
