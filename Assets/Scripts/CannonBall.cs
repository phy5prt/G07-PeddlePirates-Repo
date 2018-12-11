using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {

public int baseDamage = 1;
private string targetHit = " I dont Know what I hit ";
private int damage = 1; 	
private int damageMultiplier = 10;
private bool playerLocalCannonBall = false;
private AudioSource audioSource;
public GameObject impartedFire;

[SerializeField] AudioClip splosh;
[SerializeField] AudioClip explosion;
[SerializeField] AudioClip launchSound;
[SerializeField] float myVolume = 0.5f;
 //private AudioSource audioSource; //do i need this not using the instance

private float timeCreated;
private float timePersist = 1.1f;

//not sure if this will still work as now only the cannon ball is trigger so can use colliders for impacts
//so relying on one to be hit the other to say where may not work so may need re coding

//need a little code doesnt do damage to ally
				
	// Use this for initialization
	void Start () {
		timeCreated = Time.timeSinceLevelLoad;
	//	audioSource = GetComponent<AudioSource>(); dont know why cant use this
	

	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeSinceLevelLoad-timeCreated>timePersist){Destroy(gameObject);} // destroy cannon ball after has sun	
		if(playerLocalCannonBall && (gameObject.transform.position.y < 0f) ){
			playerLocalCannonBall=false;



		//	audioSource = AudioSource.PlayClipAtPoint(splosh, transform.position) ; //cant grab it like this
			PlayClipAt(splosh, transform.position);
			//redo below

		}
	}

	public void setAsPlayerLocalCannonBall(){
	playerLocalCannonBall=true;




	//AudioSource.PlayClipAtPoint(launchSound, transform.position);
		PlayClipAt(launchSound, transform.position);
	//play launch sound

	}



	//doesnt work because called after the ship so maybe it needs to run a method to its target
	public void OnTriggerEnter(Collider coll){

	//messy because do these lines in both cannon and cannon ball really should make a shared method
	//if change one remember to change both
	if(coll.tag != "Ship" ){return;}     //is it a ship
	if(coll.gameObject.GetComponent<Health>() == null){return;} //is the collider one for taking damage

	//this isnt working because dont use own tranform anymore but if cannons well placed should have to check if shooting self
//	if(transform.parent.transform.parent.transform.parent.transform.parent.gameObject.transform == coll.transform){return;}


		Instantiate(impartedFire,this.gameObject.transform.position, Quaternion.identity, coll.gameObject.transform);
		Debug.Log("should just of instantiated fire");

	if(coll is SphereCollider){targetHit = "bow";}
		else if(coll is CapsuleCollider){targetHit = "hull";}else if(coll is BoxCollider){targetHit = "sails";}
		if(targetHit == "bow"){damageMultiplier = 60;}else if(targetHit == "hull"){damageMultiplier = 4;}else if(targetHit == "sails"){damageMultiplier = 1;}

	damage = baseDamage*damageMultiplier;
	Debug.Log(damage);
//	Debug.Log( damage + " Damage taken from " +name+ " hit to the" + coll.name + " on its " + targetHit);

			
			//having a lot of problems with this line causing errors is cannon ball destroy itself before it should
			//its hitting a collider without health
	coll.gameObject.GetComponent<Health>().OnHitReceived(damage);
	//do a special effect here an a noise



	//here could have an if for if hitting a player makes a sound
	//not sure how would do if ai hit but by player makes a sound
		if(playerLocalCannonBall || (coll.gameObject.GetComponent<MyPlayer>() != null))
		{	
			PlayClipAt(explosion, transform.position);}	
		//AudioSource.PlayClipAtPoint(explosion, transform.position);}

		if(coll.gameObject.GetComponent<MyPlayer>() != null){

			//paticle effect at the point too amd connected to the transform put it on everything

		}





		Destroy(gameObject);
}
	private AudioSource PlayClipAt(AudioClip clip, Vector3 pos){

	//so need to put in here how im going to make it global

   GameObject tempGO = new GameObject("TempAudio"); // create the temp object
   tempGO.transform.position = pos; // set its position
   AudioSource aSource = tempGO.AddComponent<AudioSource>(); // add an audio source
   aSource.clip = clip; // define the clip
   // set other aSource properties here, if desired

   //must be a better way
   aSource.volume = myVolume;
   aSource.minDistance=500f;
	aSource.maxDistance=500f;

   aSource.Play(); // start the sound
   Destroy(tempGO, clip.length); // destroy object after clip duration
   return aSource; // return the AudioSource reference
 }
	//private void OnTriggerEnter(CapsuleCollider boatBody){targetHit = "boatBody";}
}

