using UnityEngine;
using System.Collections;

public class MainMenu: MonoBehaviour {
	
	void OnGUI () {
		
		if (GUI.Button (new Rect(Screen.width - Screen.width/2 - 100,Screen.height - Screen.height/2 - 125, 200, 100), "Start")) {
			Application.LoadLevel ("scene1");
		} 
		
		if (GUI.Button (new Rect (Screen.width - Screen.width/2 - 100, Screen.height - Screen.height/2 +50, 200, 100), "Quit")) {
			Application.Quit ();
		}
	}
}