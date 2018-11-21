using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Im setting this to check in its update for if the toggles
//later indicators are set to true
//now this potentially would be better if i used toggle group
//and if the toggles themselves rather than update
//triggered the interactability of this component
//i feel less things that are public, found by name, and run in update the tighter the product

public class usePairSelectable : MonoBehaviour {

	// Use this for initialization

	public Toggle myToggle;
	private FoundBikeIndicator[] foundBikeIndicators;
	private showSliderValue sliderMax;

	void Start () {
	sliderMax = transform.parent.parent.gameObject.GetComponentInChildren<showSliderValue>();
		myToggle = GetComponent<Toggle>();
		myToggle.onValueChanged.AddListener(delegate {ValueChangedCheck(); });
		myToggle.interactable = false;

		foundBikeIndicators = transform.parent.gameObject.GetComponentsInChildren<FoundBikeIndicator>();

	}
	
	// Update is called once per frame
	void Update () {
		checkIfBikePairFound();
	}

	private void checkIfBikePairFound(){ //this could be triggered by the value change event in the toggles


	bool bothHaveFoundABike = true;
		
	foreach(FoundBikeIndicator foundBikeIndicator in foundBikeIndicators){if(foundBikeIndicator.iDetectedABike == false){bothHaveFoundABike = false; break;}}


	myToggle.interactable = bothHaveFoundABike;

	if(myToggle.interactable == false){myToggle.isOn = false;}
	}

	private void ValueChangedCheck(){

		sliderMax.ValueChangedcheck();
		foreach(thisPlayerPairSettings pairSetting in GameManager.shipPlayerSettingsAr){

//		Debug.Log(" tag " + tag);

	//		Debug.Log(" I can find redShip.getColour from the GameManager " + GameManager.redPShip.getShipPairColor());
	//			Debug.Log(" I can find redShip.getColour from the array " + GameManager.shipPlayerSettingsAr[0].getShipPairColor());

	//		Debug.Log(" pairSetting.getShipPairColor() " + pairSetting.getShipPairColor());
	//		Debug.Log(" pairSetting.setBikePairSetAsAvailableOnEventSetup "+ pairSetting.getBikePairSetAsAvailableOnEventSetup());
	//		Debug.Log(" myToggle.isOn "+ myToggle.isOn);
		if(tag == pairSetting.getShipPairColor()){pairSetting.setBikePairSetAsAvailableOnEventSetup(myToggle.isOn);}}

	}
}
