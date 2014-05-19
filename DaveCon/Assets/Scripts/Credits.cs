using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {
	
	private float w;
	private float h;
	
	private Rect rect;
	private Vector2 scrollPosition;
	private string playerName = "Player 1";
	void Start()
	{
		//Check if defaults exsits if not create them if no set them
		//Debug.Log ("Calling CheckPrefs");
		w = PlayerPrefs.GetFloat ("defaultWidth");
		h = PlayerPrefs.GetFloat ("defaultHeight");
		playerName = PlayerPrefs.GetString ("PlayerName");
		
	}
	
	//GUIStyle style;
	// Use this for initialization
	void OnGUI()
	{
		GUIStyle style = new GUIStyle ();
		style.alignment=TextAnchor.MiddleRight;
		rect.x = (Screen.width*(1-w))/2;
		rect.y = (Screen.height*(1-h))/2;
		rect.width = Screen.width*w;
		rect.height = Screen.height*h;
		
		GUILayout.BeginArea(rect);
		{
			GUILayout.BeginScrollView(scrollPosition);
			{
				GUILayout.BeginVertical(); // also can put width in here
				{
					GUILayout.BeginHorizontal();
					{
						GUILayout.Label("Programmers: ");
						GUILayout.BeginVertical();
						{
							GUILayout.Label("David Barr",style);
							GUILayout.Label("Alan O'Brien",style);
						}
						GUILayout.EndVertical();
					}
					GUILayout.EndHorizontal();

					GUILayout.BeginHorizontal();
					{
						GUILayout.Label("Artists: ");
						GUILayout.BeginVertical();
						{
							GUILayout.Label("none",style);
						}
						GUILayout.EndVertical();
					}
					GUILayout.EndHorizontal();

					GUILayout.BeginHorizontal();
					{
						GUILayout.Label("Sound Engineer: ");
						GUILayout.BeginVertical();
						{
							GUILayout.Label("none",style);
						}
						GUILayout.EndVertical();
					}
					GUILayout.EndHorizontal();

					GUILayout.BeginHorizontal();
					{
						GUILayout.Label("Others: ");
						GUILayout.BeginVertical();
						{
							GUILayout.Label("none",style);
						}
						GUILayout.EndVertical();
					}
					GUILayout.EndHorizontal();
				}
				GUILayout.EndVertical();
			}
			GUILayout.EndScrollView();
			if (GUILayout.Button("Exit")) // also can put width here
			{
				Debug.Log("Quit button pressed from main menu, Application shutting down.");
				Application.LoadLevel("MainMEnu");
			}
		}
		GUILayout.EndArea();
	}
	

}
