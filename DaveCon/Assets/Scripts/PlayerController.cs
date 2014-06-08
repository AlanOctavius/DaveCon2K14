using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Transform Position;
	private GameObject Here;
	private NetworkStuff NetworkAlive;

	private bool Mine;

	public void Awake()
	{
		if (!networkView.isMine)
		{
			enabled = false;
			Mine = false;
			//GetComponent<NetworkInterpolatedTransforms>().enabled = false;
		}
		else
		{
			Here = GameObject.FindGameObjectWithTag("MainCamera");
			Here.gameObject.GetComponent<GameCamera>().SetAlive(true);
			Mine = true;
			//NetworkAlive = GameObject.FindGameObjectWithTag("NetworkController");
			//NetworkAlive..GetComponent<NetworkStuff>().PlayerAlive(true);
			//GetComponent<NetworkInterpolatedTransforms>().enabled = true;
		}
	}
	// Use this for initialization
	public void Start () {

	}

	public Vector3 lastPosition;
	public float minimumMovement = .05f;
	// Update is called once per frame
	public void Update () {

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
				if(Mine == true)
				{
					Here.gameObject.GetComponent<GameCamera>().SetPosition(transform);
				}
			}

			if(Input.GetAxis("Vertical") < 0)
			{
				transform.localScale = new Vector3(1,1,1);
				networkView.RPC("SetDuck", RPCMode.Others, new Vector3(1,1,1));
			}
			else
			{
				transform.localScale = new Vector3(1,2,1);
				networkView.RPC("SetDuck", RPCMode.Others, new Vector3(1,2,1));
			}


		}
	
	}

	void LateUpdate()
	{
		if (networkView.isMine)
		{

			//Debug.Log("Here");
		}
	}


	//public void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	/*{
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
	}*/


	[RPC]
	public void SetPosition(Vector3 newPosition)
	{
		transform.position = newPosition;
	}

	[RPC]
	public void SetDuck (Vector3 Scale)
	{
		transform.localScale = Scale;
	}


	public void Boom()
	{
		Here.gameObject.GetComponent<GameCamera>().SetAlive(false);
		if(networkView.isMine)
		{
			//NetworkAlive.PlayerAlive(false);
			Debug.Log ("Sending message to Network Stuff to change player to dead");
			//NetworkAlive = gameObject.GetComponent<NetworkStuff>();
			//NetworkAlive.PlayerAlive(false);
		}
		Network.Destroy (networkView.viewID);

	}
	public void Spring()
	{
		rigidbody2D.AddForce (new Vector2 (0, 100));
	}
	
	public void Destroy()
	{
		Here.gameObject.GetComponent<GameCamera>().SetAlive(false);
		if(networkView.isMine)
		{
		// NetworkAlive.PlayerAlive(false);
		// Debug.Log ("Sending message to Network Stuff to change player to dead");
		 	//NetworkAlive = gameObject.GetComponent<NetworkStuff>();
			//NetworkAlive.Alive = false;
		}
		Network.Destroy (networkView.viewID);
	}

}
