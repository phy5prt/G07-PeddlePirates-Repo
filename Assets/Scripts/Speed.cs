//delete later this just to help calibrate

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speed : MonoBehaviour {

	private Text speedText;
	public GameObject ship;

	// Use this for initialization
	void Start () {
speedText = GetComponent<Text>();		
	}
	
	// Update is called once per frame
	void Update () {

	speedText.text = ship.GetComponent<Rigidbody>().velocity.magnitude.ToString();
		
	}
}
