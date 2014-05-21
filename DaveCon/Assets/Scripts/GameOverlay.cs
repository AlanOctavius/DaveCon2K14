using UnityEngine;
using System.Collections;

public class GameOverlay : MonoBehaviour {

	private Rect rect;
	//private Rect windowRect0 = new Rect(20, 20, 120, 50);
	private bool showMenu = false;
	private Vector2 scrollPosition;
	private Vector3 mousePos;
	public void OnGUI()
	{
		rect.x = Screen.width / 2 - Screen.width / 4;
		rect.y = Screen.height / 2 - Screen.height / 4;
		rect.width = Screen.width / 2;
		rect.height = Screen.height / 2;

		//if true show menu, if false do not.


		if (showMenu == true)
		{
			rect = GUILayout.Window (0, rect, OpenMyWindow, "Menu");
			//GUI.Window(0, rect, DoWindow0, "Basic Window");
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

		if(Input.GetButtonDown("Fire1"))
		{
			mousePos = Input.mousePosition;
			if(mousePos.x < Screen.width / 2 - Screen.width / 4 || mousePos.x > 3*Screen.width / 4 || mousePos.y < Screen.height / 2 - Screen.height / 4 || mousePos.y > 3*Screen.height / 4 )
			{
				if(showMenu== true)
				{
					showMenu = false;
				}
			}
		}
	}

	void OpenMyWindow(int winID)
	{
		GUI.BringWindowToFront(winID);
		GUI.FocusWindow(winID); 

			GUILayout.BeginVertical();
			{
				//GUILayout.Label("Menu");
				GUILayout.BeginScrollView(scrollPosition);
				{
					if(GUILayout.Button("Disconnect"))
					{
						// Disconnect user if
						Debug.Log("Disconnecting from server");
						Network.Disconnect(200);
						if(Network.peerType == NetworkPeerType.Server)
						{
							MasterServer.UnregisterHost();
						}
						Application.LoadLevel("MainMEnu");
					}
					
					if(Network.peerType == NetworkPeerType.Server)
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
		GUI.DragWindow(new Rect (0,0, 10000, 20));
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
