using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeMyTxtSmallRelativeTo : MonoBehaviour {

//this has taken too much time for its importance at this point i need to focus minimal viable product and this is polish, when i need a demo

//it is complex to do but possible but not priority at the moment
//but it would be nice to get what the font is for the pirate instruction and set the
//best fit max to 2 less than that for instruction text, so it can still be smaller if it likes
//but can't be bigger
//maybe acheived it now another way

//an issue with code may be a scaling factor though they may share that scaling factor
//so may be able to get arround it if set like to like

//in inspector max size cannot be less than font size so if want to dodge scaling between the two
//would 


private Text myText;
private Text textToBeRelativeTo;
//private int txt2bRel2Size;
	public int txt2bRel2Size;
[SerializeField] float fontSizeDifference =2;

[SerializeField] string currentText;
	// Use this for initialization
	void Start () {


	myText = GetComponent<Text>();
	textToBeRelativeTo = transform.parent.GetChild(0).GetComponent<Text>();
		
	}
	
	// Update is called once per frame
	void Update () {

	//check if text has changed if it has best fits the text, if the best fit is too big resizes

	if(currentText!=myText.text){

	Debug.Log("txts dont match");
			currentText = myText.text;
			Debug.Log("setting txts to match");
			ReizeTextRelatively();

	}
		
	}

	private void ReizeTextRelatively(){


		myText.resizeTextForBestFit = true;


		//first time this is run the text wont have changed in the pirate text and so the cache will be empty returning zero
		//this will be overcome because game manager can be set to apply the index0 text instead of it being preloaded

		txt2bRel2Size = textToBeRelativeTo.cachedTextGenerator.fontSizeUsedForBestFit;
		Debug.Log("I think pirate instruction is this size = " + txt2bRel2Size + " my font size is " + myText.fontSize + " best fit size is " + myText.cachedTextGenerator.fontSizeUsedForBestFit); //is this using the font size from the best fit
		//if(txt2bRel2Size - fontSizeDifference < myText.fontSize){
		if(txt2bRel2Size - fontSizeDifference < myText.cachedTextGenerator.fontSizeUsedForBestFit){

		Debug.Log("Reseting my font size and turning off best fit because .... txt2bRel2Size - fontSizeDifference + 1 = " +(txt2bRel2Size - fontSizeDifference + 1) + " and thats less than my font size -2. My font size is " + myText.fontSize);
		myText.resizeTextForBestFit = false;
		myText.fontSize  =  txt2bRel2Size-2;} //this is why setting best fit max would be ideal because this text size is not the same as the best fit one i think due to scaling


		//okay this is funky but by setting it to zero i think i set the max to the font size
		//however on testing txt2bRelSize is often out by 1 to 3 for font im using which makes me think scaling factor
		//from rect transforms
		//could do a while loop that keep going through until the font size reduced a bit arbitarily till it is less
		//sloppy though
		//myText.resizeTextMaxSize = 0;
		//myText.resizeTextForBestFit = True;

	}
}
