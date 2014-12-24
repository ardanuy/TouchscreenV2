using UnityEngine;
using System.Collections;

public class BasicThirdPersonCamera : MonoBehaviour
{
	public float smooth = 3f;		// a public variable to adjust smoothing of camera motion
	public GameObject playerCameraPosition;	// the usual position for the camera, specified by a transform in the game
	

	void FixedUpdate ()
	{
			
		this.transform.position = Vector3.Lerp(this.transform.position, playerCameraPosition.transform.position, Time.deltaTime * smooth);	
		this.transform.forward = Vector3.Lerp(this.transform.forward, playerCameraPosition.transform.forward, Time.deltaTime * smooth);
	}
}
