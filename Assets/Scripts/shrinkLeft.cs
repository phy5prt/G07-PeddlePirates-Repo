using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shrinkLeft : MonoBehaviour {

[Range(0f,1f)]
[SerializeField] float shrinkToPercX = 0.3f;
[Range(1f,10f)]
[SerializeField] float shrinkSpeedX = 1f;
[SerializeField]  float shrinkABitX = 1f;
private float myOriginalScaleX;

[Range(0f,1f)]
[SerializeField] float shrinkToPercY = 0.75f;

[Tooltip("Do not set just visible to see")]
[SerializeField] float shrinkSpeedY = 1f; // should be private but i want to see it
[SerializeField]  float shrinkABitY = 1f;
private float myOriginalScaleY;


[SerializeField] bool shrink = false;


private RectTransform myRectTransform;
	// Use this for initialization


	void Start () {

	myRectTransform = GetComponent<RectTransform>();
	myOriginalScaleX = myRectTransform.localScale.x;	
	myOriginalScaleY = myRectTransform.localScale.y;		
	}
	
	// Update is called once per frame
	void Update () {

		if(shrink){shrinkLeftNowX();shrinkLeftNowY();}
		
	}

	public void startShrinking(){
	shrink=true;
	}

	private void shrinkLeftNowX(){

		if(myRectTransform.localScale.x > shrinkToPercX*myOriginalScaleX){
			//shrinkABitX = myRectTransform.localScale.x*(Time.deltaTime*(1/shrinkSpeedX));
			shrinkABitX = (Time.deltaTime*shrinkSpeedX);

	myRectTransform.localScale = new Vector3((myRectTransform.localScale.x-shrinkABitX),myRectTransform.localScale.y, myRectTransform.localScale.z);}
	else{shrink=false;}}


	private void shrinkLeftNowY(){

	//shrinkY needs to be at the speed of x so 
		//shrinkSpeedY = 1/(1-shrinkToPercY)/((1-shrinkToPercX)/shrinkSpeedX));
		shrinkSpeedY = (1-shrinkToPercY)/((1-shrinkToPercX)/shrinkSpeedX);

		if(myRectTransform.localScale.y > shrinkToPercY*myOriginalScaleY){
		//	shrinkABitY = myRectTransform.localScale.y*(Time.deltaTime*(1/shrinkSpeedY));
			shrinkABitY = (Time.deltaTime*shrinkSpeedY);
			myRectTransform.localScale = new Vector3(myRectTransform.localScale.x, (myRectTransform.localScale.y-shrinkABitY), myRectTransform.localScale.z);}
	}



	}

