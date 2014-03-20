using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	public GameObject player;
	
	private float maxHealth;
	public float curHealth;
	
	private float barLength = Screen.width / 2 - 20;
	
	
	void Start() {
		maxHealth = player.GetComponent<PlayerController> ().health;
		curHealth = maxHealth;
	}
	
	void Update() {
		
		curHealth = player.GetComponent<PlayerController> ().health;
		barLength = (Screen.width / 2) * (curHealth / (float)maxHealth);
	}
	
	void OnGUI() {
		if (name.Contains("1")) {
			GUI.Box(new Rect(10, 10, barLength, 20), curHealth + "");
		} else {
			GUI.Box(new Rect(Screen.width / 2, 10, barLength, 20), curHealth + "");
		}
	}
	
}
