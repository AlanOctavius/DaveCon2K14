using UnityEngine;
using System.Collections;

public class GameOverlay : MonoBehaviour {

	private Rect rect;

	public void OnGUI()
	{
		rect.x = 3 * Screen.width / 4;
		rect.y = 10;
		rect.width = Screen.width / 4 - 20;
		rect.height = 30;

		GUILayout.BeginArea (rect);
		{
			GUILayout.BeginHorizontal();
			{
				if(GUILayout.Button("Disconnect"))
				{
					// Disconnect user if
					Network.Disconnect();
					if(Network.isServer)
					{
						MasterServer.UnregisterHost();
					}
					Application.LoadLevel("MainMEnu");
				}

				if(Network.isServer)
				{
					if(GUILayout.Button("End Game"))
					{
						// Load Game Lobby
						Debug.Log("Ending game and returning to game lobby");
						EndGame();
					}
				}
			}
			GUILayout.EndHorizontal();
		

		}
		GUILayout.EndArea ();
	}

	void EndGame()
	{
		Debug.Log ("Send message to clients to load level: " + "GameLobby");
		networkView.RPC ("LoadGameLobby", RPCMode.All,"GameLobby");

		
		Network.SetSendingEnabled(0, false);	
		Network.isMessageQueueRunning = false;
		Application.LoadLevel ("GameLobby");
		Network.isMessageQueueRunning = true;
		Network.SetSendingEnabled (0, true);
		
	}

	[RPC]
	void LoadGameLobby(string msg)
	{
		Debug.Log("Load Level: " + msg);
		if(Network.isClient)
		{
			Network.SetSendingEnabled(0, false);	
			Network.isMessageQueueRunning = false;
			Application.LoadLevel (msg);
			Network.isMessageQueueRunning = true;
			Network.SetSendingEnabled (0, true);
		}
	}
}
