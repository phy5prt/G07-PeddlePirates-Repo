using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class labelOurNumbersReOrderPinIndex : MonoBehaviour {

	//this code will change the images next to the option
	//this script used multiple times
	//would be nice if we had all drop downs pulling from one template and update that
	//except the selected value that goes at the top

	//should the holding object have these as an array and be triggered when they change to collect all there values
	//and staticly run the method for updating the array

	public Sprite[] bikeIcons;
	private int[] selectedOption;
	private Sprite[] newOptionsSprites;
	private Dropdown[] dropDowns;

	void Start(){

	dropDowns = GetComponentsInChildren<Dropdown>();


	}


	public void updateTheIndexAndImageLabels(){

	//needs to find all the options in each  drop down and change 
	//if arduino static could directly send the need index array
	//apply images 

	//selected opt

	for(int i =0; i<8; i++){selectedOption[i] = dropDowns[i].value;}


	for (int i =0; i<8; i++){newOptionsSprites[i] = bikeIcons[8];}

	for(int i =0; i<8; i++){newOptionsSprites[selectedOption[i]]=bikeIcons[i];}

	for(int i = 0; i<8; i++){
	for(int j = 0; j<8; j++){

	//do i have to just make new list not assign
	//dropDowns[i].; //options. = newOptionsSprites[i];


				//https://www.youtube.com/watch?v=LRoqGsJGgA4
	}
	}
		//https://gamedev.stackexchange.com/questions/115292/how-do-i-remove-specfic-optiondata-from-dropdown





	}
}
