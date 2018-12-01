using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dropDownEventScript : MonoBehaviour {

private Dropdown thisDropDown;
private Dropdown[] dropDowns = new Dropdown[8];
private labelOurNumbersReOrderPinIndex reorder;
	// Use this for initialization
	void Start () {
		thisDropDown = GetComponent<Dropdown>();
		thisDropDown.onValueChanged.AddListener(delegate{DropdownValueChanged(thisDropDown);});
		dropDowns = this.transform.parent.gameObject.GetComponentsInChildren<Dropdown>();
		reorder =  this.transform.parent.gameObject.GetComponent<labelOurNumbersReOrderPinIndex>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void DropdownValueChanged(Dropdown change){
	Debug.Log("a drop down option changed");
	for(int i =0; i<8;i++){
	if((dropDowns[i] != thisDropDown) &&(dropDowns[i].value == thisDropDown.value)){dropDowns[i].value = 8;} //set those sharing number to blank
	}

	reorder.updateTheIndexAndImageLabels();
	}
}
