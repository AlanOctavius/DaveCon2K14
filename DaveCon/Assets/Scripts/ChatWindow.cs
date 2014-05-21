using UnityEngine;
using System.Collections;

public class ChatWindow : MonoBehaviour {

	private bool usingChat = true;
	private bool showChat = true;
	private Vector2 scrollPosition;
	private string inputField = "";
	ArrayList playerList = new ArrayList();
	public string playerName;
	//GUIStyle style;

	private int width = Screen.width/4;
	private int height = Screen.height/4;
	private float lastUnfocusTime = 0;
	Rect window;
	public class PlayerNode
	{
		public string playerName;
		public NetworkPlayer player;
	}
	
	ArrayList chatEntries = new ArrayList();
	
	public class ChatEntry
	{
		public string name = "";
		public string text = "";
	}


	public void Start()
	{
		window = new Rect (width/2,Screen.height/2 + height/2, width, height);

		playerName = PlayerPrefs.GetString ("PlayerName");

		if(playerName == "")
		{
			playerName = "RandomName"+Random.Range(1,99);
		}
		if(Network.isClient)
		{
			networkView.RPC ("ApplyGlobalChatText", RPCMode.All, playerName, " has joined the server");
		}
	}



	public void OnGUI()
	{
		if (!showChat)
		{
			return;
		}
		GUI.FocusWindow (5);

		if (Event.current.type == EventType.keyDown && Event.current.character == '\n' & inputField.Length < 1)
		{
			if(lastUnfocusTime + 25f < Time.time)
			{
				usingChat = true;
				GUI.FocusControl("Chat input field");
			}
		}

		window = GUI.Window (5, window, GlobalChatWindow, "Chat!");

	}

	public void OnDisconnectedFromServer(NetworkDisconnection info) {
		CloseChatWindow ();
		Debug.Log("Disconnected from server: " + info);
		Application.LoadLevel ("MainMEnu");
	}

	public void OnPlayerDisconnected(NetworkPlayer player) {
		addGameChatMessage (" A player has disconnected");
		playerList.Remove (GetPlayerNode(player));
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
	}

	public void OnConnectedToServer()
	{
		//ShowChatWindow ();

		//removed as is called before player loaded in which causes a failed RPC as no response is recived
		//networkView.RPC ("TellServerOurName", RPCMode.Server, playerName);
		//addGameChatMessage (playerName + "has just joined the server!"); 
	}
	
	public void OnServerInitialized()
	{
		ShowChatWindow ();
		PlayerNode newEntry = new PlayerNode ();
		newEntry.playerName = playerName;
		newEntry.player = Network.player;
		playerList.Add(newEntry);
		addGameChatMessage(playerName + " has just joined the Server!");
	}

	public PlayerNode GetPlayerNode(NetworkPlayer player)
	{
		foreach(PlayerNode entry in playerList)
		{
			if(entry.player == player)
			{
				return entry;
			}
		}
		Debug.LogError ("GetPlayerNode: Requested a playerNode of non-existing player!");
		return null;
	}
	
	[RPC]
	public void TellServerOurName(string name, NetworkMessageInfo info)
	{
		PlayerNode newEntry = new PlayerNode ();
		newEntry.playerName = playerName;
		newEntry.player = Network.player;
		playerList.Add (newEntry);
		addGameChatMessage (playerName + " has Just joined Server!");
	}
	
	public void HitEnter(string msg)
	{
		msg = msg.Replace ('\n', ' ');
		networkView.RPC ("ApplyGlobalChatText", RPCMode.All, playerName, msg);
	}


	[RPC]
	public void ApplyGlobalChatText(string name, string msg)
	{
		ChatEntry entry = new ChatEntry ();
		entry.name = name;
		entry.text = msg;
		
		chatEntries.Add (entry);
		if (chatEntries.Count > 10) {
			chatEntries.RemoveAt (0);
		}
		scrollPosition.y = 1000000;
		
	}
	
	void addGameChatMessage(string str)
	{
		ApplyGlobalChatText (" - ", str);
		if(Network.connections.Length > 0)
		{
			networkView.RPC("ApplyGlobalChatText",RPCMode.Others, " - ", str);
		}
	}

	void CloseChatWindow()
	{
		//showChat = false;
		inputField = "";
		chatEntries = new ArrayList ();
	}

	void ShowChatWindow()
	{
		showChat = true;
		inputField = "";
		chatEntries = new ArrayList ();
	}

	void GlobalChatWindow(int id)
	{
		GUILayout.BeginVertical ();
		{
			GUILayout.Space(20);
		}
		GUILayout.EndVertical ();

		scrollPosition = GUILayout.BeginScrollView (scrollPosition);
		{
			foreach(ChatEntry entry in chatEntries)
			{
				GUILayout.BeginHorizontal();
				{
					if(entry.name == " - ")
					{
						GUILayout.Label(entry.name + entry.text);
					}
					else
					{
						GUILayout.Label(entry.name + ": " + entry.text);
					}

				}
				GUILayout.EndHorizontal();
				GUILayout.Space(2);
			}

		}
		GUILayout.EndScrollView ();

		if(Event.current.type == EventType.keyDown && Event.current.character == '\n' & inputField.Length > 0)
		{
			HitEnter(inputField);
			inputField = "";
		}
		GUI.SetNextControlName ("Chat input field");
		inputField = GUILayout.TextField (inputField);

		if(Input.GetKeyDown("mouse 0"))
		{
			if(usingChat)
			{
				usingChat = false;
				GUI.UnfocusWindow();
				lastUnfocusTime = Time.time;
			}
		}

	}
	
}
