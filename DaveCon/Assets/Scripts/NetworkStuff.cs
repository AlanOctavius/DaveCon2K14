using UnityEngine;
using System.Collections;

public class NetworkStuff : MonoBehaviour {

	public Transform cubePrefab;
	private Transform myTransform;
	private bool Alive = false;

	void OnGUI()
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
				//Alive = false;
			}
		}
	}


	void SpawnPlayer()
	{
		Transform mt;
		mt = (Transform)Network.Instantiate(cubePrefab, transform.position, transform.rotation, 0);

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

	void KillPlayer(Transform mt)
	{
		//Network.Destroy (mt.GetComponent(NetworkViewID));
	}


}
