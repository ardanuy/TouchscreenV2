using UnityEngine;
using System.Collections;

public class DebugDrawLine : MonoBehaviour {

	public Transform sourceLinePosition;

	void FixedUpdate() {

		if (Input.GetMouseButton(0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit)) {
				Vector3 incomingVec = hit.point - sourceLinePosition.position;
				Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);

				Debug.DrawLine(sourceLinePosition.position, hit.point, Color.red);
				Debug.DrawRay(hit.point, reflectVec, Color.green);
			}
		}

	} // end of FixedUpdate
}
