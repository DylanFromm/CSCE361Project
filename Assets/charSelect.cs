using UnityEngine;
using System.Collections;

public class charSelect : MonoBehaviour {

	public static int numPlayers = 0;

	private int h = Screen.height;
	private int w = Screen.width;
	
	void OnGUI () {

		if (numPlayers <= 0) {
						Application.Quit();
				} else if (numPlayers == 1) {
					
				}

		if (GUI.Button (new Rect(Mathf.Floor(w/2),Mathf.Floor(h/4), 200, 100), "SinglePlayer")) {

			Application.LoadLevel ("character");
		} 
		
		if (GUI.Button (new Rect(Mathf.Floor(w/2),Mathf.Floor(h/2), 200, 100), "MultiPlayer")) {

			Application.LoadLevel ("character");
		} 
		
		if (GUI.Button (new Rect (Mathf.Floor(w/2),Mathf.Floor(3*h/4), 200, 100), "Quit")) {
			Application.Quit ();
		}
	}
}