using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {

public int baseDamage = 10;
public string targetHit = " I dont Know what I hit ";
public int damage = 10; 	
public int bowDamageMultiplier = 10;

	private float timeCreated;
public float timePersist = 2f;

//not sure if this will still work as now only the cannon ball is trigger so can use colliders for impacts
//so relying on one to be hit the other to say where may not work so may need re coding

//need a little code doesnt do damage to ally
				
	// Use this for initialization
	void Start () {
		timeCreated = Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeSinceLevelLoad-timeCreated>timePersist){Destroy(gameObject);} // destroy cannon ball after has sun	
	}





	//doesnt work because called after the ship so maybe it needs to run a method to its target
	public void OnTriggerEnter(Collider coll){

	//messy because do these lines in both cannon and cannon ball really should make a shared method
	//if change one remember to change both
	if(coll.tag != "Ship" ){return;}     //is it a ship
	if(coll.gameObject.GetComponent<Health>() == null){return;} //is the collider one for taking damage


	if(transform.parent.transform.parent.transform.parent.transform.parent.gameObject.transform == coll.transform){return;}




	if(coll is SphereCollider){targetHit = "bow";}
		else if(coll is CapsuleCollider){targetHit = "boatBody";}
	if(targetHit == "bow"){bowDamageMultiplier = 10;}else{bowDamageMultiplier = 1;}

	damage = baseDamage*bowDamageMultiplier;
//	Debug.Log( damage + " Damage taken from " +name+ " hit to the" + coll.name + " on its " + targetHit);

			
			//having a lot of problems with this line causing errors is cannon ball destroy itself before it should
			//its hitting a collider without health
	coll.gameObject.GetComponent<Health>().OnHitReceived(damage);
	//do a special effect here an a noise

	Destroy(gameObject,1f);
}

	//private void OnTriggerEnter(CapsuleCollider boatBody){targetHit = "boatBody";}
}

