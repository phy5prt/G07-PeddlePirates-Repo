using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PirateInstruction : MonoBehaviour {

private Text pirateText;
private Text gameInstructionText;

	public string[] pirateStrings;
	[SerializeField] string[] instructionStrings;

//for sliding
[SerializeField] bool slideOutNow = false;
[SerializeField] bool slideInNow = true;  




private RectTransform panelRT;
private Vector3 startRTLocPos;
private GameManager gameManager;

private bool resetingPirateText;

[SerializeField] float slideOutSpeed =300f;
[SerializeField] float slideInSpeed=300f;


	// Use this for initialization
	void Start () {

	gameManager = transform.parent.GetComponentInParent<GameManager>();
	pirateText = transform.GetChild(0).GetComponent<Text>();
	gameInstructionText = transform.GetChild(1).GetComponent<Text>();

	 
	
	panelRT = GetComponent<RectTransform>(); 
	startRTLocPos = panelRT.localPosition;

	}
	
	// Update is called once per frame
	void Update () {

	if(resetingPirateText){

	if(slideInNow){slideIn(slideInSpeed);}
	else if (slideOutNow)slideOut(slideOutSpeed);






	}else{resetingPirateText = false;}

		
	}

	public void updatePirateText(){resetingPirateText = true;slideInNow=true;}











	private void slideIn(float mySlideInSpeed){
		if(panelRT.localPosition.x>startRTLocPos.x - panelRT.rect.width*panelRT.localScale.x){
		panelRT.localPosition -= new Vector3((Time.deltaTime*slideOutSpeed),0f,0f);
		}else {slideInNow = false;
			pirateText.text = pirateStrings[gameManager.pirateSaysIndex];
			gameInstructionText.text = instructionStrings[gameManager.pirateSaysIndex]; 
			slideOutNow = true;}
		}

	private void slideOut(float mySlideOutSpeed){
		if(panelRT.localPosition.x<startRTLocPos.x){
		panelRT.localPosition += new Vector3((Time.deltaTime*slideInSpeed),0f,0f);
		}else{
			slideOutNow = false;

			}
	}
	}
