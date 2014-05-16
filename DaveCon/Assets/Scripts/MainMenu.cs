using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {


	void OnGUI()
	{
		if(GUI.Button(new Rect(10,10,200,30),"Quit"))
		{
			Application.Quit();
		}
	}
}
