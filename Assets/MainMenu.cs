using UnityEngine;
using System.Collections;

public class MainMenu: MonoBehaviour {
	
	void OnGUI () {
		
		if (GUI.Button (new Rect(700,250, 200, 100), "Start")) {
			Application.LoadLevel ("scene1");
		} 
		
		if (GUI.Button (new Rect (700, 500, 200, 100), "Quit")) {
			Application.Quit ();
		}
	}
}