using UnityEngine;
using System.Collections;

public class NetworkStuff : MonoBehaviour {

	public Transform cubePrefab;
	private Transform myTransform;
	private bool Alive = false;


	public void Start()
	{
		Network.sendRate = 30;
	}

	public void OnGUI()
	{
		if(Alive == false)
		{
			if(GUILayout.Button("Spawn"))
			{
				SpawnPlayer();
				Alive = true;
			}
		}
		else
		{
			if(GUILayout.Button("kill"))
			{
				KillPlayer(myTransform);

			}
		}
	}


	public void SpawnPlayer()
	{
		Transform mt;
		mt = (Transform)Network.Instantiate(cubePrefab, transform.position, transform.rotation, 0);
		myTransform = mt;

	}

	/*[RPC]
	void RPCSpawnPlayer()
	{
		player =  Instantiate(cubePrefab, transform.position, transform.rotation, 0);
	}

	[RPC]
	void RPCKillPlayer ()
	{

	}
	*/

	public void KillPlayer(Transform mt)
	{
		myTransform.gameObject.GetComponent<PlayerController> ().Destroy ();
	}


}
