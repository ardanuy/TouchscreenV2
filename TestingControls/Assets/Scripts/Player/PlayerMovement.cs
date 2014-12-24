using UnityEngine;
using System.Collections;

// Require these components when using this script
[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (Rigidbody))]

public class PlayerMovement : MonoBehaviour {

	
	private PlayerAction playerAction; // reference for PlayerAction class

	public float minDistanceFromThePlayerFeetToTheGround = 0.1f;  // minimum distance from the player's feet to the ground, allow us to determine if player is on the air
	public float minDistanceFromThePlayerCenterToTheGround = 2f;    // minimum distance from the player' center to the ground, allow us to determine if player is falling from hight places
	public float turnSmoothing = 10f;             // a public setting to control how fast the player rotates around while in idle state
	public float turnInputSmoothing = 10f;        // a public setting to control how fast the player rotates around while in idle state

	public float animSpeed = 1.5f;				// a public setting for overall animator animation speed
	public float lookSmoother = 3f;				// a smoothing setting for camera motion

	private Animator anim;							// a reference to the animator on the character
	private AnimatorStateInfo currentBaseState;			// a reference to the current state of the animator, used for base layer
	private CapsuleCollider col;					// a reference to the capsule collider of the character

	private RaycastHit hitInfo;                    // a raycasthit to help us find where the player is


	// player Animator Hashs

	static int idleState = Animator.StringToHash("Base Layer.Idle");	
	static int walkState = Animator.StringToHash("Base Layer.Walk");            
	static int runState = Animator.StringToHash("Base Layer.Run");

	// touch signals
	private float speed = 0f;
	private bool sprint = false;
	private bool jump = false;


	
	/*
	 * Awake is called when the script instance is being loaded. 
	 * Awake is used to initialize any variables or game state before the game starts. 
	 * Awake is called only once during the lifetime of the script instance. 
	 * Awake is called after all objects are initialized so you can safely speak to other objects 
	 * or query them using eg. GameObject.FindWithTag. 
	 * Each GameObject's Awake is called in a random order between objects. 
	 * Because of this, you should use Awake to set up references between scripts, 
	 * and use Start to pass any information back and forth. 
	 * Awake is always called before any Start functions. 
	 * This allows you to order initialization of scripts. 
	 * Awake can not act as a coroutine.
	*/
	void Awake()
	{
		// initialising reference variables
		anim = this.GetComponent<Animator> ();
		col = GetComponent<CapsuleCollider>();
		anim.speed = animSpeed;								// set the speed of our animator to the public variable 'animSpeed'

		hitInfo = new RaycastHit();

		// reference to PlayerAction class
		playerAction = this.GetComponent<PlayerAction> ();

	} // end of Awake


	/*
	 * This function is called every fixed framerate frame, if the MonoBehaviour is enabled. 
	 * FixedUpdate should be used instead of Update when dealing with Rigidbody. 
	 * For example when adding a force to a rigidbody, you have to apply the force every fixed frame inside FixedUpdate instead of every frame inside Update. 
	 */
	void FixedUpdate()
	{

		MainMovementManagement (playerAction.getSpeed(), playerAction.getDirection(), playerAction.isSprinting(), playerAction.isJumping());
		

	} // end of FixedUpdate

	/*
	 * 
	 */ 
	void MainMovementManagement(float speed, float direction, bool sprint, bool jump)
	{
		anim.SetFloat("Speed", speed);
		anim.SetFloat("Direction", direction);
		anim.SetBool ("Sprinting", sprint);

		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);	// set our currentState variable to the current state of the Base Layer (0) of animation

		// if player's current state is "Idle" and input on horizontal or vertical axis has changed
		if(currentBaseState.nameHash == idleState && (direction != 0f || speed != 0f))
		{
			// ... then rotates the player
			Rotating(direction, speed);
		}


		if(playerAction.getTurn90Left())
		{
			AddRotate(-90);
		}else if(playerAction.getTurn90Right()){
			AddRotate(90);
		}


	} // end of MainMovementManagement




















	/*
	 * 
	 */ 
	void ChangePlayerColliderYCenterThroughCurves()
	{
		if(!anim.IsInTransition(0)){
			// set the collider's y center to a float curve in the clip called "ColliderY"
			col.center = new Vector3 (0f, anim.GetFloat ("ColliderY"), 0);
		}

	} // ChangePlayerColliderYPositionThroughCurves

	/*
	 * 
	 */ 
	void ResizePlayerColliderHeighThroughCurves()
	{
		if(!anim.IsInTransition(0)){
			//... set the the collider height to a float curve in the clip called "ColliderHeight"
			col.height = anim.GetFloat("ColliderHeight");
		}

	} // end of ResizePlayerColliderHeighThroughCurves

	/**
	 * set a specific rotation based on euler angles
	 */ 
	void SetRotation(float angle){
		Vector3 targetEulerAngles = new Vector3 (0,0,0);	
		targetEulerAngles.y = angle;
		this.transform.eulerAngles = targetEulerAngles;

	} // end of set rotation

	/**
	 * adds an amount of rotation based on euler angles 
	 */
	void AddRotate(float angle)
	{

		Vector3 targetEulerAngles = this.transform.eulerAngles;	

		targetEulerAngles.y = targetEulerAngles.y + angle;

		this.transform.eulerAngles = targetEulerAngles;

		playerAction.setTurn90Left (false);
		playerAction.setTurn90Right (false);
		
	} // end of AddRotate

	/*
	 * 
	 */ 
	void ForcePlayerToLand(float startTimeAnimation, float endTimeAnimation)
	{
		// MatchTarget allows us to take over animation and smoothly transition our character towards a location - the hit point from the ray.
		// Here we're telling the Root of the character to only be influenced on the Y axis (MatchTargetWeightMask) and only occur between 0.35 and 0.5
		// of the timeline of our animation clip

		/*
		 * Parameters:
		 * matchPosition	     The position we want the body part to reach.
		 * matchRotation	     The rotation in which we want the body part to be.
		 * targetBodyPart	     The body part that is involved in the match.
		 * weightMask	         Structure that contains weights for matching position and rotation.
		 * startNormalizedTime	 Start time within the animation clip (0 - beginning of clip, 1 - end of clip).
		 * targetNormalizedTime	 End time within the animation clip (0 - beginning of clip, 1 - end of clip), 
		 * values greater than 1 can be set to trigger a match after a certain number of loops. Ex: 2.3 means at 30% of 2nd loop.
		 * 
		 * Description:
		 * Automatically adjust the gameobject position and rotation so that the AvatarTarget reaches the 
		 * matchPosition when the current state is at the specified progress.
		 * Target matching only works on the base layer (index 0).
		 */

		if(!anim.IsInTransition(0))
		{
			anim.MatchTarget(hitInfo.point, Quaternion.identity, AvatarTarget.Root, new MatchTargetWeightMask(new Vector3(0, 1, 0), 0), startTimeAnimation, endTimeAnimation);
		}

	} // end of ForcePlayerToLand

	/*
	 * 
	 */ 
	bool CheckPlayerIsFallingFromHighPlaces()
	{
		// if player is above the ground
		if(this.transform.position.y > minDistanceFromThePlayerFeetToTheGround)
		{
			// Raycast down from the feet of the player...
			//Ray ray = new Ray(this.transform.position, Vector3.down);
			// Raycast down from the center of the player...
			Ray ray = new Ray(this.transform.position + Vector3.up, Vector3.down);

			if (Physics.Raycast(ray, out hitInfo))
			{
				// if the distance is higher than a threshold
				if (hitInfo.distance > minDistanceFromThePlayerCenterToTheGround)
				{
					// then the player is on the air
					return true;
				}
			}
		}

		return false;
	} // end of CheckPlayerIsOnTheAir


	/**
	 * 
	 */
	void Rotating(float horizontal, float vertical)
	{
		
		Vector3 targetEulerAngles = this.transform.eulerAngles;;
		
		float rotateAmount = CalculateRotationDirectionAmountBasedOnInput (horizontal);
		
		float yAngle = (targetEulerAngles.y + rotateAmount);
		
		targetEulerAngles.y = Mathf.Lerp (targetEulerAngles.y, yAngle, Time.deltaTime * turnSmoothing);
		
		this.transform.eulerAngles = targetEulerAngles;
		
	} // end of Rotating

	/**
	 * 
	 * 
	 */ 
	float CalculateRotationDirectionAmountBasedOnInput (float horizontal)
	{
		if(horizontal > 0.1)
			return (1 * turnInputSmoothing);
		else if(horizontal < 0.1)
			return (-1 * turnInputSmoothing);
		
		return 0f;
	} // end of CalculateRotationDirectionAmountBasedOnInput

} // end of PlayerMovement class
