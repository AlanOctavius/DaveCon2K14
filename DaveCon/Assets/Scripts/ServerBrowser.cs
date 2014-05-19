using UnityEngine;
using System.Collections;

public class ServerBrowser : MonoBehaviour {

	private float w;
	private float h;
	
	private Rect rect;

	private bool isRefreshing = false;
	private float refreshRequestLength = 3.0f;
	HostData[] hostData;
	private string directIP = "127.0.0.1";
	private string directPort= "25001";
	private string registeredGameName = "SC_DaveCon_Network_Test_Server";
	private Vector2 scrollPosition;
	// Use this for initialization
	private int  playerCount;
	string levelToLoad = "";

	void Start () {
		w = PlayerPrefs.GetFloat ("defaultWidth");
		h = PlayerPrefs.GetFloat ("defaultHeight");
		registeredGameName = PlayerPrefs.GetString("registeredGameName");

	}

	public IEnumerator RefreshHostList()
	{
		Debug.Log ("Refreshing ... ");
		MasterServer.RequestHostList (registeredGameName);
		
		float timeStarted = Time.time;
		float timeEnd = Time.time + refreshRequestLength;
		
		while (Time.time < timeEnd)
		{
			hostData = MasterServer.PollHostList ();
			yield return new WaitForEndOfFrame ();
		}
		
		if(hostData == null || hostData.Length == 0)
		{
			Debug.Log("No Active Servers have been found");
		}
		else
		{
			Debug.Log(hostData.Length + " Servers have been found");
		}
	}
	
	// Update is called once per frame
	public void OnGUI()
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
				if (GUILayout.Button("Refresh Server List")) // also can put width here
				{
					Debug.Log("Refresh Server List Button Clciked");
					StartCoroutine("RefreshHostList");
				}
				GUILayout.BeginHorizontal(); // also can put width in here
				{
					GUILayout.Label("Direct Connect: ");
					directIP = GUILayout.TextArea(directIP);
					directPort = GUILayout.TextArea(directPort);
					if(GUILayout.Button("Join"))
					{
						ConnectToServer(directIP,int.Parse(directPort));
						//Application.LoadLevel("GameLobby");
					}
					
				}
				GUILayout.EndHorizontal();
				if (GUILayout.Button("Back")) // also can put width here
				{
					Debug.Log("Back button pressed from Server Browser, Loading Multiplayer Menu.");
					Application.LoadLevel("MuliplayerMenu");
				}

				GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(rect.width), GUILayout.Height(rect.height));
				{
					if(hostData != null)
					{
						GUILayout.BeginVertical();
						{
							for (int i = 0; i < hostData.Length; i++)
							{
								GUILayout.BeginHorizontal(); // also can put width in here
								{
									GUILayout.Label(hostData[i].gameName);
									GUILayout.Label("players " + hostData[i].connectedPlayers + "/" + hostData[i].playerLimit);
									if(GUILayout.Button("Join"))
									{
										//ConnectToServer(hostData[i].ip.ToString, hostData[i].port);
										ConnectToServer(hostData[i]);
									}
								}
								GUILayout.EndHorizontal();
						

							}
						}
						GUILayout.EndVertical();
					}
				}
				GUILayout.EndScrollView ();
			}
			GUILayout.EndVertical();
		}
		GUILayout.EndArea ();
	}


	void ConnectToServer(string IP, int Port)
	{
		Debug.Log("Connecting to server: " + IP + ":" + Port);
		Network.Connect(IP,Port);
	}
	void ConnectToServer(HostData hostData)
	{
		//Debug.Log("Connecting to server: " + IP + ":" + Port);
		Network.Connect(hostData);
	}

	
	
	[RPC]
	void sendLevel(string level)
	{
		Debug.Log ("Retriving Server Info");
		levelToLoad = level;
		Debug.Log ("LevelToLoad: " + levelToLoad);
		if(Network.isClient)
		{
			Network.SetSendingEnabled(0, false);	
			Network.isMessageQueueRunning = false;
			Application.LoadLevel (levelToLoad);
			Network.isMessageQueueRunning = true;
			Network.SetSendingEnabled (0, true);
		}
	}

	[RPC]
	void PassPlayerCount(int players)
	{
		
		playerCount = players;
	}

}