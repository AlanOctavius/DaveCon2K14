using UnityEngine;
using System.Collections;

public class SpikeControl : MonoBehaviour {

	private GameObject Here;

	void OnTriggerEnter2D(Collider2D other) {


		other.GetComponent<PlayerController> ().Destroy ();
		//Network.Destroy(other.gameObject);

		//Debug.Log("GameObject joins players");
	}
}
