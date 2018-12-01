using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

////adapt this code - maybe should just trust them not to put bikes in that havent had a reading
//icould give the ships a bool that triggers if enough cycling has happened since game start
//so game knows which were cycled but would have to be reset everytime dropdowns used and the bike to pin 
//allocation is reconfigured

//Im setting this to check in its update for if the toggles
//later indicators are set to true
//now this potentially would be better if i used toggle group
//and if the toggles themselves rather than update
//triggered the interactability of this component
//i feel less things that are public, found by name, and run in update the tighter the product

public class usePairSelectable : MonoBehaviour {

	// Use this for initialization

	public Toggle myToggle;
	private Dropdown found;
	private showSliderValue sliderMax;
	public Dropdown[] myColorBikeDropDowns;


	void Start () {
	sliderMax = transform.root.gameObject.GetComponentInChildren<showSliderValue>();
		myToggle = GetComponent<Toggle>();
		myToggle.onValueChanged.AddListener(delegate {ValueChangedCheck(); });
		myToggle.interactable = false;

	



	}
	
	// Update is called once per frame
	void Update () {
		checkIfBikePairBothSetToRealPin(); // this should be triggered in future by a listener on the dropboxes
	}

	private void checkIfBikePairBothSetToRealPin(){ //this could be triggered by the value change event in the toggles


	bool bothHaveRealPin = true;
		
	foreach(Dropdown bike in myColorBikeDropDowns){if(bike.value==8){bothHaveRealPin = false; break;}}


	myToggle.interactable = bothHaveRealPin;

	if(myToggle.interactable == false){myToggle.isOn = false;}
	}

	private void ValueChangedCheck(){

		sliderMax.ValueChangedcheck();
		foreach(thisPlayerPairSettings pairSetting in GameManager.shipPlayerSettingsAr){

		if(tag == pairSetting.getShipPairColor()){pairSetting.setBikePairSetAsAvailableOnEventSetup(myToggle.isOn);}}

	}



	/* the old code
	public class usePairSelectable : MonoBehaviour {

	// Use this for initialization

	private Toggle myToggle;
	private Dropdown found;
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

	*/


}
