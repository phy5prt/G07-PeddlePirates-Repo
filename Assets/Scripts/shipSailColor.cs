using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipSailColor : MonoBehaviour {

private Material sailColor;



	// Use this for initialization
	void Start () {
		sailColor = GetComponent<SkinnedMeshRenderer>().materials[0];
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void changeTheSails(thisPlayerPairSettings ourPPS){


	string colorToFind = ourPPS.getShipPairColor();
	Debug.Log(colorToFind);

	//try number


	//try 1 didnt error but didnt do it either it set sail to instance
		Material fetchedMaterial = Resources.Load<Material>(colorToFind);
		//sailColor = fetchedMaterial;

		//then try as shaders on sail and material try 2
		//sailColor.shader = fetchedMaterial.shader; // caused not set to instance of an object
		//try 3
		Material material = GetComponent<SkinnedMeshRenderer>().material;
		//material = fetchedMaterial;

		//try4
		//material.shader = fetchedMaterial.shader;


		//next try set options 
		//then try setting the shader ot the madetial
		material.CopyPropertiesFromMaterial(fetchedMaterial);
	}

}
