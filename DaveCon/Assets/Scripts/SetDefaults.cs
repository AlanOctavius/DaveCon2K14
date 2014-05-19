using UnityEngine;
using System.Collections;

public class SetDefaults : MonoBehaviour {

	public float width = 0.3f;
	public float height = 0.3f;

	public string ServerName = "DaveCon Server";
	public string connectionPort = "25001";

	public string playersHardcap = "8";
	public string maxPlayers = "4";

	public string registeredGameName = "SC_DaveCon_Network_Test_Server";
	public string connectionIP;

	public string playerName = "Player 1";

	// Use this for initialization
	void Awake ()
	{
		// Screen
		Debug.Log ("Setting default values for PlayerPrefs");
		//Screen Settings
		Debug.Log ("Setting default width to value " + width);
		PlayerPrefs.SetFloat ("defaultWidth", width);
		Debug.Log ("Setting default height to value " + height);
		PlayerPrefs.SetFloat ("defaultHeight", height);


		//Multipayer Settings
		Debug.Log ("Setting default ServerName to " + ServerName);
		PlayerPrefs.SetString ("defaultServerName ", ServerName);
		Debug.Log ("Setting default connectionPort to value " + connectionPort);
		PlayerPrefs.SetString ("defaultPort", connectionPort);
		Debug.Log ("Setting default playerHardcap to " + playersHardcap);
		PlayerPrefs.SetString ("playerHardcap", playersHardcap);
		Debug.Log ("Setting default maxPlayers to value " + maxPlayers);
		PlayerPrefs.SetString ("defaultMaxPlayers", maxPlayers);
		Debug.Log ("Setting RegisteredGameName to value " + registeredGameName);
		PlayerPrefs.SetString ("registeredGameName", registeredGameName);

		//PlayerPrefs.SetString ("PlayerName", playerName);

		Debug.Log ("Defaults set");
	}
	
}
