using UnityEngine;
using System.Collections;

public class WorldBondaryManagement : MonoBehaviour {

	void OnTriggerExit(Collider other) {
		Destroy(other.gameObject);
	}
}
