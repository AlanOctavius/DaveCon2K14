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
				Alive = false;

			}
		}
	}


	public void SpawnPlayer()
	{
		myTransform = (Transform)Network.Instantiate(cubePrefab, transform.position, transform.rotation, 0);
	}

	public void KillPlayer(Transform mt)
	{
		myTransform.gameObject.GetComponent<PlayerController> ().Destroy ();
	}


}
