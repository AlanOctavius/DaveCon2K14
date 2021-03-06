﻿using UnityEngine;
using System.Collections;

public class GameLobby : MonoBehaviour {

	private float w;
	private float h;
	
	private Rect rect;

	private int players = 0;
	private int playerCount = 1;
	private string playersConnected;
	private string playerName;
	private int ping = 0;
	private int i = 0;


	void Start()
	{
		w = PlayerPrefs.GetFloat ("defaultWidth");
		h = PlayerPrefs.GetFloat ("defaultHeight");
		playerName = PlayerPrefs.GetString("PlayerName");
	}
	
	// Use this for initialization
	void OnGUI()
	{
		GUIStyle style = new GUIStyle ();
		style.fontSize=24;
		style.normal.textColor=Color.black;
		style.alignment=TextAnchor.MiddleCenter;
		rect.x = 10 +(Screen.width)/2;
		rect.y = 10;//(Screen.height*(1-h))/2;
		rect.width = Screen.width/2 - 20;
		rect.height = Screen.height/2 -20;
		GUILayout.BeginArea(rect);
		{
			GUILayout.BeginVertical(); // also can put width in here
			{

				if(Network.isClient)
				{
					GUILayout.Label("Connected as Client");
				}
				if(Network.isServer)
				{
					GUILayout.Label("Connected as Server");
				}

				if(Network.isClient || Network.isServer)
				{
					if(Network.isServer)
					{
						networkView.RPC ("PassPlayerCount", RPCMode.All, Network.connections.Length+1);
						playerCount =  Network.connections.Length+1;
					}
					GUILayout.Label("Players connected: " + playerCount );
				
					GUILayout.Label ("Connected with Name : " + playerName);

				}
				if(Network.isClient)
				{
					//Stats for player
					GUILayout.Label("Ping: "+ Network.GetAveragePing(Network.connections[0]) + " ms");
				}

				GUILayout.Label("Current Scene: " + Application.loadedLevelName);
				if (GUILayout.Button("Disconnect")) // also can put width here
				{
					Network.Disconnect();
					if(Network.isServer)
					{
						MasterServer.UnregisterHost();
					}
					Application.LoadLevel("MainMEnu");
				}
				
			}
			GUILayout.EndVertical();
		}
		GUILayout.EndArea();

	}



	public void OnDisconnectedFromServer(NetworkDisconnection info) {
		Debug.Log("Disconnected from server: " + info);
		Application.LoadLevel ("MainMEnu");
	}



	public void OnPlayerDisconnected(NetworkPlayer player) {
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
		//playerCount--;
		if(Network.isServer)
		{
			networkView.RPC ("PassPlayerCount", RPCMode.All, Network.connections.Length+1);
		}
	}
	
	[RPC]
	void PassPlayerCount(int players)
	{

		playerCount = players;
	}
}
