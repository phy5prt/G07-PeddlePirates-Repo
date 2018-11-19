using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this code is fine however makes a decision on direct volt at a moment if find we get reading that are variable will need to take an average of last second and exclude the zeros or something

public class thisPairWantsAship : MonoBehaviour {

private Image shipSelectedIndicator;
private Color selectedColor;
 // will assign directly to static in future
private thisPlayerPairSettings myThisPlayerPairSettings;

private float voltMinActivation = 5f;

	// Use this for initialization
	void Start () {

	shipSelectedIndicator = GetComponent<Image>();
	selectedColor = shipSelectedIndicator.color;

		foreach(thisPlayerPairSettings pairShip in GameManager.shipPlayerSettingsAr){if(tag == pairShip.getShipPairColor()){myThisPlayerPairSettings = pairShip;}}
				
	}
	
	// Update is called once per frame
	void Update () {

	if(myThisPlayerPairSettings.GetmyLeftVolt()>voltMinActivation && myThisPlayerPairSettings.GetmyRightVolt()>voltMinActivation){

	shipSelectedIndicator.color = selectedColor;
		myThisPlayerPairSettings.setWerePlaying(true);


		}else{ myThisPlayerPairSettings.setWerePlaying(false); shipSelectedIndicator.color = Color.red;}




		
	}
}
