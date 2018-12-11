using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;
using UnityEngine.UI;

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
	//wonder if third person direction it is facing actually decided by one of its colliders
	//its a hack because there is an animator in both parent and children that i want and get in children searches in the gameobject too
	//there are two animators in the ship one for foam one for ship
	//im using the fact it just finds first in hierachy but its a sloppy way of doing it but dont really know good way to do it by which it choses the one with the right avitar
	animator = GetComponentInChildren<Animator>(); //due to navigation on the ai and not being able to align it with the boat the boat has to be on a -90 degree transform
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

//			Debug.Log( " Health is " + currentHealth + " I am " + name);

	
	}

	//animations would be better slowed alot

	private void DamagedShip(){
	damaged = true;
	animator.SetTrigger("Break");
	//animation for damaged
//Debug.Log("Damaged ship method triggered");
	}

	private void SinkShip(){
	//freeze controls and do ship sink animation and a camera zoom (if your the player not a bot)
		this.gameObject.tag = "Destroyed";
	sunk = true;
		animator.SetTrigger("Sink");
		Invoke("Sink",2f);
		//InvokeRepeating("Sink",2f,10f); // maybe just invoke
//Debug.Log("SinkShip method triggered");

	}

	private void Sink(){

		if(GetComponent<MyPlayer>() != null){
		GetComponent<MyPlayer>().ourShipsPlayerPairSettings.setAlive(false);
		GetComponentInChildren<Image>(true).enabled=true;
		GetComponentInChildren<LockCamera>().lockCamera();
		}//ideally would run all the methods except for tracking health in myplayer but because script shared with ai cant

	//Ai not sink maybe because part of it kinematic if changing this 
	//or because of its control could try turning these things off
	//cannons still shoot so they need destroying or script disabling
	//for the navigator to fall will need to deactivate the collider

	//think its colliding with navmesh and so sitting on the water once collider turned off it falls or if flicked on and off it falls
	//so need to set collision matric
	//could just make it not collide with navmesh as fixing y anyway?


	//disabling ai
		if(GetComponent<AICharacterControl>() != null){

			GetComponent<NavMeshAgent>().enabled=false;
			GetComponent<ThirdPersonCharacter>().enabled = false;
			GetComponent<AICharacterControl>().enabled = false;

		}
		//shouldnt have to disable them maybe through them disable the rigidbody
		/*
	GetComponent<SphereCollider>().enabled = false;
	GetComponent<CapsuleCollider>().enabled = false; 
	*/

		gameObject.layer = 10; //perfect!

	GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
	GetComponent<Rigidbody>().isKinematic = false; //doesnt seem to remember the gravity
	GetComponent<Rigidbody>().useGravity = true;

	Cannon[] cannons = GetComponentsInChildren<Cannon>();

		foreach(Cannon cannon in cannons){ 
		//cannon.gameObject.SetActive(false); //was this but shoots when dead
		cannon.stopShooting();} //trying this
	//CancelInvoke();
		
	}


}
