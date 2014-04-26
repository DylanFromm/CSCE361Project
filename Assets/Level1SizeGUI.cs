using UnityEngine;
using System.Collections;

public class Level1SizeGUI : MonoBehaviour {

	private GUITexture myGUITexture;
	
	void Awake()
	{
		myGUITexture = this.gameObject.GetComponent("GUITexture") as GUITexture;
	}
	
	// Use this for initialization
	void Start()
	{
		// Position the billboard in the center, 
		// but respect the picture aspect ratio

		myGUITexture.pixelInset = new Rect(-Screen.width/2,-Screen.height/2,Screen.width/2,Screen.height );
	}
}
