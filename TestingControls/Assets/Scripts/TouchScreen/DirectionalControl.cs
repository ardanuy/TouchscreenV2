using UnityEngine;
using System.Collections;

public class DirectionalControl : TouchInput {
	
	// reference to PlayerAction
	private PlayerAction playerAction;


	/*
	 * Initializing everything we need
	 */ 
	void Awake(){
		// reference to PlayerAction
		playerAction = GameObject.FindGameObjectWithTag (Tags.player).GetComponent<PlayerAction> ();
		
	} // end of Awake function

	

	// ------------------------------------------------------------------------------------------------------
	//
	// ------------------------------------------------------------------------------------------------------
	/**
	 * @param Touch 
	 */
	void OnTouchUp(Touch touch){
		playerAction.setDirection (0f);
	} // end of OnTouchUp
	
	/**
	 * 
	 */
	void OnTouchDown(Touch touch){
		playerAction.setDirection (MaxMinNormalization(touch.position.x, Screen.width/2, 0));
	} // end of OnTouchDown
	
	/**
	 * @param Touch 
	 */
	void OnTouchStay(Touch touch){
		playerAction.setDirection (MaxMinNormalization(touch.position.x, Screen.width/2, 0));
	} // end of OnToucStay
	
	
	/**
	 * @param Touch 
	 */
	void OnTouchMove(Touch touch){
		playerAction.setDirection (MaxMinNormalization(touch.position.x, Screen.width/2, 0));
	} // end of OnTouchMove
	// ------------------------------------------------------------------------------------------------------
	//
	// ------------------------------------------------------------------------------------------------------






	/**
	 * performes a linear max min normalization of the input value, based on a maximum and a minimum values
	 * @param float value, float maximum value, float minimum value
	 * @return a float at [-1, 1] range
	 */
	private float MaxMinNormalization(float value, float max, float min){
		
		return (2 * ((value - min) / (max - min))) - 1;
		
	}
}
