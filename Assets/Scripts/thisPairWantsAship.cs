using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this code is fine however makes a decision on direct volt at a moment if find we get reading that are variable will need to take an average of last second and exclude the zeros or something

public class thisPairWantsAship : MonoBehaviour {

private Image shipSelectedIndicator;
private Color selectedColor;
public bool weWantAShip = false; // will assign directly to static in future

public float voltLeftForMyColor = 5f; //later will pull from a static of player settings
public float voltRightForMyColor = 5f;

private float voltMinActivation = 5f;

	// Use this for initialization
	void Start () {

	shipSelectedIndicator = GetComponent<Image>();
	selectedColor = shipSelectedIndicator.color;
		
	}
	
	// Update is called once per frame
	void Update () {

	if((voltLeftForMyColor>voltMinActivation) && voltRightForMyColor>voltMinActivation){

	shipSelectedIndicator.color = selectedColor;
	weWantAShip = true;

	}else{weWantAShip = false; shipSelectedIndicator.color = Color.red;}




		
	}
}
