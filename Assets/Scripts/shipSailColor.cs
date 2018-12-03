using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipSailColor : MonoBehaviour {

private Shader sailColor;



	// Use this for initialization
	void Start () {
		sailColor = GetComponent<Shader>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void changeTheSails(thisPlayerPairSettings ourPPS){
	sailColor = Shader.Find(ourPPS.getShipPairColor());


	}

}
