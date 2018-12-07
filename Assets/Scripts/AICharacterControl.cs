using System;
using UnityEngine;


//using System.Collections; // i added
//using System.Collections.Generic;// i added

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]

     public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;   //if i feed into this will it update as gameobject moves  (probs method resets it)                               // target to aim for
        public Vector3 relToTargAttckPos = new Vector3(20f,0f,0f);

		[Tooltip("Woody to completely stop AI ramming will need to but a nav mesh avoid object on the ships set the ship to ignore its own which is computationally expensive")]
          public bool ram = false; // if not ram will circle

		[Tooltip("Woody it only works when not ramming")]
          public float circlingRadius = 20f;
		[Tooltip("Woody this multiplied by the circRadius is the distance away before starts trying to circle")]
		public float multiplesOfCRTostartCirc = 1.5f;
	
		[Tooltip("Woody This is the angle added to the angle between target and enemy position it is added so enemy never reaches its location but circles")]
          public float movingAnglePerSecond = 0.2f; //should this be less
		//public Vector3 relToTargAttckrot = new Vector3(0f,180f,0f);
       // private Transform adjustmentAttackPos;
		[Tooltip("Woody if you want to preset which is first point on circle starts circling")]
		public float degreesAroundCirc = 1f;



        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>(); //seems to be finding it though it is not in children
            character = GetComponent<ThirdPersonCharacter>();
		

	        agent.updateRotation = false;  //what happens if this is true
	        agent.updatePosition = true;
        }




     

        private void Update()//do i need to run set target method in the update
        {
		
            if (target != null){
		

			if(ram){agent.SetDestination(target.position );}else{agent.SetDestination(target.position + relToTargAttckPos);}
			if(!ram && agent.remainingDistance < circlingRadius*multiplesOfCRTostartCirc){

			//to circling codes


				//moving anglepersecond needs to be multiplied by normal speed of ship so that dont need to change it everytime adjust speed of ai in testing
				//or if different speed boats
				//would be nice if instead traced an arc infront of the bow and that it chose to start its arc at the nearest part of the arc
				//transect least distance math
				//but gonna leave it for now as should prioritise as tweeks could be inifinte and i want a one person made game that can be played and enjoy for
				//20 minutes and may not be played again by same people
				 
	degreesAroundCirc = degreesAroundCirc +  movingAnglePerSecond*Time.deltaTime;
				float posX = Mathf.Cos(degreesAroundCirc)*circlingRadius;
				float posZ = Mathf.Sin(degreesAroundCirc)*circlingRadius;
				Vector3 posCircular = new Vector3(posX,target.position.y,posZ);
				Vector3 posAroundTarget = posCircular  + target.position;
//				Debug.Log("x = " + posX + "z= " + posZ + "posAroundTarget= " + posAroundTarget);

					agent.SetDestination(posAroundTarget);
	
				
				

			

			}
//we want to go for ahead of the boat until 

            if (agent.remainingDistance > agent.stoppingDistance)
                character.Move(agent.desiredVelocity, false, false);
            else
                character.Move(Vector3.zero, false, false);
                if(ram){relToTargAttckPos = relToTargAttckPos*-1;}else{

                }
        }
        }

        public void SetTarget(Transform target) // call this i think when i need target updating nope this is so can reference the script
        {
            this.target = target;
        }


    }
}
