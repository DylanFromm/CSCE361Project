using UnityEngine;
using System.Collections;

public class MainMenu: MonoBehaviour {

	public int num;

	private int h = Screen.height;
	private int w = Screen.width;

	private int box_w =200;
	private int box_h =100;

	void Start () {

		if (num == 0) {
			guiText.text = "Main Menu";
			Vector2 offset = guiText.pixelOffset;
			offset.x=w/2;
			offset.y=15*h/16;
			guiText.pixelOffset=offset;
				}

		}

	void OnGUI () {
		GUI.backgroundColor = Color.red;
		if (GUI.Button (new Rect(Mathf.Floor(w/2)-box_w/2,Mathf.Floor(h/4)-box_h/2, box_w, box_h), "SinglePlayer")) {
			charSelect.numPlayers=1;
			Application.LoadLevel ("character");
		} 

		if (GUI.Button (new Rect(Mathf.Floor(w/2)-box_w/2,Mathf.Floor(h/2)-box_h/2, box_w, box_h), "MultiPlayer")) {
			charSelect.numPlayers=2;
			Application.LoadLevel ("character");
		} 

		if (GUI.Button (new Rect (Mathf.Floor(w/2)-box_w/2,Mathf.Floor(3*h/4)-box_h/2, 200, 100), "Quit")) {
			Application.Quit ();
		}
	}
}