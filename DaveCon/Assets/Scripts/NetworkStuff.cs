using UnityEngine;
using System.Collections;

public class NetworkStuff : MonoBehaviour {

	public Transform cubePrefab;
	private Transform myTransform;
	public bool Alive = false;
	private GameObject Here;
	public void Start()
	{
		Network.sendRate = 30;
		Here = GameObject.FindGameObjectWithTag("MainCamera");
	}

	void Update()
	{
		Alive = Here.gameObject.GetComponent<GameCamera>().Alive;
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

	public void PlayerAlive(bool NewStatus)
	{
		Debug.Log ("yes");
		Alive = NewStatus;
	}

	public void SpawnPlayer()
	{
		myTransform = (Transform)Network.Instantiate(cubePrefab, transform.position, transform.rotation, 0);
		Debug.Log ("Spawn Player");
	}

	public void KillPlayer(Transform mt)
	{
		myTransform.gameObject.GetComponent<PlayerController> ().Destroy ();
	}


}
