using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showSliderValue : MonoBehaviour {

	private Slider aISlider;
	private Text numberAITxt;

	// Use this for initialization
	void Start () {

	aISlider = GetComponent<Slider>();

	aISlider.onValueChanged.AddListener(delegate {	{ValueChangedcheck();}   });

	numberAITxt = GetComponentInChildren<Text>();
	numberAITxt.text = aISlider.value.ToString();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ValueChangedcheck(){

	numberAITxt.text = aISlider.value.ToString();

	}


}
