using UnityEngine;
using System.Collections;

public class TouchInputControl : TouchInput {

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
	 * 
	 */
	void OnTouchUp(Touch touch){

		playerAction.setSpeed (0f);
		playerAction.setSprinting (false);

	} // end of OnTouchUp

	/**
	 * 
	 */
	void OnTouchStay(Touch touch){
		// look for an action based on tap counts
		SetActionBasedOnTapCount (touch.tapCount);

	} // end of OnToucStay
	

	


	// ------------------------ Action Recognizer ----------------------------
	/**
	 * 
	 */
	private void SetActionBasedOnTapCount(int tapCount){
		switch(tapCount){
		case 1:
			playerAction.setSpeed(1);
			playerAction.setSprinting(false);
			break;
		case 2:
			playerAction.setSpeed(1);
			playerAction.setSprinting(true);
			break;
		default:
			playerAction.setSpeed(0f);
			playerAction.setSprinting(false);
			break;
		}// end of switch


	} // end of ActionRecognition function

} // end of TouchInputControl class

