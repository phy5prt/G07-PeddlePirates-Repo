﻿//need to turn off more constraints as now collision roughly working sinking on destruction isnt


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


/*current issue is it is tipping over could try this
Try freezing the rotation manually in the LateUpdate

 protected void LateUpdate()
 {
     transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);
 }
 */
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

public class MyPlayer : MonoBehaviour  {

	public thisPlayerPairSettings ourShipsPlayerPairSettings;
	private float ourVolt100PercMax = 1000000;
	private setupBattleCamsRenderTextures setupBattleCams;

[Range( -10f,50f)] //cant reverse in game but may be useful for testing
[SerializeField] float forwardMultiplier =3.42f; 

/* - this really handy way of controlling the ships for testing and knowing the ranges
[Range( 0f,1f)] //note that can go over 100% if cycle faster than the person who set their max at game beginning. so do not restrict on final game
public float cycPercSpeedLeft = 1f;
[Range (0f,1f)]
public float cycPercSpeedRight = 0.5f;
*/
//[Range (0f,200f)] //this is adding two percentage based on 100% is max set by rival players so could be higher also could divide it by 2 to make it 100 and put an if statement to avoid 0/2
private float forwardSpeed = 0f;
private float angularVel = 0f; 
	[Range (0f,10f)] 
[SerializeField] float angVelMultiplier = 0.24f;
[SerializeField] float backwardSpeed = -1;

private Health health;
private Rigidbody myRigidBody;

private bool shipBeenSetUp = false; //feel shouldnt need this

//public Vector3 leftCycVelAngle = new Vector3(1,0,-1); //because cyclist on the left is controlling rowwers on the left that make the boat go right 
//public Vector3 rightCycVelAngle = new Vector3(1,0,1); 
//private Vector3 boatVector = new Vector3(0,0,0);
//public GameObject GOSteerForcePos;
//public Vector3 steerForcePos; 
//private	Vector3 leftCycVelAngleNorm;
//private	Vector3 rightCycVelAngleNorm;

private Camera myMainCamera;
	// Use this for initialization
	void Start () {
	//is this the object not found!
	myMainCamera = GetComponentInChildren<Camera>(); // the boat camera has cameras in its GO assuming wont find the wrong one,should take shallowest
	health = GetComponent<Health>();
		myRigidBody = this.gameObject.GetComponent<Rigidbody>();

	}

	public void applyPlayerSettingsToShip(thisPlayerPairSettings shipsPlayerPairSettings){
	//	Debug.Log("instantiated player about to received their this player pair settings");
		ourShipsPlayerPairSettings = shipsPlayerPairSettings;

		//set battle cams to their render textures
		setupBattleCams = GetComponentInChildren<setupBattleCamsRenderTextures>();
		setupBattleCams.setupCamsRenderTexturesMethod(ourShipsPlayerPairSettings);


		//set color
		//set team number () - public so spawner can read it - and apply it to physics collision matrix sorting layers
//		Debug.Log(" orvolt100perc before received settings is " + ourVolt100PercMax);
		ourVolt100PercMax = ourShipsPlayerPairSettings.GetVolt100Perc();
//		Debug.Log(" orvolt100perc after received settings is " + ourVolt100PercMax);
		//view port
		//Rect splitScreen = ourShipsPlayerPairSettings.GetSplitScreenArea();

	//	myMainCamera.rect.Set() ; //not sure if this need word new should because its like a vector but then goes red so dunno
	//most be a better way
	//could just instead of rect have a float array but seems dumb
	//dumb to to have to pull each float out of rec
		//is the issue actually how i pass the info
		//okay so above may not be the problem so on refactor maybe rect can be passed better
		//the issue was this code running before the start code
		//i thought start was like a constructor but it isnt
		//maybe instead of start this code should require a constructor

		myMainCamera = GetComponentInChildren<Camera>();
	//	Debug.Log("my main camera " + myMainCamera + " my main camera rect " + myMainCamera.rect );
		myMainCamera.rect = new Rect(ourShipsPlayerPairSettings.GetSplitScreenArea().x,ourShipsPlayerPairSettings.GetSplitScreenArea().y,ourShipsPlayerPairSettings.GetSplitScreenArea().width,ourShipsPlayerPairSettings.GetSplitScreenArea().height);
	//	Debug.Log("confirming that " + tag + " has been set up so it should know its volt input ");
	//give the ship spray its powerbars
		percBarDisplay[] foams = GetComponentsInChildren<percBarDisplay>();
		foreach(percBarDisplay bar in foams){bar.passMeMyPlayerPairSettings(ourShipsPlayerPairSettings);}


		//set ship sail colour
		GetComponentInChildren<shipSailColor>().changeTheSails(ourShipsPlayerPairSettings);

		shipBeenSetUp = true;


	}

	void Update(){

	}
	// Update is called once per frame
	void FixedUpdate () {


	if(shipBeenSetUp != true){return;} // needs removing but cant till got allocated screens //ive removed !isLocalPlayer
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

		forwardSpeed = (ourShipsPlayerPairSettings.GetmyLeftVolt() + ourShipsPlayerPairSettings.GetmyRightVolt() )*forwardMultiplier/ourVolt100PercMax; //soemtime get not set to instance of object wonder if to do with death
		if(forwardSpeed<=0){forwardSpeed = backwardSpeed;}
		angularVel = (ourShipsPlayerPairSettings.GetmyLeftVolt() - ourShipsPlayerPairSettings.GetmyRightVolt())*angVelMultiplier/ourVolt100PercMax;
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
		//angular velocity could be what making it fall over
		//testing below
		//could change boat angle with deltatime
		//this.gameObject.GetComponent<Rigidbody>().angularVelocity.Set(0,angularVel,0);



		//im getting every frame will be faster if put in start


		myRigidBody.angularVelocity = transform.up*angularVel;
		myRigidBody.velocity = transform.right*forwardSpeed;

		//local or world
	
	//are both necessary - seems to work now with out either the only other change was in the start grabbing the rigidbody
	//	transform.up.Set(0f,1f,0f); - works just with this

	//	transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
		 
	}
/* - think this now unnecessary
public override void OnStartLocalPlayer() //just delete
//here could retag privately i think as player1 - maybe things can have multiple tags
	{Camera[] cameras = this.transform.GetComponentsInChildren<Camera>();
	foreach(Camera camera in cameras){camera.enabled = true;}


}

*/
}
