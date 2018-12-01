using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class labelOurNumbersReOrderPinIndex : MonoBehaviour {

	//https://www.youtube.com/watch?v=LRoqGsJGgA4
	//https://gamedev.stackexchange.com/questions/115292/how-do-i-remove-specfic-optiondata-from-dropdown


	//this code will change the images next to the option
	//this script used multiple times
	//would be nice if we had all drop downs pulling from one template and update that
	//except the selected value that goes at the top

	//should the holding object have these as an array and be triggered when they change to collect all there values
	//and staticly run the method for updating the array

	public Sprite[] bikeIcons = new Sprite[9];
	private int[] selectedOption = new int[9];
	private Sprite[] newOptionsSprites= new Sprite[9]; //should it be 9 with the blank
	private Dropdown[] dropDowns;
	private List<Dropdown.OptionData> dropDownOptionsTxtsSprites;

	void Start(){

	dropDowns = GetComponentsInChildren<Dropdown>();
	Debug.Log("I found " +dropDowns.Length+ " dropdowns");

		updateTheIndexAndImageLabels();
	}


	public void updateTheIndexAndImageLabels(){

	//needs to find all the options in each  drop down and change 
	//if arduino static could directly send the need index array
	//apply images 

	//selected opt

	//list of the value on all 8 drop downs
	for(int i =0; i<8; i++){selectedOption[i] = dropDowns[i].value;}

	//make all the sprites to the blank incon
	for (int i =0; i<9; i++){newOptionsSprites[i] = bikeIcons[8];}

	//reorder sprites based on the value selected in drop downs but dont want to change blank so 8
	for(int i =0; i<9; i++){
			if(selectedOption[i]==8){newOptionsSprites[selectedOption[i]]=bikeIcons[8];}else{
	newOptionsSprites[selectedOption[i]]=bikeIcons[i];}}




	//clearing the list and remaking it
	foreach(Dropdown drop in dropDowns){ drop.options.Clear();}
	dropDownOptionsTxtsSprites = new  List<Dropdown.OptionData>();

		//do i have to just make new list not assign
	//options. = newOptionsSprites[i];

	//making an option
	//then adding the list
	for(int j = 0; j<9; j++){
		Dropdown.OptionData option = new Dropdown.OptionData(j.ToString(), newOptionsSprites[j]);
		dropDownOptionsTxtsSprites.Add(option);
		}
	foreach(Dropdown drop in dropDowns){ drop.AddOptions(dropDownOptionsTxtsSprites);}
	}
		




	
}
