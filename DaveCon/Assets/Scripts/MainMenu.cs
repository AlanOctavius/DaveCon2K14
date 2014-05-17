using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	private float w;
	private float h;
	
	private Rect rect;

	void Start()
	{
		//Check if defaults exsits if not create them if no set them
		//Debug.Log ("Calling CheckPrefs");
		w = PlayerPrefs.GetFloat ("defaultWidth");
		h = PlayerPrefs.GetFloat ("defaultHeight");
	}

	//GUIStyle style;
	// Use this for initialization
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
				if (GUILayout.Button("Multiplayer")) // also can put width here
				{
					Debug.Log("Multiplayer Button Pressed, loading MultiplayerMenu");
					Application.LoadLevel("MuliplayerMenu");
				}
				if (GUILayout.Button("Settings")) // also can put width here
				{
					//Application.LoadLevel(1);
				}
				if (GUILayout.Button("Credits")) // also can put width here
				{
					//Application.LoadLevel(1);
				}
				if (GUILayout.Button("Exit")) // also can put width here
				{
					Debug.Log("Quit button pressed from main menu, Application shutting down.");
					Application.Quit();
				}

			}
			GUILayout.EndVertical();
		}
		GUILayout.EndArea();
	}


	void Update ()
	{
		//Do stuff on press
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Debug.Log("ESC key pressed from main menu, Loading start page");
			Application.LoadLevel("StartPage");
		}
	}
}
