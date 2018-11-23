using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setupBattleCamsRenderTextures : MonoBehaviour {

//if works down hierachy then im working left to right
private RawImage[] tacticalScreens;
private Camera[] tacticalCams;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void setupCamsRenderTexturesMethod(thisPlayerPairSettings ourShipsPlayerPairSettings){

	//putting the finding objects in here because found previously that the start wont necessarily run first
	//later on refactor maybe the setup void should be a constructor
	//also its just wiring up so why have it in trigger battle cams maybe own script - yep now it is

	//code assumes goes left right maybe thats sloppy though less intensive than string comparison

	//if works would be far neater if made playersettings use a string array going left right

	tacticalScreens = GetComponentsInChildren<RawImage>();
	tacticalCams = GetComponentsInChildren<Camera>();

	for(int i =0; i<2;i++){

		if(i==0){
			RenderTexture renderTexture = Resources.Load<RenderTexture>(ourShipsPlayerPairSettings.GetBattleCamRenderTextureLeft());
			tacticalCams[i].targetTexture = renderTexture;
			tacticalScreens[i].texture = renderTexture;
				}
			if(i==1){
				RenderTexture renderTexture = Resources.Load<RenderTexture>(ourShipsPlayerPairSettings.GetBattleCamRenderTextureRight());
				tacticalCams[i].targetTexture = renderTexture;
				tacticalScreens[i].texture = renderTexture;
					}


	}



	}
}
