using UnityEngine;
using System.Collections;

public class mButton : MonoBehaviour {
	
	void OnGUI () {

		
		if (GUI.Button (new Rect (0, 0, 100, 100), "Quit")) {
			Application.LoadLevel("MainMenu");
		}
	}
}