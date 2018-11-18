using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//in future code should just receive a bool and then change colour
//however for moment so can run through the scenes it opperates through tick box

public class FoundBikeIndicator : MonoBehaviour {

public bool iDetectedABike; // so code can access the indicator and see status however depending on process may be better just to directly ask aduino
//instead of checking horizontally have both this and the use pair option enabled from the arduino so not passing info around

private Image bikeDetectedIndicatorColour;
		// Use this for initialization
	void Start () {

		bikeDetectedIndicatorColour = GetComponent<Image>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void bikeDetectionStatus(bool bikeDetected){

	iDetectedABike = bikeDetected; // unnecessary if end up reading straight from source

	if(bikeDetected){bikeDetectedIndicatorColour.color = Color.green;}
		else{bikeDetectedIndicatorColour.color = Color.red;}
	}
}
