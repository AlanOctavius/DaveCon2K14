using UnityEngine;
using System.Collections;

public class GameLobby : MonoBehaviour {

	private float w;
	private float h;
	
	private Rect rect;
	
	//GUIStyle style;
	
	void Start()
	{
		w = PlayerPrefs.GetFloat ("defaultWidth");
		h = PlayerPrefs.GetFloat ("defaultHeight");
	}
	
	// Use this for initialization
	void OnGUI()
	{
		GUIStyle style = new GUIStyle ();
		style.fontSize=24;
		style.normal.textColor=Color.black;
		style.alignment=TextAnchor.MiddleCenter;
		rect.x = (Screen.width*(1-w))/2;
		rect.y = (Screen.height*(1-h))/2;
		rect.width = Screen.width*w;
		rect.height = Screen.height*h;
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


	void OnDisconnectedFromServer(NetworkDisconnection info) {
		Debug.Log("Disconnected from server: " + info);
		Application.LoadLevel ("MainMEnu");
	}
}
