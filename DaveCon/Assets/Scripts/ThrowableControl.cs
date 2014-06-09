using UnityEngine;
using System.Collections;

public class ThrowableControl : MonoBehaviour {

	private float strength = 100;
	
	void Start()
	{
		Vector3 pz = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector3 pos = transform.position;
		float X =strength*(pz.x - pos.x);
		float Y =strength*(pz.y - pos.y);
		rigidbody2D.AddForce (new Vector2 (X, Y));
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		
		
		Destroy ();
	}
	
	public void Destroy()
	{
		Network.Destroy (networkView.viewID);
	}
}
