using UnityEngine;
using System.Collections;

public class FanControl : MonoBehaviour {

	private GameObject Here;
	public float Springforce = 500;
	private Vector3 myPosition;
	private Transform myAirPlume;
	public Transform AirPlume;
	public float offset = 5.5f;

	void Start()
	{
		// Spawn air plume.
		//get position
		myPosition = transform.position;
		myAirPlume = (Transform)Network.Instantiate(AirPlume,new Vector3(myPosition.x,myPosition.y + offset,0),transform.rotation,0);
		//Network.Instantiate(AirPlume, transform.position, transform.rotation, 0);

	}

	void OnTriggerEnter2D(Collider2D other) {
		
		
		other.GetComponent<PlayerController> ().Destroy ();
		//Network.Destroy(other.gameObject);
		
		//Debug.Log("GameObject joins players");
	}
	
	public void Destroy()
	{
		// Destroy Air Plume then destroy 
		Network.Destroy (myAirPlume.networkView.viewID);
		Network.Destroy (networkView.viewID);
	}
}
