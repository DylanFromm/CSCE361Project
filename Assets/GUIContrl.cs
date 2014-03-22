using UnityEngine;
using System.Collections;

public class GUIContrl : MonoBehaviour {
	void OnGUI(){
		GUI.Box (new Rect (10,10,220,90), "Loader Menu");
		GUI.Box (new Rect (Screen.width/2 - 400, Screen.height/2 - 200, 400, 500), "Player 1");
		GUI.Box (new Rect (Screen.width/2, Screen.height/2 - 200, 400, 500), "Player 2");
	
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if (GUI.Button (new Rect (20,40,200,20), "Another Match")) {
			Application.LoadLevel (1);
		}
		
		// Make the second8 button.
		if (GUI.Button (new Rect (20,70,200,20), "Quit")) {
			Application.Quit();
		}
	}
}