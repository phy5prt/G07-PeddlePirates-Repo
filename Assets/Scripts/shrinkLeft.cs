using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enable max bars not working manually changing from not enabled to enabled also crashing not sure why
//for now its commented out
//its possibly due to canvases not being in canvas groups so need to research that

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

[SerializeField] float divideGizmosScaleYBy = 2f;
private selectorPBGizmo[] selectorGizmos;




	void Start () {


	setUpPanelShrink ();
	setUpGizmosYShrink();

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
		else{
				shrink=false;
				shrinkGizmosInstantly();
				//enableMaxBar();
		}}


	private void shrinkLeftNowY(){

	//shrinkY needs to be at the speed of x so 
		//shrinkSpeedY = 1/(1-shrinkToPercY)/((1-shrinkToPercX)/shrinkSpeedX));
		shrinkSpeedY = (1-shrinkToPercY)/((1-shrinkToPercX)/shrinkSpeedX);

		if(myRectTransform.localScale.y > shrinkToPercY*myOriginalScaleY){
		//	shrinkABitY = myRectTransform.localScale.y*(Time.deltaTime*(1/shrinkSpeedY));
			shrinkABitY = (Time.deltaTime*shrinkSpeedY);
			myRectTransform.localScale = new Vector3(myRectTransform.localScale.x, (myRectTransform.localScale.y-shrinkABitY), myRectTransform.localScale.z);}
	}



	void setUpPanelShrink ()
	{
		myRectTransform = GetComponent<RectTransform> ();
		myOriginalScaleX = myRectTransform.localScale.x;
		myOriginalScaleY = myRectTransform.localScale.y;
	}

	private void setUpGizmosYShrink(){

	selectorGizmos = GetComponentsInChildren<selectorPBGizmo>();   ///will this work
	Debug.Log("I've collected this many selectorgizmos = " + selectorGizmos.Length);


	}

	private void shrinkGizmosInstantly(){
	Debug.Log("in the shrink gizmo instantly method");
		foreach(selectorPBGizmo selector in selectorGizmos){
			Debug.Log("selector Gizmo start scale " + selector.gameObject.GetComponent<RectTransform>().localScale);
			selector.gameObject.GetComponent<RectTransform>().localScale = new Vector3(selector.gameObject.GetComponent<RectTransform>().localScale.x,(selector.gameObject.GetComponent<RectTransform>().localScale.y/divideGizmosScaleYBy),selector.gameObject.GetComponent<RectTransform>().localScale.z);
			Debug.Log("selector Gizmo changed scale " + selector.gameObject.GetComponent<RectTransform>().localScale);
	}
	}

	private void enableMaxBar(){

	MXSBCtagSloppy[] maxBarsSCs = GetComponentsInChildren<MXSBCtagSloppy>(true); //sloppy if it gets a script on a different tag use that
		foreach(MXSBCtagSloppy maxBar in maxBarsSCs){maxBar.gameObject.SetActive(true);}

	}

	//private void enlargePositionsSoStillAligned(){
	//GameObject asp = GameObject.Find("antiShrinkPositions");
	//	asp.GetComponent<RectTransform>().localScale = new Vector3(((asp.GetComponent<RectTransform>().localScale.x*100)/shrinkToPercX), asp.GetComponent<RectTransform>().localScale.y, asp.GetComponent<RectTransform>().localScale.z);
	//}

	}

