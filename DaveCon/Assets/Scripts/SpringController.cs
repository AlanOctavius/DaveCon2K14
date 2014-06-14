using UnityEngine;
using System.Collections;

public class SpringController : MonoBehaviour {

	private GameObject Here;
	public float Springforce = 1000;
	void OnTriggerEnter2D(Collider2D other) {
		
		
		other.GetComponent<PlayerController> ().Spring(Springforce);
		//Network.Destroy(other.gameObject);
		
		//Debug.Log("GameObject joins players");
	}

	public void Destroy()
	{
		Network.Destroy (networkView.viewID);
	}

}
