﻿using UnityEngine;
using System.Collections;

public class GameOptions : MonoBehaviour {

	private Rect rect;
	private string LevelToLoad = "MAP1";
	private string Next = "";
	private Vector2 scrollPosition;


	void OnGUI()
	{
		GUIStyle style = new GUIStyle ();
		style.fontSize=24;
		style.normal.textColor=Color.black;
		style.alignment=TextAnchor.MiddleCenter;
		rect.x = 10;
		rect.y = 10;
		rect.width = Screen.width/2 - 20;
		rect.height = Screen.height/2 -20;
		GUILayout.BeginArea(rect);
		{
			GUILayout.BeginVertical(); // also can put width in here
			{
				GUILayout.Label("Game Options");

				GUILayout.BeginHorizontal();
				{
					GUILayout.Label("Map: ");
					if(Network.isServer)
					{
						LevelToLoad = GUILayout.TextField(LevelToLoad);
						if(GUILayout.Button("Set"))
						{
							Debug.Log("Sending message to Other users");
							networkView.RPC("SendString",RPCMode.OthersBuffered,LevelToLoad);

						}
					}
					if(Network.isClient)
					{
						GUILayout.Label(Next);
					}
				}
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				{
					if(Network.isServer)
					{
						GUILayout.Label("Start Game");
						if(GUILayout.Button("Start"))
						{
							StartGame();
						}

					}
				}
				GUILayout.EndHorizontal();
			}
			GUILayout.EndVertical();
		}
		GUILayout.EndArea();
		
	}

	void Update()
	{
	}



	public void StartGame()
	{
		Debug.Log ("Send message to clients to load level: " + LevelToLoad);
		networkView.RPC ("sendLevel", RPCMode.OthersBuffered , LevelToLoad);
		Next = LevelToLoad;
		Debug.Log ("Loading Level: " + LevelToLoad);
		Network.SetSendingEnabled(0, false);	
		Network.isMessageQueueRunning = false;
		Application.LoadLevel (Next);
		Network.isMessageQueueRunning = true;
		Network.SetSendingEnabled (0, true);
		
	}
	
/*	[RPC]
	void sendLevel(string level)
	{
		Next = LevelToLoad;
		Debug.Log("Load Level: " + LevelToLoad);
		if(Network.isClient)
		{
			Network.SetSendingEnabled(0, false);	
			Network.isMessageQueueRunning = false;
			//Application.LoadLevel (Next);
			Network.isMessageQueueRunning = true;
			Network.SetSendingEnabled (0, true);
		}
	}*/

	[RPC]
	public void sendLevel(string level)
	{
		//Debug.Log("Load Level: " + level);
		//Next = level;
		//Debug.Log("Load Level: " + levelToLoad);
		//Next = levelToLoad;
		if(Network.isClient)
		{
			//Network.SetSendingEnabled(0, false);	
			//Network.isMessageQueueRunning = false;
			Application.LoadLevel ("level");
			Debug.Log("Loading level client side: " + level);
			//Network.isMessageQueueRunning = true;
			//Network.SetSendingEnabled (0, true);
		}
	}

	[RPC]
	void SendString(string msg)
	{
		//Debug.Log ("Sending Message " + msg);
		Next = msg;
	}
	
}
