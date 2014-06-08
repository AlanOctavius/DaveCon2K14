using UnityEngine;
using System.Collections;

public class SpringController : MonoBehaviour {

	private GameObject Here;
	
	void OnTriggerEnter2D(Collider2D other) {
		
		
		other.GetComponent<PlayerController> ().Spring();
		//Network.Destroy(other.gameObject);
		
		//Debug.Log("GameObject joins players");
	}

	public void Destroy()
	{
		Network.Destroy (networkView.viewID);
	}

}
