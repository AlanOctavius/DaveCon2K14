using UnityEngine;
using System.Collections;

public class GameOverlay : MonoBehaviour {

	private Rect rect;

	private bool showMenu = false;
	private Vector2 scrollPosition;
	public void OnGUI()
	{
		/*rect.x = 3 * Screen.width / 4;
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
		GUILayout.EndArea ();*/


		rect.x = Screen.width / 2 - Screen.width / 4;
		rect.y = Screen.height / 2 - Screen.height / 4;
		rect.width = Screen.width / 2;
		rect.height = Screen.height / 2;

		//if true show menu, if false do not.
		if (showMenu == true)
		{
			GUILayout.BeginArea (rect);
			{
				GUILayout.BeginVertical();
				{
					GUILayout.Label("Menu");
					GUILayout.BeginScrollView(scrollPosition);
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
					GUILayout.EndScrollView();
				}
				GUILayout.EndVertical();

			}
			GUILayout.EndArea ();
		}
	}







	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(showMenu == true)
			{
				showMenu = false;
			}
			else
			{
				showMenu = true;
			}
		}
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
