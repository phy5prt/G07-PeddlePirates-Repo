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

	private Toggle myToggle;
	private FoundBikeIndicator[] foundBikeIndicators;

	void Start () {
		myToggle = GetComponent<Toggle>();
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
}
