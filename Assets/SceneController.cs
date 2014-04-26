using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {

	public GameObject player1;
	public GameObject player2;
	public GameObject text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float health1 = player1.GetComponent<PlayerController> ().health;
		float health2 = player2.GetComponent<PlayerController> ().health;

		// end scene scripts here
		if (health1 <= 0 || health2 <= 0) {
			text.GetComponent<FPS>().over = true;
		}
	}
}
