using UnityEngine;
using System.Collections;

public class SettingsMenu : MonoBehaviour {

	
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
				if (GUILayout.Button("placeholder")) // also can put width here
				{

				}
				if (GUILayout.Button("placeholder")) // also can put width here
				{
					//Application.LoadLevel(1);
				}
				if (GUILayout.Button("placeholder")) // also can put width here
				{
					//Application.LoadLevel(1);
				}
				if (GUILayout.Button("back")) // also can put width here
				{
					Application.LoadLevel("MainMEnu");
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
