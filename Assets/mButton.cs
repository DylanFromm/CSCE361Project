using UnityEngine;
using System.Collections;

public class mButton : MonoBehaviour {

	public bool paused = false;
	
	void OnGUI () {

		if (paused) {
						if (GUI.Button (new Rect (20, 50, 100, 100), "Quit")) {
								Application.LoadLevel ("MainMenu");
						}
				} 
	}
}