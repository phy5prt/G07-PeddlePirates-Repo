using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showSliderValue : MonoBehaviour { //needs renaming

	private Slider aISlider;
	private Text numberAITxt;
	private int numberSpawnPoints = 22 , numberOfPlayerPairs;
	private usePairSelectable[] pairsSelected; // in future may just get straight from elsewhere

	// Use this for initialization
	void Start () {
	pairsSelected = transform.parent.gameObject.GetComponentsInChildren<usePairSelectable>();

	aISlider = GetComponent<Slider>();

	aISlider.onValueChanged.AddListener(delegate {	{ValueChangedcheck();}   });

	numberAITxt = GetComponentInChildren<Text>();
	numberAITxt.text = aISlider.value.ToString();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ValueChangedcheck(){ //careful this needs to be run if the max is reduced by increasing the no.players
		numberOfPlayerPairs = 0;
		foreach(usePairSelectable pair in pairsSelected){if (pair.myToggle.isOn == true){numberOfPlayerPairs++;}}


	aISlider.maxValue = numberSpawnPoints - numberOfPlayerPairs;
	numberAITxt.text = aISlider.value.ToString();
		GameManager.numberAIs = (int) aISlider.value; //messy especially as later want to allocate more complex info 

		//doing this here instead of just updating when i hit the buttton 
		//seems wastyeful however if want to do it when i hit the buttom would need both be static, 
		//which seems fine as only one of each but trying to refrain from statics as supposedly its bad practice 
		//though not sure why as disadvantages listed are the features im using it for
	}


}
