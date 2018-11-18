using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoundBikeTickBox : MonoBehaviour {

private Toggle foundBiketoggle;
private FoundBikeIndicator foundBikeIndicator;

	// Use this for initialization
	void Start () {
		foundBiketoggle = GetComponent<Toggle>();
		foundBiketoggle.onValueChanged.AddListener(delegate {ValueChangeCheck(); });

		foundBikeIndicator = GetComponentInChildren<FoundBikeIndicator>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void ValueChangeCheck(){
	foundBikeIndicator.bikeDetectionStatus(foundBiketoggle.isOn);

	}
}
