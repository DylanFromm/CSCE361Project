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
	}
	
	void OnGUI() {
		if (name.Contains("1")) {
		    GUI.Box(new Rect(10, 10, barLength, 20), curHealth + "");
		} else {
			GUI.Box(new Rect(Screen.width / 2, 10, barLength, 20), curHealth + "");
		}
	}

	void hurt(float amount) {
		curHealth = curHealth - (float)amount;
		if (curHealth < 0) {
			curHealth = 0;
		}
		barLength = (Screen.width / 2) * (curHealth / (float)maxHealth);
	}
}