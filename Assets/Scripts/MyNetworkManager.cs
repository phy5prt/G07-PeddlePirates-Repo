using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : NetworkManager {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void MyStartHost(){
	Debug.Log(Time.timeSinceLevelLoad + " Starting Host");
	StartHost();
	}

	public override void OnStartHost(){
	Debug.Log(Time.timeSinceLevelLoad + " Host Started");


	}

	public override void OnStartClient(NetworkClient myClient){
	Debug.Log(Time.timeSinceLevelLoad + " client start requested");
	InvokeRepeating("PrintDot",0,1.0f);
	}

	void PrintDot(){
	Debug.Log(".");
	}

	public override void OnClientConnect(NetworkConnection conn){
	CancelInvoke();
	Debug.Log(Time.timeSinceLevelLoad + " Client is connected to IP: " + conn.address);

	}
}
