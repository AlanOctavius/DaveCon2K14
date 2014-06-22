using UnityEngine;
using System.Collections;

public class PickUpControl : MonoBehaviour {

	private GameObject Here;
	public float Springforce = 1000;
	void OnTriggerEnter2D(Collider2D other) {
		
		//Add Attribute to Player
		//other.GetComponent<PlayerController> ().Spring(Springforce);

		// Destroy Self
		Destroy ();

	}
	
	public void Destroy()
	{
		Network.Destroy (networkView.viewID);
	}

}
