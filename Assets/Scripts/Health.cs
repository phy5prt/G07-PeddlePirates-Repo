using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//there is a sink animation and some other could run this when die
//there is break also for when they take half damage

public class Health : MonoBehaviour {

public int startHealth = 1000;
public int currentHealth =1000;
private Animator animator;
private bool damaged = false;
private bool sunk = false;

//public int standardDamage = - or should we just pull damage of projectile
//so we can increase it based on cycling

	// Use this for initialization
	void Start () {
	animator = GetComponent<Animator>();
	currentHealth = startHealth;
		
	}
	
	// Update is called once per frame
	void Update () {


	//they need bools so that these methods get called only once
	if(currentHealth<0 && !sunk){
	SinkShip();} else if(currentHealth<startHealth/2 && !damaged){DamagedShip();} 

		
	}

	//is it the collider that hits on the collider that receives
	//hard to do but could pull it off the canonball own ontrigger enter.
	public void OnHitReceived(int damage){

			currentHealth -= damage;

			Debug.Log( " Health is " + currentHealth);

	
	}

	//animations would be better slowed alot

	private void DamagedShip(){
	damaged = true;
	animator.SetTrigger("Break");
	//animation for damaged
Debug.Log("Damaged ship method triggered");
	}

	private void SinkShip(){
	//freeze controls and do ship sink animation and a camera zoom (if your the player not a bot)
		this.gameObject.tag = "Destroyed";
	sunk = true;
		animator.SetTrigger("Sink");
		InvokeRepeating("Sink",2f,10f);
Debug.Log("SinkShip method triggered");

	}

	private void Sink(){

	//Ai not sink maybe because part of it kinematic if changing this 
	//or because of its control could try turning these things off
	//cannons still shoot so they need destroying or script disabling
	//for the navigator to fall will need to deactivate the collider

	GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
	GetComponent<Rigidbody>().isKinematic = false;
	Cannon[] cannons = GetComponentsInChildren<Cannon>();
	foreach(Cannon cannon in cannons){cannon.gameObject.SetActive(false);}
	CancelInvoke();

	}


}
