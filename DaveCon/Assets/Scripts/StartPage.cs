using UnityEngine;
using System.Collections;

public class StartPage : MonoBehaviour {

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

		GUI.Label(rect, "Press any button to continue!",style);

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.anyKey)
		{
			Debug.Log("A key or mouse click has been detected, loading Main Menu");
			Application.LoadLevel("MainMEnu");
		}
			
	}
	
}