using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectorPBGizmo : MonoBehaviour { // may work once we have singleton - pretty sure this is issue

	// Use this for initialization
	void Start () {
	//Debug.Log("seeing whether gizmo should exist" + " my tag is " + tag);
	foreach(thisPlayerPairSettings pair in GameManager.shipPlayerSettingsAr){
	//Debug.Log(pair.getShipPairColor());
	if(pair.getShipPairColor() == tag){

	//Debug.Log("found my colour " + " and my pair is set to " + pair.getBikePairSetAsAvailableOnEventSetup());

	this.gameObject.SetActive(pair.getBikePairSetAsAvailableOnEventSetup());}}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
