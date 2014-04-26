using UnityEngine;
using System.Collections;

public class PlayerWinGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (PlayerController.WhichPlayerDied == 1) {
			guiText.text = "Player 2 Wins!";
			guiText.color = Color.black;
		} else if (PlayerController.WhichPlayerDied == 2) {
			guiText.text = "Player 1 Wins!";
			guiText.color = Color.black;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
