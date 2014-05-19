using UnityEngine;
using System.Collections;

public class CreateServerMenu : MonoBehaviour {

	private float w = 0.5f;
	private float h = 0.5f;
	
	private Rect rect;
	private string ServerName = "Server name";
	private string connectionPort = "25001";

	private string playersHardcap = "8";
	private string maxPlayers = "4";

	private string registeredGameName = "SC_DaveCon_Network_Test_Server";
	private string serverDescript = "Test";


	private bool lan;
	private bool Nats = false;

	private string connectionIP = "127.0.0.1";


	void Start()
	{
		w = PlayerPrefs.GetFloat ("defaultWidth");
		h = PlayerPrefs.GetFloat ("defaultHeight");

		ServerName = PlayerPrefs.GetString ("defaultServerName");
		connectionPort = PlayerPrefs.GetString ("defaultPort");

		playersHardcap = PlayerPrefs.GetString ("playersHardcap");
		maxPlayers = PlayerPrefs.GetString ("defaultMaxPlayers");

		registeredGameName = PlayerPrefs.GetString("registeredGameName");
	}

	void OnGUI()
	{
		GUIStyle style = new GUIStyle ();
		style.alignment=TextAnchor.MiddleCenter;
		rect.x = (Screen.width*(1-w))/2;
		rect.y = (Screen.height*(1-h))/2;
		rect.width = Screen.width*w;
		rect.height = Screen.height*h;
		
		GUILayout.BeginArea(rect);
		{
			GUILayout.BeginVertical(); // also can put width in here
			{
				GUILayout.BeginHorizontal();
				{
					GUILayout.Label("Server Name:");
					ServerName = GUILayout.TextField(ServerName);
				}
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				{
					GUILayout.Label("Server description:");
					serverDescript = GUILayout.TextField(serverDescript);
				}
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				{
					GUILayout.Label("Port:");
					connectionPort = GUILayout.TextField(connectionPort,5);
				}
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				{
					GUILayout.Label("Max Players:");
					maxPlayers = GUILayout.TextField(maxPlayers,2);
				}
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				{
					GUILayout.Label("LAN: ");
					GUILayout.Toggle(lan,"");
					if(lan)
					{
						connectionIP = "LocalHost";
					}

				}
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				{
					if(GUILayout.Button("Reset to defaults"))
					{
						Debug.Log("Applying defaults");
						ServerName = PlayerPrefs.GetString("defaultServerName");
						connectionPort = PlayerPrefs.GetString("defaultPort");
						maxPlayers = PlayerPrefs.GetString("defaultMaxPlayers");
						Debug.Log("Server Name " + ServerName);
						Debug.Log("connectionPort " + connectionPort );
						Debug.Log("Max Players " + maxPlayers);

					}
					if(GUILayout.Button("Create Server"))
					{
						Debug.Log("Create Server Button Pressed from Create Server Menu, Creating server");
						Debug.Log("Server Name " + ServerName);
						Debug.Log("connectionPort " + connectionPort );
						Debug.Log("Max Players " + maxPlayers);

						Debug.Log ("Initialising Server");
						Network.InitializeServer (int.Parse(maxPlayers)-1,int.Parse(connectionPort), Nats);
						MasterServer.RegisterHost (registeredGameName, ServerName, serverDescript);
						Debug.Log("Server Created");

						Application.LoadLevel("GameLobby");

					}
				}
				GUILayout.EndHorizontal();
			
				if (GUILayout.Button("Back")) // also can put width here
				{
					Debug.Log("Back button pressed from Create Server Menu, returning to Multiplayer Menu");
					Application.LoadLevel("MuliplayerMenu");
				}
				
			}
			GUILayout.EndVertical();
		}
		GUILayout.EndArea();
	}

	// Update is called once per frame
	void Update () {
	
	}
	
}
