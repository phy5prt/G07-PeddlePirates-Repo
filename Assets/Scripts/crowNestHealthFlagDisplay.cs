using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class crowNestHealthFlagDisplay : MonoBehaviour {

private Text[] flagTexts;
public Health healthToDisplay; // using public because the ai and the player do not share number of hierachy steps

	// Use this for initialization
	void Start () {

	flagTexts = GetComponentsInChildren<Text>();
	 // this is nasty because different levels in ai and player due to the holder that puts ai through 90 degree	
	 //maybe add a holder to player just a blank game object so hierachy shares number of levels
	//		Health = gameObject.transform.parent.parent.parent.GetComponentInChildren
	}
	
	// Update is called once per frame
	void Update () {

	foreach(Text flagText in flagTexts){flagText.text = healthToDisplay.currentHealth.ToString();}
		
	}
}
