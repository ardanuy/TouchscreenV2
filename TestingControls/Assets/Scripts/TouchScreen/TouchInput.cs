using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TouchInput : MonoBehaviour {

	void Update(){

		// if there is a touch on screen
		if(Input.touchCount > 0){
			// for each captured touch
			foreach (Touch touch in Input.touches){
				// if the touch hits the GUITexture area
				if(this.guiTexture.HitTest(touch.position)){

					switch(touch.phase){
					
					case TouchPhase.Began:
						this.SendMessage("OnTouchDown", touch, SendMessageOptions.DontRequireReceiver);
						break;
					case TouchPhase.Ended:
						this.SendMessage("OnTouchUp", touch, SendMessageOptions.DontRequireReceiver);
						break;
					case TouchPhase.Stationary:
						this.SendMessage("OnTouchStay", touch, SendMessageOptions.DontRequireReceiver);
						break;
					case TouchPhase.Moved:
						this.SendMessage("OnTouchMove", touch, SendMessageOptions.DontRequireReceiver);
						break;
					case TouchPhase.Canceled:
						this.SendMessage("OnTouchExit", touch, SendMessageOptions.DontRequireReceiver);
						break;
					default:
						break;

					} // end of switch
				} // end of if


			} // end of foreach


		} // end of if

	} // end of Update
} // end of class
