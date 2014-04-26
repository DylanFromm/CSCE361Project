using UnityEngine;
using System.Collections;

public class characterText : MonoBehaviour {

	public int num;
	private int h = Screen.height;
	private int w = Screen.width;

	private int box_w =200;
	private int box_h =100;

	private bool flag1 = false;
	private bool flag2 = false;
	private bool flag3 = false;
	private bool flag4 = false;


	private GUIStyle style = new GUIStyle ();
	// Use this for initialization
	void Start () {
				if (num == 0) {
			Vector2 offset = guiText.pixelOffset;
			offset.x=w/2;
			offset.y=15*h/16;
			guiText.pixelOffset=offset;
				} else if (num == 1) {
			Vector2 offset = guiText.pixelOffset;
			offset.x=w/2;
			offset.y=3*h/4 + guiText.fontSize/2;
			guiText.pixelOffset=offset;
				} else if (num == 2) {
			Vector2 offset = guiText.pixelOffset;
			offset.x=w/2;
			offset.y=h/4 + guiText.fontSize/2;
			guiText.pixelOffset=offset;
				}

			if (charSelect.numPlayers == 2) {
						if (num == 1) {
								guiText.text = "Player 1";
						} else if (num == 2){
								guiText.text = "Player 2";
						}
				} else if (charSelect.numPlayers == 1) {
						if (num == 1) {
							guiText.text = "Player";
						} else if (num ==2){
							guiText.text = "Computer";
						}
				}

		}


	// Update is called once per frame
	void OnGUI () {
		if (!flag1) {
			GUI.backgroundColor= Color.white;
		} else {
			GUI.backgroundColor = Color.red;
		}
		if (GUI.Button (new Rect(Mathf.Floor(w/4-box_w/2),Mathf.Floor(h/4-box_h/2), box_w, box_h), "Red")) {
			//set player 1 as red
			flag1=true;
			if(flag2){
				flag2=false;
			}
		} 
		if (!flag2) {
			GUI.backgroundColor= Color.white;
				} else {
						GUI.backgroundColor = Color.blue;
				}
		if (GUI.Button (new Rect(Mathf.Floor(3*w/4-box_w/2),Mathf.Floor(h/4-box_h/2), box_w, box_h), "Blue")) {
			//set player 1 as blue
			flag2=true;
			if(flag1){
				flag1=false;
			}
		} 


		if (!flag3) {
			GUI.backgroundColor= Color.white;
		} else {
			GUI.backgroundColor = Color.red;
		}
		if (GUI.Button (new Rect (Mathf.Floor(w/4-box_w/2),Mathf.Floor(3*h/4-box_h/2), box_w, box_h), "Red")) {
			//set player 2 as red
			flag3=true;
			if(flag4){
				flag4=false;
			}
		}

		if (!flag4) {
			GUI.backgroundColor= Color.white;
		} else {
			GUI.backgroundColor = Color.blue;
		}
		
		if (GUI.Button (new Rect (Mathf.Floor(3*w/4-box_w/2),Mathf.Floor(3*h/4-box_h/2), box_w, box_h), "Blue")) {
			//set player 2 as blue
			flag4=true;
			if(flag3){
				flag3=false;
			}
		}

		GUI.backgroundColor = Color.white;
		if (GUI.Button (new Rect (Mathf.Floor(w/4-box_w),Mathf.Floor(h/2-box_h/2), 2*box_w, box_h), "Select Scene")) {
			Application.LoadLevel("LevelSelect");
		}

		if (GUI.Button (new Rect (Mathf.Floor(3*w/4-box_w),Mathf.Floor(h/2-box_h/2), 2*box_w, box_h), "Main Menu")) {
			Application.LoadLevel("MainMenu");
		}

	}
}
