using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiRandomNumberGenerator : MonoBehaviour {

//i want to make a random number that create another random number near to itself
//with a bias i can alter so they move up and down


//think it leaks up but hmm 

[Range( 2,100)]
	[SerializeField] int roundnessBellCurveInRNAddedRange= 2;

[Range( -1f,1f)]
	[SerializeField] float directionBiasOfRn = 0f;



[Range( 0f,200f)]
public float theRandomNumber;

[Range( 0f,200f)]
[SerializeField] float tRNCeiling = 120f;

[Range( 0f,1f)]
	[SerializeField] float startingPlace = 0.6f;

[Range( 0f,1000f)]
	[SerializeField] float sensitivityToRateOfUpdate = 10; // to balance Time.delta time too 

private float rNAdded;

[Range( 0f,0.1f)]  // so if set to ).2f that means if direction of bias is 1 the max increase step will be 20% of current position with bell point at 10%
	[SerializeField] float rNAddedRange = 0.1f;
private float	rNAddedRangeLow;
private float	rNAddedRangeHigh;
	// Use this for initialization
	void Start () {
	theRandomNumber = startingPlace;	
	}
	
	// Update is called once per frame
	void Update () {

	//get bell curve - will start to form as move away from 0 being the middle point in the randomsRange



		theRandomNumber = createRandomNumber ();
		//TODO graph this! (RandomNumber)
	}

	private float makeRangeBell ()
	{
	float rangeBell = 0;
		for(int i = 0; i <roundnessBellCurveInRNAddedRange; i++){
			rangeBell += Random.Range (rNAddedRangeLow, rNAddedRangeHigh) /roundnessBellCurveInRNAddedRange ;
		}

		//TODO graph this!
		return rangeBell;
	}

	private float createRandomNumber ()
	{
		rNAddedRangeLow = -1*rNAddedRange + directionBiasOfRn * rNAddedRange;
		rNAddedRangeHigh = +rNAddedRange + directionBiasOfRn * rNAddedRange;
		rNAdded = makeRangeBell ();
		theRandomNumber = theRandomNumber + rNAdded * Time.deltaTime * sensitivityToRateOfUpdate;
		//catching it getting out of bound
		if (theRandomNumber < 0) {
			theRandomNumber = 0;
		}
		else
			if (tRNCeiling < theRandomNumber) {
				theRandomNumber = tRNCeiling;
			}
		return theRandomNumber;
	}
}
