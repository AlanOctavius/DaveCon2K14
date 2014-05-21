﻿using UnityEngine;
using System.Collections;

public class NetworkContrller : MonoBehaviour {

	private string currentLevel= "";
	private string levelToLoad = "";
	private string Next = "";


	// Send player current level
	void OnPlayerConnected(NetworkPlayer player) {
		//Player connected
		//get current level name
		currentLevel = Application.loadedLevelName;
		networkView.RPC ("sendLevel", player, currentLevel);
	}


	[RPC]
	void sendLevel(string level)
	{
		Debug.Log("Load Level: " + currentLevel);
		levelToLoad = currentLevel;

		Debug.Log("Load Level: " + levelToLoad);
		Next = levelToLoad;
		if(Network.isClient)
		{
			Network.SetSendingEnabled(0, false);	
			Network.isMessageQueueRunning = false;
			Application.LoadLevel ("MAP1");
			Network.isMessageQueueRunning = true;
			Network.SetSendingEnabled (0, true);
		}
	}

}
