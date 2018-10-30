//need to turn off more constraints as now collision roughly working sinking on destruction isnt


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

//physics approach im not doing
//what in the game soaks up velocity maybe need make the air thick
//made the wood high friction
//get drag right
//if go back to using force could have constant backwards force increases with positive force and max and min velocity so cant constantly accelerate
//or could just increase resistance so as you reach 200% after 100% you have the force against you so if going 200% 100% any futrther output from you goes against you
//so if you put 100% force in 75% takes forward but 25% actual siphened off to go in opposite direction until the force you put in 50% goes into forward force 50% backwards and you no longer accelerate
//because are not adding to velocity 
//just put the forces on like two tanks tricks
//think the root motion of animator may have had an effect t
		//this doesnt seem to provide momentum - this game is fine without - but would be cool
		//currently would work however the root motion of the animation stops it - have taken it off so playing with speed drag and weight will work now
		//if wanted not to freeze in y still could try and see if animation keeps it at a set y (currently below water will need lower terrain)
		//root motion may affect this

public class MyPlayer : NetworkBehaviour  {

[Range( -10f,10f)] //cant reverse in game but may be useful for testing
public float forwardMultiplier =3.42f; //why does it have to be huge - boat only weighs 100kg at moment and drag it 1


[Range( 0f,1f)] //note that can go over 100% if cycle faster than the person who set their max at game beginning. so do not restrict on final game
public float cycPercSpeedLeft = 1f;
[Range (0f,1f)]
public float cycPercSpeedRight = 0.5f;
[Range (0f,200f)] //this is adding two percentage based on 100% is max set by rival players so could be higher also could divide it by 2 to make it 100 and put an if statement to avoid 0/2
private float forwardSpeed = 0f;
public float angularVel = 0f; //delete later this is just to help with adjustments
	[Range (0f,2f)] 
public float angVelMultiplier = 0.24f;

private Health health;


//public Vector3 leftCycVelAngle = new Vector3(1,0,-1); //because cyclist on the left is controlling rowwers on the left that make the boat go right 
//public Vector3 rightCycVelAngle = new Vector3(1,0,1); 
//private Vector3 boatVector = new Vector3(0,0,0);
//public GameObject GOSteerForcePos;
//public Vector3 steerForcePos; 
//private	Vector3 leftCycVelAngleNorm;
//private	Vector3 rightCycVelAngleNorm;


	// Use this for initialization
	void Start () {

	health = GetComponent<Health>();


	}
	void Update(){

	}
	// Update is called once per frame
	void FixedUpdate () {


	if(!isLocalPlayer){return;}
	if(health.currentHealth < 0){return;}   

    //could retag local player as local player but this may retag them for enemy too
    //need to find a way to tag personally and not globally


         //TODO restore this later so can use buttons
		//cycPercSpeedLeft = CrossPlatformInputManager.GetAxis("XZVector");
		//cycPercSpeedRight = CrossPlatformInputManager.GetAxis("X-ZVector");

				//just normalising but vectors tricky to code straight into something
		//leftCycVelAngleNorm = leftCycVelAngle.normalized;
		//rightCycVelAngleNorm = rightCycVelAngle.normalized;
		//leftCycVelAngle = leftCycVelAngleNorm;
		//rightCycVelAngle = rightCycVelAngleNorm;
		//was trying to do it with velocities not forces
		//Debug.Log("left " + cycPercSpeedLeft + "right " + cycPercSpeedRight );
		 // boatVector = (leftCycVelAngle*cycPercSpeedLeft+ rightCycVelAngle*cycPercSpeedRight).normalized;
       //boatVector = boatVector needs to fit to the current rotation?>
           //  MovePower=MovePowerRaw;
      // if(cycPercSpeedLeft > 0f || cycPercSpeedRight > 0f){
		//MovePower = MovePower*(1+(cycPercSpeedLeft+cycPercSpeedRight)/2 );//not acounting for angle as the angle wont change between the velocities will also mean turns at half speed
	//	}else {MovePower = 0;}
			//steerForcePos = GOSteerForcePos.transform.position;
		//Debug.Log(" steer force pos" +steerForcePos);
				//having added the force at the position it doesnt do anything other than add the force as a world component
		//should these angles allway be added to the angle of the ship before normalised
		//moves in the angle of world then spin
				//GetComponent<Rigidbody>().AddForceAtPosition(boatVelocityRaw*MovePower, steerForcePos, ForceMode.VelocityChange);
			//GetComponent<Rigidbody>().AddForce(boatVector*MovePower,ForceMode.Impulse);

			//was using impulse due to issues with colliders but maybe can set instead of adding

		forwardSpeed = (cycPercSpeedLeft+cycPercSpeedRight)*forwardMultiplier;
		angularVel = (cycPercSpeedLeft - cycPercSpeedRight)*angVelMultiplier;
		//torqueModifier = cycPercSpeedLeft - cycPercSpeedRight; //works but changing to velocity

//		Debug.Log(" forward " + Vector3.right*forwardSpeed*forwardMultiplier + " torque Mod " + torqueModifier + " torque " + torqueModifier*torqueMultiplier );

		//should it try multiplying by time.delta time on my forward speed as force should be and acceleration
		//changed from impulse
		//this.gameObject.GetComponent<Rigidbody>().AddRelativeTorque(0f, torqueModifier*torqueMultiplier,0f,ForceMode.VelocityChange);
		//this.gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.right*forwardSpeed*forwardMultiplier, ForceMode.VelocityChange);

		//though suppost to use forces like this better no accelerations
		//isnt local
		//needs code not to always give some percent forwards otherwise there will be no turning
		//actually should do above anyway

		//not sure it likes the  fixed update seems to jerk as turns
		this.gameObject.GetComponent<Rigidbody>().angularVelocity = transform.up*angularVel;
		this.gameObject.GetComponent<Rigidbody>().velocity = transform.right*forwardSpeed;

		 
	}

public override void OnStartLocalPlayer()
//here could retag privately i think as player1 - maybe things can have multiple tags
	{Camera[] cameras = this.transform.GetComponentsInChildren<Camera>();
	foreach(Camera camera in cameras){camera.enabled = true;}


				//Debug.Log(this.transform.childCount);
				//need to do this for first child and then get compents in childrens Transform to get all children
	//for(int i =0; i < this.transform.childCount; i++){this.transform.GetChild(i).gameObject.SetActive(true);}}
}
public void gettingMySettings(thisPlayerPairSettings cyclePair){





}

}
