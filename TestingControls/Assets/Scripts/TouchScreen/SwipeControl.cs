/*
 * 
 * 8-point Compass Rose system for swipe direction recognition
 * North		N	  0°				
 * North-East	NE	 45° (45°×1)		
 * East			E	 90° (45°×2)		
 * South-East	SE	135° (45°×3)	
 * South		S	180° (45°×4)	
 * South-West	SW	225° (45°×5)	
 * West			W	270° (45°×6)	
 * North-West	NW	315° (45°×7)	
 * 
 * 
 * 
 */ 
using UnityEngine;
using System.Collections;

public class SwipeControl : TouchInput {

		
	// stores last down point 
	private Vector2 lastDownPointPosition;


	// calculates distances from the hit down and up points
	private float xDistance = 0f;
	private float yDistance = 0f;
	private float distance = 0f;
	
	// tweaks 
	public float xPixelDistanceThreshold = 45f;
	public float yPixelDistanceThreshold = 45f;
	public float distanceThreshold = 30f;
	private float countedTime = 0f;
	public float timeAmountForSwipe = 0.7f;

	// reference to PlayerAction
	private PlayerAction playerAction;


	/*
	 * 
	 */ 
	void Awake(){
		// reference to PlayerAction class
		playerAction = GameObject.FindGameObjectWithTag (Tags.player).GetComponent<PlayerAction> ();

	} // end of Awake function




	/**
	 * @param touch 
	 */
	void OnTouchUp(Touch touch){

		// update distances
		UpdateDistances (touch.position);

		countedTime = countedTime + timeAmountForSwipe;
		// recognizes the swipe direction based on the 8 point compass rose
		SwipeDirectionRecognition ();
		

	} // end of OnTouchUp
	
	/**
	 * 
	 */
	void OnTouchDown(Touch touch){
		lastDownPointPosition = touch.position;
		countedTime = Time.time;
	} // end of OnTouchDown
	
	

	
	// ------------------------ Gesture Recognizer ----------------------------
	
	/**
	 * 
	 */
	private void UpdateDistances(Vector2 hitPoint){
		// update distances
		xDistance = hitPoint.x - lastDownPointPosition.x;
		yDistance = hitPoint.y - lastDownPointPosition.y;
		distance = Vector2.Distance(lastDownPointPosition, hitPoint);
	} // end of UpdateDistances
	
	/**
	 * 
	 */
	private void SwipeDirectionRecognition(){
		
		// if distance is greater than a threshold and the swipe was fast enough
		if(distance > distanceThreshold && Time.time < countedTime){
			// there was a swipe on the touch screen
			
			if(isNorthMove()){
				playerAction.setJumping(true);
			}
			else if(isNorthEastMove()){
			}
			else if(isEastMove()){
				playerAction.setTurn90Right(true);
			}
			else if(isSouthEastMove()){
			}
			else if(isSouthMove()){
			}
			else if(isSouthWestMove()){
			}
			else if(isWestMove()){
				playerAction.setTurn90Left(true);
			}
			else if(isNorthWestMove()){
			}
			else{
				playerAction.setJumping(false);
				playerAction.setTurn90Right(false);
				playerAction.setTurn90Left(false);
			}
		}
		
		
	} // end of SwipeDirectionRecognition
	
	/**
	 * 
	 */
	private bool isNorthMove(){
		// if Y distance is greater than a threshold and has positive value, and X distance is not greater than a threshold
		if( ((Mathf.Abs(yDistance) > yPixelDistanceThreshold) && (yDistance > 0)) && (Mathf.Abs(xDistance) < xPixelDistanceThreshold) ){
			return true;
		}
		return false;		
	} // end of isNorthMove
	
	/**
	 * 
	 */
	private bool isNorthEastMove(){
		// if X and Y distances are greater than a threshold and Y and X distances are positive values
		if ( ((Mathf.Abs (yDistance) > yPixelDistanceThreshold) && (Mathf.Abs (xDistance) > xPixelDistanceThreshold)) && 
		    ((yDistance > 0) && (xDistance > 0)) ){
			return true;
		}
		return false;
	} // end of isNorthEastMove
	
	/**
	 * 
	 */
	private bool isEastMove(){
		// if Y distance is not greater than a threshold and if X distance is greater than a threshold and has positive value
		if((Mathf.Abs(yDistance) < yPixelDistanceThreshold) && ((Mathf.Abs(xDistance) > xPixelDistanceThreshold) && (xDistance > 0)) ){
			return true;
		}
		return false;
	} // end of isEastMove
	
	/**
	 * 
	 */
	private bool isSouthEastMove(){
		// // if Y distance is greater than a threshold and has negative value, and X distance is greater than a threshold and has positive value
		if( ((Mathf.Abs(yDistance) > yPixelDistanceThreshold) && (yDistance < 0)) && 
		   ((Mathf.Abs(xDistance) > xPixelDistanceThreshold) && (xDistance > 0)) ){
			return true;
		}
		return false;
	} // end of isSouthEastMove
	
	/**
	 * 
	 */
	private bool isSouthMove(){
		// if Y distance is greater than a threshold and has negative value, and X distance is not greater than a threshold
		if( ((Mathf.Abs(yDistance) > yPixelDistanceThreshold) && (yDistance < 0)) && (Mathf.Abs(xDistance) < xPixelDistanceThreshold) ){
			return true;
		}
		
		return false;
	} // end of isSouthMove
	
	/**
	 * 
	 */
	private bool isSouthWestMove(){
		// // if Y distance is greater than a threshold and has negative value, and X distance is greater than a threshold and has negative value
		if( ((Mathf.Abs(yDistance) > yPixelDistanceThreshold) && (yDistance < 0)) && 
		   ((Mathf.Abs(xDistance) > xPixelDistanceThreshold) && (xDistance < 0)) ){
			return true;
		}
		
		return false;
	} // end of isSouthWestMove
	
	/**
	 * 
	 */
	private bool isWestMove(){
		// if Y distance is not greater than a threshold and if X distance is greater than a threshold and has negative value
		if((Mathf.Abs(yDistance) < yPixelDistanceThreshold) && ((Mathf.Abs(xDistance) > xPixelDistanceThreshold) && (xDistance < 0)) ){
			return true;
		}
		
		return false;
	} // end of isWestMove
	
	/**
	 * 
	 */
	private bool isNorthWestMove(){
		// if X and Y distances are greater than a threshold and Y is positive and x is negative
		if ( ((Mathf.Abs (yDistance) > yPixelDistanceThreshold) && (Mathf.Abs (xDistance) > xPixelDistanceThreshold)) && 
		    ((yDistance > 0) && (xDistance < 0)) ){
			return true;
		}
		return false;
	} // end of isNorthWestMove

} // end of SwipeControl class
