using UnityEngine;
using System.Collections;

public class LevelSelectGUI : MonoBehaviour {

	void OnGUI(){
		
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if (GUI.Button (new Rect (0, 70,Screen.width/2, 20), "Level 1")) {
			Application.LoadLevel ("scene1");
		}
		
		// Make the second8 button.
		if (GUI.Button (new Rect (Screen.width/2,70,Screen.width/2,20), "Level 2")) {
			Application.LoadLevel ("scene2");
		}
	}
}
