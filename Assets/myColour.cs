using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myColour : MonoBehaviour {

public Material myMaterial;
public Color myColourCol;
public bool updateColour = true;
	// Use this for initialization
	void Start () {


		
	}
	
	// Update is called once per frame
	void Update () {

		if(updateColour){updateColour = false; updateColourNow();}
		
	}

private void updateColourNow(){

myMaterial.color = myColourCol;


}
}
