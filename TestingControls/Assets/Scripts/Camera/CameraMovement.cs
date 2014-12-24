using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public float smooth = 3f;		// a public variable to adjust smoothing of camera motion
	public Transform playerTransform;
	
	void FixedUpdate (){


		this.transform.position = new Vector3(Mathf.Lerp(this.transform.position.x, playerTransform.position.x, Time.deltaTime * smooth), 
		                                      this.transform.position.y, this.transform.position.z);	
			
	}


}

