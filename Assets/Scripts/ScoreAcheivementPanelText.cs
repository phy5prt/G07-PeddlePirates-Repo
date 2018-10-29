using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAcheivementPanelText : MonoBehaviour {

//is x the right direction?

public bool slideOutNow = false; //remove once using inputs no input for x 2 minutes slide out
public bool slideInNow = true;  //remove once using inputs any input slide in

//private Transform startPosition;
private RectTransform panelRT;
private Vector3 startRTLocPos;

	public float slideOutSpeed =150f;
	public float slideInSpeed=300f;

	// Use this for initialization
	void Start () {
		//startPosition = transform;
		panelRT = GetComponent<RectTransform>(); // i want to save the values
		startRTLocPos = panelRT.localPosition;
	}
	
	// Update is called once per frame
	void Update () {


		if(slideInNow){slideOutNow=false;
	slideIn(slideInSpeed);}

	if(slideOutNow ==true && slideInNow ==true){slideOutNow = false;} //shouldnt be necessary

		if(slideOutNow){slideOut(slideOutSpeed);}
	//no input slide out

	//input slide in
		
	}

	private void slideOut(float slideOutSpeed){

		//while(panelRT.localPosition.x>startRTLocPos.x - panelRT.rect.width/panelRT.localScale.x && slideInNow==false){panelRT.localPosition -= new Vector3((Time.deltaTime*slideOutSpeed),0f,0f);}//trying divide by local scale as think its affecting width
		//while loop doesnt work because does the while then feeds out the change.

		if(panelRT.localPosition.x>startRTLocPos.x - panelRT.rect.width*panelRT.localScale.x && slideInNow==false){

		panelRT.localPosition -= new Vector3((Time.deltaTime*slideOutSpeed),0f,0f);
		//Debug.Log("finished sliding out");

		}else {slideInNow = false; slideOutNow = false;}

	

	}

	private void slideIn(float slideInSpeed){

		if(panelRT.localPosition.x<startRTLocPos.x){

		panelRT.localPosition += new Vector3((Time.deltaTime*slideInSpeed),0f,0f);
		//Debug.Log("finished sliding In");

		//someDebugs();

	}else{slideInNow = false;}
	}




		private void someDebugs(){

		Debug.Log("startRTLocPos = " + startRTLocPos);
	Debug.Log("panelRT.localPosition.x = " + panelRT.localPosition.x);
		Debug.Log("panelRT.sizeDelta.x = " + panelRT.sizeDelta.x);


	}
	
}
