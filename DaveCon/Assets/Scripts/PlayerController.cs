using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private bool ifAlive = false;
	public Transform cubePrefab;



	void Awake()
	{
		if (!networkView.isMine)
		{
			enabled = false;
		}
	}
	// Use this for initialization
	void Start () {
	
	}

	Vector3 lastPosition;
	float minimumMovement = .05f;
	// Update is called once per frame
	void FixedUpdate () {

		if (networkView.isMine)
		{
			Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Jump") , 0);
			float speed = 5;
			Vector3 move = new Vector3(moveDir.x*speed*Time.deltaTime,4*moveDir.y*speed*Time.deltaTime,0);
			transform.Translate(move);

			if (Vector3.Distance(transform.position, lastPosition) > minimumMovement)
			{
				lastPosition = transform.position;
				networkView.RPC("SetPosition", RPCMode.Others, transform.position);
			}


		}
	
	}
	
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		if (stream.isWriting)
		{
			Vector3 myPosition = transform.position;
			stream.Serialize(ref myPosition);
		}
		else
		{
			Vector3 receivedPosition = Vector3.zero;
			stream.Serialize(ref receivedPosition); //"Decode" it and receive it
			transform.position = receivedPosition;
		}
	}


	[RPC]
	void SetPosition(Vector3 newPosition)
	{
		transform.position = newPosition;
	}


	void Boom()
	{
		Network.Destroy (networkView.viewID);
	}

	public void Destroy()
	{
		Network.Destroy (networkView.viewID);
	}

}
