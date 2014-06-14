using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Transform Position;
	private GameObject Here;
	private NetworkStuff NetworkAlive;
	public Texture Stand;
	public Texture Duck;
	public float NormalSpeed = 5;
	private float SpeedMulti = 1;
	private int throwable = 1;
	public Transform Throwable;

	private bool Mine;

	public void Awake()
	{
		renderer.material.SetTexture("_MainTex", Stand);
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
		renderer.material.SetTexture("_MainTex", Stand);
	}

	public Vector3 lastPosition;
	public float minimumMovement = .05f;
	// Update is called once per frame
	public void Update () {

		if (networkView.isMine)
		{
			Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Jump") , 0);
			float speed = NormalSpeed*SpeedMulti;
			Vector3 move = new Vector3(moveDir.x*speed*Time.deltaTime,4*moveDir.y*NormalSpeed*Time.deltaTime,0);
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
				renderer.material.SetTexture("_MainTex", Duck);
			}
			else
			{
				transform.localScale = new Vector3(1,2,1);
				networkView.RPC("SetDuck", RPCMode.Others, new Vector3(1,2,1));
				renderer.material.SetTexture("_MainTex", Stand);
			}

			if(Input.GetKey(KeyCode.LeftShift))
			{
				SpeedMulti = 2;
			}
			else
			{
				SpeedMulti = 1;
			}

			if(Input.GetKeyDown(KeyCode.G))
			{
				//check for throwable
				if(throwable > 0)
				{
					//throw
					throwItem();
					//throwable--;
				}

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
		if(Scale.y == 2)
		{
			renderer.material.SetTexture("_MainTex", Stand);
		}
		else
		{
			renderer.material.SetTexture("_MainTex", Duck);
		}
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
	public void Spring(float A)
	{
		rigidbody2D.AddForce (new Vector2 (0, A));
	}

	private void throwItem()
	{
		Debug.Log ("Item Throw");
		Vector3 pz = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector3 pos = transform.position;
		float X =(pz.x - pos.x);
		float Y =(pz.y - pos.y);
		if(X >0)
		{
			Network.Instantiate(Throwable,new Vector3(transform.position.x + 1, transform.position.y,0), transform.rotation, 0);
		}
		else
		{
			Network.Instantiate(Throwable,new Vector3(transform.position.x - 1, transform.position.y,0), transform.rotation, 0);
		}
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
