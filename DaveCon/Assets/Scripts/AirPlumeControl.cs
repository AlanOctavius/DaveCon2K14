using UnityEngine;
using System.Collections;

public class AirPlumeControl : MonoBehaviour {

	private GameObject Here;
	public float Springforce = 20;
	void OnTriggerEnter2D(Collider2D other) {
		
		
		other.GetComponent<PlayerController> ().Spring(Springforce);

	}
	
	public void Destroy()
	{
		Network.Destroy (networkView.viewID);
	}
	
}
